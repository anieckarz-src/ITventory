# Admin company start

## Executive Summary

S-01 zamyka pierwszy uzywalny przeplyw ITventory: Administrator zaklada firme, loguje sie i trafia do pustej przestrzeni danych swojej firmy. F-01 dostarczyl juz kontrakt firm, membershipow, roli `admin` i ochrony `/dashboard`; ten change nie redefiniuje fundamentu, tylko zamienia techniczny smoke screen i starterowy landing w produktowy start aplikacji.

Plan skupia sie na trzech widocznych efektach: po udanym signup/signin uzytkownik laduje w przestrzeni firmy, dashboard pokazuje sensowny pusty workspace gotowy pod S-03, a publiczny ekran wejscia mocno komunikuje ITventory zamiast `10x Astro Starter`.

## Context

- Roadmap item: `S-01` in `context/foundation/roadmap.md`.
- Change ID: `admin-company-start`.
- PRD refs: `FR-001`, `FR-002`, `FR-004`, `FR-005`, `US-01`.
- Prerequisite: F-01 implemented company boundary, membership lookup, signup company creation, and membership-protected dashboard.
- User decisions:
  - Signup with an active session should go directly to dashboard/workspace.
  - Empty company screen should be an operational dashboard with empty sections for employees, hardware, licenses, and renewals.
  - Public `/` should be strongly rebranded from starter content to ITventory.
  - Existing signed-in users without membership should keep the fail-closed `company-required` recovery path.

## Current State Analysis

- `src/pages/api/auth/signup.ts` validates `companyName`, creates the auth user, and calls `createCompanyForCurrentUser`, but still redirects to `/auth/confirm-email` even when a session exists.
- `src/pages/api/auth/signin.ts` redirects successful sign-ins to `/`, which slows the first company workspace path.
- `src/middleware.ts` already protects `/dashboard`, resolves company/membership/role context, and redirects unsupported membership states to `/auth/company-required`.
- `src/pages/dashboard.astro` only displays company name, role, email, and a sign-out button. It proves context, but it is not yet a user-facing empty workspace.
- `src/components/Welcome.astro` still presents `10x Astro Starter`, generic starter claims, and scaffold feature cards.
- `src/layouts/Layout.astro` defaults the document title to `10x Astro Starter`.
- `README.md` already documents the F-01 smoke path, but it does not yet describe S-01's product-level start behavior.

## Decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Signup destination | Redirect active-session signup to `/dashboard` | S-01 requires the admin to land in the company workspace when the session is usable. |
| No-session signup | Keep `/auth/confirm-email` | Supabase email confirmation may prevent an immediate session; the existing fallback remains necessary. |
| Signin destination | Redirect successful signin to `/dashboard` | The signed-in admin's primary destination is the company workspace, not the public landing page. |
| Empty workspace shape | Operational dashboard with empty sections | Gives the next slices obvious landing zones without creating inventory data prematurely. |
| Public page | Strong ITventory rebrand | Removes starter scaffolding from the first impression and matches the product being built. |
| Missing membership | Keep `company-required` fail-closed path | F-01 already handles this safely; self-service recovery is outside S-01. |
| Data model | No new tables or migrations | Company and role persistence already exists in F-01. |

## Scope

### In Scope

- Update signup and signin redirects so usable sessions enter `/dashboard`.
- Preserve the existing confirm-email path when Supabase does not return a session after signup.
- Replace the technical dashboard with an ITventory empty workspace:
  - company header,
  - current role and signed-in email,
  - empty-state sections for employees, hardware, licenses, and renewals,
  - next-step messaging that points toward manual inventory entry without linking to routes that do not exist yet.
- Strongly rebrand the public `/` experience away from `10x Astro Starter` and toward ITventory's promise.
- Update default metadata/title text where it still exposes starter naming.
- Keep `company-required` as the signed-in no-membership recovery state.
- Update README or route documentation to describe the S-01 start path.

### Out of Scope

- Manager invitation flow (`S-02`).
- Employee, hardware, license, assignment, dashboard-cost, or reminder CRUD (`S-03+`).
- Company settings UI.
- Self-service company creation for existing authenticated users without membership.
- Custom roles, role editor, or multi-company switcher.
- New Supabase migrations or RLS changes.
- Real data metrics, charts, costs, or email delivery.

## Target Architecture

S-01 should use F-01's existing access boundary as-is. Auth endpoints decide where the user goes after successful authentication. Middleware remains the single gate that resolves company context for `/dashboard`. The dashboard becomes the first product workspace and reads only from `Astro.locals`; because inventory tables do not exist yet, all operational cards are explicit empty states rather than database-driven summaries.

