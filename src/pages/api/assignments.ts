import type { APIRoute } from "astro";
import { createAssignment, deleteAssignment } from "@/lib/inventory-records";
import { createClient } from "@/lib/supabase";

function dashboardRedirect(context: Parameters<APIRoute>[0], status: string, message?: string) {
  const params = new URLSearchParams({ assignment: status });
  if (message) params.set("message", message);
  return context.redirect(`/dashboard?${params.toString()}`, 303);
}

export const POST: APIRoute = async (context) => {
  const companyId = context.locals.company?.id;
  const supabase = createClient(context.request.headers, context.cookies);

  if (!companyId || !supabase) {
    return dashboardRedirect(context, "error", "Company context is required");
  }

  try {
    const form = await context.request.formData();
    const action = form.get("action");
    const result =
      action === "delete"
        ? await deleteAssignment(supabase, companyId, form)
        : await createAssignment(supabase, companyId, form);

    if (result.error) {
      return dashboardRedirect(context, "error", result.error.message);
    }

    return dashboardRedirect(context, action === "delete" ? "deleted" : "saved");
  } catch (error) {
    return dashboardRedirect(context, "error", error instanceof Error ? error.message : "Assignment update failed");
  }
};
