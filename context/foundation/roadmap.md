---
project: "ITventory"
version: 2
status: proposed
created: 2026-05-28
updated: 2026-06-06
prd_version: 2
main_goal: market-feedback
top_blocker: time
---

# ITventory Roadmap

## Vision recap

ITventory ma dac wlascicielowi firmy jedno miejsce do sprawdzania sprzetu, licencji, przypisan pracownikow i zblizajacych sie odnowien. Najwazniejszy sygnal wartosci to sytuacja, w ktorej system pomaga zauwazyc nadchodzace odnowienie licencji zanim firma poniesie niechciany koszt.

Roadmapa optymalizuje pod market-feedback: jak najszybsze dostarczenie dzialajacego przeplywu, ktory realny uzytkownik moze ocenic na podstawie wprowadzonych danych, dashboardu i przypomnienia mailowego.

## North star

North star - najmniejszy przeplyw end-to-end, ktory pokazuje glowna wartosc produktu - to **S-05: Mailowe przypomnienie o odnowieniu licencji**. Ten przeplyw potwierdza, czy reczna ewidencja licencji, dat odnowien i kosztow rzeczywiscie zmniejsza ryzyko przeoczonej subskrypcji.

## At a glance

| ID   | Type       | Outcome                                                                                                        | Change ID                   | Prerequisites    | PRD refs                                            | Status      |
| ---- | ---------- | -------------------------------------------------------------------------------------------------------------- | --------------------------- | ---------------- | --------------------------------------------------- | ----------- |
| F-01 | foundation | Minimalny kontrakt firmy i pojedynczej roli owner pozwala planowac kazdy przeplyw jako odseparowany per firma. | owner-company-boundary      | none             | FR-001, FR-003, FR-004, US-01                       | implemented |
| F-02 | foundation | Minimalny kontrakt bezpiecznego wysylania przypomnien ogranicza ryzyko duplikatow i blednych odbiorcow.        | reminder-delivery-guardrail | none             | FR-011, US-07                                       | implemented |
| S-01 | slice      | Wlasciciel zaklada firme, loguje sie i trafia do pustej przestrzeni danych swojej firmy.                       | owner-company-start         | F-01             | FR-001, FR-002, FR-004, US-01                       | implemented |
| S-02 | slice      | Wlasciciel tworzy podstawowa kartoteke pracownikow, sprzetu i licencji z datami odnowien.                      | inventory-records           | S-01             | FR-005, FR-006, FR-007, FR-010, US-02, US-03, US-04 | implemented |
| S-03 | slice      | Wlasciciel przypisuje sprzet i licencje do pracownikow lub urzadzen i widzi aktualne powiazania.               | asset-assignments           | S-02             | FR-008, FR-009, US-05                               | proposed    |
| S-04 | slice      | Wlasciciel widzi dashboard kosztow subskrypcji, podstawowego stanu sprzetu i zblizajacych sie odnowien.        | renewal-dashboard           | S-02             | FR-010, FR-012, FR-013, US-06                       | proposed    |
| S-05 | slice      | System wysyla mailowe przypomnienie do wlasciciela firmy o konkretnej licencji przed data odnowienia.          | renewal-email-alert         | F-01, F-02, S-02 | FR-010, FR-011, US-07                               | proposed    |
| S-06 | slice      | Wlasciciel tworzy prosty szablon onboardingowy i stosuje go do nowego pracownika.                              | onboarding-template         | S-02, S-03       | FR-014, FR-015, FR-016, US-08                       | proposed    |

## Streams

| Stream                | Chain                | Purpose                                                                                                            |
| --------------------- | -------------------- | ------------------------------------------------------------------------------------------------------------------ |
| Access and trust      | F-01 -> S-01         | Najpierw zamyka izolacje firmy i pojedynczego wlasciciela, bo kazdy kolejny przeplyw opiera sie na tym kontekście. |
| Inventory core        | S-02 -> S-03         | Buduje reczna ewidencje i powiazania, czyli dane potrzebne do dashboardu, alertow i onboardingu.                   |
| Renewal signal        | F-02 -> S-04 -> S-05 | Doprowadza produkt do pierwszego mierzalnego sygnalu wartosci: koszt, odnowienie i mailowe przypomnienie.          |
| Onboarding operations | S-06                 | Dodaje operacyjny przeplyw pakietow onboardingowych po tym, jak istnieja pracownicy, zasoby i przypisania.         |

