# Onboarding Template

## Executive Summary

S-06 adds the last proposed MVP slice: the owner can define reusable onboarding templates and apply one to an existing employee. A template represents the recurring package for a role or team: textual hardware requirements plus concrete software licenses from the company inventory.

The implementation stays manual and internal. Applying a template creates real current-state license assignments for the employee, shows which requirements still need a concrete hardware choice, and skips duplicate assignments instead of failing the whole operation. No external provisioning, invitations, role changes, or automated account creation are included.

## Context

- Roadmap: S-06 `onboarding-template`, prerequisite S-02 inventory and S-03 assignments, status `proposed`.
- PRD refs: FR-014, FR-015, FR-016, US-08.
- Current code already has company-scoped employees, hardware, software licenses, current assignments, dashboard metrics, and form-based POST endpoints.
- Existing patterns to follow:
  - `src/lib/inventory-records.ts` centralizes typed inventory records, form cleaning, reads, and mutations.
  - `/api/inventory/*` and `/api/assignments` use POST form submissions, redirect back to `/dashboard`, and surface status through query params.
  - `src/middleware.ts` protects `/dashboard`, `/api/inventory`, and `/api/assignments`.
  - Supabase migrations define `company_id`, useful indexes, `set_updated_at` triggers, grants, and owner-only RLS policies.

## Current State Analysis

Inventory records are available through `getInventoryRecords`, which returns employees, hardware, licenses, and current assignments. Assignment creation already validates that selected records belong to the current company and inserts into `asset_assignments`; the database adds shape checks and uniqueness guards for current relationships.

The dashboard is currently a single protected workspace. It renders review signals, assignment forms, record creation forms, and editable record lists. This is getting dense, but keeping S-06 on `/dashboard` follows the current MVP pattern and avoids a navigation/routing detour.

There is no onboarding-specific schema or API yet. Existing references to onboarding templates only live in foundation docs and old scope boundaries.

## Decisions

| Decision | Choice | Reason |
| --- | --- | --- |
| Apply behavior | Applying a template creates real assignment rows where possible | This makes the workflow immediately visible in "Current assignments" and proves product value. |
| Template item model | Hardware is stored as textual requirements; licenses reference concrete `software_licenses` | Hardware instances are chosen at onboarding time, while licenses are reusable catalog records. |
| Employee target | Apply requires an existing employee | This preserves the current employee CRUD flow and keeps the first implementation focused. |
| Conflict behavior | Skip duplicate license assignments and report created/skipped counts | Idempotent apply is friendlier than failing when a user retries or partially prepared a person. |
| Editing scope | Create, edit, and delete templates and template items | A template feature needs correction paths to remain usable after the first typo or role change. |
| UI placement | Add onboarding templates to `/dashboard` | Matches the established dashboard-first MVP pattern. |
| Hardware apply | Show hardware requirements and let the owner choose concrete available hardware during apply | Avoids assigning the same device through a reusable template. |
| Verification | Use lint/build plus manual UI flow; do not make local Supabase reset a hard gate | This is realistic for the current repo while still calling out migration validation separately. |

## Scope

### In Scope

- Company-scoped onboarding template schema.
- Template name, optional description/notes, and ordered template items.
- Template item kinds:
  - hardware requirement with required asset type/name and optional notes.
  - software license requirement referencing an existing company software license.
- Typed onboarding records and mutation helpers in the existing inventory boundary.
- Protected API endpoint for template create/update/delete, item create/delete, and apply.
- Dashboard section for managing templates, adding/removing items, applying a template to an existing employee, selecting concrete hardware for hardware requirements, and showing apply results.
- Duplicate-safe application of license assignments and selected hardware assignments.
- Lint/build verification and manual dashboard verification.

### Out of Scope

- Creating employees inside the apply flow.
- External provisioning, account creation, SSO, MDM, SaaS APIs, or email invitations.
- Template-driven license seat accounting.
- Assignment history, audit trail, effective dates, or approval workflow.
- Multi-user roles, manager permissions, or template sharing.
- Dedicated `/onboarding` page or richer navigation.
- Bulk import/export of templates.

## Target Architecture

S-06 should extend the current inventory module rather than introduce a parallel domain layer. The dashboard reads onboarding templates together with inventory records, then posts form actions to one onboarding API endpoint.

Data flow:

1. Owner creates or edits a template on `/dashboard`.
2. Owner adds hardware requirement items or license requirement items.
3. Owner selects an existing employee and applies the template.
4. Server validates the template, employee, selected hardware, and licenses all belong to the current company.
5. Server inserts missing `asset_assignments` for licenses and selected hardware, skips existing duplicates, and redirects back with a concise result.

