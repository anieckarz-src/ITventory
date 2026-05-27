---
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
---

## Why this stack

ITventory is a small greenfield web app with a 3-week after-hours MVP target, authentication, company-scoped data, dashboard views, and scheduled email reminders. The recommended JavaScript/TypeScript starter for this product shape is 10x Astro Starter, which gives a TypeScript-first full-stack baseline with auth, database, UI, and Cloudflare deployment conventions already aligned. The standard path keeps the scope tight for a solo build: Cloudflare Pages as the deployment target, GitHub Actions for CI, and auto-deploy on merge so the project can move from PRD to working scaffold with minimal setup overhead.
