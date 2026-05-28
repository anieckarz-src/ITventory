import { defineMiddleware } from "astro:middleware";
import { createCompanyForCurrentUser, getCurrentAccessContext } from "@/lib/access-context";
import { createClient } from "@/lib/supabase";

const PROTECTED_ROUTES = ["/dashboard"];
const COMPANY_REQUIRED_PATH = "/auth/company-required";

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
    if (!context.locals.user) {
      return context.redirect("/auth/signin");
    }

    if (!supabase) {
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
      return context.redirect(COMPANY_REQUIRED_PATH);
    }

    context.locals.company = accessContext.company;
    context.locals.membership = accessContext.membership;
    context.locals.role = accessContext.role;
  }

  return next();
});