Public `/` remains a lightweight entry page, but its content should now be ITventory-specific. It should direct anonymous users to sign up/sign in and direct signed-in users through the existing topbar to dashboard.

## User Flow Contract

### New Administrator With Immediate Session

1. User opens `/auth/signup`.
2. User enters company name, email, and password.
3. Signup API creates the auth user and company/admin membership through the existing F-01 helper.
4. User is redirected to `/dashboard`.
5. Dashboard shows the company name, Administrator role, signed-in email, and empty operational sections.

### New Administrator Requiring Email Confirmation

1. User opens `/auth/signup`.
2. User enters company name, email, and password.
3. Supabase does not return a session.
4. User is redirected to `/auth/confirm-email`.
5. After confirmation and signin, successful signin redirects to `/dashboard`.

### Existing Signed-In User Without Membership

1. User attempts `/dashboard`.
2. Middleware fails closed to `/auth/company-required`.
3. Recovery page offers sign out and exposes no company-scoped data.

## Phase 1: Auth Entry Routing

Make the successful auth path land in the company workspace while preserving no-session fallback behavior.

### Changes Required

- `src/pages/api/auth/signup.ts`
  - **Intent**: Align signup behavior with S-01 by sending admins with a usable session directly to their company workspace.
  - **Contract**: If `data.session` is absent, keep redirecting to `/auth/confirm-email`; if company creation succeeds with a session, redirect to `/dashboard`.

- `src/pages/api/auth/signin.ts`
  - **Intent**: Make returning users enter the protected workspace after authentication.
  - **Contract**: Successful `signInWithPassword` redirects to `/dashboard`; error behavior remains query-param based on `/auth/signin`.

- `README.md`
  - **Intent**: Keep the local smoke path aligned with the actual route behavior.
  - **Contract**: Document that successful signin and active-session signup land on `/dashboard`, while confirm-email remains the no-session fallback.

### Success Criteria

#### Automated Verification

- Signup API keeps the no-session `/auth/confirm-email` branch and redirects active-session signup to `/dashboard`.
- Signin API redirects successful login to `/dashboard`.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Creating an account with company name lands on `/dashboard` when local Supabase returns a session.
- If email confirmation prevents an immediate session, signup still lands on `/auth/confirm-email`.
- Signing in with an existing membership lands on `/dashboard`.

**Implementation Note**: Do not change the F-01 company creation helper or the `company-required` route in this phase.

---

## Phase 2: Empty Company Workspace

Turn `/dashboard` from a technical context check into the first usable empty workspace for a newly created company.

### Changes Required

- `src/pages/dashboard.astro`
  - **Intent**: Present a product-grade empty company workspace that confirms the admin is inside the right company and shows what data will come next.
  - **Contract**: Render company name, role label, signed-in email, and four stable empty-state sections: Employees, Hardware, Licenses, Renewals. Sections must not fetch nonexistent tables and must not link to routes that do not exist yet.

- `src/pages/dashboard.astro`
  - **Intent**: Keep the workspace useful as the base for S-03 and S-05.
  - **Contract**: Use neutral zero states such as `0 records` / `No data yet` and short action text like `Planned in inventory records`; avoid fake metrics.

- `src/pages/dashboard.astro`
  - **Intent**: Preserve safe account controls.
  - **Contract**: Keep a visible sign-out action and keep all company-scoped content behind the existing middleware-protected route.

### Success Criteria

#### Automated Verification

- Dashboard renders without requiring any inventory, license, hardware, employee, or reminder tables.
- Dashboard uses `Astro.locals.company`, `Astro.locals.role`, and `Astro.locals.user` without unsafe client-submitted company identifiers.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- A newly signed-up admin sees the company name, Administrator role, and email on `/dashboard`.
- The dashboard clearly shows an empty workspace with employees, hardware, licenses, and renewals sections.
- Sign out still works from the dashboard.

**Implementation Note**: This phase should improve UI enough to be credible, but it must not implement S-03 data entry or S-05 calculations.

---

## Phase 3: Public Rebrand and Handoff

Remove starter-facing first impressions and document the completed S-01 behavior.

### Changes Required

- `src/components/Welcome.astro`
  - **Intent**: Replace scaffold marketing with ITventory-specific product entry.
  - **Contract**: Use ITventory as the primary first-viewport signal, explain inventory/licensing/renewal value, and keep sign-in/sign-up calls to action. Remove `10x Astro Starter` and generic starter feature cards.

- `src/layouts/Layout.astro`
  - **Intent**: Stop exposing starter naming in browser metadata defaults.
  - **Contract**: Default title becomes ITventory-oriented.

