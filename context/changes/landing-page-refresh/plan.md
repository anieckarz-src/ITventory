# Improve landing page

## Executive Summary

Ten change poprawia publiczny landing page ITventory tak, aby przestal wygladac jak lekko przerobiony starter i zaczal komunikowac konkretny produkt: operacyjne narzedzie IT dla malych firm, ktore pomaga nie przegapic odnowien licencji i buduje jedno miejsce dla sprzetu, licencji oraz przypisan.

Zakres jest frontendowy. Nie dodajemy nowych funkcji domenowych, routingu aplikacyjnego ani danych. Landing ma pokazac wiarygodny obraz produktu przez statyczny mock dashboardu i jasny flow CTA do zalozenia workspace. Funkcje jeszcze niezaimplementowane moga byc pokazane subtelnie jako kierunek, bez udawania, ze sa juz gotowe.

## Context

- Change ID: `landing-page-refresh`.
- Trigger: user feedback that the current landing page does not feel good enough.
- Product context: ITventory MVP focuses on company workspace, inventory records, renewal dashboard, and renewal email alerts.
- Current page: `src/components/Welcome.astro` uses a dark cosmic background, centered hero, and generic cards.
- Related component: `src/components/Topbar.astro` is shared by the public entry and authenticated state.
- User decisions:
  - Primary landing goal: drive signup / workspace creation.
  - Visual style: SaaS operational, calm and dashboard-oriented.
  - Hero product signal: static dashboard mock.
  - Dominant message: do not miss license renewals.
  - Page depth: three sections beyond nav-level structure.
  - Planned features: shown subtly as coming next, not as already shipped.
  - Topbar: include a light redesign.

## Current State Analysis

- `src/components/Welcome.astro` presents ITventory by name, but the page structure still feels generic: large centered hero, abstract background, three broad feature cards, and no product surface.
- The current copy mentions hardware, licenses, assignments, and renewals, but it does not prioritize the North Star renewal signal from `context/foundation/roadmap.md`.
- The landing has no dashboard preview, no specific renewal scenario, and no clear visual bridge to the protected `/dashboard`.
- `src/components/Topbar.astro` is functional but reads like a starter auth bar: "Not signed in", simple text links, and no product-specific nav/CTA hierarchy.
- Existing app constraints are favorable: this can remain a static Astro page with Tailwind classes and no client-side state.

## Decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Landing goal | Drive workspace creation | The current MVP path starts with signup and company workspace creation. |
| Visual direction | SaaS operational | ITventory is an operational IT tool; dense but readable product cues fit better than decorative marketing. |
| Hero product signal | Static dashboard mock | A concrete preview explains the product faster than abstract copy. |
| Main message | Do not miss license renewals | This matches the roadmap North Star and strongest pain point. |
| Page depth | Three main sections | Enough to explain problem, product preview, and workflow without overbuilding a marketing site. |
| Planned features | Subtle "coming next" language | Avoids overpromising while still showing the roadmap direction. |
| Topbar | Light redesign | The first viewport needs a product-grade nav and CTA hierarchy. |

## Scope

### In Scope

- Rework `src/components/Welcome.astro` into a more polished ITventory landing page.
- Add a product-style hero with:
  - clear ITventory brand signal,
  - renewal-risk headline,
  - primary signup CTA,
  - secondary signin CTA,
  - static dashboard mock showing licenses, renewals, hardware, and assignments.
- Add three landing sections:
  - problem/value around missed renewals and scattered IT records,
  - product preview / operating model,
  - MVP workflow and CTA.
- Lightly redesign `src/components/Topbar.astro` for public and signed-in states.
- Keep the page static and fast; no new data fetching, no new runtime dependencies.
- Use existing Tailwind/Astro patterns.
- Update copy only enough to support the new landing direction.

### Out of Scope

- New backend/API work.
- New database tables or migrations.
- Real dashboard data on the public landing page.
- Screenshots generated from live app state.
- Full marketing site with pricing, FAQ, testimonials, SEO campaign pages, or analytics.
- Implementing inventory, renewal dashboard, manager invitations, or email alerts.
- Major auth form redesign.

## Target Architecture

