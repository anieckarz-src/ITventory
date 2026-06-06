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

export interface InventoryRecords {
  employees: EmployeeRecord[];
  hardware: HardwareRecord[];
  licenses: LicenseRecord[];
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

export async function getInventoryRecords(supabase: SupabaseClient, companyId: string): Promise<InventoryRecords> {
  const [employees, hardware, licenses] = await Promise.all([
    supabase.from("employees").select("*").eq("company_id", companyId).order("created_at", { ascending: false }),
    supabase.from("hardware_assets").select("*").eq("company_id", companyId).order("created_at", { ascending: false }),
    supabase
      .from("software_licenses")
      .select("*")
      .eq("company_id", companyId)
      .order("renewal_date", { ascending: true, nullsFirst: false }),
  ]);

  const error = employees.error ?? hardware.error ?? licenses.error;
  if (error) {
    throw new Error(error.message);
  }

  return {
    employees: (employees.data ?? []) as EmployeeRecord[],
    hardware: (hardware.data ?? []) as HardwareRecord[],
    licenses: (licenses.data ?? []) as LicenseRecord[],
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
