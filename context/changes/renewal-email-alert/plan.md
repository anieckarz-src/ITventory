# Owner Renewal Email Alert

## Executive Summary

S-05 delivers the north-star MVP signal: ITventory sends an email reminder to the company owner before a software license renewal date. The existing F-02 reminder guardrail already provides deduplication, lifecycle status, and append-only attempts; this change adds the scheduled delivery path that uses those contracts against real license records.

The implementation keeps the first pipeline narrow. A secret-protected cron endpoint scans software licenses due soon, creates or reuses a reminder contract for each owner/license/day, sends through Resend, and records `sent` or `failed`.

## Scope

### In Scope

- Service Supabase client for server-side scheduled work.
- Secret-protected renewal reminder endpoint.
- Due-license selection from existing `software_licenses`.
- Owner recipient resolution from `company_memberships` and Supabase Auth.
- Resend HTTP email delivery.
- Reminder contract lifecycle updates for sent and failed attempts.
- Environment and deployment documentation.
- Lint/build verification.

### Out of Scope

- User-configurable reminder thresholds.
- Retry backoff, queues, or durable background workers.
- Multiple recipients, CC, Slack, Teams, or web push.
- Rich email templates beyond a clear text/html MVP message.
- A UI for reminder history.

## Decisions

| Decision           | Choice                                     | Reason                                                                                   |
| ------------------ | ------------------------------------------ | ---------------------------------------------------------------------------------------- |
| Reminder threshold | Fixed 14-day window                        | Satisfies "before renewal" without adding settings scope.                                |
| Trigger            | Secret-protected HTTP endpoint             | Works with Cloudflare cron or manual triggering and keeps the app runtime simple.        |
| Provider           | Resend HTTP API                            | Avoids a new SDK dependency and fits Cloudflare Workers through `fetch`.                 |
| Recipient          | Company owner auth email                   | Matches the owner-only MVP model and PRD recipient rule.                                 |
| Dedup key          | `license.id + reminder_date + owner email` | Reuses the F-02 contract and prevents duplicate sends on repeated runs for the same day. |

## Phase 1: Renewal Email Pipeline

### Changes Required

- `astro.config.mjs`, `.env.example`, `wrangler.jsonc`
  - **Intent**: Declare the secrets required for scheduled reminders and email delivery.
  - **Contract**: Add optional typed env fields for `SUPABASE_SERVICE_ROLE_KEY`, `REMINDER_INTERNAL_SECRET`, `RESEND_API_KEY`, and `REMINDER_FROM_EMAIL`; document production secrets required to actually send emails.

- `src/lib/supabase.ts`
  - **Intent**: Support scheduled server work without a user cookie session.
  - **Contract**: Export a service client creator using the Supabase service role key while preserving the existing SSR client.

- `src/lib/renewal-email-alerts.ts`
  - **Intent**: Keep due-license scanning, recipient resolution, delivery, and reminder lifecycle in one testable server module.
  - **Contract**: Export a `processRenewalEmailAlerts` function that accepts a Supabase service client and delivery config, then returns counts for scanned/sent/failed/skipped reminders.

- `src/pages/api/cron/renewal-reminders.ts`
  - **Intent**: Provide the operational trigger point for cron/manual runs.
  - **Contract**: POST-only endpoint; validates `Authorization: Bearer <REMINDER_INTERNAL_SECRET>` or `x-reminder-secret`; returns JSON summary; does not require user cookies.

- `README.md`
  - **Intent**: Document how to configure and trigger the renewal email pipeline.
  - **Contract**: List required secrets, endpoint, and expected lifecycle behavior.

### Success Criteria

#### Automated Verification

- Cron endpoint rejects requests without the reminder secret.
- Renewal pipeline scans licenses with renewal dates in the 14-day window.
- Renewal pipeline resolves the company owner email as recipient.
- Email delivery uses Resend HTTP API and records provider message id on success.
- Failed delivery records a failed reminder attempt.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- With secrets configured, triggering the endpoint sends a reminder email for a due license.
- Re-triggering the endpoint on the same day does not send a duplicate for an already sent reminder.
- If provider config is missing or invalid, the reminder is marked failed with a useful error.

## Testing Strategy

### Automated

- `rg -n "processRenewalEmailAlerts|renewal-reminders|RESEND_API_KEY|REMINDER_INTERNAL_SECRET" src astro.config.mjs README.md .env.example wrangler.jsonc`
- `npm run lint`
- `npm run build`

### Manual

1. Set `SUPABASE_SERVICE_ROLE_KEY`, `REMINDER_INTERNAL_SECRET`, `RESEND_API_KEY`, and `REMINDER_FROM_EMAIL`.
2. Add a software license with a renewal date within 14 days.
3. Trigger `POST /api/cron/renewal-reminders` with the reminder secret.
4. Confirm the owner receives an email naming the license and renewal date.
5. Trigger again on the same date and confirm no duplicate send.
6. Temporarily remove or invalidate provider config and confirm failed attempts are recorded.

## Migration Notes

No database migration is required. S-05 uses the existing F-02 reminder tables and the S-02 `software_licenses` table.

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles.

### Phase 1: Renewal Email Pipeline

#### Automated

- [x] 1.1 Cron endpoint rejects requests without the reminder secret.
- [x] 1.2 Renewal pipeline scans licenses with renewal dates in the 14-day window.
- [x] 1.3 Renewal pipeline resolves the company owner email as recipient.
- [x] 1.4 Email delivery uses Resend HTTP API and records provider message id on success.
- [x] 1.5 Failed delivery records a failed reminder attempt.
- [x] 1.6 `npm run lint` passes.
- [x] 1.7 `npm run build` passes.

#### Manual

- [ ] 1.8 With secrets configured, triggering the endpoint sends a reminder email for a due license.
- [ ] 1.9 Re-triggering the endpoint on the same day does not send a duplicate for an already sent reminder.
- [ ] 1.10 If provider config is missing or invalid, the reminder is marked failed with a useful error.
