import type { APIRoute } from "astro";
import { createReminderContract, getReminderByDedupKey, markReminderFailed } from "@/lib/reminder-contract";
import { createClient } from "@/lib/supabase";

interface ContractCheckPayload {
  licenseRef?: string;
  recipientEmail?: string;
  reminderDate?: string;
}

export const POST: APIRoute = async (context) => {
  if (!context.locals.company) {
    return new Response(JSON.stringify({ error: "Company context is required" }), { status: 403 });
  }

  const payload = (await context.request.json().catch(() => null)) as ContractCheckPayload | null;
  const licenseRef = payload?.licenseRef?.trim();
  const recipientEmail = payload?.recipientEmail?.trim();
  const reminderDate = payload?.reminderDate?.trim();

  if (!licenseRef || !recipientEmail || !reminderDate) {
    return new Response(JSON.stringify({ error: "licenseRef, recipientEmail, and reminderDate are required" }), {
      status: 400,
    });
  }

  const supabase = createClient(context.request.headers, context.cookies);
  if (!supabase) {
    return new Response(JSON.stringify({ error: "Supabase is not configured" }), { status: 500 });
  }

  const created = await createReminderContract(supabase, {
    companyId: context.locals.company.id,
    licenseRef,
    recipientEmail,
    reminderDate,
  });

  if (created.error) {
    return new Response(JSON.stringify({ error: created.error }), { status: 400 });
  }

  const dedup = await createReminderContract(supabase, {
    companyId: context.locals.company.id,
    licenseRef,
    recipientEmail,
    reminderDate,
  });

  if (!dedup.error?.includes("already exists")) {
    return new Response(JSON.stringify({ error: "Dedup guard did not block duplicate reminder creation" }), {
      status: 500,
    });
  }

  const failed = await markReminderFailed(supabase, created.data, "contract-check");
  if (failed.error) {
    return new Response(JSON.stringify({ error: failed.error }), { status: 500 });
  }

  const current = await getReminderByDedupKey(supabase, {
    companyId: context.locals.company.id,
    licenseRef,
    recipientEmail,
    reminderDate,
  });

  if (current.error || !current.data) {
    return new Response(JSON.stringify({ error: current.error ?? "Reminder not found after transition" }), {
      status: 500,
    });
  }

  return new Response(
    JSON.stringify({
      ok: true,
      dedupBlocked: true,
      status: current.data.status,
      attemptCount: current.data.attempt_count,
      lastAttemptedAt: current.data.last_attempted_at,
      lastError: current.data.last_error,
    }),
    { status: 200 },
  );
};
