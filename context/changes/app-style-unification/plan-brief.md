# Apply Landing Page Style Across the App — Plan Brief

> Full plan: `context/changes/app-style-unification/plan.md`

## What & Why

Przenosimy styl zaakceptowanego landing page na reszte aplikacji. Auth flow i dashboard nadal wygladaja jak ciemny starter, wiec po kliknieciu z landing page produkt traci spojnosc.

## Starting Point

`Welcome.astro` i `Topbar.astro` maja juz jasny, operacyjny styl SaaS. Dashboard oraz strony auth nadal uzywaja `bg-cosmic`, purple gradient headings, dark glass cards i bialo-niebieskiego tekstu.

## Desired End State

Uzytkownik przechodzi z `/` do `/auth/signup`, `/auth/signin` i `/dashboard` bez wizualnego przeskoku. Wszystkie ekrany uzywaja tego samego jezyka: jasne tlo, slate typography, sky accents, amber risk cues, biale karty i kompaktowy operacyjny layout.

## Key Decisions Made

| Decision         | Choice                                      | Why                                                   |
| ---------------- | ------------------------------------------- | ----------------------------------------------------- |
| Visual direction | Landing page as source of truth             | User explicitly approved this style.                  |
| Scope            | Auth pages, dashboard, shared auth controls | These are the remaining user-visible mismatches.      |
| Backend          | No changes                                  | This is a UI consistency change only.                 |
| Feature honesty  | Keep planned labels                         | Inventory and renewals are not fully implemented yet. |
| Implementation   | Two phases                                  | Auth and dashboard can be verified independently.     |

## Scope

**In scope:** auth page restyle, dashboard restyle, shared form control restyle, route smoke checks, lint/build.

**Out of scope:** backend/API changes, migrations, real inventory features, renewal email delivery, new design-system package.

## Architecture / Approach

Keep the landing/topbar stable and adapt the rest of the app to that grammar. Auth screens get a light centered workspace-entry shell; dashboard becomes a light operational workspace with the same card/risk/status language as the landing mock.

## Phases at a Glance

| Phase                                  | What it delivers                                                                           | Key risk                                                      |
| -------------------------------------- | ------------------------------------------------------------------------------------------ | ------------------------------------------------------------- |
| 1. Auth Surface Unification            | Signup, signin, confirm-email, company-required, and auth controls match the landing style | Accidentally breaking form behavior while changing UI         |
| 2. Dashboard and App Shell Unification | Protected dashboard matches the same visual system                                         | Overpromising inventory/dashboard functionality not built yet |

**Prerequisites:** `landing-page-refresh` implemented.
**Estimated effort:** One focused implementation session across two phases.

## Open Risks & Assumptions

- Assumption: "wszędzie" means current user-visible app surfaces, not future roadmap screens that do not exist yet.
- Risk: Browser plugin may remain unavailable; automated checks and local HTTP smoke tests can run, but visual QA may need human confirmation.

## Success Criteria (Summary)

- Auth and dashboard no longer look like the dark starter.
- Existing auth/dashboard behavior still works.
- Landing, auth, and dashboard feel like one cohesive ITventory product.
