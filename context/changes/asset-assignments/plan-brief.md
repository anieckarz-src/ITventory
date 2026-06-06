# Owner Asset Assignments - Plan Brief

> Full plan: `context/changes/asset-assignments/plan.md`

## What & Why

Build S-03: current assignment relationships between employees, hardware, and software licenses. This turns the S-02 inventory lists into the practical "who has what" view required before dashboard analytics and onboarding templates.

## Starting Point

Employees, hardware, and software licenses already exist as company-scoped records with typed helpers, POST endpoints, and dashboard forms. The dashboard currently says assignments are planned but does not store or render relationships.

## Desired End State

The owner can create and remove current assignments from `/dashboard`: hardware to employee, license to employee, and license to hardware. The dashboard shows the current relationships in a compact, readable way while keeping assignment history, seat accounting, and onboarding outside this slice.

## Key Decisions Made

| Decision         | Choice                                       | Why                                                                                           |
| ---------------- | -------------------------------------------- | --------------------------------------------------------------------------------------------- |
| Assignment model | One company-scoped `asset_assignments` table | It supports the three MVP relationship types without fragmenting the schema.                  |
| License target   | One target per assignment row                | It keeps each row unambiguous and matches the confirmed MVP behavior.                         |
| Unassignment     | Delete the assignment row                    | This fits current-state MVP behavior and avoids audit-history scope.                          |
| UI location      | Extend `/dashboard`                          | The existing dashboard is the working inventory surface and already has the required records. |

## Scope

**In scope:**

- Assignment migration and RLS.
- Typed reads and mutation helpers.
- POST endpoint for create/delete.
- Dashboard forms and current assignment lists.

**Out of scope:**

- Assignment history.
- Seat-limit enforcement.
- Bulk workflows.
- Onboarding templates.
- Renewal dashboard analytics.

## Architecture / Approach

Use the same shape as S-02: migration first, then typed helper functions, then a protected form endpoint, then dashboard rendering. The assignment helper validates that referenced records belong to the current company before inserting an assignment row.

## Phases at a Glance

| Phase            | What it delivers                                 | Key risk                                                     |
| ---------------- | ------------------------------------------------ | ------------------------------------------------------------ |
| 1. Data Contract | Schema, RLS, typed reads, mutation validation    | Cross-company references must be rejected.                   |
| 2. Dashboard UI  | Create/remove forms and current assignment views | The dashboard can become dense if layout is not constrained. |

**Prerequisites:** S-02 inventory records are implemented and manually verified.
**Estimated effort:** 1 implementation session across 2 phases.

## Open Risks & Assumptions

- Local Supabase may not be available for live manual DB verification in this environment.
- Seat accounting is intentionally deferred, so assigning the same license to many targets is allowed unless duplicate target rows are attempted.

## Success Criteria (Summary)

- Owner can assign hardware to employees and licenses to employees or hardware.
- Assignment records stay company-scoped and protected by RLS/middleware.
- Dashboard remains readable and passes lint/build.
