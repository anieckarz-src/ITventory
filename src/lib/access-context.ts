import type { SupabaseClient, User } from "@supabase/supabase-js";

export type CompanyRole = "admin" | "manager";

export interface Company {
  id: string;
  name: string;
  created_at: string;
  updated_at: string;
}

export interface CompanyMembership {
  id: string;
  company_id: string;
  user_id: string;
  role: CompanyRole;
  created_at: string;
  updated_at: string;
}

export type AccessContextResult =
  | {
      status: "ok";
      company: Company;
      membership: CompanyMembership;
      role: CompanyRole;
    }
  | { status: "no-membership" }
  | { status: "ambiguous-membership" }
  | { status: "error"; message: string };

interface MembershipRow extends CompanyMembership {
  companies: Company | Company[] | null;
}

function getJoinedCompany(row: MembershipRow) {
  if (Array.isArray(row.companies)) {
    return row.companies[0] ?? null;
  }

  return row.companies;
}

export async function getCurrentAccessContext(supabase: SupabaseClient, user: User): Promise<AccessContextResult> {
  const { data, error } = await supabase
    .from("company_memberships")
    .select("id, company_id, user_id, role, created_at, updated_at, companies(id, name, created_at, updated_at)")
    .eq("user_id", user.id)
    .limit(2);

  if (error) {
    return { status: "error", message: error.message };
  }

  const rows = data as unknown as MembershipRow[];

  if (rows.length === 0) {
    return { status: "no-membership" };
  }

  if (rows.length > 1) {
    return { status: "ambiguous-membership" };
  }

  const [membership] = rows;
  const company = getJoinedCompany(membership);

  if (!company) {
    return { status: "error", message: "Membership is missing company data" };
  }

  return {
    status: "ok",
    company,
    membership: {
      id: membership.id,
      company_id: membership.company_id,
      user_id: membership.user_id,
      role: membership.role,
      created_at: membership.created_at,
      updated_at: membership.updated_at,
    },
    role: membership.role,
  };
}

export async function createCompanyForCurrentUser(supabase: SupabaseClient, companyName: string) {
  return supabase.rpc("create_company_for_current_user", { company_name: companyName });
}
