---
project: ITventory
researched_at: 2026-05-27
recommended_platform: Cloudflare Workers + Pages
runner_up: Vercel
context_type: mvp
tech_stack:
  language: TypeScript
  framework: Astro 6 + React 19
  runtime: Cloudflare Workers via @astrojs/cloudflare and Wrangler
---

# Infrastructure Decision

## Recommendation

**Deploy on Cloudflare Workers + Pages.**

Cloudflare is the strongest fit for this MVP because the scaffold already uses `@astrojs/cloudflare`, `wrangler`, Supabase as an external provider, and Cloudflare-oriented deployment config. The app does not need persistent server processes, the user is comfortable with Cloudflare, and scheduled reminder work can be handled with Cloudflare cron-style triggers plus idempotent application logic.

## Platform Comparison

| Platform | CLI-first | Managed / Serverless | Agent-readable docs | Stable deploy API | MCP / Integration | Total |
| --- | --- | --- | --- | --- | --- | --- |
| Cloudflare Workers + Pages | Pass | Pass | Pass | Pass | Pass | 5/5 |
| Vercel | Pass | Pass | Pass | Pass | Partial | 4.5/5 |
| Netlify | Pass | Pass | Pass | Partial | Pass | 4.5/5 |
| Railway | Pass | Pass | Pass | Partial | Partial | 3.5/5 |
| Render | Partial | Pass | Pass | Partial | Partial | 3/5 |
| Fly.io | Pass | Pass | Pass | Pass | Partial | 4/5 |

Cloudflare wins because it matches the starter's runtime and deployment assumptions directly. Its Wrangler CLI supports deploys, logs, rollbacks, secrets, and scheduled triggers. The main caveat is that the app runs in the Workers runtime rather than ordinary Node.js, so Node-only packages must be avoided or isolated.

Vercel is the runner-up because its CLI, logs, and rollback story are mature and easy to automate. It is less aligned with this scaffold because the project is already Cloudflare-adapted and does not need Vercel-native Next.js features.

Netlify is credible for frontend-style deployments and has strong agent-facing positioning, but it is not the most direct target for an Astro 6 project configured around Cloudflare's adapter and Wrangler.

Railway and Render are good general-purpose PaaS options when co-located services or container-style deployment matter. For ITventory, Supabase is already external and the app does not need long-lived processes, so both add operational surface without a matching MVP benefit.

Fly.io is strong for persistent processes, regional control, and container workloads. It is intentionally not the recommendation here because the app does not need always-on workers or WebSockets, and the selected starter is not container-first.

### Shortlisted Platforms

#### 1. Cloudflare Workers + Pages (Recommended)

Cloudflare fits the actual scaffold: Astro 6, `@astrojs/cloudflare`, `wrangler`, Cloudflare Pages/Workers deployment, and external Supabase for auth/database/storage. It has the best runtime match, strong CLI-first operations, globally distributed edge execution, scheduled triggers for reminder jobs, and explicit rollback support.

#### 2. Vercel

Vercel is the strongest alternative if Cloudflare-specific runtime constraints become painful. It has mature CLI deploys, logs, rollbacks, and preview deployment ergonomics. The tradeoff is that this project would stop following the starter's most direct deployment path.

#### 3. Netlify

Netlify is viable for modern frontend apps and has good deploy-preview ergonomics. It ranks third because the current app is not just a static Astro site; it uses Cloudflare adapter assumptions, so Netlify would require more adaptation than Cloudflare.

## Anti-Bias Cross-Check: Cloudflare Workers + Pages

### Devil's Advocate - Weaknesses

1. The Workers runtime is not Node.js. Any dependency that assumes Node APIs can fail at build time or runtime.
2. Rollback restores Worker code, but it does not roll back Supabase schema/data changes or external email side effects.
3. Preview environments can accidentally point at production Supabase unless environment separation is designed early.
4. Scheduled reminders require idempotency. A cron retry or duplicate run must not send duplicate renewal emails.
5. Runtime debugging differs from local Node development; the team needs to use Wrangler and platform logs from the start.

