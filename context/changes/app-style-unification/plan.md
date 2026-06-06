# Apply Landing Page Style Across the App

## Executive Summary

Ten change przenosi jasny, operacyjny styl landing page na pozostale ekrany aplikacji: dashboard, signin, signup, confirm-email, company-required oraz wspolne komponenty formularzy auth. Celem jest usuniecie pozostalosci starterowego, ciemnego `bg-cosmic` / purple-gradient UI i stworzenie jednej spojnej powierzchni SaaS.

Zakres pozostaje frontendowy. Nie zmieniamy logiki auth, routingu, middleware, API, bazy danych ani domenowych funkcji roadmapy. Zmieniamy layout, copy pomocnicze, klasy Tailwind i stany formularzy tak, aby aplikacja wygladala jak ten sam produkt po przejsciu z landing page do auth i dashboardu.

## Context

- Change ID: `app-style-unification`.
- Trigger: user likes the new landing page style and wants it everywhere.
- Baseline style to preserve: `src/components/Welcome.astro` and `src/components/Topbar.astro`.
- Current mismatch: auth and dashboard screens still use dark cosmic background, purple gradients, translucent glass panels, and white/blue text from the starter.
- Product tone: calm operational SaaS for IT inventory and renewal risk.

## Current State Analysis

- `src/components/Welcome.astro` now defines the desired visual direction: light `#f6f8fb` page background, subtle grid texture, slate text, sky accents, amber risk indicators, restrained borders, and dashboard-like white cards.
- `src/components/Topbar.astro` already matches the new style and should remain the shared navigation anchor.
- `src/pages/dashboard.astro` is visually disconnected: dark cosmic background, white text, glass panels, and planned inventory cards that do not match the landing mock.
- `src/pages/auth/signup.astro`, `src/pages/auth/signin.astro`, `src/pages/auth/confirm-email.astro`, and `src/pages/auth/company-required.astro` are still starter-like dark cards with purple gradient headings.
- `src/components/auth/FormField.tsx`, `SubmitButton.tsx`, `ServerError.tsx`, and `PasswordToggle.tsx` encode dark form styling, so page-level changes alone will not be enough.

## Decisions

| Decision           | Choice                                              | Rationale                                                                                 |
| ------------------ | --------------------------------------------------- | ----------------------------------------------------------------------------------------- |
| Visual system      | Adopt landing page light operational SaaS style     | This is the style the user explicitly approved.                                           |
| Scope              | Auth flow, dashboard, and shared auth form controls | These are the user-visible screens that still clash with the landing.                     |
| Backend/data       | No backend changes                                  | The request is visual consistency only.                                                   |
| Dashboard content  | Keep empty workspace semantics                      | Domain slices S-03/S-05 are not implemented yet; avoid overpromising live inventory data. |
| Component strategy | Small reusable layout/style constants where helpful | Avoids duplicating the same shell/card/form classes across auth pages.                    |

## Scope

### In Scope

- Restyle auth pages to match the landing page visual language.
- Restyle dashboard to match the landing/dashboard mock direction.
- Restyle shared auth controls for light cards, slate text, sky focus states, and red error states.
- Keep all form actions, input names, validation behavior, protected route behavior, and signout behavior unchanged.
- Remove remaining product-surface use of `bg-cosmic`, purple gradient headings, and dark glass cards.
- Run lint/build and local route smoke checks.

### Out of Scope

- New inventory, license, assignment, renewal dashboard, or email alert features.
- Database migrations.
- Auth API changes.
- New design system package or component library.
- Full rewrite of Tailwind theme tokens.
- Custom domain, deployment, or production verification.

## Target Architecture

The landing page remains the visual source of truth. App screens should use the same background treatment and component grammar: light page shell, constrained content width, white cards, slate typography, sky support accents, amber renewal/risk signals, and compact operational spacing.

Auth pages should share a consistent centered auth shell with a small product context panel and a white form card. Dashboard should become a light workspace page with topbar, company header, role/user context, empty-state inventory cards, and a next-step panel that feels related to the landing preview.

## Phase 1: Auth Surface Unification

Restyle the authentication and recovery screens so signup/signin no longer feel like a separate starter app.

### Changes Required

- `src/pages/auth/signup.astro`
  - **Intent**: Replace the dark starter auth shell with a light ITventory workspace entry screen.
  - **Contract**: Keep `SignUpForm serverError={error} client:load`, the `/auth/signin` link, and the route title. Add product-specific context without changing form behavior.

- `src/pages/auth/signin.astro`
  - **Intent**: Match signup styling and preserve the existing sign-in flow.
  - **Contract**: Keep `SignInForm serverError={error} client:load`, the `/auth/signup` link, and the route title.

- `src/pages/auth/confirm-email.astro`
  - **Intent**: Restyle the status card to the same light UI language.
  - **Contract**: Preserve the DEV auto-confirm branch, the `/auth/signin` destination, and current content meaning.

- `src/pages/auth/company-required.astro`
  - **Intent**: Restyle the recovery page and keep the signout action obvious.
  - **Contract**: Preserve `POST /api/auth/signout`.

- `src/components/auth/FormField.tsx`, `SubmitButton.tsx`, `ServerError.tsx`, `PasswordToggle.tsx`
  - **Intent**: Convert shared controls from dark/glass styling to light card styling.
  - **Contract**: Preserve props, controlled input behavior, validation messages, icons, `useFormStatus`, button disabled behavior, and password toggle accessibility.