## Baseline

- Frontend: present. Aplikacja ma produktowy landing, auth flow i pusty dashboard w stylu ITventory.
- Backend/API: partial. Istnieja trasy auth i serwerowy runtime, ale nie ma jeszcze domenowych przeplywow ewidencji ITventory.
- Data: partial. Istnieje integracja z dostawca danych/auth oraz model firmy/membershipu, ale nie ma jeszcze modelu domenowego zasobow, przypisan i przypomnien powiazanych z licencjami.
- Auth: present for MVP. Logowanie, rejestracja, ochrona trasy i pojedyncza rola `owner` sa obecne. Zaproszenia i Manager nie sa czescia MVP.
- Deploy/infra: present. Sciezka produkcyjna zostala wdrozona i zweryfikowana, a uzytkownik potwierdzil, ze produkcja dziala dobrze.
- Observability: partial. Platformowa obserwowalnosc jest wlaczona, ale aplikacja nie ma jeszcze domenowego sladu zdarzen dla alertow i operacji IT.

## Foundations

### F-01: Minimalny kontrakt firmy i wlasciciela

- Outcome: Minimalny kontrakt firmy i pojedynczej roli owner pozwala planowac kazdy przeplyw jako odseparowany per firma.
- Change ID: owner-company-boundary
- PRD refs: FR-001, FR-003, FR-004, US-01
- Prerequisites: none
- Parallel with: F-02
- Blockers: none
- Unknowns: none
- Risk: Jesli granica firmy i wlasciciel zostana potraktowane jako dodatek, pozniejsze przeplywy moga wymieszac dane albo wymagac kosztownego cofania decyzji.
- Status: implemented
- Unlocks: S-01, S-05

### F-02: Minimalny kontrakt bezpiecznego wysylania przypomnien

- Outcome: Minimalny kontrakt bezpiecznego wysylania przypomnien ogranicza ryzyko duplikatow i blednych odbiorcow.
- Change ID: reminder-delivery-guardrail
- PRD refs: FR-011, US-07
- Prerequisites: none
- Parallel with: F-01, S-01
- Blockers: none
- Unknowns: none
- Risk: Przypomnienia sa wartoscia produktu, ale bez minimalnej kontroli odbiorcy i powtorzen moga szybko zniszczyc zaufanie do systemu.
- Status: implemented
- Unlocks: S-05

## Slices

### S-01: Wlasciciel zaklada firme i zaczyna w pustej przestrzeni

- Outcome: Wlasciciel zaklada firme, loguje sie i trafia do pustej przestrzeni danych swojej firmy.
- Change ID: owner-company-start
- PRD refs: FR-001, FR-002, FR-004, US-01
- Prerequisites: F-01
- Parallel with: none
- Blockers: none
- Unknowns: none
- Risk: Bez tego przeplywu kazda kolejna funkcja nie ma pewnego kontekstu firmy ani jasnego wlasciciela danych.
- Status: implemented

### S-02: Wlasciciel prowadzi podstawowa kartoteke

- Outcome: Wlasciciel tworzy podstawowa kartoteke pracownikow, sprzetu i licencji z datami odnowien.
- Change ID: inventory-records
- PRD refs: FR-005, FR-006, FR-007, FR-010, US-02, US-03, US-04
- Prerequisites: S-01
- Parallel with: none
- Blockers: none
- Unknowns: none
- Risk: Jesli kartoteka bedzie zbyt szeroka, zje zakres MVP; jesli bedzie zbyt uboga, dashboard i alerty nie beda mialy uzytecznych danych.
- Status: implemented

### S-03: Wlasciciel widzi aktualne przypisania

