# Minimalny kontrakt firmy, czlonkostwa i rol

## Executive Summary

F-01 establishes the access boundary every later ITventory slice depends on: each authenticated user works inside a company, each company has memberships, and memberships carry one of two MVP roles: `admin` or `manager`.

The implementation should convert the current Supabase-auth-only scaffold into a minimal company-aware application contract. Signup will require a company name, create a company, and create an `admin` membership for the new user. Server helpers and middleware will expose the current company/role context so later inventory, dashboard, invitation, and reminder work can enforce company isolation consistently.

## Context

- Roadmap item: `F-01` in `context/foundation/roadmap.md`.
- Related roadmap change ID: `company-role-boundary`.
- PRD refs: `FR-001`, `FR-003`, `FR-004`, `FR-005`, `US-01`, `US-02`.
- Stack: Astro 6, React 19, TypeScript, Supabase auth/database, Cloudflare Workers.
- Current scaffold has Supabase auth, protected dashboard routing, and no domain schema yet.

## Current State Analysis

- `src/lib/supabase.ts:5` creates a request-scoped Supabase server client from Astro request headers and cookies.
- `src/middleware.ts:4` protects `/dashboard`, but the guard only checks `Astro.locals.user`.
- `src/pages/api/auth/signup.ts:13` calls `supabase.auth.signUp({ email, password })` and does not create domain records.
- `src/components/auth/SignUpForm.tsx:14` renders the signup form without company fields.
- `src/env.d.ts:1` defines `App.Locals.user`, but there is no typed company, membership, or role context.
- `supabase/config.toml:58` has migrations enabled, but `schema_paths` is empty and there is no `supabase/migrations/` directory yet.
- `README.md:114` still documents that no database tables or migrations are required.

## Decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Complexity | Medium | The change touches schema, RLS, signup integration, middleware context, and verification, but does not yet build invitations or inventory features. |
| Company creation | Signup creates company and admin membership | This proves the company boundary end-to-end and directly unlocks S-01. |
| Company name | Required signup field | Explicit user input avoids inaccurate inferred company names and placeholder cleanup. |
| Role values | `admin` / `manager` | Lowercase values are compact, code-friendly, and can map cleanly to Administrator/Manager labels. |
| Missing membership | Redirect to company setup or a sign-out-safe error state | Missing membership is recoverable for legitimate users after partial failure and should not silently allow dashboard access. |
| Membership scope | One active company per user for MVP | The schema can support future multi-company membership, while the app selects one membership and treats ambiguity as unsupported for now. |
| Isolation strategy | Database RLS plus server helpers | RLS protects data boundaries, while app helpers avoid duplicated role lookup and error handling. |
| Verification depth | Migration, signup smoke, and role helper checks | The phase must prove schema, app integration, and current-role lookup work together. |

## Scope

### In Scope

- Add a Supabase migration for companies, memberships, role enum/check contract, indexes, RLS, and helper functions/policies needed for current-user membership reads.
- Add typed server utilities that resolve the current user, current company, current membership, and role from the request-scoped Supabase client.
- Extend `App.Locals` with company/membership/role context.
- Update signup UI and API behavior so signup requires `companyName` and creates a company plus `admin` membership.
- Update protected-route handling so `/dashboard` requires both an authenticated user and a valid current membership.
- Add a minimal incomplete-onboarding/error route or state for signed-in users with no membership.
- Update dashboard copy enough to display the current company and role context for verification.
- Update README/Supabase setup notes so migrations are no longer documented as unnecessary.

### Out of Scope

- Manager invitation flow. That belongs to S-02.
- Full company settings UI beyond the minimal missing-membership recovery state.
- Custom roles, permissions editor, or role-specific capability matrix beyond `admin` and `manager`.
- Inventory, employee, hardware, license, assignment, dashboard-cost, or reminder schemas.
- Full multi-company switcher.
- External email provider work.

## Target Architecture

Supabase Auth remains the identity provider. The application adds domain tables in `public`: `companies` and `company_memberships`. Every later company-scoped table should be able to reference `companies.id`, and every RLS policy should be able to check the current `auth.uid()` against `company_memberships`.

Astro server code should centralize current-access resolution in a small helper module instead of repeating membership queries in every route. Middleware should populate `Astro.locals.user`, `Astro.locals.company`, `Astro.locals.membership`, and `Astro.locals.role`. For the MVP, a user with exactly one active membership is supported; no membership goes to onboarding recovery, and multiple active memberships should fail closed until a future company switcher exists.

## Data Contract

### Tables

- `public.companies`
  - `id uuid primary key default gen_random_uuid()`
  - `name text not null`
  - `created_at timestamptz not null default now()`
  - `updated_at timestamptz not null default now()`

- `public.company_memberships`
  - `id uuid primary key default gen_random_uuid()`
  - `company_id uuid not null references public.companies(id) on delete cascade`
  - `user_id uuid not null references auth.users(id) on delete cascade`
  - `role text not null check (role in ('admin', 'manager'))`
  - `created_at timestamptz not null default now()`
  - `updated_at timestamptz not null default now()`
  - unique constraint on `(company_id, user_id)`

