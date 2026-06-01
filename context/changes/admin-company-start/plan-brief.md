# Admin company start - Plan Brief

> Full plan: `context/changes/admin-company-start/plan.md`

## What & Why

Build the first usable ITventory company start path: an Administrator signs up with a company, authenticates, and lands in an empty company workspace. This matters because every later slice needs a clear company context and a credible starting point before inventory, invitations, dashboards, and reminders are added.

## Starting Point

F-01 already created companies, memberships, role context, protected `/dashboard`, and signup company creation. The app still behaves like a starter in key places: signup/signin routing does not prioritize the workspace, `/dashboard` is a technical smoke screen, and `/` still markets `10x Astro Starter`.

## Desired End State

An active-session signup and any successful signin land on `/dashboard`. The dashboard shows the current company, Administrator role, signed-in email, and empty operational sections for employees, hardware, licenses, and renewals. Anonymous users see a strongly ITventory-branded public entry instead of starter content.

## Key Decisions Made

| Decision | Choice | Why |
| --- | --- | --- |
| Signup destination | Active session goes to `/dashboard` | S-01 is about reaching the company workspace immediately when possible. |
| No-session fallback | Keep `/auth/confirm-email` | Supabase may require email confirmation before a session exists. |
| Signin destination | Successful signin goes to `/dashboard` | Returning users should enter the workspace, not the public landing page. |
| Empty workspace | Operational dashboard sections | Gives S-03/S-05 obvious landing zones without inventing data. |
| Public page | Strong ITventory rebrand | Removes starter scaffolding from the first impression. |
| No-membership users | Keep `company-required` | Existing F-01 fail-closed behavior is safe and sufficient for S-01. |
| Data changes | No migrations | F-01 already owns the company and role contract. |

## Scope

**In scope:**

- Auth redirects for signup/signin.
- Empty company workspace at `/dashboard`.
- ITventory public entry at `/`.
- Default title/relevant docs cleanup.
- README smoke path update.

**Out of scope:**

- Manager invitations.
- Inventory CRUD.
- Company settings.
- Self-service recovery for users without membership.
- New database tables, migrations, or role changes.

## Architecture / Approach

Keep F-01's access boundary unchanged. Auth endpoints control post-auth navigation, middleware continues to resolve `Astro.locals.company`, `Astro.locals.role`, and `Astro.locals.user`, and dashboard renders static empty-state sections from those locals. Public `/` becomes a product entry page with sign-up/sign-in CTAs.

## Phases at a Glance

| Phase | What it delivers | Key risk |
| --- | --- | --- |
| 1. Auth Entry Routing | Signup/signin land in the workspace when possible | Breaking confirm-email fallback for Supabase configs without immediate sessions. |
| 2. Empty Company Workspace | Product-grade empty dashboard for a new company | Accidentally implying data entry features exist before S-03. |
| 3. Public Rebrand and Handoff | ITventory public entry and docs alignment | Leaving starter copy in user-facing surfaces. |

**Prerequisites:** F-01 implemented and migrations available locally.
**Estimated effort:** 1-2 implementation sessions across 3 small phases.

## Open Risks & Assumptions

- Supabase email confirmation behavior may differ between local and production; the plan preserves both paths.
- No inventory tables exist yet, so dashboard sections must stay static empty states.
- Existing users without membership remain a recovery/error path, not a setup wizard.

## Success Criteria (Summary)

- A new admin with a usable session reaches `/dashboard` after signup and sees the correct company context.
- The empty dashboard communicates the next operational areas without fake data or unavailable links.
- Public `/` and default metadata present ITventory, not the starter.
