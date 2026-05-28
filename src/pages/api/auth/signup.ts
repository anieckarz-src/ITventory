import type { APIRoute } from "astro";
import { createCompanyForCurrentUser } from "@/lib/access-context";
import { createClient } from "@/lib/supabase";

export const POST: APIRoute = async (context) => {
  const form = await context.request.formData();
  const companyName = (form.get("companyName") as string | null)?.trim() ?? "";
  const email = form.get("email") as string;
  const password = form.get("password") as string;

  if (!companyName) {
    return context.redirect(`/auth/signup?error=${encodeURIComponent("Company name is required")}`);
  }

  const supabase = createClient(context.request.headers, context.cookies);
  if (!supabase) {
    return context.redirect(`/auth/signup?error=${encodeURIComponent("Supabase is not configured")}`);
  }
  const { data, error } = await supabase.auth.signUp({
    email,
    password,
    options: {
      data: {
        company_name: companyName,
      },
    },
  });

  if (error) {
    return context.redirect(`/auth/signup?error=${encodeURIComponent(error.message)}`);
  }

  if (!data.session) {
    return context.redirect("/auth/confirm-email");
  }

  const { error: companyError } = await createCompanyForCurrentUser(supabase, companyName);

  if (companyError) {
    return context.redirect(`/auth/signup?error=${encodeURIComponent(companyError.message)}`);
  }

  return context.redirect("/auth/confirm-email");
};
