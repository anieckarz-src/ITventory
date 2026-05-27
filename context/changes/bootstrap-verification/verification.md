---
bootstrapped_at: 2026-05-26T00:00:00+02:00
starter_id: 10x-astro-starter
starter_name: "10x Astro Starter (Astro + Supabase + Cloudflare)"
project_name: itventory
language_family: js
package_manager: npm
cwd_strategy: git-clone
bootstrapper_confidence: first-class
phase_3_status: ok
audit_command: "npm audit --json"
---

## Hand-off

```yaml
starter_id: 10x-astro-starter
package_manager: npm
project_name: itventory
hints:
  language_family: js
  team_size: solo
  deployment_target: cloudflare-pages
  ci_provider: github-actions
  ci_default_flow: auto-deploy-on-merge
  bootstrapper_confidence: first-class
  path_taken: standard
  quality_override: false
  self_check_answers: null
  has_auth: true
  has_payments: false
  has_realtime: false
  has_ai: false
  has_background_jobs: true
```

ITventory is a small greenfield web app with a 3-week after-hours MVP target, authentication, company-scoped data, dashboard views, and scheduled email reminders. The recommended JavaScript/TypeScript starter for this product shape is 10x Astro Starter, which gives a TypeScript-first full-stack baseline with auth, database, UI, and Cloudflare deployment conventions already aligned. The standard path keeps the scope tight for a solo build: Cloudflare Pages as the deployment target, GitHub Actions for CI, and auto-deploy on merge so the project can move from PRD to working scaffold with minimal setup overhead.

## Pre-scaffold verification

| Signal | Value | Severity | Notes |
| --- | --- | --- | --- |
| npm package | not run | n/a | starter uses git clone, not an npm create CLI |
| GitHub repo | not run | n/a | `gh` command was not available in PATH |

## Scaffold log

**Resolved invocation**: `git clone https://github.com/przeprogramowani/10x-astro-starter .bootstrap-scaffold && cd .bootstrap-scaffold && npm install`
**Strategy**: git-clone
**Exit code**: 0
**Files moved**: 20
**Conflicts (.scaffold siblings)**: none
**.gitignore handling**: moved silently
**.bootstrap-scaffold cleanup**: deleted

The cloned starter `.git/` directory was removed before moving files into the project.

## Post-scaffold audit

**Tool**: `npm audit --json`
**Summary**: 0 CRITICAL, 1 HIGH, 9 MODERATE, 0 LOW
**Direct vs transitive**: direct packages with findings include `@astrojs/check` and `wrangler`; remaining findings are transitive.

#### CRITICAL findings

None.

#### HIGH findings

- `devalue` — GHSA-77vg-94rm-hx3p, DoS via sparse array deserialization, transitive dependency. Fix available according to npm audit.

#### MODERATE findings

- `@astrojs/check` — direct dependency affected through `@astrojs/language-server`. npm audit reports a fix path, but it may require a semver-major downgrade.
- `@astrojs/language-server` — transitive dependency affected through `volar-service-yaml`.
- `@cloudflare/vite-plugin` — transitive dependency affected through `miniflare`, `wrangler`, and `ws`.
- `miniflare` — transitive dependency affected through `ws`.
- `volar-service-yaml` — transitive dependency affected through `yaml-language-server`.
- `wrangler` — direct dependency affected through `miniflare`.
- `ws` — GHSA-58qx-3vcg-4xpx, uninitialized memory disclosure, transitive dependency.
- `yaml` — GHSA-48c2-rrv3-qjmp, stack overflow via deeply nested YAML collections, transitive dependency.
- `yaml-language-server` — transitive dependency affected through `yaml`.

#### LOW / INFO findings

None.

## Hints recorded but not acted on

| Hint | Value |
| --- | --- |
| bootstrapper_confidence | first-class |
| quality_override | false |
| path_taken | standard |
| self_check_answers | null |
| team_size | solo |
| deployment_target | cloudflare-pages |
| ci_provider | github-actions |
| ci_default_flow | auto-deploy-on-merge |
| has_auth | true |
| has_payments | false |
| has_realtime | false |
| has_ai | false |
| has_background_jobs | true |

## Next steps

Next: a future skill will set up agent context (CLAUDE.md, AGENTS.md). For now, your project is scaffolded and verified.

Useful manual steps in the meantime:
- `git init` if you have not already, to start your own repo history.
- Review any `.scaffold` siblings the conflict policy created and decide which version of each file to keep.
- Address audit findings per your project's risk tolerance; the full breakdown is in this log.