## Data Contract

### Tables

- `public.onboarding_templates`
  - `id uuid primary key default gen_random_uuid()`
  - `company_id uuid not null references public.companies(id) on delete cascade`
  - `name text not null check (length(btrim(name)) > 0)`
  - `description text`
  - `created_at timestamptz not null default now()`
  - `updated_at timestamptz not null default now()`

- `public.onboarding_template_items`
  - `id uuid primary key default gen_random_uuid()`
  - `company_id uuid not null references public.companies(id) on delete cascade`
  - `template_id uuid not null references public.onboarding_templates(id) on delete cascade`
  - `item_type text not null check (item_type in ('hardware_requirement', 'software_license'))`
  - `hardware_label text`
  - `software_license_id uuid references public.software_licenses(id) on delete cascade`
  - `notes text`
  - `position integer not null default 0`
  - `created_at timestamptz not null default now()`
  - `updated_at timestamptz not null default now()`
  - shape check requiring `hardware_label` only for hardware items and `software_license_id` only for software items.

### Invariants

- Every template and item belongs to exactly one company.
- Every item's `company_id` must match its parent template's company.
- Every software item must reference a license from the same company.
- Applying a template can only target an employee from the same company.
- Applying selected hardware can only assign hardware from the same company.
- Applying the same template twice to the same employee should not create duplicate license assignments.
- A reusable template must not store concrete hardware asset ids as its default requirement.

### RLS Contract

Use owner-only RLS matching inventory and assignment migrations:

- authenticated owners can select, insert, update, and delete their company's onboarding templates.
- authenticated owners can select, insert, update, and delete their company's onboarding template items.
- cross-company template, item, license, hardware, and employee references fail closed through RLS and trigger/helper validation.

## Phase 1: Onboarding Template Data Contract

Add the persistent, company-scoped template model and typed server boundary.

### Changes Required

- `supabase/migrations/<timestamp>_onboarding_templates.sql`
  - **Intent**: Add the minimal schema for reusable, manual onboarding templates.
  - **Contract**: Create `onboarding_templates` and `onboarding_template_items` with company ownership, item shape constraints, indexes, `set_updated_at` triggers, same-company validation triggers, grants, and owner-only RLS policies.

- `src/lib/inventory-records.ts`
  - **Intent**: Extend the existing typed inventory boundary with onboarding template records and CRUD helpers.
  - **Contract**: Export template/item interfaces, include `onboardingTemplates` in the dashboard read model, and add helpers for upserting/deleting templates plus creating/deleting items. Reuse the existing text cleaning helpers and company ownership assertions where possible.

- `src/middleware.ts`
  - **Intent**: Protect onboarding template mutations with the same company context as inventory and assignments.
  - **Contract**: Add the onboarding API prefix to protected routes.

### Success Criteria

#### Automated Verification

- Migration defines `onboarding_templates` and `onboarding_template_items` with `company_id`, item shape checks, indexes, and updated-at triggers.
- Migration enforces same-company template item and software license references.
- RLS policies restrict template and item access to authenticated owners in the same company.
- Typed inventory reads include onboarding templates and their items.
- Template and item mutations reject records that do not belong to the current company.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Reviewer confirms the data contract stays manual and internal, with no external provisioning implied.
- Reviewer confirms archived changes were not edited while creating the S-06 plan or implementation.

---

## Phase 2: Template Management UI and API

Let the owner create, edit, delete, and populate templates from the dashboard.

### Changes Required

- `src/pages/api/onboarding-templates.ts`
  - **Intent**: Handle form submissions for template and item management.
  - **Contract**: POST-only endpoint; supports actions for template upsert/delete and item create/delete; redirects to `/dashboard` with onboarding-specific status params.

- `src/pages/dashboard.astro`
  - **Intent**: Add a dashboard section for onboarding template management without creating a new route.
  - **Contract**: Render template counts, create/edit forms, item add forms for hardware requirements and software licenses, item lists, and delete controls. Reuse existing `inputClass`, `textareaClass`, `buttonClass`, and compact card patterns.

### Success Criteria

#### Automated Verification