The landing page remains `src/components/Welcome.astro`, rendered by `src/pages/index.astro` inside the existing `Layout`. `Welcome.astro` should own static landing content and mock data arrays near the top of the component. `Topbar.astro` remains the shared nav component and should keep its current auth-aware behavior, but with stronger ITventory brand and CTA styling.

No new state management or API calls are needed. The dashboard mock should be static and clearly product-like, using sample labels such as license names and renewal dates. Copy should avoid implying that all shown domain features are fully implemented today; supporting text can frame the preview as the workspace direction and MVP flow.

## Content Contract

### Hero

- H1 should lead with the literal product/category, not an abstract slogan.
- Supporting copy should name the pain: missed license renewals, scattered hardware/license records, and unclear assignments.
- Primary CTA should point to `/auth/signup`.
- Secondary CTA should point to `/auth/signin`.
- The first viewport must show a product surface, not only text.

### Product Mock

- Static mock may include:
  - upcoming renewal row,
  - monthly/annual cost summary,
  - hardware count,
  - assignment sample,
  - reminder status or "coming next" label.
- It must not fetch from database or require auth.
- It should read as an illustrative preview, not a fake live account.

### Sections

- Section 1: practical problem and value.
- Section 2: how the workspace organizes data.
- Section 3: MVP workflow and final CTA.

## Phase 1: Landing Structure and Product Hero

Rebuild the first viewport around a concrete product story and static dashboard mock.

### Changes Required

- `src/components/Welcome.astro`
  - **Intent**: Replace the generic centered hero with a product-led landing hero.
  - **Contract**: Render ITventory brand, renewal-risk headline, supporting copy, signup/signin CTAs, and a static dashboard mock in the first viewport.

- `src/components/Welcome.astro`
  - **Intent**: Make the hero visually operational rather than decorative.
  - **Contract**: Use restrained layout, dashboard-like panels, readable spacing, and stable responsive dimensions. Avoid adding decorative orb/blob layers beyond what already exists unless they are reduced or simplified.

### Success Criteria

#### Automated Verification

- `Welcome.astro` renders a first-viewport ITventory hero with signup and signin links.
- Hero includes a static product/dashboard mock without API calls or auth requirements.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Anonymous `/` immediately communicates ITventory as an IT inventory / renewal-risk product.
- The first viewport shows a credible product surface, not only copy.
- Mobile and desktop layouts do not overlap or clip hero text, CTA labels, or mock content.

---

## Phase 2: Value Sections and Topbar Refresh

Complete the page narrative and improve the nav/CTA treatment.

### Changes Required

- `src/components/Welcome.astro`
  - **Intent**: Add enough page structure to explain the product without turning it into a long marketing site.
  - **Contract**: Add three focused sections: problem/value, product operating model, and MVP workflow/final CTA.

- `src/components/Topbar.astro`
  - **Intent**: Make the shared topbar feel like ITventory navigation rather than starter auth chrome.
  - **Contract**: Show ITventory brand, preserve signed-in email/dashboard/signout behavior, and give anonymous users a clear signup CTA plus signin path.

- `src/components/Welcome.astro`
  - **Intent**: Keep planned features honest.
  - **Contract**: Use subtle labels such as "coming next" or "planned" where the page references capabilities not fully implemented yet.

### Success Criteria

#### Automated Verification

- `/` includes sections for problem/value, operating model, and MVP workflow/final CTA.
- Topbar preserves signed-in and signed-out navigation behavior.
- Planned/not-yet-implemented capabilities are not presented as already live.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Anonymous `/` feels more polished and more specific to ITventory than the current version.
- Anonymous users can clearly choose signup or signin.
- Signed-in users still see a dashboard path and sign-out action.
- The page does not overpromise completed inventory, assignment, renewal dashboard, or email alert functionality.

---

## Phase 3: Polish, Responsive QA, and Handoff

Verify the landing page as a product surface and document the outcome.

### Changes Required

- `src/components/Welcome.astro`
  - **Intent**: Polish layout, spacing, copy, and responsive behavior after the main rewrite.
  - **Contract**: Ensure stable desktop/mobile rendering, no overlapping text, no clipped CTA labels, and no one-note purple-heavy visual impression.

