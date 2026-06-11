# Onboarding Template - Plan Brief

> Full plan: `context/changes/onboarding-template/plan.md`

## What & Why

Build S-06: a company owner can create a reusable onboarding template and apply it to an existing employee. The feature turns recurring "new hire package" work into a repeatable manual workflow using the inventory and assignment records already built.

## Starting Point

The app already has company-scoped employees, hardware, software licenses, current assignments, renewal dashboard signals, and protected form-based API endpoints. There is no onboarding template schema, API, or UI yet.

## Desired End State

The owner can define a template with textual hardware requirements and concrete software license requirements. Applying the template to an existing employee creates visible current assignments where possible, skips duplicates, and leaves unresolved hardware requirements as a manual choice rather than pretending to provision anything externally.

## Key Decisions Made

| Decision | Choice | Why |
| --- | --- | --- |
| Apply behavior | Create real assignments | The workflow becomes visible immediately in current assignments. |
| Template items | Text hardware requirements plus concrete license refs | Hardware instances are chosen during onboarding; licenses are reusable records. |
| Employee target | Existing employee only | Keeps S-06 focused and reuses current employee CRUD. |
| Conflicts | Skip duplicates and report counts | Re-applying stays idempotent and user-friendly. |
| Editing | Create, edit, delete templates and items | Templates need correction paths to be practically useful. |
| UI placement | Dashboard section | Matches the current MVP workspace pattern. |
| Verification | Lint/build plus manual UI flow | Strong enough for this repo without making local Supabase reset a hard blocker. |

## Scope

**In scope:**

- Supabase tables for onboarding templates and template items.
- Hardware requirement items stored as text, not concrete hardware ids.
- Software license items referencing existing company licenses.
- Typed helper functions and protected API actions.
- Dashboard UI for create/edit/delete, item management, and applying templates.
- Duplicate-safe assignment creation and clear apply results.

**Out of scope:**

- Creating employees inside the apply flow.
- External provisioning, SaaS APIs, invitations, or account creation.
- Seat accounting, audit history, approval workflows, exports, or a dedicated onboarding page.

## Architecture / Approach

Extend the existing inventory boundary. `src/lib/inventory-records.ts` gains template types, reads, mutations, and apply logic; `/api/onboarding-templates` handles dashboard form actions; `/dashboard` renders management and apply forms. Supabase enforces company ownership through schema checks, triggers, grants, and owner-only RLS.

## Phases at a Glance

| Phase | What it delivers | Key risk |
| --- | --- | --- |
| 1. Data Contract | Tables, RLS, typed reads, mutations, route protection | Cross-company references must fail closed. |
| 2. Management UI/API | Create/edit/delete templates and items on dashboard | Dashboard may become too dense. |
| 3. Apply Workflow | Apply template to employee with duplicate-safe assignments | Conflict handling must be clear and reliable. |
| 4. Polish & Handoff | Updated copy, final verification, progress discipline | Copy must not imply external provisioning. |

**Prerequisites:** S-02 inventory and S-03 assignments are implemented.
**Estimated effort:** ~3-4 focused sessions across 4 phases.

## Open Risks & Assumptions

- Local Supabase migration validation may need a manual environment; lint/build alone cannot prove RLS behavior.
- Dashboard density is the main UX risk because the MVP currently keeps all workspace operations on one page.
- Hardware requirements intentionally stay manual; choosing concrete available hardware happens during apply.

## Success Criteria (Summary)

- Owner can create a template, add hardware and license requirements, and edit/delete those records.
- Owner can apply a template to an existing employee and see resulting assignments.
- Re-applying a template skips duplicates and does not promise external provisioning.