- Dashboard contains forms posting onboarding template actions to the protected API.
- Dashboard renders existing onboarding templates with hardware and software items.
- Dashboard can add hardware requirement items without concrete hardware asset ids.
- Dashboard can add software license items only from existing company licenses.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Owner can create an onboarding template.
- Owner can edit a template name and description.
- Owner can delete a template.
- Owner can add and remove a hardware requirement item.
- Owner can add and remove a software license item.
- Empty states are clear when no templates or no licenses exist.
- Dashboard remains readable on desktop and mobile.

---

## Phase 3: Apply Template Workflow

Turn a saved template into current employee assignments while staying duplicate-safe.

### Changes Required

- `src/lib/inventory-records.ts`
  - **Intent**: Add the application workflow as a server helper so validation and duplicate handling remain testable.
  - **Contract**: Export an apply helper that accepts company id and form data, validates target employee, validates template ownership, validates selected hardware ids for hardware requirements, creates missing `asset_assignments`, skips duplicates, and returns created/skipped counts plus unresolved hardware requirements.

- `src/pages/api/onboarding-templates.ts`
  - **Intent**: Route the apply action through the same protected endpoint.
  - **Contract**: Support an `apply` action and redirect to `/dashboard` with a concise success/error message that distinguishes created assignments from skipped duplicates.

- `src/pages/dashboard.astro`
  - **Intent**: Let the owner apply a template to an existing employee and choose concrete hardware for hardware requirements.
  - **Contract**: For each template, render an apply form with employee selection, license requirements shown as automatic assignment candidates, and hardware requirement selectors that can choose available hardware or leave a requirement unresolved.

### Success Criteria

#### Automated Verification

- Apply validates that the employee belongs to the current company.
- Apply validates that the template and all selected hardware records belong to the current company.
- Apply creates missing license-to-employee assignments for software license items.
- Apply creates hardware-to-employee assignments for selected hardware requirements.
- Apply skips duplicate assignments without failing the whole request.
- Apply reports created and skipped assignment counts in the redirect message.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Owner can apply a template to an existing employee.
- Applying a template creates visible current assignments for license items.
- Applying a template creates visible current assignments for selected hardware items.
- Re-applying the same template does not create duplicates and shows a useful skipped count.
- Leaving a hardware requirement unresolved does not block license assignments.
- Cross-company records cannot be selected or applied through the UI.
- Dashboard remains readable on desktop and mobile.

---

## Phase 4: Polish, Copy, and Handoff

Make the slice feel complete and update stale dashboard messaging.

### Changes Required

- `src/pages/dashboard.astro`
  - **Intent**: Replace stale "next slices" copy with current MVP status and make onboarding templates part of the workspace summary.
  - **Contract**: Workspace cards and footer copy should reflect that assignments, renewals, reminders, and onboarding templates are live after S-06, without implying external provisioning.

- `context/changes/onboarding-template/plan.md`
  - **Intent**: Keep implementation progress auditable.
  - **Contract**: Only the `## Progress` section receives checkbox updates during implementation; append commit SHA when a phase lands.

### Success Criteria

#### Automated Verification

- Text search confirms the dashboard no longer presents S-06 as a future slice after implementation.
- Text search confirms onboarding copy does not promise external provisioning or integrations.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Owner can understand the onboarding template flow from dashboard labels and empty states.
- Reviewer confirms the feature remains within PRD FR-014, FR-015, and FR-016.
- Reviewer confirms the dashboard still feels coherent after adding the new section.

## Testing Strategy

### Automated

- `rg -n "onboarding_templates|onboarding_template_items|onboarding-template|/api/onboarding-templates|apply" supabase src`
- `rg -n "external provisioning|coming next|S-06|Next slices" src/pages/dashboard.astro`
- `npm run lint`
- `npm run build`

### Manual

1. Apply migrations in a local or staging Supabase environment when available.
2. Sign in as an owner and open `/dashboard`.
3. Create at least one employee, one available hardware record, and one software license.
4. Create an onboarding template.
5. Add one hardware requirement and one software license item to the template.
6. Apply the template to the employee, selecting concrete hardware for the hardware requirement.
7. Confirm the current assignments list shows the license-to-employee and hardware-to-employee relationships.
8. Apply the same template again and confirm duplicate assignments are skipped with a useful message.
9. Apply with a hardware requirement left unresolved and confirm license assignment still succeeds.
10. Check desktop and mobile layouts for readable cards, forms, and status messages.

## Performance Considerations

The MVP data volume is small. Template reads can be included in the existing dashboard load with company-scoped filters and simple joins. Indexes on `company_id`, `template_id`, and item position are sufficient for expected scale. Apply should do bounded work over a single template's items rather than scanning all records.

## Migration Notes

