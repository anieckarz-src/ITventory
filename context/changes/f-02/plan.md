# Minimalny kontrakt bezpiecznego wysylania przypomnien

## Executive Summary

F-02 buduje minimalny, bezpieczny kontrakt dostarczania przypomnien, ktory ogranicza dwa najwazniejsze ryzyka MVP: duplikaty i blednych odbiorcow. Zakres tego change'a celowo nie obejmuje realnej wysylki email (to zostaje dla S-06), tylko warstwe danych, statusow, idempotencji i audytu, na ktorej S-06 moze bezpiecznie operowac.

Kontrakt wymusza jawny `recipient_email`, deduplikacje per dzien (`reminder_date`) oraz sledzenie prob dostarczenia (`attempts`) ze statusem i diagnostyka bledu. Tworzenie i modyfikacja rekordow przypomnien ma isc wyłącznie sciezka serwerowa z RLS i helperami backendowymi.

## Context

- Roadmap item: `F-02` in `context/foundation/roadmap.md`.
- Related roadmap change ID: `reminder-delivery-guardrail`.
- PRD refs: `FR-012`, `US-08`.
- Stack: Astro 6, TypeScript, Supabase Postgres/Auth, Cloudflare runtime.
- F-01 juz dostarczyl izolacje firmy (`companies`, `company_memberships`) i role (`admin`, `manager`).

## Current State Analysis

- `supabase/migrations/20260528180000_company_role_boundary.sql` wprowadza granice firm i RLS dla membershipow, ale nie ma modelu reminderow.
- `supabase/migrations/20260528190000_enforce_single_membership_and_signup_fallback.sql` uszczelnia kontrakt F-01, nadal bez warstwy przypomnien.
- `src/lib/access-context.ts` i `src/middleware.ts` daja gotowy kontekst firmy/roli po stronie serwera.
- Brak domenowych tabel dla licencji i brak scheduler/job runnera; to oznacza, ze F-02 musi byc niezalezne od implementacji S-03/S-06.
- W kodzie brak API przypomnien, brak audytu prob wysylki oraz brak idempotency guard dla dostarczenia.

## Decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Scope | Tylko kontrakt i guardraile (bez realnej wysylki) | Minimalizuje ryzyko i odseparowuje fundament od implementacji dostarczenia w S-06. |
| Recipient model | Jawny `recipient_email` na rekordzie przypomnienia | Eliminuje niejednoznacznosc „wlasciwej osoby” przy wielu uzytkownikach firmy. |
| Dedup key | Unikalnosc `(company_id, license_ref, reminder_date, recipient_email)` | Twardo blokuje duplikaty dla tej samej licencji/progu/dnia/odbiorcy. |
| Delivery state | `pending` / `sent` / `failed` + `last_error` + `attempt_count` | Daje gotowa baze pod retry i debug bez przebudowy schematu. |
| Time boundary | Dedup per `reminder_date` (date) | Odpornosc na roznice timestamp/stref oraz stabilna semantyka biznesowa. |
| Audit | Osobna tabela attempts (append-only) | Pelny slad operacyjny i diagnostyczny dla przyszlego pipeline'u S-06. |
| Security | RLS + serwerowe helpery, bez client-side insert | Spójne z F-01 i bezpieczne domyslnie dla danych firmowych. |

## Scope

### In Scope

- Dodanie modelu danych kontraktu przypomnien w Supabase (tabela glowna + tabela attempts).
- Wymuszenie jawnego odbiorcy email i deduplikacji per dzien.
- Zdefiniowanie statusow dostarczenia i pol diagnostycznych.
- Dodanie RLS i minimalnych polityk dostepu zgodnych z izolacja firmy.
- Dodanie serwerowych helperow/repozytorium do tworzenia i aktualizacji reminderow.
- Dodanie wewnetrznego API (serwer-only) do rejestrowania attempts i aktualizacji statusu.
- Aktualizacja dokumentacji (README + plan progress) pod kontrakt F-02.

### Out of Scope

- Realna wysylka email (SMTP/provider) i harmonogram uruchamiania.
- UI do zarzadzania przypomnieniami dla koncowego uzytkownika.
- Integracja z tabela licencji z S-03 przez FK (S-03 jeszcze nie istnieje).
- Policy engine z wieloma recipientami (CC/broadcast).
- Zaawansowane retry backoff/circuit-breaker.

## Target Architecture

F-02 wprowadza dwa byty:

1. `license_renewal_reminders` jako kontrakt „co ma byc dostarczone”, z unikalnoscia chroniaca przed duplikatem i statusem lifecycle (`pending/sent/failed`).
2. `license_renewal_reminder_attempts` jako append-only dziennik prob dostarczenia.

Warstwa aplikacyjna udostepnia tylko serwerowe helpery do:
- utworzenia remindera,
- przejscia remindera do `sent`/`failed`,
- dopisania attemptu.

RLS ogranicza odczyt i zapis do firm, w ktorych user ma membership. Brak bezposredniego client-side insert dla endpointow publicznych.

## Data Contract

### Tables

- `public.license_renewal_reminders`
  - `id uuid primary key default gen_random_uuid()`
  - `company_id uuid not null references public.companies(id) on delete cascade`
  - `license_ref text not null check (length(btrim(license_ref)) > 0)`
  - `recipient_email text not null check (position('@' in recipient_email) > 1)`
  - `reminder_date date not null`
  - `status text not null check (status in ('pending', 'sent', 'failed')) default 'pending'`
  - `attempt_count integer not null default 0 check (attempt_count >= 0)`
  - `last_error text null`
  - `last_attempted_at timestamptz null`
  - `sent_at timestamptz null`
  - `created_at timestamptz not null default now()`
  - `updated_at timestamptz not null default now()`
  - unique constraint on `(company_id, license_ref, reminder_date, recipient_email)`

- `public.license_renewal_reminder_attempts`
  - `id uuid primary key default gen_random_uuid()`
  - `reminder_id uuid not null references public.license_renewal_reminders(id) on delete cascade`
  - `company_id uuid not null references public.companies(id) on delete cascade`
  - `attempt_no integer not null check (attempt_no > 0)`
  - `status text not null check (status in ('sent', 'failed'))`
  - `error_message text null`
  - `attempted_at timestamptz not null default now()`
  - `provider_message_id text null`

### Invariants

- Jeden reminder per `(company_id, license_ref, reminder_date, recipient_email)`.
- `recipient_email` jest wymagany i jawny.
- `attempt_count` odzwierciedla liczbe attemptow w lifecycle remindera.
- `sent` oznacza dostarczenie logiczne i ustawia `sent_at`.
- Wszystkie rekordy sa scope'owane do `company_id`.

### RLS Contract

- RLS enabled na obu tabelach.
- Odczyt: tylko user z membershipem w `company_id`.
- Zapis/aktualizacja: tylko przez kontrolowane sciezki serwerowe (endpointy backendowe + helpery), z walidacja company scope.
- Brak klientowego write-path wystawionego bezposrednio do bazy.

## Phase 1: Database Guardrail Contract

Dodaj schemat i zasady integralnosci, ktore fizycznie blokuja duplikaty i wymuszaja jawny odbiorca.

### Changes Required

- `supabase/migrations/<timestamp>_reminder_delivery_guardrail.sql`
  - **Intent**: Zdefiniowac minimalny, produkcyjnie bezpieczny model reminderow i audit log attemptow.
  - **Contract**: Tworzy tabele `license_renewal_reminders` i `license_renewal_reminder_attempts`, constraints statusow, dedup unique key, indeksy (`company_id`, `reminder_date`, `status`, `reminder_id`) oraz trigger `updated_at`.

- `supabase/migrations/<timestamp>_reminder_delivery_guardrail.sql` (RLS/policies)
  - **Intent**: Utrzymac izolacje firm i kontrolowany dostep do przypomnien.
  - **Contract**: Wlacza RLS i definiuje polityki read/write powiazane z `company_memberships`.

- `README.md`
  - **Intent**: Udokumentowac, ze przypomnienia maja juz kontrakt danych bez realnej wysylki.
  - **Contract**: Sekcja opisuje dedup key, statusy i fakt, ze delivery provider jest poza F-02.

### Success Criteria

#### Automated Verification

- Istnieje migracja z obiema tabelami reminderow i constraints.
- SQL zawiera unique key `(company_id, license_ref, reminder_date, recipient_email)`.
- SQL zawiera status contract (`pending/sent/failed`) i pola attempt diagnostyki.
- SQL wlacza RLS i polityki oparte o membership.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Reviewer potwierdza, ze kontrakt blokuje duplikat remindera dla tego samego dnia.
- Reviewer potwierdza, ze recipient jest jawny i wymagany.

**Implementation Note**: Po przejsciu automatycznej walidacji zrobic reczna weryfikacje SQL przed przejsciem do warstwy API/helperow.

---