### Pre-Mortem - How This Could Fail

Six months after launch, the Cloudflare decision fails because the team treats Workers like a normal Node server. A dependency added for email processing uses unsupported Node APIs and only fails after deployment. Preview deployments share production Supabase credentials, so a test invite pollutes real company data. The license reminder job is implemented as a simple scheduled function without idempotency, and a retry sends duplicate renewal emails to customers. A rollback fixes a bad code deploy, but the Supabase schema migration remains incompatible with the old Worker version. Debugging becomes slow because the team relies on local `astro dev` behavior and does not inspect Wrangler/runtime logs during development. None of these problems are inherent blockers, but they become costly when not handled as platform constraints from day one.

### Unknown Unknowns

- Astro with Cloudflare adapter may behave differently from local Node-style assumptions; test deployment paths early, not after the feature set is complete.
- Cloudflare rollback cannot recover missing or incompatible external resources such as Supabase schema changes.
- Secrets and environment variables need separate preview/staging/production handling before collaborators or agents start deploying.
- Scheduled jobs need explicit deduplication and audit trails so reminder emails are operationally safe.
- Platform logs and local logs are not equivalent; use Cloudflare/Wrangler logs for any behavior tied to Workers runtime.

## Operational Story

- **Preview deploys**: GitHub-connected Cloudflare deployments can provide branch/preview deploys. Preview access should be protected before real customer data is used, and preview environment variables must not point at production Supabase unless explicitly intended.
- **Secrets**: Supabase keys, email provider tokens, and other secrets live in Cloudflare environment variables / Workers secrets and GitHub Secrets for CI. Production secrets should be readable only by the account owner/admin, and rotation means updating the platform secret plus redeploying.
- **Rollback**: Use Cloudflare deployment history or `wrangler rollback` for Worker code rollback. Treat Supabase migrations and data changes as separate rollback work; code rollback does not undo external database changes.
- **Approval**: Production deployment, secret rotation, and any Supabase schema/data migration require human approval. Agents may run local builds, preview deployments, log reads, and non-destructive diagnostics.
- **Logs**: Agents should read build logs from GitHub Actions / Cloudflare deploy output and runtime logs via Wrangler/Cloudflare observability. Logs should be read-only during diagnosis.

## Risk Register

| Risk | Source | Likelihood | Impact | Mitigation |
| --- | --- | --- | --- | --- |
| Node-only dependency breaks in Workers runtime | Devil's advocate | M | H | Prefer Web API-compatible packages; test with the Cloudflare build/deploy path early. |
| Preview deploy uses production Supabase | Unknown unknowns | M | H | Create separate Supabase projects or schemas for preview/staging and production; document env var mapping. |
| Duplicate reminder emails from scheduled job retries | Devil's advocate / Pre-mortem | M | H | Store reminder send attempts with idempotency keys based on license, threshold, and date. |
| Code rollback does not undo Supabase migration | Devil's advocate | M | H | Treat database changes as manually approved changes with forward/backward-compatible migrations. |
| Runtime logs differ from local behavior | Pre-mortem | M | M | Add a deployment smoke test and use Wrangler/platform logs in every deploy verification. |
| Cloudflare-specific config drifts from Astro adapter expectations | Research finding | M | M | Keep `@astrojs/cloudflare`, `wrangler`, and Astro versions reviewed together; verify upgrade notes before dependency bumps. |

## Getting Started

1. Confirm local build works: `npm run build`.
2. Confirm Cloudflare output/config is valid for the current Astro 6 and `@astrojs/cloudflare` setup.
3. Create Cloudflare project and connect the GitHub repository for preview and production deploys.
4. Add Supabase and email-provider secrets separately for preview/staging and production.
5. Deploy once to a preview environment and verify auth, dashboard routes, and scheduled reminder path before using production data.

## Out of Scope

The following were not evaluated in this research:

- Docker image configuration
- CI/CD pipeline setup
- Production-scale architecture (multi-region, HA, DR)