### MVP Invariants

- Every usable authenticated session must resolve to one active membership.
- A newly signed-up user is inserted as `admin` for the newly created company.
- `manager` is part of the contract now, even though no invitation flow creates it until S-02.
- App code must not rely on client-submitted `company_id` for current-company access.
- Future domain tables should use `company_id` and policies that check membership via `auth.uid()`.

### RLS Contract

- RLS is enabled on `companies` and `company_memberships`.
- Authenticated users can read companies for which they have a membership.
- Authenticated users can read their own membership rows.
- Company and membership creation for signup must happen through a server-controlled path that preserves the invariant that the creator receives `admin` membership.
- If direct client inserts are not sufficiently safe with the anon key, use a database function/RPC with a narrow contract for `create_company_for_current_user(company_name text)` rather than open table insert policies.

## Phase 1: Database Access Contract

Create the Supabase schema and RLS foundation.

### Changes Required

- `supabase/migrations/<timestamp>_company_role_boundary.sql`
  - **Intent**: Add the minimum persistent domain model for company isolation and role membership.
  - **Contract**: Defines `companies`, `company_memberships`, `admin`/`manager` role constraint, timestamps, unique `(company_id, user_id)`, indexes for `user_id` and `company_id`, RLS policies, and any narrow RPC/helper needed by signup.

- `supabase/config.toml`
  - **Intent**: Keep migration behavior aligned with the new schema files.
  - **Contract**: Do not disable migrations; only adjust config if the local Supabase CLI requires explicit schema path handling for this repo.

- `README.md`
  - **Intent**: Stop documenting the project as auth-only.
  - **Contract**: Replace the "no database tables or migrations" statement with concise migration setup/reset guidance.

### Success Criteria

#### Automated Verification

- Supabase migration file exists under `supabase/migrations/` and defines both company tables.
- SQL includes RLS enablement and policies for company/membership reads.
- SQL includes a safe creation path for a signed-in user to create a company and receive `admin` membership.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Reviewer confirms the schema supports future company-scoped tables through `company_id`.
- Reviewer confirms no custom RBAC or invitation workflow slipped into the phase.

**Implementation Note**: After completing this phase and all automated verification passes, pause for manual confirmation before proceeding to Phase 2.

---

## Phase 2: Server Context and Signup Integration

Wire the database contract into the existing auth flow and protected routes.

### Changes Required

- `src/lib/access-context.ts` or equivalent
  - **Intent**: Centralize company and role lookup for the current authenticated user.
  - **Contract**: Exposes a typed helper that receives the request-scoped Supabase client and user, then returns one of: authenticated membership context, no membership, ambiguous memberships, or query error.

- `src/env.d.ts`
  - **Intent**: Make company and membership context available to Astro pages and endpoints without ad hoc casts.
  - **Contract**: Extends `App.Locals` with `company`, `membership`, and `role` fields using local TypeScript types.

- `src/middleware.ts`
  - **Intent**: Protect company-scoped routes with membership context, not only auth state.
  - **Contract**: `/dashboard` continues to redirect unauthenticated users to `/auth/signin`; signed-in users without exactly one membership are sent to the chosen onboarding recovery/error route.

- `src/components/auth/SignUpForm.tsx`
  - **Intent**: Collect explicit company name during signup.
  - **Contract**: Adds required `companyName` client validation and posts it with the existing email/password form.

- `src/pages/api/auth/signup.ts`
  - **Intent**: Create the auth user and company boundary together.
  - **Contract**: Reads `companyName`; after successful signup/session availability, creates company plus `admin` membership through the safe database path; redirects to dashboard or confirm-email depending on Supabase session behavior.

- `src/pages/dashboard.astro`
  - **Intent**: Make successful company-context resolution visible during verification.
  - **Contract**: Displays current company name and role from `Astro.locals`.

- `src/pages/auth/company-required.astro` or equivalent
  - **Intent**: Provide a recoverable, sign-out-safe state when an authenticated user lacks supported membership context.
  - **Contract**: Explains the account is missing company access and offers sign out; does not expose domain data.

### Success Criteria

#### Automated Verification

- Signup form type checks with required company name state.
- Signup API validates missing/blank company name before calling Supabase.
- Middleware populates typed company, membership, and role locals for protected routes.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Signing up with email, password, and company name creates a company and `admin` membership.
- Signed-in user with valid membership can load `/dashboard` and see company name plus role.
- Signed-in user without membership cannot access company-scoped dashboard content.

**Implementation Note**: Full email-confirmation behavior depends on Supabase configuration. If local auth does not return a session immediately after signup, preserve company creation through a documented, safe fallback rather than silently skipping the company contract.

---

## Phase 3: Verification, Fixtures, and Handoff

Make the foundation easy to verify and ready for S-01/S-02.

### Changes Required

- `README.md`
  - **Intent**: Document the new local setup and smoke path.
  - **Contract**: Include migration/reset commands, expected signup behavior, and a note that preview/production Supabase schema changes require human approval.

