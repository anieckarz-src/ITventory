import { defineMiddleware } from "astro:middleware";
import { createCompanyForCurrentUser, getCurrentAccessContext } from "@/lib/access-context";
import { createClient } from "@/lib/supabase";

const PROTECTED_ROUTES = ["/dashboard", "/api/internal/reminders", "/api/inventory"];
const COMPANY_REQUIRED_PATH = "/auth/company-required";
const INTERNAL_REMINDER_PREFIX = "/api/internal/reminders";

export const onRequest = defineMiddleware(async (context, next) => {
  const supabase = createClient(context.request.headers, context.cookies);
  context.locals.company = null;
  context.locals.membership = null;
  context.locals.role = null;

  if (supabase) {
    const {
      data: { user },
    } = await supabase.auth.getUser();
    context.locals.user = user ?? null;
  } else {
    context.locals.user = null;
  }

  if (PROTECTED_ROUTES.some((route) => context.url.pathname.startsWith(route))) {
    const isInternalReminderRoute = context.url.pathname.startsWith(INTERNAL_REMINDER_PREFIX);

    if (!context.locals.user) {
      if (isInternalReminderRoute) {
        return new Response(JSON.stringify({ error: "Authentication required" }), { status: 401 });
      }
      return context.redirect("/auth/signin");
    }

    if (!supabase) {
      if (isInternalReminderRoute) {
        return new Response(JSON.stringify({ error: "Supabase is not configured" }), { status: 500 });
      }
      return context.redirect(COMPANY_REQUIRED_PATH);
    }

    let accessContext = await getCurrentAccessContext(supabase, context.locals.user);

    if (accessContext.status === "no-membership") {
      const userMetadata: unknown = context.locals.user.user_metadata;
      let normalizedCompanyName = "";

      if (userMetadata && typeof userMetadata === "object" && "company_name" in userMetadata) {
        const companyNameFromMetadata = (userMetadata as { company_name?: unknown }).company_name;

        if (typeof companyNameFromMetadata === "string") {
          normalizedCompanyName = companyNameFromMetadata.trim();
        }
      }

      if (normalizedCompanyName) {
        const { error: bootstrapError } = await createCompanyForCurrentUser(supabase, normalizedCompanyName);

        if (!bootstrapError) {
          accessContext = await getCurrentAccessContext(supabase, context.locals.user);
        }
      }
    }

    if (accessContext.status !== "ok") {
      if (isInternalReminderRoute) {
        return new Response(JSON.stringify({ error: "Company context is required" }), { status: 403 });
      }
      return context.redirect(COMPANY_REQUIRED_PATH);
    }

    context.locals.company = accessContext.company;
    context.locals.membership = accessContext.membership;
    context.locals.role = accessContext.role;
  }

  return next();
});
