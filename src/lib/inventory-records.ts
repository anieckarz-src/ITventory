import type { SupabaseClient } from "@supabase/supabase-js";

export interface EmployeeRecord {
  id: string;
  company_id: string;
  full_name: string;
  email: string | null;
  title: string | null;
  notes: string | null;
  created_at: string;
  updated_at: string;
}

export interface HardwareRecord {
  id: string;
  company_id: string;
  asset_type: string;
  model: string;
  serial_number: string | null;
  status: "available" | "in_use" | "retired";
  notes: string | null;
  created_at: string;
  updated_at: string;
}

export interface LicenseRecord {
  id: string;
  company_id: string;
  name: string;
  vendor: string | null;
  seats: number | null;
  cost_amount: number | null;
  cost_period: "monthly" | "annual";
  renewal_date: string | null;
  notes: string | null;
  created_at: string;
  updated_at: string;
}

export type AssignmentType = "hardware_employee" | "license_employee" | "license_hardware";

export interface AssignmentRecord {
  id: string;
  company_id: string;
  assignment_type: AssignmentType;
  employee_id: string | null;
  hardware_asset_id: string | null;
  software_license_id: string | null;
  notes: string | null;
  created_at: string;
  updated_at: string;
  employee: Pick<EmployeeRecord, "id" | "full_name" | "email"> | null;
  hardware: Pick<HardwareRecord, "id" | "asset_type" | "model" | "serial_number"> | null;
  license: Pick<LicenseRecord, "id" | "name" | "vendor"> | null;
}

export interface InventoryRecords {
  employees: EmployeeRecord[];
  hardware: HardwareRecord[];
  licenses: LicenseRecord[];
  assignments: AssignmentRecord[];
}

export function cleanText(value: FormDataEntryValue | null) {
  const text = typeof value === "string" ? value.trim() : "";
  return text.length > 0 ? text : null;
}

export function requireText(value: FormDataEntryValue | null, label: string) {
  const text = cleanText(value);
  if (!text) {
    throw new Error(`${label} is required`);
  }
  return text;
}

export function cleanInteger(value: FormDataEntryValue | null) {
  const text = cleanText(value);
  if (!text) return null;
  const parsed = Number.parseInt(text, 10);
  if (!Number.isFinite(parsed) || parsed <= 0) {
    throw new Error("Seats must be a positive number");
  }
  return parsed;
}

export function cleanMoney(value: FormDataEntryValue | null) {
  const text = cleanText(value);
  if (!text) return null;
  const parsed = Number.parseFloat(text);
  if (!Number.isFinite(parsed) || parsed < 0) {
    throw new Error("Cost must be zero or greater");
  }
  return parsed;
}

export function cleanDate(value: FormDataEntryValue | null) {
  const text = cleanText(value);
  if (!text) return null;
  if (!/^\d{4}-\d{2}-\d{2}$/.test(text)) {
    throw new Error("Renewal date must use YYYY-MM-DD format");
  }
  return text;
}

function requireAssignmentType(value: FormDataEntryValue | null): AssignmentType {
  const assignmentType = cleanText(value);
  if (
    assignmentType !== "hardware_employee" &&
    assignmentType !== "license_employee" &&
    assignmentType !== "license_hardware"
  ) {
    throw new Error("Assignment type is invalid");
  }
  return assignmentType;
}

async function assertRecordBelongsToCompany(
  supabase: SupabaseClient,
  table: "employees" | "hardware_assets" | "software_licenses",
  id: string,
  companyId: string,
  label: string,
) {
  const { data, error } = await supabase
    .from(table)
    .select("id")
    .eq("id", id)
    .eq("company_id", companyId)
    .maybeSingle();

  if (error) {
    throw new Error(error.message);
  }

  if (!data) {
    throw new Error(`${label} does not belong to this company`);
  }
}

export async function getInventoryRecords(supabase: SupabaseClient, companyId: string): Promise<InventoryRecords> {
  const [employees, hardware, licenses, assignments] = await Promise.all([
    supabase.from("employees").select("*").eq("company_id", companyId).order("created_at", { ascending: false }),
    supabase.from("hardware_assets").select("*").eq("company_id", companyId).order("created_at", { ascending: false }),
    supabase
      .from("software_licenses")
      .select("*")
      .eq("company_id", companyId)
      .order("renewal_date", { ascending: true, nullsFirst: false }),
    supabase
      .from("asset_assignments")
      .select(
        "*, employee:employees(id, full_name, email), hardware:hardware_assets(id, asset_type, model, serial_number), license:software_licenses(id, name, vendor)",
      )
      .eq("company_id", companyId)
      .order("created_at", { ascending: false }),
  ]);

  const error = employees.error ?? hardware.error ?? licenses.error ?? assignments.error;
  if (error) {
    throw new Error(error.message);
  }

  return {
    employees: (employees.data ?? []) as EmployeeRecord[],
    hardware: (hardware.data ?? []) as HardwareRecord[],
    licenses: (licenses.data ?? []) as LicenseRecord[],
    assignments: (assignments.data ?? []) as AssignmentRecord[],
  };
}

