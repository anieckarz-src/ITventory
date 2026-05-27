# Cloudflare Integration And Deployment Plan

## Summary

- [x] Use **Cloudflare Workers deployment via Wrangler**, not `wrangler pages deploy`, because the repo is Astro SSR with `@astrojs/cloudflare`, `output: "server"`, and `wrangler.jsonc` pointing at a Worker entrypoint plus `./dist` assets.
- [x] Use **GitHub Actions as the audited deploy path**: PRs run checks only; `master` deploys production after checks pass.
- [x] Use **separate Supabase projects** for preview/staging and production.
- [x] Defer renewal email provider setup until reminder code exists; keep Supabase Auth email as the only active email integration for first production deploy.
- [x] During execution, write the approved plan to `context/deployment/deploy-plan.md`.

## Key Changes

- [x] Rename Cloudflare Worker from `10x-astro-starter` to `itventory` in `wrangler.jsonc`.
- [x] Keep `assets.directory: "./dist"`, `main: "@astrojs/cloudflare/entrypoints/server"`, `nodejs_compat`, and `observability.enabled: true`.
- [x] Add explicit required secrets for runtime validation: `SUPABASE_URL`, `SUPABASE_KEY`.
- [x] Add npm scripts:
  - [x] `deploy:production`: `npm run build && npx wrangler deploy`
  - [x] `deploy:preview`: `npm run build && npx wrangler deploy --env preview`
  - [x] `logs:production`: `npx wrangler tail itventory`
- [x] Update GitHub Actions:
  - [x] `ci` job remains `npm ci`, `npx astro sync`, `npm run lint`, `npm run build`.
  - [x] `deploy-production` runs only on `push` to `master` after `ci`.
  - [x] Deploy command is `npx wrangler deploy`.
  - [x] Use GitHub Environment `production` with secrets: `CLOUDFLARE_API_TOKEN`, `CLOUDFLARE_ACCOUNT_ID`, `SUPABASE_URL`, `SUPABASE_KEY`.
- [ ] Add a manual `workflow_dispatch` preview deploy later using `npx wrangler deploy --env preview`, wired to separate preview Supabase secrets.

## Manual Gates

- [ ] Cloudflare account owner creates a scoped API token using Cloudflare's **Edit Cloudflare Workers** permissions and restricts it to the deployment account.
- [ ] Cloudflare account owner adds GitHub environment secrets for production.
- [ ] Supabase owner creates separate preview/staging and production projects; production deploy uses production Supabase only.
- [x] Production deploy is blocked until `npm run lint` and `npm run build` pass locally or in CI.
- [ ] Any Supabase schema/data migration remains a separate human-approved step; Cloudflare rollback does not roll back Supabase.

## Deployment Steps

- [ ] Phase 1: Local readiness
  - [x] Run `npm ci` if dependencies are not installed.
  - [x] Run `npx astro sync`.
  - [x] Run `npm run lint`.
  - [x] Run `npm run build`.
- [ ] Phase 2: Cloudflare setup
  - [x] Confirm Worker name `itventory`.
  - [x] Add production secrets through Cloudflare/GitHub, never committed files.
  - [x] Confirm `wrangler.jsonc` has observability enabled.
- [x] Phase 3: CI/CD setup
  - [x] Add production deploy job after CI.
  - [x] Ensure deploy job uses `CLOUDFLARE_API_TOKEN` and `CLOUDFLARE_ACCOUNT_ID`.
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

- [x] `npx astro sync`
- [x] `npm run lint`
- [ ] GitHub Actions `ci` passes on PR.
- [ ] GitHub Actions `deploy-production` passes on `master`.
- [ ] Production smoke test covers landing page, auth pages, Supabase-backed signup/signin, and dashboard redirect behavior.
- [ ] Runtime verification uses Cloudflare Workers Logs or `wrangler tail`.
- [x] `npm run build`
- [x] `npx wrangler deploy --dry-run`
- [x] `npx wrangler deploy --env preview --dry-run`

## Execution Notes

- [x] `npx astro sync` completed successfully and confirmed required-secret warnings for missing local `SUPABASE_URL` and `SUPABASE_KEY`.
- [x] `npm run lint` passed after `npm run lint:fix` normalized repo-wide Prettier/CRLF issues.
- [x] `npm run build` completed successfully with the same missing-local-secret warnings.
- [x] Wrangler dry runs completed without publishing and confirmed the Worker bundle, assets, `SESSION` KV binding, `IMAGES` binding, and `ASSETS` binding.
- [x] Cloudflare Worker runtime secrets `SUPABASE_URL` and `SUPABASE_KEY` are present according to `npx wrangler secret list`.
- [ ] GitHub production environment secrets still need to be verified in GitHub before the approved GitHub Actions deploy path can run.

## Assumptions

- [x] Main branch remains `master`, matching current CI.
- [x] First deploy does not include custom domain setup.
- [x] First deploy does not include renewal reminder cron or third-party email provider.
- [x] Sources checked: Cloudflare Astro Workers deploy docs, Workers secrets docs, Workers cron docs, Workers logs docs, rollback docs, and GitHub Actions CI/CD docs.
