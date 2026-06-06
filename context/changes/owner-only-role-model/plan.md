# Owner-Only Role Model

## Executive Summary

Ten change zmienia zalozenie produktu z modelu Administrator/Menedzer na jedna plaska role `owner`. MVP ma obslugiwac jednego wlasciciela firmy bez zaproszen i bez delegacji pracy do Menedzera.

Zmiana dotyka foundation docs, roadmapy, Supabase role contract, typed access context, dashboard label i README. Nie dodajemy inventory ani nowych domenowych ekranow.

## Context

- Trigger: user decision: "chce miec tylko jedna plaska role jako wlasciciela firmy, bez menadzera".
- Existing implementation: `company_memberships.role` allows `admin`/`manager`; signup RPC creates `admin`; dashboard maps `admin` to Administrator.
- Existing roadmap: S-02 manager invitation exists and S-06 email recipient is blocked by multi-user ambiguity.
- New product contract: one company owner, no manager, no invitations, email reminders go to the owner.

## Decisions

| Decision           | Choice               | Rationale                                                                                 |
| ------------------ | -------------------- | ----------------------------------------------------------------------------------------- |
| Role value         | `owner`              | Matches the new product language and avoids carrying old admin/manager assumptions.       |
| Migration strategy | Add a new migration  | Existing migrations remain history; the final schema is corrected by a forward migration. |
| Invitation slice   | Removed from roadmap | With one flat owner role, manager invitation is out of MVP.                               |
| Email recipient    | Company owner        | Single owner removes S-06 recipient ambiguity.                                            |

## Scope

### In Scope

- Update PRD and roadmap to owner-only assumptions.
- Add Supabase migration that converts existing `admin`/`manager` memberships to `owner`, constrains future memberships to `owner`, and updates signup RPC.
- Update TypeScript role type and dashboard display.
- Update README setup/smoke-test role references.
- Run text search, lint, and build.

### Out of Scope

- New inventory records.
- New owner profile/settings UI.
- Multi-user invitations.
- Data deletion for historical manager accounts beyond role normalization to owner.
- Remote database migration execution.

## Phase 1: Product and Role Contract Rewrite

### Changes Required

- `context/foundation/prd.md`
  - **Intent**: Make owner-only the product source of truth.
  - **Contract**: Remove Administrator/Menedzer role model, invitation user story, and manager requirements.

- `context/foundation/roadmap.md`
  - **Intent**: Recompute roadmap around one owner.
  - **Contract**: Remove manager invitation slice and unblock renewal email recipient decision by using owner as recipient.

- `supabase/migrations/20260606173000_owner_only_role_model.sql`
  - **Intent**: Move the database contract to owner-only without editing historical migrations.
  - **Contract**: Convert existing role values to `owner`, enforce `role = 'owner'`, and update `create_company_for_current_user` to insert `owner`.

- `src/lib/access-context.ts`
  - **Intent**: Keep app role typing aligned with the database.
  - **Contract**: `CompanyRole` is only `"owner"`.

- `src/pages/dashboard.astro`
  - **Intent**: Show owner language in the protected workspace.
  - **Contract**: Preserve existing locals and signout behavior.

- `README.md`
  - **Intent**: Keep setup and smoke-test docs aligned with owner role.
  - **Contract**: No old admin smoke-test instructions remain.

### Success Criteria

#### Automated Verification

- Search confirms active product docs and UI no longer describe Administrator/Menedzer as the MVP role model.
- Search confirms signup code and the latest role migration produce `owner` as the final role.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Product assumption is clear: one company owner, no manager, no invitations.
- Dashboard shows `Owner` for the current role.
- Roadmap next product slice is inventory records, not manager invitations.

## Testing Strategy

### Automated

- `rg -n "manager|Menedzer|Menedżer|Administrator" src README.md context/foundation`
- `rg -n "owner" src README.md context/foundation supabase/migrations/20260606173000_owner_only_role_model.sql`
- `npm run lint`
- `npm run build`

### Manual

1. Read PRD access control and confirm it says one `owner` role.
2. Read roadmap and confirm no manager invitation slice remains.
3. Open dashboard and confirm the role label is `Owner`.
4. When local Supabase is available, run `npx supabase db reset` and smoke-test signup creates owner membership.

## Migration Notes

The new migration updates existing memberships in-place:

- `admin` -> `owner`
- `manager` -> `owner`

This is acceptable for MVP because manager is no longer a supported concept. Do not run remote database migrations without explicit human approval.

## References

- PRD: `context/foundation/prd.md`
- Roadmap: `context/foundation/roadmap.md`
- Access helper: `src/lib/access-context.ts`
- Dashboard: `src/pages/dashboard.astro`
- Supabase migrations: `supabase/migrations/`

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles. See `references/progress-format.md`.

### Phase 1: Product and Role Contract Rewrite

#### Automated

- [x] 1.1 Search confirms active product docs and UI no longer describe Administrator/Menedzer as the MVP role model. — b0da251
- [x] 1.2 Search confirms signup code and the latest role migration produce `owner` as the final role. — b0da251
- [x] 1.3 `npm run lint` passes. — b0da251
- [x] 1.4 `npm run build` passes. — b0da251

#### Manual

- [x] 1.5 Product assumption is clear: one company owner, no manager, no invitations. — b0da251
- [x] 1.6 Dashboard shows `Owner` for the current role. — b0da251
- [x] 1.7 Roadmap next product slice is inventory records, not manager invitations. — b0da251