export async function upsertEmployee(supabase: SupabaseClient, companyId: string, form: FormData) {
  const id = cleanText(form.get("id"));
  const payload = {
    company_id: companyId,
    full_name: requireText(form.get("full_name"), "Employee name"),
    email: cleanText(form.get("email")),
    title: cleanText(form.get("title")),
    notes: cleanText(form.get("notes")),
  };

  return id
    ? supabase.from("employees").update(payload).eq("id", id).eq("company_id", companyId)
    : supabase.from("employees").insert(payload);
}

export async function upsertHardware(supabase: SupabaseClient, companyId: string, form: FormData) {
  const id = cleanText(form.get("id"));
  const status = cleanText(form.get("status")) ?? "available";
  if (!["available", "in_use", "retired"].includes(status)) {
    throw new Error("Hardware status is invalid");
  }

  const payload = {
    company_id: companyId,
    asset_type: requireText(form.get("asset_type"), "Asset type"),
    model: requireText(form.get("model"), "Model"),
    serial_number: cleanText(form.get("serial_number")),
    status: status as HardwareRecord["status"],
    notes: cleanText(form.get("notes")),
  };

  return id
    ? supabase.from("hardware_assets").update(payload).eq("id", id).eq("company_id", companyId)
    : supabase.from("hardware_assets").insert(payload);
}

export async function deleteHardware(supabase: SupabaseClient, companyId: string, form: FormData) {
  const id = requireText(form.get("id"), "Hardware id");
  return supabase.from("hardware_assets").delete().eq("id", id).eq("company_id", companyId);
}

export async function upsertLicense(supabase: SupabaseClient, companyId: string, form: FormData) {
  const id = cleanText(form.get("id"));
  const period = cleanText(form.get("cost_period")) ?? "monthly";
  if (!["monthly", "annual"].includes(period)) {
    throw new Error("Cost period is invalid");
  }

  const payload = {
    company_id: companyId,
    name: requireText(form.get("name"), "License name"),
    vendor: cleanText(form.get("vendor")),
    seats: cleanInteger(form.get("seats")),
    cost_amount: cleanMoney(form.get("cost_amount")),
    cost_period: period as LicenseRecord["cost_period"],
    renewal_date: cleanDate(form.get("renewal_date")),
    notes: cleanText(form.get("notes")),
  };

  return id
    ? supabase.from("software_licenses").update(payload).eq("id", id).eq("company_id", companyId)
    : supabase.from("software_licenses").insert(payload);
}

export async function createAssignment(supabase: SupabaseClient, companyId: string, form: FormData) {
  const assignmentType = requireAssignmentType(form.get("assignment_type"));
  const notes = cleanText(form.get("notes"));

  if (assignmentType === "hardware_employee") {
    const employeeId = requireText(form.get("employee_id"), "Employee");
    const hardwareAssetId = requireText(form.get("hardware_asset_id"), "Hardware");

    await Promise.all([
      assertRecordBelongsToCompany(supabase, "employees", employeeId, companyId, "Employee"),
      assertRecordBelongsToCompany(supabase, "hardware_assets", hardwareAssetId, companyId, "Hardware"),
    ]);

    return supabase.from("asset_assignments").insert({
      company_id: companyId,
      assignment_type: assignmentType,
      employee_id: employeeId,
      hardware_asset_id: hardwareAssetId,
      software_license_id: null,
      notes,
    });
  }

  if (assignmentType === "license_employee") {
    const employeeId = requireText(form.get("employee_id"), "Employee");
    const softwareLicenseId = requireText(form.get("software_license_id"), "License");

    await Promise.all([
      assertRecordBelongsToCompany(supabase, "employees", employeeId, companyId, "Employee"),
      assertRecordBelongsToCompany(supabase, "software_licenses", softwareLicenseId, companyId, "License"),
    ]);

    return supabase.from("asset_assignments").insert({
      company_id: companyId,
      assignment_type: assignmentType,
      employee_id: employeeId,
      hardware_asset_id: null,
      software_license_id: softwareLicenseId,
      notes,
    });
  }

  const hardwareAssetId = requireText(form.get("hardware_asset_id"), "Hardware");
  const softwareLicenseId = requireText(form.get("software_license_id"), "License");

  await Promise.all([
    assertRecordBelongsToCompany(supabase, "hardware_assets", hardwareAssetId, companyId, "Hardware"),
    assertRecordBelongsToCompany(supabase, "software_licenses", softwareLicenseId, companyId, "License"),
  ]);

  return supabase.from("asset_assignments").insert({
    company_id: companyId,
    assignment_type: assignmentType,
    employee_id: null,
    hardware_asset_id: hardwareAssetId,
    software_license_id: softwareLicenseId,
    notes,
  });
}

export async function deleteAssignment(supabase: SupabaseClient, companyId: string, form: FormData) {
  const id = requireText(form.get("id"), "Assignment id");
  return supabase.from("asset_assignments").delete().eq("id", id).eq("company_id", companyId);
}
