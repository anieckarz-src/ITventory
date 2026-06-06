# Owner Asset Assignments

## Executive Summary

S-03 connects the inventory records from S-02 into current ownership relationships. The owner can assign hardware to an employee, assign a software license to an employee, or assign a software license to a hardware device, then see those relationships from the dashboard.

The implementation keeps the MVP narrow: assignments are current-state records, not historical audit trails. Each assignment record has exactly one assignment shape, and unassignment is represented by deleting that assignment record.

## Scope

### In Scope

- Supabase table for company-scoped asset assignments.
- RLS policies matching the owner-only company model.
- Typed assignment reads and mutation helpers.
- Protected `/api/assignments` endpoint for create/delete form submissions.
- Dashboard assignment panel with current assignment lists and add/remove forms.
- Lint/build verification.

### Out of Scope

- Assignment history, effective dates, or audit trails.
- Seat accounting or license over-allocation enforcement.
- Bulk assignment workflows.
- Onboarding templates.
- Renewal dashboard analytics.

## Decisions

| Decision         | Choice                                                                 | Reason                                                                               |
| ---------------- | ---------------------------------------------------------------------- | ------------------------------------------------------------------------------------ |
| Assignment shape | One table with an `assignment_type` enum-like text column              | Keeps current-state relationships simple while supporting the three PRD shapes.      |
| License target   | Each assignment row targets either one employee or one hardware record | Matches the user's confirmed MVP direction and avoids ambiguous rows.                |
| Unassignment     | Delete assignment rows                                                 | Fits the existing form-based CRUD pattern and keeps S-03 out of audit-history scope. |

## Phase 1: Assignment Data Contract

### Changes Required

- `supabase/migrations/<timestamp>_asset_assignments.sql`
  - **Intent**: Add the minimal relationship model for S-03.
  - **Contract**: Define `asset_assignments` with `company_id`, `assignment_type`, nullable employee/hardware/license references, shape check constraints, useful uniqueness guards, indexes, timestamps, and owner RLS policies.

- `src/lib/inventory-records.ts`
  - **Intent**: Extend the typed inventory boundary with assignment records and mutation helpers.
  - **Contract**: `getInventoryRecords` returns assignments alongside employees, hardware, and licenses; mutation helpers validate assignment type, target ids, and company ownership.

- `src/middleware.ts`
  - **Intent**: Protect assignment mutation endpoints with the same company context as inventory endpoints.
  - **Contract**: `/api/assignments` requires authenticated owner company context.

### Success Criteria

#### Automated Verification

- Migration defines `asset_assignments` with `company_id` and shape constraints.
- Assignment reads join employee, hardware, and license display fields.
- Assignment mutations reject records that do not belong to the current company.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Unauthenticated assignment API requests do not mutate data.
- Owner company context is required for all assignment mutations.

---

## Phase 2: Dashboard Assignment UI

### Changes Required

- `src/pages/api/assignments.ts`
  - **Intent**: Handle dashboard form submissions for assignment creation and deletion.
  - **Contract**: POST-only endpoint; supports `create` and `delete`; redirects back to `/dashboard` with status query params.

- `src/pages/dashboard.astro`
  - **Intent**: Add a current assignments workspace to the existing dashboard.
  - **Contract**: Render assignment counts, add forms for hardware-to-employee, license-to-employee, and license-to-hardware, plus removable current assignment rows.

### Success Criteria

#### Automated Verification

- Dashboard contains forms posting to `/api/assignments`.
- Dashboard renders current hardware and license assignments from typed helpers.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Owner can assign hardware to an employee.
- Owner can assign a license to an employee.
- Owner can assign a license to a hardware device.
- Owner can remove each assignment type.
- Dashboard remains readable on desktop and mobile.

## Testing Strategy

### Automated

- `rg -n "asset_assignments|/api/assignments|assignment_type" supabase src`
- `npm run lint`
- `npm run build`

### Manual

1. Apply migrations locally with `npx supabase db reset` when local Supabase is available.
2. Sign in as owner and open `/dashboard`.
3. Create at least one employee, hardware record, and license if needed.
4. Assign hardware to an employee.
5. Assign a license to an employee.
6. Assign a license to hardware.
7. Remove each assignment and confirm it disappears from current assignments.

## Migration Notes

This is a local schema migration only. Do not run remote database migrations without explicit human approval.

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles.

### Phase 1: Assignment Data Contract

#### Automated

- [x] 1.1 Migration defines `asset_assignments` with `company_id` and shape constraints.
- [x] 1.2 Assignment reads join employee, hardware, and license display fields.
- [x] 1.3 Assignment mutations reject records that do not belong to the current company.
- [x] 1.4 `npm run lint` passes.
- [x] 1.5 `npm run build` passes.

#### Manual

- [x] 1.6 Unauthenticated assignment API requests do not mutate data.
- [ ] 1.7 Owner company context is required for all assignment mutations.

### Phase 2: Dashboard Assignment UI

#### Automated

- [x] 2.1 Dashboard contains forms posting to `/api/assignments`.
- [x] 2.2 Dashboard renders current hardware and license assignments from typed helpers.
- [x] 2.3 `npm run lint` passes.
- [x] 2.4 `npm run build` passes.

#### Manual

- [ ] 2.5 Owner can assign hardware to an employee.
- [ ] 2.6 Owner can assign a license to an employee.
- [ ] 2.7 Owner can assign a license to a hardware device.
- [ ] 2.8 Owner can remove each assignment type.
- [ ] 2.9 Dashboard remains readable on desktop and mobile.