## Phase 2: Server Access Layer for Reminder Lifecycle

Zaimplementuj serwerowa warstwe tworzenia reminderow i rejestrowania attemptow ze spojnym company scope.

### Changes Required

- `src/lib/reminder-contract.ts` (new)
  - **Intent**: Skupic operacje kontraktowe F-02 w jednym module serwerowym.
  - **Contract**: Eksportuje typy i funkcje: `createReminderContract`, `markReminderSent`, `markReminderFailed`, `appendReminderAttempt`, `getReminderByDedupKey`.

- `src/pages/api/internal/reminders/*.ts` (new internal endpoints)
  - **Intent**: Udostepnic kontrolowany write-path dla przyszlego schedulera/workera.
  - **Contract**: Endpointy przyjmuja dane wejscia, waliduja company scope przez `Astro.locals` i uzywaja helperow z `reminder-contract.ts`; brak publicznej ekspozycji dla anon klienta.

- `src/env.d.ts` i/lub lokalne typy
  - **Intent**: Zapewnic typowany model reminder status/attempt payloadow.
  - **Contract**: Typy obejmuja `ReminderStatus`, `ReminderContract`, `ReminderAttempt`.

- `src/middleware.ts` (ew. drobna aktualizacja)
  - **Intent**: Zapewnic, ze internal endpoints korzystaja z tego samego company context co pozostale sciezki chronione.
  - **Contract**: Endpointy internal wymagaja authenticated user z membershipem lub jawnie dokumentowanego serwisowego mechanizmu auth.

### Success Criteria

#### Automated Verification

- Helpery kontraktowe kompiluja sie i nie lamia lint/build.
- Internal endpoints odrzucaja request bez poprawnego kontekstu firmy.
- Tworzenie remindera zapisuje rekord `pending` z dedup enforcement.
- Aktualizacja na `sent`/`failed` podbija `attempt_count` i zapisuje `last_attempted_at`.
- `npm run lint` passes.
- `npm run build` passes.

#### Manual Verification

- Powtorna proba utworzenia remindera z tym samym dedup key nie tworzy duplikatu.
- Rejestracja attemptu dodaje wpis append-only i zostawia spojny stan remindera.
- Uzytkownik spoza firmy nie ma dostepu do rekordow reminderow danej firmy.

**Implementation Note**: To nadal nie jest faza wysylki email; API ma modelowac kontrakt, nie pipeline dostarczenia.

---

## Phase 3: Verification and Handoff to S-06

Domknij dokumentacje i lekkie testy kontraktu, zeby S-06 mogl wejsc bez redefiniowania podstaw.

### Changes Required

- `README.md`
  - **Intent**: Jasno rozdzielic F-02 (guardrail) od S-06 (real delivery).
  - **Contract**: Dodaje sekcje „Reminder contract (F-02)” z przykładowym lifecycle `pending -> sent/failed` i opisem dedup.

- `context/changes/f-02/plan.md`
  - **Intent**: Zaktualizowac postep i SHA po wdrozeniu.
  - **Contract**: `## Progress` odzwierciedla status faz po przejsciu weryfikacji.

- `tests` lub lekkie skrypty walidacyjne (jesli zgodne ze wzorcem repo)
  - **Intent**: Zlapac regresje kontraktu dedup/status.
  - **Contract**: Co najmniej jeden automatyczny check dla dedup key i jeden dla transition/status attempt.

### Success Criteria

#### Automated Verification

- `npm run lint` passes.
- `npm run build` passes.
- Istnieje lekka automatyczna walidacja dedup/status (test lub script).
- Dokumentacja zawiera rozdzial F-02 i granice odpowiedzialnosci wzgledem S-06.

#### Manual Verification

- Reviewer potwierdza, ze S-06 moze korzystac z kontraktu bez zmian breaking.
- Reviewer potwierdza, ze kontrakt nie wymaga jeszcze modelu licencji z S-03.
- Reviewer potwierdza, ze ryzyka duplikatu i blednego odbiorcy sa zaadresowane w warstwie foundation.

## Testing Strategy

### Automated

- `npm run lint`
- `npm run build`
- (Jesli dostepne) lokalna walidacja migracji: `npx supabase db reset`
- Dedup test: druga proba insertu z tym samym dedup key zwraca conflict/constraint error.
- Lifecycle test: `pending -> failed -> pending(retry decision in S-06)` lub `pending -> sent` z append attempt.

### Manual