- `README.md` or `context/changes/landing-page-refresh/plan.md`
  - **Intent**: Capture any relevant handoff notes for future landing/page work.
  - **Contract**: Keep documentation minimal; do not create marketing docs unless needed.

- `context/changes/landing-page-refresh/plan.md`
  - **Intent**: Record completed progress as phases land.
  - **Contract**: Only the `## Progress` section receives checkbox updates during implementation.

### Success Criteria

#### Automated Verification

- Text search confirms no starter/template copy was reintroduced.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Desktop landing page is visually coherent and clearly product-specific.
- Mobile landing page is readable, with no overlap/clipping.
- CTAs work: signup goes to `/auth/signup`, signin goes to `/auth/signin`, signed-in dashboard path goes to `/dashboard`.
- Reviewer confirms the landing page is materially improved enough to proceed with the next product slice.

## Testing Strategy

### Automated

- `npm run lint`
- `npm run build`
- `rg -n "10x Astro Starter|starter template|production-ready starter|cosmic developer" src README.md`

### Manual

1. Open `/` signed out on desktop width.
2. Confirm the first viewport shows ITventory, the renewal-risk message, CTAs, and a product mock.
3. Confirm signup and signin links navigate correctly.
4. Resize to mobile width and confirm hero, cards, topbar, and CTA labels remain readable.
5. Sign in and open `/`; confirm topbar still offers dashboard and sign out.
6. Confirm no copy implies unfinished S-03/S-05/S-06 features are already live.

## Performance Considerations

The landing page should remain static Astro markup with no hydration and no API calls. The product mock should be rendered with HTML/CSS and small inline data arrays. Avoid large images or generated media unless later explicitly chosen.

## Migration Notes

No database or backend migration is required.

## References

- Current landing component: `src/components/Welcome.astro`
- Current topbar: `src/components/Topbar.astro`
- Public route: `src/pages/index.astro`
- Product PRD: `context/foundation/prd.md`
- Roadmap North Star: `context/foundation/roadmap.md`
- Completed company start plan: `context/changes/admin-company-start/plan.md`

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles. See `references/progress-format.md`.

### Phase 1: Landing Structure and Product Hero

#### Automated

- [x] 1.1 `Welcome.astro` renders a first-viewport ITventory hero with signup and signin links.
- [x] 1.2 Hero includes a static product/dashboard mock without API calls or auth requirements.
- [x] 1.3 `npm run lint` passes.
- [x] 1.4 `npm run build` passes.

#### Manual

- [x] 1.5 Anonymous `/` immediately communicates ITventory as an IT inventory / renewal-risk product.
- [x] 1.6 The first viewport shows a credible product surface, not only copy.
- [x] 1.7 Mobile and desktop layouts do not overlap or clip hero text, CTA labels, or mock content.

### Phase 2: Value Sections and Topbar Refresh

#### Automated

- [ ] 2.1 `/` includes sections for problem/value, operating model, and MVP workflow/final CTA.
- [ ] 2.2 Topbar preserves signed-in and signed-out navigation behavior.
- [ ] 2.3 Planned/not-yet-implemented capabilities are not presented as already live.
- [ ] 2.4 `npm run lint` passes.
- [ ] 2.5 `npm run build` passes.

#### Manual

- [ ] 2.6 Anonymous `/` feels more polished and more specific to ITventory than the current version.
- [ ] 2.7 Anonymous users can clearly choose signup or signin.
- [ ] 2.8 Signed-in users still see a dashboard path and sign-out action.
- [ ] 2.9 The page does not overpromise completed inventory, assignment, renewal dashboard, or email alert functionality.

### Phase 3: Polish, Responsive QA, and Handoff

#### Automated

- [ ] 3.1 Text search confirms no starter/template copy was reintroduced.
- [ ] 3.2 `npm run lint` passes.
- [ ] 3.3 `npm run build` passes.

#### Manual

- [ ] 3.4 Desktop landing page is visually coherent and clearly product-specific.
- [ ] 3.5 Mobile landing page is readable, with no overlap/clipping.
- [ ] 3.6 CTAs work: signup goes to `/auth/signup`, signin goes to `/auth/signin`, signed-in dashboard path goes to `/dashboard`.
- [ ] 3.7 Reviewer confirms the landing page is materially improved enough to proceed with the next product slice.
