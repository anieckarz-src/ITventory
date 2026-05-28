import type { SupabaseClient } from "@supabase/supabase-js";

export type ReminderStatus = "pending" | "sent" | "failed";
export type ReminderAttemptStatus = "sent" | "failed";

export interface ReminderContract {
  id: string;
  company_id: string;
  license_ref: string;
  recipient_email: string;
  reminder_date: string;
  status: ReminderStatus;
  attempt_count: number;
  last_error: string | null;
  last_attempted_at: string | null;
  sent_at: string | null;
  created_at: string;
  updated_at: string;
}

export interface ReminderAttempt {
  id: string;
  reminder_id: string;
  company_id: string;
  attempt_no: number;
  status: ReminderAttemptStatus;
  error_message: string | null;
  attempted_at: string;
  provider_message_id: string | null;
}

type Result<T> = { data: T; error: null } | { data: null; error: string };
interface DbResponse<T> {
  data: T | null;
  error: { message: string } | null;
}

interface CreateReminderInput {
  companyId: string;
  licenseRef: string;
  recipientEmail: string;
  reminderDate: string;
}

interface AppendReminderAttemptInput {
  reminderId: string;
  companyId: string;
  attemptNo: number;
  status: ReminderAttemptStatus;
  errorMessage?: string | null;
  providerMessageId?: string | null;
}

function asReminder(row: unknown): ReminderContract {
  return row as ReminderContract;
}

function asAttempt(row: unknown): ReminderAttempt {
  return row as ReminderAttempt;
}

export async function getReminderByDedupKey(
  supabase: SupabaseClient,
  input: CreateReminderInput,
): Promise<Result<ReminderContract | null>> {
  const response = (await supabase
    .from("license_renewal_reminders")
    .select("*")
    .eq("company_id", input.companyId)
    .eq("license_ref", input.licenseRef)
    .eq("recipient_email", input.recipientEmail)
    .eq("reminder_date", input.reminderDate)
    .maybeSingle()) as DbResponse<ReminderContract>;

  if (response.error) {
    return { data: null, error: response.error.message };
  }

  return { data: response.data ? asReminder(response.data) : null, error: null };
}

export async function createReminderContract(
  supabase: SupabaseClient,
  input: CreateReminderInput,
): Promise<Result<ReminderContract>> {
  const existing = await getReminderByDedupKey(supabase, input);
  if (existing.error) {
    return { data: null, error: existing.error };
  }
  if (existing.data) {
    return { data: null, error: "Reminder already exists for this dedup key" };
  }

  const response = (await supabase
    .from("license_renewal_reminders")
    .insert({
      company_id: input.companyId,
      license_ref: input.licenseRef,
      recipient_email: input.recipientEmail,
      reminder_date: input.reminderDate,
    })
    .select("*")
    .single()) as DbResponse<ReminderContract>;

  if (response.error) {
    return { data: null, error: response.error.message };
  }

  return { data: asReminder(response.data), error: null };
}

export async function appendReminderAttempt(
  supabase: SupabaseClient,
  input: AppendReminderAttemptInput,
): Promise<Result<ReminderAttempt>> {
  const response = (await supabase
    .from("license_renewal_reminder_attempts")
    .insert({
      reminder_id: input.reminderId,
      company_id: input.companyId,
      attempt_no: input.attemptNo,
      status: input.status,
      error_message: input.errorMessage ?? null,
      provider_message_id: input.providerMessageId ?? null,
    })
    .select("*")
    .single()) as DbResponse<ReminderAttempt>;

  if (response.error) {
    return { data: null, error: response.error.message };
  }

  return { data: asAttempt(response.data), error: null };
}

export async function markReminderSent(
  supabase: SupabaseClient,
  reminder: ReminderContract,
  providerMessageId?: string,
): Promise<Result<{ reminder: ReminderContract; attempt: ReminderAttempt }>> {
  const attemptNo = reminder.attempt_count + 1;
  const attempt = await appendReminderAttempt(supabase, {
    reminderId: reminder.id,
    companyId: reminder.company_id,
    attemptNo,
    status: "sent",
    providerMessageId: providerMessageId ?? null,
  });

  if (attempt.error) {
    return { data: null, error: attempt.error };
  }

  const response = (await supabase
    .from("license_renewal_reminders")
    .update({
      status: "sent",
      attempt_count: attemptNo,
      last_error: null,
      last_attempted_at: new Date().toISOString(),
      sent_at: new Date().toISOString(),
    })
    .eq("id", reminder.id)
    .eq("company_id", reminder.company_id)
    .select("*")
    .single()) as DbResponse<ReminderContract>;

  if (response.error) {
    return { data: null, error: response.error.message };
  }

  return { data: { reminder: asReminder(response.data), attempt: attempt.data }, error: null };
}

export async function markReminderFailed(
  supabase: SupabaseClient,
  reminder: ReminderContract,
  failureMessage: string,
): Promise<Result<{ reminder: ReminderContract; attempt: ReminderAttempt }>> {
  const attemptNo = reminder.attempt_count + 1;
  const attempt = await appendReminderAttempt(supabase, {
    reminderId: reminder.id,
    companyId: reminder.company_id,
    attemptNo,
    status: "failed",
    errorMessage: failureMessage,
  });

  if (attempt.error) {
    return { data: null, error: attempt.error };
  }

  const response = (await supabase
    .from("license_renewal_reminders")
    .update({
      status: "failed",
      attempt_count: attemptNo,
      last_error: failureMessage,
      last_attempted_at: new Date().toISOString(),
    })
    .eq("id", reminder.id)
    .eq("company_id", reminder.company_id)
    .select("*")
    .single()) as DbResponse<ReminderContract>;

  if (response.error) {
    return { data: null, error: response.error.message };
  }

  return { data: { reminder: asReminder(response.data), attempt: attempt.data }, error: null };
}
