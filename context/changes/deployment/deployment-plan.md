# Cloudflare Integration And Deployment Plan

## Summary

- [ ] Use **Cloudflare Workers deployment via Wrangler**, not `wrangler pages deploy`, because the repo is Astro SSR with `@astrojs/cloudflare`, `output: "server"`, and `wrangler.jsonc` pointing at a Worker entrypoint plus `./dist` assets.
- [ ] Use **GitHub Actions as the audited deploy path**: PRs run checks only; `master` deploys production after checks pass.
- [ ] Use **separate Supabase projects** for preview/staging and production.
- [ ] Defer renewal email provider setup until reminder code exists; keep Supabase Auth email as the only active email integration for first production deploy.
- [ ] During execution, write the approved plan to `context/deployment/deploy-plan.md`.

## Key Changes

- [ ] Rename Cloudflare Worker from `10x-astro-starter` to `itventory` in `wrangler.jsonc`.
- [ ] Keep `assets.directory: "./dist"`, `main: "@astrojs/cloudflare/entrypoints/server"`, `nodejs_compat`, and `observability.enabled: true`.
- [ ] Add explicit required secrets for runtime validation: `SUPABASE_URL`, `SUPABASE_KEY`.
- [ ] Add npm scripts:
  - [ ] `deploy:production`: `npm run build && npx wrangler deploy`
  - [ ] `deploy:preview`: `npm run build && npx wrangler deploy --env preview`
  - [ ] `logs:production`: `npx wrangler tail itventory`
- [ ] Update GitHub Actions:
  - [ ] `ci` job remains `npm ci`, `npx astro sync`, `npm run lint`, `npm run build`.
  - [ ] `deploy-production` runs only on `push` to `master` after `ci`.
  - [ ] Deploy command is `npx wrangler deploy`.
  - [ ] Use GitHub Environment `production` with secrets: `CLOUDFLARE_API_TOKEN`, `CLOUDFLARE_ACCOUNT_ID`, `SUPABASE_URL`, `SUPABASE_KEY`.
- [ ] Add a manual `workflow_dispatch` preview deploy later using `npx wrangler deploy --env preview`, wired to separate preview Supabase secrets.

## Manual Gates

- [ ] Cloudflare account owner creates a scoped API token using Cloudflare's **Edit Cloudflare Workers** permissions and restricts it to the deployment account.
- [ ] Cloudflare account owner adds GitHub environment secrets for production.
- [ ] Supabase owner creates separate preview/staging and production projects; production deploy uses production Supabase only.
- [ ] Production deploy is blocked until `npm run lint` and `npm run build` pass locally or in CI.
- [ ] Any Supabase schema/data migration remains a separate human-approved step; Cloudflare rollback does not roll back Supabase.

## Deployment Steps

- [ ] Phase 1: Local readiness
  - [ ] Run `npm ci` if dependencies are not installed.
  - [ ] Run `npx astro sync`.
  - [ ] Run `npm run lint`.
  - [ ] Run `npm run build`.
- [ ] Phase 2: Cloudflare setup
  - [ ] Confirm Worker name `itventory`.
  - [ ] Add production secrets through Cloudflare/GitHub, never committed files.
  - [ ] Confirm `wrangler.jsonc` has observability enabled.
- [ ] Phase 3: CI/CD setup
  - [ ] Add production deploy job after CI.
  - [ ] Ensure deploy job uses `CLOUDFLARE_API_TOKEN` and `CLOUDFLARE_ACCOUNT_ID`.
  - [ ] Push to `master` to trigger production deploy.
- [ ] Phase 4: Verification
  - [ ] Open production URL.
  - [ ] Verify `/`, `/auth/signup`, `/auth/signin`, and `/dashboard`.
  - [ ] Sign up/sign in against production Supabase.
  - [ ] Confirm Cloudflare Workers logs show no runtime exceptions.
  - [ ] Confirm Supabase contains only expected production auth/test records.
- [ ] Phase 5: Rollback support
  - [ ] If deploy fails before activation, fix CI/build and redeploy.
  - [ ] If runtime breaks after deploy, use `npx wrangler rollback` or Cloudflare dashboard rollback.
  - [ ] If Supabase data/schema caused the issue, pause automated redeploys and handle database recovery manually.

## Edge Case Support

- [ ] If CI says `CLOUDFLARE_API_TOKEN` is missing, verify GitHub Environment secret scope and that the deploy job is attached to `environment: production`.
- [ ] If assets 404, verify `npm run build` produced `dist` and `wrangler.jsonc` still points assets to `./dist`.
- [ ] If auth works locally but fails on Cloudflare, verify production `SUPABASE_URL` and `SUPABASE_KEY`, plus Supabase allowed redirect/site URLs.
- [ ] If Worker runtime fails due to Node APIs, keep `nodejs_compat` but prefer Web API-compatible packages; do not add Node-only dependencies without a Cloudflare deploy smoke test.
- [ ] If preview deploys are added later, never point preview at production Supabase by default.
- [ ] When reminder emails are implemented later, add Cloudflare cron triggers in `wrangler.jsonc`, use UTC schedules, and require idempotency keys before sending any renewal email.

## Test Plan

- [ ] `npx astro sync`
- [ ] `npm run lint`
- [ ] `npm run build`
- [ ] GitHub Actions `ci` passes on PR.
- [ ] GitHub Actions `deploy-production` passes on `master`.
- [ ] Production smoke test covers landing page, auth pages, Supabase-backed signup/signin, and dashboard redirect behavior.
- [ ] Runtime verification uses Cloudflare Workers Logs or `wrangler tail`.

## Assumptions

- [ ] Main branch remains `master`, matching current CI.
- [ ] First deploy does not include custom domain setup.
- [ ] First deploy does not include renewal reminder cron or third-party email provider.
- [ ] Sources checked: Cloudflare Astro Workers deploy docs, Workers secrets docs, Workers cron docs, Workers logs docs, rollback docs, and GitHub Actions CI/CD docs.