- `README.md`
  - **Intent**: Mark the app routes and smoke instructions as product-specific rather than scaffold-specific.
  - **Contract**: Route table and local smoke path mention `/` as ITventory entry and `/dashboard` as the empty company workspace.

- `context/changes/admin-company-start/plan.md`
  - **Intent**: Record progress as phases land.
  - **Contract**: Only the `## Progress` section receives checkbox updates during implementation.

### Success Criteria

#### Automated Verification

- No user-facing page copy still presents the app as `10x Astro Starter`.
- README documents the S-01 start path.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Anonymous `/` clearly presents ITventory, not the starter.
- Anonymous `/` offers sign-up and sign-in entry points.
- Signed-in users can still navigate to `/dashboard` from the public entry/topbar.
- Reviewer confirms S-02 and S-03 can build on this start path without redefining company context.

## Testing Strategy

### Automated

- `npm run lint`
- `npm run build`
- Text search for remaining user-facing `10x Astro Starter` references after phase 3.

### Manual

1. Start the app locally.
2. Open `/` while signed out and confirm the page is ITventory-branded.
3. Open `/auth/signup`, create a new account with company name, email, and password.
4. Confirm active-session signup lands on `/dashboard`; if local auth requires confirmation, confirm `/auth/confirm-email` remains clear.
5. Sign in with a user that has a membership and confirm redirect to `/dashboard`.
6. Confirm `/dashboard` shows company name, Administrator role, email, and empty sections for employees, hardware, licenses, and renewals.
7. Confirm sign out works.
8. Confirm a signed-in user without membership still cannot see dashboard content and is routed to `/auth/company-required`.

## Performance Considerations

No new database reads should be added beyond the existing protected-route membership lookup. Dashboard empty states are static and should render within the existing Astro page budget. Avoid client-side hydration for static dashboard content unless a future phase adds interactive data entry.

## Migration Notes

No database migration is required. Existing F-01 migrations remain the company and role contract. Existing auth users without membership remain supported only by the fail-closed `company-required` recovery path.

## References

- Roadmap: `context/foundation/roadmap.md`
- PRD: `context/foundation/prd.md`
- F-01 plan: `context/changes/f-01/plan.md`
- Current signup API: `src/pages/api/auth/signup.ts`
- Current signin API: `src/pages/api/auth/signin.ts`
- Current protected routing: `src/middleware.ts`
- Current dashboard: `src/pages/dashboard.astro`
- Current public entry: `src/components/Welcome.astro`
- Current layout metadata: `src/layouts/Layout.astro`

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles. See `references/progress-format.md`.

### Phase 1: Auth Entry Routing

#### Automated

- [x] 1.1 Signup API keeps the no-session `/auth/confirm-email` branch and redirects active-session signup to `/dashboard`.
- [x] 1.2 Signin API redirects successful login to `/dashboard`.
- [x] 1.3 `npm run lint` passes.
- [x] 1.4 `npm run build` passes.

#### Manual

- [x] 1.5 Creating an account with company name lands on `/dashboard` when local Supabase returns a session.
- [x] 1.6 If email confirmation prevents an immediate session, signup still lands on `/auth/confirm-email`.
- [x] 1.7 Signing in with an existing membership lands on `/dashboard`.

### Phase 2: Empty Company Workspace

#### Automated

- [ ] 2.1 Dashboard renders without requiring any inventory, license, hardware, employee, or reminder tables.
- [ ] 2.2 Dashboard uses `Astro.locals.company`, `Astro.locals.role`, and `Astro.locals.user` without unsafe client-submitted company identifiers.
- [ ] 2.3 `npm run lint` passes.
- [ ] 2.4 `npm run build` passes.

#### Manual

- [ ] 2.5 A newly signed-up admin sees the company name, Administrator role, and email on `/dashboard`.
- [ ] 2.6 The dashboard clearly shows an empty workspace with employees, hardware, licenses, and renewals sections.
- [ ] 2.7 Sign out still works from the dashboard.

### Phase 3: Public Rebrand and Handoff

#### Automated

- [ ] 3.1 No user-facing page copy still presents the app as `10x Astro Starter`.
- [ ] 3.2 README documents the S-01 start path.
- [ ] 3.3 `npm run lint` passes.
- [ ] 3.4 `npm run build` passes.

#### Manual

- [ ] 3.5 Anonymous `/` clearly presents ITventory, not the starter.
- [ ] 3.6 Anonymous `/` offers sign-up and sign-in entry points.
- [ ] 3.7 Signed-in users can still navigate to `/dashboard` from the public entry/topbar.
- [ ] 3.8 Reviewer confirms S-02 and S-03 can build on this start path without redefining company context.
