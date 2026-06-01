# Improve Landing Page - Plan Brief

> Full plan: `context/changes/landing-page-refresh/plan.md`

## What & Why

Improve the ITventory landing page so it no longer feels like a generic starter page. The page should quickly communicate the product's real promise: one IT workspace that helps small companies track assets, licenses, assignments, and upcoming renewals before they become operational surprises.

## Starting Point

The current landing has ITventory copy, but still uses a generic centered hero, abstract visual treatment, and broad feature cards. It does not show a product surface or strongly lead with the roadmap's renewal-risk value.

## Desired End State

Anonymous users see a polished SaaS operational landing page with a concrete dashboard mock, clear signup/signin CTAs, and a concise three-section narrative. Signed-in users still have a dashboard path and sign-out action. Planned features are described honestly without implying they are already shipped.

## Key Decisions Made

| Decision | Choice | Why |
| --- | --- | --- |
| Primary goal | Drive workspace creation | The MVP starts with signup and company workspace creation. |
| Visual direction | SaaS operational | Fits an IT management tool better than decorative starter styling. |
| Hero product signal | Static dashboard mock | Shows what the product does faster than abstract copy. |
| Main message | Do not miss license renewals | Matches the roadmap North Star and strongest pain point. |
| Page depth | Three sections | Enough narrative without overbuilding a marketing site. |
| Planned features | Subtle coming-next language | Keeps the page honest while showing direction. |
| Topbar | Light redesign | The first viewport needs product-grade navigation and CTA hierarchy. |

## Scope

**In scope:**

- Rebuild `Welcome.astro` hero and landing sections.
- Add static dashboard/product mock.
- Add problem/value, operating model, and MVP workflow/final CTA sections.
- Lightly redesign `Topbar.astro`.
- Keep the page static and dependency-free.

**Out of scope:**

- Backend/API work.
- Real data or authenticated dashboard previews.
- New routes, migrations, analytics, pricing, FAQ, testimonials, or full marketing site.
- Implementing unfinished product slices.

## Architecture / Approach

Keep `/` as an Astro-rendered static page. `Welcome.astro` owns the landing content and mock data, while `Topbar.astro` preserves auth-aware navigation. The product preview is HTML/CSS only and must not fetch data or require auth.

## Phases at a Glance

| Phase | What it delivers | Key risk |
| --- | --- | --- |
| 1. Landing Structure and Product Hero | Product-led hero with static dashboard mock | Mock could overpromise unfinished features. |
| 2. Value Sections and Topbar Refresh | Three-section narrative plus better nav/CTA | Topbar changes could regress signed-in behavior. |
| 3. Polish, Responsive QA, and Handoff | Responsive polish and final verification | Mobile layout or visual balance could still feel template-like. |

**Prerequisites:** Completed `admin-company-start` so `/dashboard`, signup, and signin paths exist.
**Estimated effort:** 1-2 focused implementation sessions across 3 phases.

## Open Risks & Assumptions

- The page should stay honest about unfinished inventory, dashboard, and email-alert features.
- No in-app Browser tool is currently exposed, so manual responsive QA may need user confirmation unless a browser tool becomes available.
- Visual work should avoid one-note purple/dark-template styling even if current CSS starts from that palette.

## Success Criteria (Summary)

- `/` clearly presents ITventory as an IT inventory and renewal-risk product.
- The first viewport includes a credible product/dashboard surface and clear signup/signin paths.
- Desktop and mobile layouts are readable, polished, and do not overpromise unfinished features.