1. Uruchom lokalne Supabase i zastosuj migracje.
2. Utworz reminder przez internal endpoint ze wskazanym `recipient_email`.
3. Powtorz ten sam request i potwierdz brak duplikatu.
4. Zarejestruj `failed` attempt i sprawdz `attempt_count`, `last_error`, `last_attempted_at`.
5. Zarejestruj `sent` attempt i sprawdz `sent_at` oraz wpis attempt.
6. Zweryfikuj, ze user spoza firmy nie moze odczytac ani zmienic remindera.

## Performance Considerations

Najczestsze zapytania S-06 beda filtrowac po `status` i `reminder_date`, dlatego indeksy na tych kolumnach sa wymagane od poczatku. Deduplikacja oparta o unique key jest O(log n) i wystarczajaca dla skali MVP.

## Migration Notes

F-02 nie zaklada istnienia tabeli licencji z S-03; dlatego używa `license_ref` jako stabilnego identyfikatora domenowego na etapie foundation. Po wejściu S-03 mozna dodac kompatybilny bridge do `license_id` bez lamania kontraktu dedup.

Supabase schema deploy na preview/production pozostaje aktywnoscia wymagajaca recznej zgody (oddzielnie od deployu Workera).

## References

- Roadmap: `context/foundation/roadmap.md`
- PRD: `context/foundation/prd.md`
- Existing access boundary: `supabase/migrations/20260528180000_company_role_boundary.sql`
- Existing company context: `src/lib/access-context.ts`
- Existing protected routing: `src/middleware.ts`

## Progress

> Convention: `- [ ]` pending, `- [x]` done. Append ` — <commit sha>` when a step lands. Do not rename step titles.

### Phase 1: Database Guardrail Contract

#### Automated

- [x] 1.1 Istnieje migracja z obiema tabelami reminderow i constraints. — 5b61d92
- [x] 1.2 SQL zawiera unique key `(company_id, license_ref, reminder_date, recipient_email)`. — 5b61d92
- [x] 1.3 SQL zawiera status contract (`pending/sent/failed`) i pola attempt diagnostyki. — 5b61d92
- [x] 1.4 SQL wlacza RLS i polityki oparte o membership. — 5b61d92
- [x] 1.5 `npm run lint` passes. — 5b61d92
- [x] 1.6 `npm run build` passes. — 5b61d92

#### Manual

- [x] 1.7 Reviewer potwierdza, ze kontrakt blokuje duplikat remindera dla tego samego dnia. — 5b61d92
- [x] 1.8 Reviewer potwierdza, ze recipient jest jawny i wymagany. — 5b61d92

### Phase 2: Server Access Layer for Reminder Lifecycle

#### Automated

- [x] 2.1 Helpery kontraktowe kompiluja sie i nie lamia lint/build. — 9d4ac2e
- [x] 2.2 Internal endpoints odrzucaja request bez poprawnego kontekstu firmy. — 9d4ac2e
- [x] 2.3 Tworzenie remindera zapisuje rekord `pending` z dedup enforcement. — 9d4ac2e
- [x] 2.4 Aktualizacja na `sent`/`failed` podbija `attempt_count` i zapisuje `last_attempted_at`. — 9d4ac2e
- [x] 2.5 `npm run lint` passes. — 9d4ac2e
- [x] 2.6 `npm run build` passes. — 9d4ac2e

#### Manual

- [x] 2.7 Powtorna proba utworzenia remindera z tym samym dedup key nie tworzy duplikatu. — 9d4ac2e
- [x] 2.8 Rejestracja attemptu dodaje wpis append-only i zostawia spojny stan remindera. — 9d4ac2e
- [x] 2.9 Uzytkownik spoza firmy nie ma dostepu do rekordow reminderow danej firmy. — 9d4ac2e

### Phase 3: Verification and Handoff to S-06

#### Automated

- [ ] 3.1 `npm run lint` passes.
- [ ] 3.2 `npm run build` passes.
- [ ] 3.3 Istnieje lekka automatyczna walidacja dedup/status (test lub script).
- [ ] 3.4 Dokumentacja zawiera rozdzial F-02 i granice odpowiedzialnosci wzgledem S-06.

#### Manual

- [ ] 3.5 Reviewer potwierdza, ze S-06 moze korzystac z kontraktu bez zmian breaking.
- [ ] 3.6 Reviewer potwierdza, ze kontrakt nie wymaga jeszcze modelu licencji z S-03.
- [ ] 3.7 Reviewer potwierdza, ze ryzyka duplikatu i blednego odbiorcy sa zaadresowane w warstwie foundation.
