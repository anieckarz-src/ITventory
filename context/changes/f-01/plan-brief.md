# Minimalny kontrakt firmy, czlonkostwa i rol — Plan Brief

> Full plan: `context/changes/f-01/plan.md`

## What & Why

F-01 creates the minimum company and role boundary required before ITventory can safely add inventory, invitations, dashboards, and reminders. The goal is to ensure each authenticated user operates inside one company context and has a clear MVP role: `admin` or `manager`.

## Starting Point

The app currently has Supabase auth, sign in/sign up routes, and an auth-protected dashboard. It does not yet have company tables, memberships, RLS policies, or server-side role context.

## Desired End State

Signup requires a company name, creates a company, and records the signer as `admin`. Protected routes resolve the current company and role before showing company-scoped content. Missing membership fails closed into a recoverable onboarding/error state.

## Key Decisions Made

| Decision | Choice | Why |
| --- | --- | --- |
| Company creation | Signup creates company + admin membership | Proves the company boundary end-to-end and unlocks S-01. |
| Company name | Required on signup | Avoids inferred or placeholder company records. |
| Role values | `admin` / `manager` | Simple database values with clear UI label mapping. |
| Missing membership | Recoverable fail-closed route/state | Handles partial accounts without leaking dashboard data. |
| Membership scope | One active company per user for MVP | Keeps the app simple while leaving schema room for future multi-company support. |
| Isolation | RLS plus server helpers | Puts the boundary in the database and keeps app code consistent. |
| Verification | Migration + signup smoke + role helper checks | Proves schema and app integration together. |

## Scope

**In scope:**

- Supabase migration for `companies` and `company_memberships`.
- RLS policies and safe current-user company creation path.
- Server helper for current company/membership/role lookup.
- Middleware locals for company and role context.
- Signup form/API update for required company name.
- Dashboard display of current company and role for smoke verification.
- Missing-membership recovery/error state.
- README updates for migrations and smoke path.

**Out of scope:**

- Manager invitations.
- Company settings UI beyond recovery/error handling.
- Custom RBAC.
- Inventory, assets, licenses, assignments, dashboard costs, and reminders.
- Full multi-company switcher.

## Architecture / Approach

Supabase Auth remains responsible for identity. New domain tables hold companies and memberships, with RLS checking `auth.uid()` membership. Astro middleware uses a shared access-context helper to resolve the current company and role once per protected request and exposes that through `Astro.locals`.

## Phases at a Glance

| Phase | What it delivers | Key risk |
| --- | --- | --- |
| 1. Database Access Contract | Tables, role contract, RLS, and safe creation path | RLS can be too open or too restrictive. |
| 2. Server Context and Signup Integration | Signup creates company/admin membership; protected routes require membership | Supabase email-confirmation/session behavior may affect when company creation runs. |
| 3. Verification, Fixtures, and Handoff | README, smoke path, and optional lightweight helper checks | Local Supabase availability may limit automated verification. |

**Prerequisites:** Supabase env vars available for full smoke testing; production schema migration remains human-approved.
**Estimated effort:** ~2-3 focused implementation sessions across 3 phases.

## Open Risks & Assumptions

- Existing auth users may have no membership after migration; the app must fail closed and recover cleanly.
- The safe company creation path must account for whether Supabase returns a session immediately after signup in the local/prod auth configuration.
- Future multi-company support is deliberately deferred; multiple memberships should be unsupported until a switcher exists.

## Success Criteria (Summary)

- A new signup with company name creates one company and one `admin` membership.
- `/dashboard` only shows company-scoped content after membership context resolves.
- Later slices can attach domain tables to `company_id` without redefining company or role concepts.
