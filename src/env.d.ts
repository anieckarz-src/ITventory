declare namespace App {
  interface Locals {
    company: import("@/lib/access-context").Company | null;
    membership: import("@/lib/access-context").CompanyMembership | null;
    role: import("@/lib/access-context").CompanyRole | null;
    user: import("@supabase/supabase-js").User | null;
  }
}
