# Owner-Only Role Model — Plan Brief

> Full plan: `context/changes/owner-only-role-model/plan.md`

## What & Why

ITventory MVP przechodzi z dwoch rol Administrator/Menedzer na jedna plaska role `owner`. Upraszcza to produkt, usuwa manager invitation i odblokowuje decyzje o odbiorcy mailowych przypomnien.

## Starting Point

Kod i dokumenty zakladaly `admin`/`manager`: migracje, signup RPC, dashboard label, PRD i roadmapa. S-02 bylo zaproszeniem Menedzera, a alert mailowy byl zablokowany pytaniem o odbiorce przy wielu uzytkownikach.

## Desired End State

Tworca firmy dostaje role `owner`. MVP nie ma zaproszen, Menedzera ani granic rol. Roadmapa prowadzi dalej bezposrednio do inventory records.

## Key Decisions Made

| Decision        | Choice            | Why                                      |
| --------------- | ----------------- | ---------------------------------------- |
| Role model      | One `owner` role  | Matches the user's new assumption.       |
| Invitations     | Out of MVP        | No manager means no invitation slice.    |
| Email recipient | Owner             | Single user removes recipient ambiguity. |
| Database change | Forward migration | Historical migrations stay intact.       |

## Scope

**In scope:** PRD, roadmap, README, role type, dashboard label, Supabase migration.

**Out of scope:** inventory implementation, remote migration execution, multi-user settings, invitation UI.

## Phases at a Glance

| Phase                                | What it delivers                                | Key risk                                                |
| ------------------------------------ | ----------------------------------------------- | ------------------------------------------------------- |
| 1. Product and Role Contract Rewrite | Owner-only docs, schema, code, and verification | Missing stale admin/manager language in active surfaces |

**Prerequisites:** Existing company boundary implementation.
**Estimated effort:** One focused implementation pass.

## Success Criteria (Summary)

- Signup creates owner membership in the final schema.
- Dashboard says Owner.
- Roadmap no longer routes through manager invitation.
