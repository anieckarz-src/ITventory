---
project: "ITventory"
version: 1
status: proposed
created: 2026-05-28
updated: 2026-05-28
prd_version: 1
main_goal: market-feedback
top_blocker: time
---

# ITventory Roadmap

## Vision recap

ITventory ma dac Menedzerowi IT jedno miejsce do sprawdzania sprzetu, licencji, przypisan pracownikow i zblizajacych sie odnowien. Najwazniejszy sygnal wartosci to sytuacja, w ktorej system pomaga zauwazyc nadchodzace odnowienie licencji zanim firma poniesie niechciany koszt.

Roadmapa optymalizuje pod market-feedback: jak najszybsze dostarczenie dzialajacego przeplywu, ktory realny uzytkownik moze ocenic na podstawie wprowadzonych danych, dashboardu i przypomnienia mailowego.

## North star

North star - najmniejszy przeplyw end-to-end, ktory pokazuje glowna wartosc produktu - to **S-06: Mailowe przypomnienie o odnowieniu licencji**. Ten przeplyw potwierdza, czy reczna ewidencja licencji, dat odnowien i kosztow rzeczywiscie zmniejsza ryzyko przeoczonej subskrypcji.

## At a glance

| ID | Type | Outcome | Change ID | Prerequisites | PRD refs | Status |
| --- | --- | --- | --- | --- | --- | --- |
| F-01 | foundation | Minimalny kontrakt firmy, czlonkostwa i rol pozwala planowac kazdy przeplyw jako odseparowany per firma. | company-role-boundary | none | FR-001, FR-003, FR-004, FR-005, US-01, US-02 | ready |
| F-02 | foundation | Minimalny kontrakt bezpiecznego wysylania przypomnien ogranicza ryzyko duplikatow i blednych odbiorcow. | reminder-delivery-guardrail | none | FR-012, US-08 | ready |
| S-01 | slice | Administrator zaklada firme, loguje sie i trafia do pustej przestrzeni danych swojej firmy. | admin-company-start | F-01 | FR-001, FR-002, FR-004, FR-005, US-01 | proposed |
| S-02 | slice | Administrator zaprasza Menedzera, a Menedzer dolacza bez dostepu do ustawien i zapraszania uzytkownikow. | manager-invitation | S-01 | FR-003, FR-005, US-02 | proposed |
| S-03 | slice | Menedzer tworzy podstawowa kartoteke pracownikow, sprzetu i licencji z datami odnowien. | inventory-records | S-01 | FR-006, FR-007, FR-008, FR-011, US-03, US-04, US-05 | proposed |
| S-04 | slice | Menedzer przypisuje sprzet i licencje do pracownikow lub urzadzen i widzi aktualne powiazania. | asset-assignments | S-03 | FR-009, FR-010, US-06 | proposed |
| S-05 | slice | Menedzer widzi dashboard kosztow subskrypcji, podstawowego stanu sprzetu i zblizajacych sie odnowien. | renewal-dashboard | S-03 | FR-011, FR-013, FR-014, US-07 | proposed |
| S-06 | slice | System wysyla mailowe przypomnienie o konkretnej licencji przed data odnowienia. | renewal-email-alert | F-01, F-02, S-03 | FR-011, FR-012, US-08 | blocked |
| S-07 | slice | Menedzer tworzy prosty szablon onboardingowy i stosuje go do nowego pracownika. | onboarding-template | S-03, S-04 | FR-015, FR-016, FR-017, US-09 | proposed |

## Streams

| Stream | Chain | Purpose |
| --- | --- | --- |
| Access and trust | F-01 -> S-01 -> S-02 | Najpierw zamyka izolacje firmy i role, bo kazdy kolejny przeplyw opiera sie na tych granicach. |
| Inventory core | S-03 -> S-04 | Buduje reczna ewidencje i powiazania, czyli dane potrzebne do dashboardu, alertow i onboardingu. |
| Renewal signal | F-02 -> S-05 -> S-06 | Doprowadza produkt do pierwszego mierzalnego sygnalu wartosci: koszt, odnowienie i mailowe przypomnienie. |
| Onboarding operations | S-07 | Dodaje operacyjny przeplyw pakietow onboardingowych po tym, jak istnieja pracownicy, zasoby i przypisania. |

## Baseline

