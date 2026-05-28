import type { APIRoute } from "astro";
import { getReminderByDedupKey, markReminderSent } from "@/lib/reminder-contract";
import { createClient } from "@/lib/supabase";

interface MarkSentPayload {
  licenseRef?: string;
  recipientEmail?: string;
  reminderDate?: string;
  providerMessageId?: string;
}

export const POST: APIRoute = async (context) => {
  if (!context.locals.company) {
    return new Response(JSON.stringify({ error: "Company context is required" }), { status: 403 });
  }

  const payload = (await context.request.json().catch(() => null)) as MarkSentPayload | null;
  const licenseRef = payload?.licenseRef?.trim();
  const recipientEmail = payload?.recipientEmail?.trim();
  const reminderDate = payload?.reminderDate?.trim();
  const providerMessageId = payload?.providerMessageId?.trim();

  if (!licenseRef || !recipientEmail || !reminderDate) {
    return new Response(JSON.stringify({ error: "licenseRef, recipientEmail, and reminderDate are required" }), {
      status: 400,
    });
  }

  const supabase = createClient(context.request.headers, context.cookies);
  if (!supabase) {
    return new Response(JSON.stringify({ error: "Supabase is not configured" }), { status: 500 });
  }

  const reminderResult = await getReminderByDedupKey(supabase, {
    companyId: context.locals.company.id,
    licenseRef,
    recipientEmail,
    reminderDate,
  });

  if (reminderResult.error) {
    return new Response(JSON.stringify({ error: reminderResult.error }), { status: 400 });
  }

  if (!reminderResult.data) {
    return new Response(JSON.stringify({ error: "Reminder not found" }), { status: 404 });
  }

  const markResult = await markReminderSent(supabase, reminderResult.data, providerMessageId);
  if (markResult.error) {
    return new Response(JSON.stringify({ error: markResult.error }), { status: 400 });
  }

  return new Response(JSON.stringify(markResult.data), { status: 200 });
};
