import type { APIRoute } from "astro";
import { REMINDER_FROM_EMAIL, REMINDER_INTERNAL_SECRET, RESEND_API_KEY } from "astro:env/server";
import { processRenewalEmailAlerts } from "@/lib/renewal-email-alerts";
import { createServiceClient } from "@/lib/supabase";

function getBearerToken(request: Request) {
  const authorization = request.headers.get("Authorization");
  if (!authorization?.startsWith("Bearer ")) return null;
  return authorization.slice("Bearer ".length).trim();
}

function isAuthorized(request: Request) {
  if (!REMINDER_INTERNAL_SECRET) return false;
  const headerSecret = request.headers.get("x-reminder-secret")?.trim();
  const bearerSecret = getBearerToken(request);
  return headerSecret === REMINDER_INTERNAL_SECRET || bearerSecret === REMINDER_INTERNAL_SECRET;
}

export const POST: APIRoute = async ({ request }) => {
  if (!isAuthorized(request)) {
    return new Response(JSON.stringify({ error: "Reminder secret is required" }), { status: 401 });
  }

  const supabase = createServiceClient();
  if (!supabase) {
    return new Response(JSON.stringify({ error: "Supabase service role is not configured" }), { status: 500 });
  }

  const result = await processRenewalEmailAlerts(supabase, {
    resendApiKey: RESEND_API_KEY,
    fromEmail: REMINDER_FROM_EMAIL,
  });

  return new Response(JSON.stringify({ ok: true, ...result }), {
    status: 200,
    headers: { "Content-Type": "application/json" },
  });
};

export const GET: APIRoute = () =>
  new Response(JSON.stringify({ error: "Use POST to process renewal reminders" }), { status: 405 });
