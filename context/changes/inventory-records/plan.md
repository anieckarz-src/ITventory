# Owner Inventory Records

## Executive Summary

S-02 adds the first domain data the product needs: employees, hardware, and software licenses inside the owner company workspace. This gives later assignment, dashboard, and renewal email slices real records to build on.

The implementation is a narrow MVP CRUD surface on `/dashboard`: create/edit/view employees, create/edit/delete/view hardware, and create/edit/view licenses with renewal dates and monthly/annual cost metadata. It does not implement assignments, renewal dashboard calculations, onboarding templates, or email delivery.

## Scope

### In Scope

- Supabase tables for employees, hardware assets, and software licenses.
- Company-scoped RLS for those tables.
- Protected `/api/inventory/*` endpoints for form submissions.
- Dashboard lists, counts, add forms, and compact edit forms.
- Owner-only copy and route protection.
- Lint/build verification.

### Out of Scope

- Assignments between assets and employees.
- Renewal dashboard analytics.
- Reminder scheduling or email sending.
- Onboarding templates.
- Bulk import/export.

## Phase 1: Data Contract and Server Mutations

### Changes Required

- `supabase/migrations/<timestamp>_inventory_records.sql`
  - **Intent**: Add the minimal domain tables for S-02.
  - **Contract**: Define company-scoped `employees`, `hardware_assets`, and `software_licenses` with timestamps, indexes, RLS, and owner membership policies.

- `src/middleware.ts`
  - **Intent**: Protect inventory mutation endpoints with the same company context as dashboard.
  - **Contract**: `/api/inventory` routes require authenticated company context.

- `src/lib/inventory-records.ts`
  - **Intent**: Centralize typed reads and mutations for inventory records.
  - **Contract**: Expose read helpers for dashboard and mutation helpers for API routes; every mutation receives `companyId`.

- `src/pages/api/inventory/employees.ts`, `hardware.ts`, `licenses.ts`
  - **Intent**: Handle dashboard form submissions.
  - **Contract**: POST-only endpoints; support create/update where required, hardware delete, redirect back to `/dashboard` with status query params.

### Success Criteria

#### Automated Verification

- Migration defines all three inventory tables with `company_id`.
- Inventory API routes are protected by middleware.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Unauthenticated inventory API requests do not mutate data.
- Owner company context is required for all inventory mutations.

---

## Phase 2: Dashboard Inventory UI

### Changes Required

- `src/pages/dashboard.astro`
  - **Intent**: Turn the empty workspace into the S-02 inventory workspace.
  - **Contract**: Render counts, add forms, and existing records for employees, hardware, and software licenses. Preserve owner/company topbar and signout behavior.

### Success Criteria

#### Automated Verification

- Dashboard queries employees, hardware, and licenses through typed helpers.
- Dashboard contains forms posting to `/api/inventory/employees`, `/api/inventory/hardware`, and `/api/inventory/licenses`.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Owner can add and edit an employee.
- Owner can add, edit, and delete hardware.
- Owner can add and edit a software license with renewal date and cost.
- Dashboard remains readable on desktop and mobile.

## Testing Strategy

### Automated

- `rg -n "create table public.(employees|hardware_assets|software_licenses)|/api/inventory" supabase src`
- `npm run lint`
- `npm run build`

### Manual

1. Apply migrations locally with `npx supabase db reset` when local Supabase is available.
2. Sign up/sign in as owner and open `/dashboard`.
3. Add an employee, edit the name/email/title.
4. Add hardware, edit its status/serial/model, then delete it.
5. Add a software license with renewal date and cost, then edit it.
6. Confirm records remain company-scoped and no assignment behavior is implied as live.

## Migration Notes

This is a local schema migration only. Do not run remote database migrations without explicit human approval.

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles.

### Phase 1: Data Contract and Server Mutations

#### Automated

- [x] 1.1 Migration defines all three inventory tables with `company_id`.
- [x] 1.2 Inventory API routes are protected by middleware.
- [x] 1.3 `npm run lint` passes.
- [x] 1.4 `npm run build` passes.

#### Manual

- [x] 1.5 Unauthenticated inventory API requests do not mutate data.
- [x] 1.6 Owner company context is required for all inventory mutations.

### Phase 2: Dashboard Inventory UI

#### Automated

- [x] 2.1 Dashboard queries employees, hardware, and licenses through typed helpers.
- [x] 2.2 Dashboard contains forms posting to `/api/inventory/employees`, `/api/inventory/hardware`, and `/api/inventory/licenses`.
- [x] 2.3 `npm run lint` passes.
- [x] 2.4 `npm run build` passes.

#### Manual

- [ ] 2.5 Owner can add and edit an employee.
- [ ] 2.6 Owner can add, edit, and delete hardware.
- [ ] 2.7 Owner can add and edit a software license with renewal date and cost.
- [ ] 2.8 Dashboard remains readable on desktop and mobile.
