import type { SupabaseClient } from "@supabase/supabase-js";
import {
  createReminderContract,
  getReminderByDedupKey,
  markReminderFailed,
  markReminderSent,
  type ReminderContract,
} from "@/lib/reminder-contract";

const DEFAULT_REMINDER_WINDOW_DAYS = 14;

interface LicenseDueForReminder {
  id: string;
  company_id: string;
  name: string;
  vendor: string | null;
  renewal_date: string;
  cost_amount: number | null;
  cost_period: "monthly" | "annual";
  companies: { name: string } | { name: string }[] | null;
}

interface OwnerMembership {
  company_id: string;
  user_id: string;
}

interface DeliveryConfig {
  resendApiKey?: string;
  fromEmail?: string;
}

export interface ProcessRenewalEmailAlertsOptions {
  currentDate?: Date;
  windowDays?: number;
}

export interface ProcessRenewalEmailAlertsResult {
  scanned: number;
  sent: number;
  failed: number;
  skipped: number;
  reminders: {
    licenseId: string;
    companyId: string;
    recipientEmail?: string;
    status: "sent" | "failed" | "skipped";
    reason?: string;
  }[];
}

interface ResendResponse {
  id?: string;
  message?: string;
  name?: string;
}

function toIsoDate(value: Date) {
  return value.toISOString().slice(0, 10);
}

function addDays(value: Date, days: number) {
  const next = new Date(value);
  next.setDate(next.getDate() + days);
  return next;
}

function getCompanyName(license: LicenseDueForReminder) {
  if (Array.isArray(license.companies)) {
    return license.companies[0]?.name ?? "your company";
  }

  return license.companies?.name ?? "your company";
}

function licenseDisplayName(license: LicenseDueForReminder) {
  return license.vendor ? `${license.name} by ${license.vendor}` : license.name;
}

function escapeHtml(value: string) {
  return value.replaceAll("&", "&amp;").replaceAll("<", "&lt;").replaceAll(">", "&gt;").replaceAll('"', "&quot;");
}

function formatCost(license: LicenseDueForReminder) {
  if (license.cost_amount === null) return "Cost is not recorded.";
  return `Recorded cost: $${license.cost_amount.toFixed(2)} / ${license.cost_period}.`;
}

function buildEmail(license: LicenseDueForReminder) {
  const licenseName = licenseDisplayName(license);
  const companyName = getCompanyName(license);
  const escapedLicenseName = escapeHtml(licenseName);
  const escapedCompanyName = escapeHtml(companyName);
  const escapedCost = escapeHtml(formatCost(license));
  const subject = `ITventory renewal reminder: ${license.name}`;
  const text = [
    `Reminder for ${companyName}`,
    "",
    `${licenseName} renews on ${license.renewal_date}.`,
    formatCost(license),
    "",
    "Open ITventory to review the license before renewal.",
  ].join("\n");
  const html = `
    <div style="font-family: Arial, sans-serif; line-height: 1.5; color: #0f172a;">
      <p style="font-size: 14px; color: #0369a1; font-weight: 700; text-transform: uppercase;">ITventory reminder</p>
      <h1 style="font-size: 22px; margin: 0 0 16px;">${escapedLicenseName} renews on ${license.renewal_date}</h1>
      <p>This reminder is for ${escapedCompanyName}.</p>
      <p>${escapedCost}</p>
      <p>Open ITventory to review the license before renewal.</p>
    </div>
  `;

  return { subject, text, html };
}