- `context/changes/f-01/plan.md`
  - **Intent**: Record completed progress as phases land.
  - **Contract**: Mark Progress checkboxes only after corresponding verification succeeds; append commit SHA when available.

- Optional test or script files, if the existing repo pattern supports them without adding large tooling
  - **Intent**: Capture helper behavior for one-membership, no-membership, and multi-membership states.
  - **Contract**: Keep verification lightweight; do not add a large test framework solely for F-01.

### Success Criteria

#### Automated Verification

- `npm run lint` passes.
- `npm run build` passes.
- Local Supabase migration/reset command is documented and, when environment is available, applies cleanly.
- Access helper behavior is covered by a lightweight automated check if practical within the current toolchain.

#### Manual Verification

- Fresh local signup smoke test confirms company and admin membership creation.
- Missing-membership recovery path is manually reachable and does not expose dashboard data.
- Reviewer confirms S-01 can build on this contract without redefining company or role concepts.

**Implementation Note**: Do not require production Supabase migration during implementation. Production schema changes remain a separate human-approved deployment activity per `context/foundation/infrastructure.md`.

---

## Testing Strategy

### Automated

- Run `npm run lint`.
- Run `npm run build`.
- If local Supabase is available, run the documented migration/reset command and inspect that `companies`, `company_memberships`, RLS policies, and helper/RPC exist.
- If a lightweight test harness exists or can be added without scope creep, test access-context outcomes for no membership, one membership, multiple memberships, and query error.

### Manual

1. Start local Supabase with the configured project.
2. Apply/reset migrations.
3. Start the Astro dev server.
4. Sign up with company name, email, and password.
5. Confirm the database contains one company and one `admin` membership for the auth user.
6. Visit `/dashboard` and confirm company name plus role are shown.
7. Create or simulate a signed-in user without membership and confirm `/dashboard` fails closed to the recovery/error state.

## Performance Considerations

Membership lookup runs on protected requests. Keep it to a single indexed query by `user_id`, selecting only the fields needed for current company and role context. For MVP scale, no caching is required; later slices can revisit if protected routes multiply and request latency becomes measurable.

## Migration Notes

This is the first domain schema migration. There is no existing production company data to backfill. Auth users that already exist before this change may have no membership; the app must handle that state explicitly instead of assuming all authenticated users have company access.

Cloudflare rollback does not roll back Supabase schema/data changes. Production migration must be approved separately from code deployment.

## References

- Roadmap: `context/foundation/roadmap.md`
- PRD: `context/foundation/prd.md`
- Stack handoff: `context/foundation/tech-stack.md`
- Infrastructure risk notes: `context/foundation/infrastructure.md`
- Current Supabase client: `src/lib/supabase.ts:5`
- Current middleware auth-only guard: `src/middleware.ts:4`
- Current signup API: `src/pages/api/auth/signup.ts:13`
- Current signup form: `src/components/auth/SignUpForm.tsx:14`
- Current migration config: `supabase/config.toml:58`
- Current auth-only README claim: `README.md:114`

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles. See `references/progress-format.md`.

### Phase 1: Database Access Contract

#### Automated

- [x] 1.1 Supabase migration file exists under `supabase/migrations/` and defines both company tables — 45ffdef
- [x] 1.2 SQL includes RLS enablement and policies for company/membership reads — 45ffdef
- [x] 1.3 SQL includes a safe creation path for a signed-in user to create a company and receive `admin` membership — 45ffdef
- [x] 1.4 `npm run lint` passes — 45ffdef
- [x] 1.5 `npm run build` passes — 45ffdef

#### Manual

- [x] 1.6 Reviewer confirms the schema supports future company-scoped tables through `company_id` — 45ffdef
- [x] 1.7 Reviewer confirms no custom RBAC or invitation workflow slipped into the phase — 45ffdef

### Phase 2: Server Context and Signup Integration

#### Automated

- [x] 2.1 Signup form type checks with required company name state — 8335624
- [x] 2.2 Signup API validates missing/blank company name before calling Supabase — 8335624
- [x] 2.3 Middleware populates typed company, membership, and role locals for protected routes — 8335624
- [x] 2.4 `npm run lint` passes — 8335624
- [x] 2.5 `npm run build` passes — 8335624

#### Manual

- [x] 2.6 Signing up with email, password, and company name creates a company and `admin` membership — 8335624
- [x] 2.7 Signed-in user with valid membership can load `/dashboard` and see company name plus role — 8335624
- [x] 2.8 Signed-in user without membership cannot access company-scoped dashboard content — 8335624

### Phase 3: Verification, Fixtures, and Handoff

#### Automated

- [x] 3.1 `npm run lint` passes
- [x] 3.2 `npm run build` passes
- [x] 3.3 Local Supabase migration/reset command is documented and, when environment is available, applies cleanly
- [x] 3.4 Access helper behavior is covered by a lightweight automated check if practical within the current toolchain

#### Manual

- [x] 3.5 Fresh local signup smoke test confirms company and admin membership creation
- [x] 3.6 Missing-membership recovery path is manually reachable and does not expose dashboard data
- [x] 3.7 Reviewer confirms S-01 can build on this contract without redefining company or role concepts
