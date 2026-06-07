# Owner Renewal Email Alert — Plan Brief

> Full plan: `context/changes/renewal-email-alert/plan.md`

## What & Why

S-05 sends a concrete email reminder to the company owner before a software license renews. This is the roadmap's north-star slice because it proves ITventory can prevent a missed subscription renewal, not just display records.

## Starting Point

F-02 already created reminder tables, deduplication, lifecycle status, and attempt history. S-02/S-04 created license renewal dates and a dashboard review surface.

## Desired End State

A secret-protected cron endpoint can scan due software licenses, send the owner an email through Resend, and record `sent` or `failed` attempts in the existing reminder contract. Repeated runs on the same day do not duplicate already sent reminders.

## Key Decisions Made

| Decision  | Choice                         | Why                                               |
| --------- | ------------------------------ | ------------------------------------------------- |
| Threshold | Fixed 14 days                  | Keeps MVP useful without settings scope.          |
| Trigger   | Secret-protected HTTP endpoint | Works for Cloudflare cron and manual smoke tests. |
| Provider  | Resend HTTP API                | No SDK dependency and compatible with Workers.    |
| Recipient | Owner auth email               | Matches the owner-only product model.             |
| State     | Reuse F-02 reminder lifecycle  | Keeps dedup and attempt audit in one contract.    |

## Scope

**In scope:** service Supabase client, cron endpoint, due-license scan, owner recipient resolution, Resend delivery, lifecycle updates, docs, lint/build.

**Out of scope:** configurable thresholds, queues, retry backoff, multiple recipients, UI history, Slack/Teams, rich templates.

## Architecture / Approach

`POST /api/cron/renewal-reminders` validates `REMINDER_INTERNAL_SECRET`, creates a Supabase service client, then calls `processRenewalEmailAlerts`. The pipeline reads licenses due in 14 days, resolves owner email, creates or reuses a reminder contract, sends via Resend, and records `sent` or `failed`.

## Phases at a Glance

| Phase                     | What it delivers                           | Key risk                                       |
| ------------------------- | ------------------------------------------ | ---------------------------------------------- |
| 1. Renewal Email Pipeline | End-to-end reminder scan, send, and status | Secret/provider config must be correct in env. |

**Prerequisites:** F-02 reminder guardrail and S-02 software licenses.
**Estimated effort:** One focused implementation pass plus real provider smoke test.

## Open Risks & Assumptions

- Production needs valid Resend sender/domain configuration before real mail delivery.
- Cloudflare cron wiring can call the endpoint with the same secret, but this phase keeps the trigger as HTTP.

## Success Criteria (Summary)

- Due licenses trigger owner email reminders.
- Reminder lifecycle records sent/failed attempts.
- Repeated same-day runs do not duplicate sent reminders.