- Frontend: present. Aplikacja ma scaffold UI, routing, komponenty auth i dashboard startowy.
- Backend/API: partial. Istnieja trasy auth i serwerowy runtime, ale nie ma jeszcze domenowych przeplywow ITventory.
- Data: partial. Istnieje integracja z dostawca danych/auth, ale nie ma jeszcze modelu domenowego firmy, zasobow, przypisan i przypomnien.
- Auth: partial. Logowanie, rejestracja i ochrona trasy sa obecne, ale brakuje firm, zaproszen i granic rol Administrator/Menedzer.
- Deploy/infra: present. Sciezka produkcyjna zostala wdrozona i zweryfikowana, a uzytkownik potwierdzil, ze produkcja dziala dobrze.
- Observability: partial. Platformowa obserwowalnosc jest wlaczona, ale aplikacja nie ma jeszcze domenowego sladu zdarzen dla alertow i operacji IT.

## Foundations

### F-01: Minimalny kontrakt firmy, czlonkostwa i rol

- Outcome: Minimalny kontrakt firmy, czlonkostwa i rol pozwala planowac kazdy przeplyw jako odseparowany per firma.
- Change ID: company-role-boundary
- PRD refs: FR-001, FR-003, FR-004, FR-005, US-01, US-02
- Prerequisites: none
- Parallel with: F-02
- Blockers: none
- Unknowns: none
- Risk: Jesli granica firmy i role zostana potraktowane jako dodatek, pozniejsze przeplywy moga wymieszac dane albo wymagac kosztownego cofania decyzji.
- Status: ready
- Unlocks: S-01, S-02, S-06

### F-02: Minimalny kontrakt bezpiecznego wysylania przypomnien

- Outcome: Minimalny kontrakt bezpiecznego wysylania przypomnien ogranicza ryzyko duplikatow i blednych odbiorcow.
- Change ID: reminder-delivery-guardrail
- PRD refs: FR-012, US-08
- Prerequisites: none
- Parallel with: F-01, S-01
- Blockers: none
- Unknowns: none
- Risk: Przypomnienia sa wartoscia produktu, ale bez minimalnej kontroli odbiorcy i powtorzen moga szybko zniszczyc zaufanie do systemu.
- Status: ready
- Unlocks: S-06

## Slices

### S-01: Administrator zaklada firme i zaczyna w pustej przestrzeni

- Outcome: Administrator zaklada firme, loguje sie i trafia do pustej przestrzeni danych swojej firmy.
- Change ID: admin-company-start
- PRD refs: FR-001, FR-002, FR-004, FR-005, US-01
- Prerequisites: F-01
- Parallel with: none
- Blockers: none
- Unknowns: none
- Risk: Bez tego przeplywu kazda kolejna funkcja nie ma pewnego kontekstu firmy ani jasnego wlasciciela danych.
- Status: proposed

### S-02: Administrator zaprasza Menedzera

- Outcome: Administrator zaprasza Menedzera, a Menedzer dolacza bez dostepu do ustawien i zapraszania uzytkownikow.
- Change ID: manager-invitation
- PRD refs: FR-003, FR-005, US-02
- Prerequisites: S-01
- Parallel with: S-03
- Blockers: none
- Unknowns: none
- Risk: Delegacja pracy jest jednym z kryteriow sukcesu, ale zbyt szerokie uprawnienia Menedzera podwazaja granice MVP.
- Status: proposed

### S-03: Menedzer prowadzi podstawowa kartoteke

- Outcome: Menedzer tworzy podstawowa kartoteke pracownikow, sprzetu i licencji z datami odnowien.
- Change ID: inventory-records
- PRD refs: FR-006, FR-007, FR-008, FR-011, US-03, US-04, US-05
- Prerequisites: S-01
- Parallel with: S-02
- Blockers: none
- Unknowns: none
- Risk: Jesli kartoteka bedzie zbyt szeroka, zje zakres MVP; jesli bedzie zbyt uboga, dashboard i alerty nie beda mialy uzytecznych danych.
- Status: proposed

### S-04: Menedzer widzi aktualne przypisania

- Outcome: Menedzer przypisuje sprzet i licencje do pracownikow lub urzadzen i widzi aktualne powiazania.
- Change ID: asset-assignments
- PRD refs: FR-009, FR-010, US-06
- Prerequisites: S-03
- Parallel with: S-05
- Blockers: none
- Unknowns: none
- Risk: Przypisania sa praktycznym dowodem "kto ma co", wiec ich brak zostawia produkt jako zwykla liste zasobow.
- Status: proposed

### S-05: Menedzer widzi dashboard kosztow i odnowien