### Success Criteria

#### Automated Verification

- Auth pages no longer use `bg-cosmic`, purple gradient headings, or dark glass card classes.
- Shared auth controls use light inputs, slate text, sky focus states, and readable error styling.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Signup page visually matches the landing page style and still submits to `/api/auth/signup`.
- Signin page visually matches the landing page style and still submits to `/api/auth/signin`.
- Confirm-email and company-required pages feel like the same product, not the old starter.
- Mobile auth layouts remain readable with no clipped button or input text.

---

## Phase 2: Dashboard and App Shell Unification

Bring the protected workspace into the same visual system without adding unimplemented inventory behavior.

### Changes Required

- `src/pages/dashboard.astro`
  - **Intent**: Replace the dark dashboard with a light operational workspace matching the landing mock.
  - **Contract**: Preserve use of `Astro.locals.company`, `Astro.locals.role`, `Astro.locals.user`, and `POST /api/auth/signout`. Keep all inventory sections empty/planned.

- `src/styles/global.css`
  - **Intent**: Remove or stop relying on the old cosmic utility if no app surface needs it.
  - **Contract**: Do not disrupt Tailwind/theme imports or existing shadcn-style variables.

- `src/components/Welcome.astro`, `src/components/Topbar.astro`
  - **Intent**: Leave the approved landing/topbar design stable while aligning shared style decisions.
  - **Contract**: Only touch if implementation reveals a small consistency issue needed by the app-wide style.

### Success Criteria

#### Automated Verification

- Product screens under `src/pages` no longer use `bg-cosmic`.
- Search confirms no old purple-gradient starter styling remains on auth/dashboard surfaces.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Anonymous `/`, `/auth/signup`, and `/auth/signin` feel like one cohesive product.
- Authenticated `/dashboard` uses the same visual vocabulary as the landing page.
- Dashboard still communicates an empty company workspace and does not imply S-03/S-05/S-06 are already built.
- Desktop and mobile layouts have no overlapping text, clipped CTAs, or unreadable form controls.

## Testing Strategy

### Automated

- `rg -n "bg-cosmic|from-blue-200|to-purple-200|text-purple|bg-purple|bg-white/10|backdrop-blur-xl" src/pages src/components/auth`
- `npm run lint`
- `npm run build`
- HTTP smoke checks for `/`, `/auth/signup`, `/auth/signin`, `/auth/confirm-email`, `/auth/company-required`, and anonymous `/dashboard` redirect.

### Manual

1. Open `/` and confirm landing remains unchanged in spirit.
2. Open `/auth/signup` and `/auth/signin` on desktop and mobile widths.
3. Confirm form labels, placeholders, password toggle, submit buttons, and validation errors are readable.
4. Open `/auth/confirm-email` and `/auth/company-required`.
5. Sign in and open `/dashboard`; confirm the workspace looks cohesive with the landing page.
6. Confirm unfinished features are labeled as planned/next, not live.

## Performance Considerations

The change remains static Astro/React markup and Tailwind classes. No new dependencies, images, client state, or API calls should be added.

## Migration Notes

No data or schema migration is required.

## References

- Approved landing style: `src/components/Welcome.astro`
- Shared topbar: `src/components/Topbar.astro`
- Dashboard: `src/pages/dashboard.astro`
- Auth pages: `src/pages/auth/*.astro`
- Auth controls: `src/components/auth/*.tsx`
- Previous landing plan: `context/changes/landing-page-refresh/plan.md`

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles. See `references/progress-format.md`.

### Phase 1: Auth Surface Unification

#### Automated

- [x] 1.1 Auth pages no longer use `bg-cosmic`, purple gradient headings, or dark glass card classes. — 1ae1a6e
- [x] 1.2 Shared auth controls use light inputs, slate text, sky focus states, and readable error styling. — 1ae1a6e
- [x] 1.3 `npm run lint` passes. — 1ae1a6e
- [x] 1.4 `npm run build` passes. — 1ae1a6e

#### Manual

- [x] 1.5 Signup page visually matches the landing page style and still submits to `/api/auth/signup`. — 1ae1a6e
- [x] 1.6 Signin page visually matches the landing page style and still submits to `/api/auth/signin`. — 1ae1a6e
- [x] 1.7 Confirm-email and company-required pages feel like the same product, not the old starter. — 1ae1a6e
- [x] 1.8 Mobile auth layouts remain readable with no clipped button or input text. — 1ae1a6e

### Phase 2: Dashboard and App Shell Unification

#### Automated

- [x] 2.1 Product screens under `src/pages` no longer use `bg-cosmic`. — 1ae1a6e
- [x] 2.2 Search confirms no old purple-gradient starter styling remains on auth/dashboard surfaces. — 1ae1a6e
- [x] 2.3 `npm run lint` passes. — 1ae1a6e
- [x] 2.4 `npm run build` passes. — 1ae1a6e

#### Manual

- [x] 2.5 Anonymous `/`, `/auth/signup`, and `/auth/signin` feel like one cohesive product. — 1ae1a6e
- [x] 2.6 Authenticated `/dashboard` uses the same visual vocabulary as the landing page. — 1ae1a6e
- [x] 2.7 Dashboard still communicates an empty company workspace and does not imply S-03/S-05/S-06 are already built. — 1ae1a6e
- [x] 2.8 Desktop and mobile layouts have no overlapping text, clipped CTAs, or unreadable form controls. — 1ae1a6e