- Outcome: Wlasciciel przypisuje sprzet i licencje do pracownikow lub urzadzen i widzi aktualne powiazania.
- Change ID: asset-assignments
- PRD refs: FR-008, FR-009, US-05
- Prerequisites: S-02
- Parallel with: S-04
- Blockers: none
- Unknowns: none
- Risk: Przypisania sa praktycznym dowodem "kto ma co", wiec ich brak zostawia produkt jako zwykla liste zasobow.
- Status: proposed

### S-04: Wlasciciel widzi dashboard kosztow i odnowien

- Outcome: Wlasciciel widzi dashboard kosztow subskrypcji, podstawowego stanu sprzetu i zblizajacych sie odnowien.
- Change ID: renewal-dashboard
- PRD refs: FR-010, FR-012, FR-013, US-06
- Prerequisites: S-02
- Parallel with: S-03
- Blockers: none
- Unknowns: none
- Risk: Dashboard musi pokazac priorytety uwagi, nie tylko liczby, bo inaczej nie potwierdzi glownej obietnicy produktu.
- Status: proposed

### S-05: System wysyla przypomnienie mailowe

- Outcome: System wysyla mailowe przypomnienie do wlasciciela firmy o konkretnej licencji przed data odnowienia.
- Change ID: renewal-email-alert
- PRD refs: FR-010, FR-011, US-07
- Prerequisites: F-01, F-02, S-02
- Parallel with: none
- Blockers: none
- Unknowns: none
- Risk: To glowny sygnal wartosci, ale duplikat maila moze wygladac gorzej niz brak automatyzacji.
- Status: proposed

### S-06: Wlasciciel uzywa prostego szablonu onboardingowego

- Outcome: Wlasciciel tworzy prosty szablon onboardingowy i stosuje go do nowego pracownika.
- Change ID: onboarding-template
- PRD refs: FR-014, FR-015, FR-016, US-08
- Prerequisites: S-02, S-03
- Parallel with: none
- Blockers: none
- Unknowns: none
- Risk: Szablon ma pozostac reczna lista zasobow i licencji; automatyczne provisioning lub integracje przekroczylyby zakres MVP.
- Status: proposed

## Backlog Handoff

| Roadmap ID | Change ID           | Outcome                                                                                                 | PRD refs                                            | Status   |
| ---------- | ------------------- | ------------------------------------------------------------------------------------------------------- | --------------------------------------------------- | -------- |
| S-03       | asset-assignments   | Wlasciciel przypisuje sprzet i licencje do pracownikow lub urzadzen i widzi aktualne powiazania.        | FR-008, FR-009, US-05                               | proposed |
| S-04       | renewal-dashboard   | Wlasciciel widzi dashboard kosztow subskrypcji, podstawowego stanu sprzetu i zblizajacych sie odnowien. | FR-010, FR-012, FR-013, US-06                       | proposed |
| S-05       | renewal-email-alert | System wysyla mailowe przypomnienie do wlasciciela firmy o konkretnej licencji przed data odnowienia.   | FR-010, FR-011, US-07                               | proposed |
| S-06       | onboarding-template | Wlasciciel tworzy prosty szablon onboardingowy i stosuje go do nowego pracownika.                       | FR-014, FR-015, FR-016, US-08                       | proposed |

## Open Roadmap Questions

No open questions.

## Parked

- Zaproszenia uzytkownikow i rola Menedzera.
- Integracje z zewnetrznymi systemami ewidencji, chmury, pakietow biurowych i komunikatorow.
- Automatyczne wykrywanie licencji lub sprzetu.
- Niestandardowy model uprawnien poza rola `owner`.
- Wielowalutowosc kosztow.
- Pelny cykl zycia sprzetu, serwis, amortyzacja i fizyczne kody inwentaryzacyjne.

## Done

- F-01: Company boundary migrated to one flat `owner` role.
- F-02: Reminder delivery guardrail implemented.
- S-01: Owner can create a company and start in an empty workspace.
- S-02: Owner can create and maintain employees, hardware, and software licenses with renewal dates.
