import type { APIRoute } from "astro";
import { createReminderContract } from "@/lib/reminder-contract";
import { createClient } from "@/lib/supabase";

interface CreateReminderPayload {
  licenseRef?: string;
  recipientEmail?: string;
  reminderDate?: string;
}

export const POST: APIRoute = async (context) => {
  if (!context.locals.company) {
    return new Response(JSON.stringify({ error: "Company context is required" }), { status: 403 });
  }

  const payload = (await context.request.json().catch(() => null)) as CreateReminderPayload | null;
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

  const result = await createReminderContract(supabase, {
    companyId: context.locals.company.id,
    licenseRef,
    recipientEmail,
    reminderDate,
  });

  if (result.error) {
    const status = result.error.includes("already exists") ? 409 : 400;
    return new Response(JSON.stringify({ error: result.error }), { status });
  }

  return new Response(JSON.stringify(result.data), { status: 201 });
};
