# Owner Renewal Dashboard

## Executive Summary

S-04 turns the inventory data from S-02 into a useful dashboard signal: monthly and annual software subscription costs, a basic hardware status summary, and upcoming renewal priorities. The owner should be able to open `/dashboard` and immediately see what needs review, without leaving the existing company workspace.

The implementation is intentionally read-only. Licenses and hardware are already editable through the S-02 forms; this slice adds derived metrics and a clearer renewal review surface on top of the current records.

## Scope

### In Scope

- Derived dashboard metrics for monthly software cost, annual software cost, upcoming renewals, missing renewal dates, and hardware status counts.
- Typed helper functions in the inventory boundary so dashboard calculations stay testable and reusable.
- Dashboard UI refresh that replaces the S-04 placeholder copy with real cost, hardware, and renewal sections.
- Lint/build verification.

### Out of Scope

- New Supabase tables or migrations.
- Reminder scheduling or email delivery.
- Cost forecasting beyond monthly and annual totals.
- Multi-currency support.
- Charts, exports, filters, or custom renewal windows.

## Decisions

| Decision       | Choice                                          | Reason                                                                               |
| -------------- | ----------------------------------------------- | ------------------------------------------------------------------------------------ |
| Data model     | Reuse `software_licenses` and `hardware_assets` | The fields required by FR-010, FR-012, and FR-013 already exist from S-02.           |
| Renewal window | Upcoming means the next 45 days                 | This matches the current placeholder logic and landing-page promise.                 |
| Cost display   | Separate monthly and annual totals              | Avoids hiding billing cadence differences behind an arbitrary conversion.            |
| UI placement   | Put renewal dashboard signals above CRUD panels | The dashboard's main job is now review and prioritization before record maintenance. |

## Phase 1: Dashboard Signals

### Changes Required

- `src/lib/inventory-records.ts`
  - **Intent**: Centralize renewal dashboard calculations next to the typed inventory records they depend on.
  - **Contract**: Export a dashboard summary helper that accepts `InventoryRecords` plus an optional current date and returns software cost totals, upcoming renewal rows with days remaining, missing renewal count, and hardware status counts.

- `src/pages/dashboard.astro`
  - **Intent**: Replace the planned-renewal placeholder with a real owner-facing dashboard surface.
  - **Contract**: Render cost cards, hardware status summary, and a prioritized upcoming-renewals list using the typed summary helper. Preserve existing inventory and assignment forms.

### Success Criteria

#### Automated Verification

- Dashboard metrics are derived from typed inventory records, not duplicated ad hoc in the page.
- Dashboard shows monthly and annual software subscription totals.
- Dashboard shows a basic hardware status summary.
- Dashboard shows upcoming renewals from the next 45 days.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Owner can see monthly and annual software cost totals after adding licenses with costs.
- Owner can see hardware counts split by available, in use, and retired.
- Owner can see upcoming license renewals ordered by urgency.
- Dashboard remains readable on desktop and mobile.

## Testing Strategy

### Automated

- `rg -n "buildDashboardSummary|upcoming renewals|monthly software" src`
- `npm run lint`
- `npm run build`

### Manual

1. Sign in as owner and open `/dashboard`.
2. Add or edit software licenses with monthly and annual costs.
3. Add renewal dates within and outside the next 45 days.
4. Add hardware records across available, in-use, and retired statuses.
5. Confirm the top dashboard shows cost totals, hardware status counts, and urgent renewals without breaking the existing forms.

## Migration Notes

No database migration is needed. S-04 reads fields already created by `20260606182000_inventory_records.sql`.

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles.

### Phase 1: Dashboard Signals

#### Automated

- [x] 1.1 Dashboard metrics are derived from typed inventory records, not duplicated ad hoc in the page.
- [x] 1.2 Dashboard shows monthly and annual software subscription totals.
- [x] 1.3 Dashboard shows a basic hardware status summary.
- [x] 1.4 Dashboard shows upcoming renewals from the next 45 days.
- [x] 1.5 `npm run lint` passes.
- [x] 1.6 `npm run build` passes.

#### Manual

- [ ] 1.7 Owner can see monthly and annual software cost totals after adding licenses with costs.
- [ ] 1.8 Owner can see hardware counts split by available, in use, and retired.
- [ ] 1.9 Owner can see upcoming license renewals ordered by urgency.
- [ ] 1.10 Dashboard remains readable on desktop and mobile.