This change adds new Supabase tables. Do not run remote database migrations without explicit human approval. Rollback is straightforward before production use by dropping `onboarding_template_items` and `onboarding_templates`; after production use, export or preserve template data before destructive rollback.

## References

- PRD: `context/foundation/prd.md`
- Roadmap: `context/foundation/roadmap.md`
- Current inventory helper: `src/lib/inventory-records.ts`
- Current dashboard: `src/pages/dashboard.astro`
- Assignment API pattern: `src/pages/api/assignments.ts`
- Inventory migration pattern: `supabase/migrations/20260606182000_inventory_records.sql`
- Assignment migration pattern: `supabase/migrations/20260606203000_asset_assignments.sql`
- Prior S-03 plan: `context/archive/2026-06-06-asset-assignments/plan.md`

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles.

### Phase 1: Onboarding Template Data Contract

#### Automated

- [x] 1.1 Migration defines `onboarding_templates` and `onboarding_template_items` with `company_id`, item shape checks, indexes, and updated-at triggers.
- [x] 1.2 Migration enforces same-company template item and software license references.
- [x] 1.3 RLS policies restrict template and item access to authenticated owners in the same company.
- [x] 1.4 Typed inventory reads include onboarding templates and their items.
- [x] 1.5 Template and item mutations reject records that do not belong to the current company.
- [x] 1.6 `npm run lint` passes.
- [x] 1.7 `npm run build` passes.

#### Manual

- [x] 1.8 Reviewer confirms the data contract stays manual and internal, with no external provisioning implied.
- [x] 1.9 Reviewer confirms archived changes were not edited while creating the S-06 plan or implementation.

### Phase 2: Template Management UI and API

#### Automated

- [ ] 2.1 Dashboard contains forms posting onboarding template actions to the protected API.
- [ ] 2.2 Dashboard renders existing onboarding templates with hardware and software items.
- [ ] 2.3 Dashboard can add hardware requirement items without concrete hardware asset ids.
- [ ] 2.4 Dashboard can add software license items only from existing company licenses.
- [ ] 2.5 `npm run lint` passes.
- [ ] 2.6 `npm run build` passes.

#### Manual

- [ ] 2.7 Owner can create an onboarding template.
- [ ] 2.8 Owner can edit a template name and description.
- [ ] 2.9 Owner can delete a template.
- [ ] 2.10 Owner can add and remove a hardware requirement item.
- [ ] 2.11 Owner can add and remove a software license item.
- [ ] 2.12 Empty states are clear when no templates or no licenses exist.
- [ ] 2.13 Dashboard remains readable on desktop and mobile.

### Phase 3: Apply Template Workflow

#### Automated

- [ ] 3.1 Apply validates that the employee belongs to the current company.
- [ ] 3.2 Apply validates that the template and all selected hardware records belong to the current company.
- [ ] 3.3 Apply creates missing license-to-employee assignments for software license items.
- [ ] 3.4 Apply creates hardware-to-employee assignments for selected hardware requirements.
- [ ] 3.5 Apply skips duplicate assignments without failing the whole request.
- [ ] 3.6 Apply reports created and skipped assignment counts in the redirect message.
- [ ] 3.7 `npm run lint` passes.
- [ ] 3.8 `npm run build` passes.

#### Manual

- [ ] 3.9 Owner can apply a template to an existing employee.
- [ ] 3.10 Applying a template creates visible current assignments for license items.
- [ ] 3.11 Applying a template creates visible current assignments for selected hardware items.
- [ ] 3.12 Re-applying the same template does not create duplicates and shows a useful skipped count.
- [ ] 3.13 Leaving a hardware requirement unresolved does not block license assignments.
- [ ] 3.14 Cross-company records cannot be selected or applied through the UI.
- [ ] 3.15 Dashboard remains readable on desktop and mobile.

### Phase 4: Polish, Copy, and Handoff

#### Automated

- [ ] 4.1 Text search confirms the dashboard no longer presents S-06 as a future slice after implementation.
- [ ] 4.2 Text search confirms onboarding copy does not promise external provisioning or integrations.
- [ ] 4.3 `npm run lint` passes.
- [ ] 4.4 `npm run build` passes.

#### Manual

- [ ] 4.5 Owner can understand the onboarding template flow from dashboard labels and empty states.
- [ ] 4.6 Reviewer confirms the feature remains within PRD FR-014, FR-015, and FR-016.
- [ ] 4.7 Reviewer confirms the dashboard still feels coherent after adding the new section.