- Outcome: Menedzer widzi dashboard kosztow subskrypcji, podstawowego stanu sprzetu i zblizajacych sie odnowien.
- Change ID: renewal-dashboard
- PRD refs: FR-011, FR-013, FR-014, US-07
- Prerequisites: S-03
- Parallel with: S-04
- Blockers: none
- Unknowns: none
- Risk: Dashboard musi pokazac priorytety uwagi, nie tylko liczby, bo inaczej nie potwierdzi glownej obietnicy produktu.
- Status: proposed

### S-06: System wysyla przypomnienie mailowe

- Outcome: System wysyla mailowe przypomnienie o konkretnej licencji przed data odnowienia.
- Change ID: renewal-email-alert
- PRD refs: FR-011, FR-012, US-08
- Prerequisites: F-01, F-02, S-03
- Parallel with: none
- Blockers: Odbiorca przypomnienia wymaga doprecyzowania, gdy firma ma wiecej niz jednego Administratora lub Menedzera.
- Unknowns: [Block: yes] Kto jest "wlasciwa osoba" dla przypomnienia przy wielu uzytkownikach firmy? Owner: Product.
- Risk: To glowny sygnal wartosci, ale bledny odbiorca albo duplikat maila moze wygladac gorzej niz brak automatyzacji.
- Status: blocked

### S-07: Menedzer uzywa prostego szablonu onboardingowego

- Outcome: Menedzer tworzy prosty szablon onboardingowy i stosuje go do nowego pracownika.
- Change ID: onboarding-template
- PRD refs: FR-015, FR-016, FR-017, US-09
- Prerequisites: S-03, S-04
- Parallel with: none
- Blockers: none
- Unknowns: none
- Risk: Szablon ma pozostac reczna lista zasobow i licencji; automatyczne provisioning lub integracje przekroczylyby zakres MVP.
- Status: proposed

## Backlog Handoff

| Roadmap ID | Change ID | Outcome | PRD refs | Status |
| --- | --- | --- | --- | --- |
| F-01 | company-role-boundary | Minimalny kontrakt firmy, czlonkostwa i rol pozwala planowac kazdy przeplyw jako odseparowany per firma. | FR-001, FR-003, FR-004, FR-005, US-01, US-02 | ready |
| F-02 | reminder-delivery-guardrail | Minimalny kontrakt bezpiecznego wysylania przypomnien ogranicza ryzyko duplikatow i blednych odbiorcow. | FR-012, US-08 | ready |
| S-01 | admin-company-start | Administrator zaklada firme, loguje sie i trafia do pustej przestrzeni danych swojej firmy. | FR-001, FR-002, FR-004, FR-005, US-01 | proposed |
| S-02 | manager-invitation | Administrator zaprasza Menedzera, a Menedzer dolacza bez dostepu do ustawien i zapraszania uzytkownikow. | FR-003, FR-005, US-02 | proposed |
| S-03 | inventory-records | Menedzer tworzy podstawowa kartoteke pracownikow, sprzetu i licencji z datami odnowien. | FR-006, FR-007, FR-008, FR-011, US-03, US-04, US-05 | proposed |
| S-04 | asset-assignments | Menedzer przypisuje sprzet i licencje do pracownikow lub urzadzen i widzi aktualne powiazania. | FR-009, FR-010, US-06 | proposed |
| S-05 | renewal-dashboard | Menedzer widzi dashboard kosztow subskrypcji, podstawowego stanu sprzetu i zblizajacych sie odnowien. | FR-011, FR-013, FR-014, US-07 | proposed |
| S-06 | renewal-email-alert | System wysyla mailowe przypomnienie o konkretnej licencji przed data odnowienia. | FR-011, FR-012, US-08 | blocked |
| S-07 | onboarding-template | Menedzer tworzy prosty szablon onboardingowy i stosuje go do nowego pracownika. | FR-015, FR-016, FR-017, US-09 | proposed |

## Open Roadmap Questions

- Kto jest "wlasciwa osoba" dla przypomnienia mailowego, gdy firma ma wielu Administratorow lub Menedzerow? Owner: Product. Unblocks: S-06.

## Parked

- Integracje z zewnetrznymi systemami ewidencji, chmury, pakietow biurowych i komunikatorow.
- Automatyczne wykrywanie licencji lub sprzetu.
- Niestandardowy model uprawnien poza rolami Administrator i Menedzer.
- Wielowalutowosc kosztow.
- Pelny cykl zycia sprzetu, serwis, amortyzacja i fizyczne kody inwentaryzacyjne.

## Done

No roadmap items completed yet.
