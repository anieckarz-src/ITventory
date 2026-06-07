# Owner Renewal Dashboard — Plan Brief

> Full plan: `context/changes/renewal-dashboard/plan.md`

## What & Why

S-04 turns existing inventory records into the first real dashboard signal: software subscription costs, hardware status, and upcoming renewals. This is the next step toward the product's north star, because renewal email alerts need visible renewal data before automation is useful.

## Starting Point

The app already has company-scoped employees, hardware, software licenses, and assignments on `/dashboard`. Licenses already store `cost_amount`, `cost_period`, and `renewal_date`; hardware already stores `status`.

## Desired End State

The owner opens `/dashboard` and sees review-level metrics before the CRUD panels: monthly and annual software costs, hardware status counts, and upcoming renewals from the next 45 days. Existing inventory and assignment workflows remain intact.

## Key Decisions Made

| Decision       | Choice                                     | Why                                                                             |
| -------------- | ------------------------------------------ | ------------------------------------------------------------------------------- |
| Data model     | Reuse existing inventory tables            | S-02 already created every field required by this slice.                        |
| Renewal window | Next 45 days                               | Matches the current placeholder logic and landing-page promise.                 |
| Cost display   | Separate monthly and annual totals         | Keeps billing cadence clear without inventing currency conversion or forecasts. |
| UI approach    | Dashboard signals above record maintenance | The dashboard should prioritize attention before editing workflows.             |

## Scope

**In scope:** typed dashboard calculations, top-level cost cards, hardware status summary, upcoming renewal list, lint/build verification.

**Out of scope:** new migrations, email delivery, charts, exports, filters, multi-currency, reminder scheduling.

## Architecture / Approach

Add a dashboard summary helper to `src/lib/inventory-records.ts`, then consume it from `src/pages/dashboard.astro`. The page remains a server-rendered owner workspace; S-04 adds derived read-only signals on top of the existing records.

## Phases at a Glance

| Phase                | What it delivers                                       | Key risk                                           |
| -------------------- | ------------------------------------------------------ | -------------------------------------------------- |
| 1. Dashboard Signals | Cost totals, hardware status, and upcoming renewals UI | Keeping the already large dashboard page readable. |

**Prerequisites:** S-02 inventory records implemented.
**Estimated effort:** One focused implementation pass.

## Open Risks & Assumptions

- Costs are displayed in the existing implicit dollar format; multi-currency remains out of scope.
- The renewal review window stays fixed at 45 days for MVP.

## Success Criteria (Summary)

- Owner can see monthly and annual software costs from license records.
- Owner can see hardware counts split by status.
- Owner can see upcoming renewals ordered by urgency.
