# Minimalny kontrakt bezpiecznego wysylania przypomnien — Plan Brief

> Full plan: `context/changes/f-02/plan.md`

## What & Why

Budujemy fundament przypomnien, ktory ma byc bezpieczny zanim system zacznie faktycznie wysylac maile. Celem F-02 jest zablokowanie duplikatow i wymuszenie poprawnego odbiorcy na poziomie kontraktu danych, aby S-06 mogl skupic sie na samym delivery pipeline, a nie naprawianiu podstaw.

## Starting Point

Repo ma juz granice firmy i role z F-01, ale nie ma modelu reminderow, statusow dostarczenia ani audytu prob. Nie istnieje jeszcze scheduler ani domena licencji, wiec F-02 musi byc niezalezne od S-03 i skoncentrowane na guardrailach.

## Desired End State

Aplikacja ma gotowy kontrakt server-side dla remindera: jawny `recipient_email`, deduplikacja per `reminder_date`, lifecycle `pending/sent/failed`, i append-only historia prob. Dane sa izolowane per firma przez RLS, a write-path idzie tylko przez kontrolowane endpointy backendowe.

## Key Decisions Made

| Decision | Choice | Why |
| --- | --- | --- |
| Scope | Tylko kontrakt i guardraile, bez realnej wysylki | Najmniejsza bezpieczna podstawa pod S-06 bez scope creep. |
| Odbiorca | Jawny `recipient_email` | Eliminuje niejednoznacznosc przy wielu userach firmy. |
| Deduplikacja | Unique `(company_id, license_ref, reminder_date, recipient_email)` | Twarda ochrona przed duplikatem dla tego samego dnia i odbiorcy. |
| Statusy | `pending/sent/failed` + `last_error` + `attempt_count` | Gotowosc pod retry i debug bez przebudowy modelu. |
| Granica czasu | Dedup per `reminder_date` | Stabilna semantyka mimo roznic timestamp/stref. |
| Audyt | Osobna tabela attempts (append-only) | Pelna historia operacyjna i baza pod observability. |
| Bezpieczenstwo | RLS + serwerowe helpery, bez client-side insert | Spójnosc z F-01 i mniejsza powierzchnia ryzyka. |

## Scope

**In scope:**

- Migracja Supabase dla tabel kontraktu reminderow i attemptow.
- Constraints statusu, odbiorcy i deduplikacji.
- RLS/polityki oparte o membership firmy.
- Serwerowe helpery i internal API do lifecycle remindera.
- Dokumentacja granic F-02 vs S-06.

**Out of scope:**

- Realna wysylka email i harmonogram.
- UI zarzadzania przypomnieniami.
- Integracja FK do tabeli licencji z S-03.
- Zaawansowany silnik retry/backoff.

## Architecture / Approach

Warstwa danych dostaje tabele `license_renewal_reminders` (stan kontraktu) i `license_renewal_reminder_attempts` (dziennik prob). Warstwa serwerowa udostepnia operacje tworzenia/aktualizacji remindera i append attempt. Dostep i mutacje sa scope'owane company contextem i chronione RLS.

## Phases at a Glance

| Phase | What it delivers | Key risk |
| --- | --- | --- |
| 1. Database Guardrail Contract | Tabele, dedup key, status contract, RLS | Niewlasciwe constraints moga utrudnic S-06 lub przepuscic duplikaty. |
| 2. Server Access Layer for Reminder Lifecycle | Helpery i internal API lifecycle remindera | Bledny scope autoryzacji moze naruszyc izolacje firmy. |
| 3. Verification and Handoff to S-06 | Testy kontraktu + dokumentacja granic | Niedostateczna walidacja utrudni bezpieczne wejscie w S-06. |

**Prerequisites:** F-01 na branchu (company boundary), dostepne lokalne Supabase.
**Estimated effort:** ~2-3 sesje implementacyjne przez 3 fazy.

## Open Risks & Assumptions

- `license_ref` jest tymczasowym identyfikatorem kontraktu do czasu modelu licencji z S-03.
- Brak realnego scheduler/delivery w F-02 oznacza, ze poprawna semantyka przejsc statusu musi byc jasno opisana.
- RLS i endpoint auth musza pozostac kompatybilne z przyszlym workerem S-06.

## Success Criteria (Summary)

- Istnieje twardy kontrakt danych blokujacy duplikaty reminderow i wymagajacy jawnego odbiorcy.
- Lifecycle remindera (`pending/sent/failed`) i audyt attemptow sa obslugiwane server-side.
- S-06 moze ruszyc bez redefiniowania modelu bezpieczenstwa i idempotencji.