async function sendRenewalEmail(input: {
  config: DeliveryConfig;
  to: string;
  license: LicenseDueForReminder;
}): Promise<{ providerMessageId: string | null; error: string | null }> {
  if (!input.config.resendApiKey || !input.config.fromEmail) {
    return { providerMessageId: null, error: "Email provider is not configured" };
  }

  const email = buildEmail(input.license);
  const response = await fetch("https://api.resend.com/emails", {
    method: "POST",
    headers: {
      Authorization: `Bearer ${input.config.resendApiKey}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      from: input.config.fromEmail,
      to: input.to,
      subject: email.subject,
      text: email.text,
      html: email.html,
    }),
  });

  const payload = (await response.json().catch(() => null)) as ResendResponse | null;
  if (!response.ok) {
    return {
      providerMessageId: null,
      error: payload?.message ?? payload?.name ?? `Email provider returned ${response.status}`,
    };
  }

  return { providerMessageId: payload?.id ?? null, error: null };
}

async function getDueLicenses(
  supabase: SupabaseClient,
  currentDate: Date,
  windowDays: number,
): Promise<{ data: LicenseDueForReminder[]; error: string | null }> {
  const today = toIsoDate(currentDate);
  const windowEnd = toIsoDate(addDays(currentDate, windowDays));
  const { data, error } = await supabase
    .from("software_licenses")
    .select("id, company_id, name, vendor, renewal_date, cost_amount, cost_period, companies(name)")
    .not("renewal_date", "is", null)
    .gte("renewal_date", today)
    .lte("renewal_date", windowEnd)
    .order("renewal_date", { ascending: true });

  if (error) {
    return { data: [], error: error.message };
  }

  return { data, error: null };
}

async function getOwnerEmail(supabase: SupabaseClient, companyId: string) {
  const { data, error } = await supabase
    .from("company_memberships")
    .select("company_id, user_id")
    .eq("company_id", companyId)
    .eq("role", "owner")
    .maybeSingle();

  if (error) {
    return { email: null, error: error.message };
  }

  const membership: OwnerMembership | null = data;
  if (!membership) {
    return { email: null, error: "Company owner membership was not found" };
  }

  const user = await supabase.auth.admin.getUserById(membership.user_id);
  if (user.error) {
    return { email: null, error: user.error.message };
  }

  const email = user.data.user.email?.trim();
  return email ? { email, error: null } : { email: null, error: "Company owner email was not found" };
}

async function getOrCreateReminder(
  supabase: SupabaseClient,
  input: {
    companyId: string;
    licenseRef: string;
    recipientEmail: string;
    reminderDate: string;
  },
) {
  const existing = await getReminderByDedupKey(supabase, input);
  if (existing.error) {
    return { reminder: null, error: existing.error };
  }

  if (existing.data) {
    return { reminder: existing.data, error: null };
  }

  const created = await createReminderContract(supabase, input);
  return { reminder: created.data, error: created.error };
}

async function recordFailed(
  supabase: SupabaseClient,
  reminder: ReminderContract | null,
  reason: string,
): Promise<string> {
  if (!reminder) return reason;
  const result = await markReminderFailed(supabase, reminder, reason);
  return result.error ?? reason;
}

export async function processRenewalEmailAlerts(
  supabase: SupabaseClient,
  config: DeliveryConfig,
  options: ProcessRenewalEmailAlertsOptions = {},
): Promise<ProcessRenewalEmailAlertsResult> {
  const currentDate = options.currentDate ?? new Date();
  const windowDays = options.windowDays ?? DEFAULT_REMINDER_WINDOW_DAYS;
  const reminderDate = toIsoDate(currentDate);
  const dueLicenses = await getDueLicenses(supabase, currentDate, windowDays);
  const summary: ProcessRenewalEmailAlertsResult = {
    scanned: dueLicenses.data.length,
    sent: 0,
    failed: 0,
    skipped: 0,
    reminders: [],
  };

  if (dueLicenses.error) {
    return {
      ...summary,
      failed: 1,
      reminders: [{ licenseId: "unknown", companyId: "unknown", status: "failed", reason: dueLicenses.error }],
    };
  }

  for (const license of dueLicenses.data) {
    const owner = await getOwnerEmail(supabase, license.company_id);
    if (!owner.email) {
      summary.failed += 1;
      summary.reminders.push({
        licenseId: license.id,
        companyId: license.company_id,
        status: "failed",
        reason: owner.error ?? "Owner email is missing",
      });
      continue;
    }

    const reminder = await getOrCreateReminder(supabase, {
      companyId: license.company_id,
      licenseRef: license.id,
      recipientEmail: owner.email,
      reminderDate,
    });

    if (reminder.error || !reminder.reminder) {
      summary.failed += 1;
      summary.reminders.push({
        licenseId: license.id,
        companyId: license.company_id,
        recipientEmail: owner.email,
        status: "failed",
        reason: reminder.error ?? "Reminder could not be created",
      });
      continue;
    }

    if (reminder.reminder.status === "sent") {
      summary.skipped += 1;
      summary.reminders.push({
        licenseId: license.id,
        companyId: license.company_id,
        recipientEmail: owner.email,
        status: "skipped",
        reason: "Reminder already sent for this date",
      });
      continue;
    }

    const delivery = await sendRenewalEmail({ config, to: owner.email, license });
    if (delivery.error) {
      const reason = await recordFailed(supabase, reminder.reminder, delivery.error);
      summary.failed += 1;
      summary.reminders.push({
        licenseId: license.id,
        companyId: license.company_id,
        recipientEmail: owner.email,
        status: "failed",
        reason,
      });
      continue;
    }

    const marked = await markReminderSent(supabase, reminder.reminder, delivery.providerMessageId ?? undefined);
    if (marked.error) {
      summary.failed += 1;
      summary.reminders.push({
        licenseId: license.id,
        companyId: license.company_id,
        recipientEmail: owner.email,
        status: "failed",
        reason: marked.error,
      });
      continue;
    }

    summary.sent += 1;
    summary.reminders.push({
      licenseId: license.id,
      companyId: license.company_id,
      recipientEmail: owner.email,
      status: "sent",
    });
  }

  return summary;
}
