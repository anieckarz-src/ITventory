---
project: "ITventory"
version: 2
status: draft
created: 2026-05-25
updated: 2026-06-06
context_type: greenfield
product_type: web-app
target_scale:
  users: small
  qps: low
  data_volume: small
timeline_budget:
  mvp_weeks: 3
  hard_deadline: null
  after_hours_only: true
---

# ITventory PRD

## Vision & Problem Statement

Manualne zarzadzanie licencjami oprogramowania i sprzetem firmowym tworzy kosztowny narzut koordynacyjny dla osoby odpowiedzialnej za IT. Bol pojawia sie szczegolnie przy konczacych sie subskrypcjach, onboardingu, offboardingu oraz przy pytaniu "kto ma jaki sprzet i software".

Status quo nie daje jednego zrodla prawdy ani mechanizmu priorytetyzacji ryzyk. ITventory ma laczyc ewidencje zasobow z przypomnieniami i praktycznymi zestawami operacyjnymi, tak aby wlasciciel firmowej przestrzeni IT szybciej widzial wygasajace licencje, przypisania pracownikow oraz gotowe pakiety onboardingowe.

## User & Persona

Primary persona: wlasciciel firmy albo osoba jednoosobowo odpowiedzialna za operacyjne IT w malej firmie.

Uzytkownik siega po produkt, gdy musi sprawdzic aktualny stan licencji lub sprzetu, przygotowac stanowisko dla nowego pracownika, przeprowadzic offboarding albo upewnic sie, ze zblizajaca sie subskrypcja nie zostanie przeoczona.

## Success Criteria

### Primary

- Wlasciciel widzi w jednym miejscu sprzet, licencje, przypisania pracownikow oraz zblizajace sie odnowienia.
- System wysyla skuteczne przypomnienie mailowe o zblizajacym sie koncu subskrypcji, zanim firma poniesie niechciany koszt odnowienia.

### Secondary

- Wlasciciel moze w kilka minut przygotowac pakiet onboardingowy dla nowego pracownika przy uzyciu prostego szablonu.
- Produkt pozostaje prosty: jedna firma, jeden wlasciciel, brak zaproszen i brak rozbudowanego RBAC w MVP.

### Guardrails

- MVP musi utrzymac zakres do przeplywu mozliwego do zbudowania w 3 tygodnie pracy after-hours.
- MVP nie moze wymagac integracji z zewnetrznymi systemami ani automatycznego wykrywania licencji lub sprzetu.
- Dane firm musza pozostac odseparowane miedzy organizacjami.
- MVP obsluguje jedna plaska role: `owner`.

## User Stories

### US-01: Wlasciciel zaklada konto firmy

- **Given** nowy wlasciciel bez konta firmy
- **When** zaklada konto i loguje sie przez email oraz haslo
- **Then** widzi pusta przestrzen danych swojej firmy

#### Acceptance Criteria

- Konto firmy jest oddzielone od danych innych firm.
- Tworca firmy otrzymuje role `owner`.

### US-02: Wlasciciel dodaje pracownika

- **Given** zalogowany wlasciciel
- **When** dodaje pracownika do kartoteki
- **Then** pracownik jest dostepny jako wlasciciel sprzetu i odbiorca przypisan licencji

#### Acceptance Criteria

- Pracownik moze byc uzyty w przypisaniach sprzetu i licencji.
- Pracownik jest widoczny tylko w ramach firmy.

### US-03: Wlasciciel dodaje sprzet

- **Given** zalogowany wlasciciel
- **When** tworzy wpis sprzetu z typem, modelem, numerem seryjnym i wlascicielem
- **Then** sprzet trafia do kartoteki firmy

#### Acceptance Criteria

- Sprzet mozna tworzyc, edytowac i usuwac.
- Numer seryjny pomaga odroznic egzemplarze sprzetu.

### US-04: Wlasciciel dodaje licencje

- **Given** zalogowany wlasciciel
- **When** tworzy wpis licencji z kosztem i data konca subskrypcji
- **Then** licencja jest widoczna w ewidencji i moze wejsc do alertow oraz dashboardu

#### Acceptance Criteria

- Licencje mozna tworzyc, edytowac i przegladac.
- Licencja moze miec koszt miesieczny albo roczny.

### US-05: Wlasciciel przypisuje zasoby

- **Given** istniejacy pracownik, sprzet i licencja
- **When** wlasciciel przypisuje sprzet lub licencje do pracownika albo urzadzenia
- **Then** system pokazuje aktualne powiazania zasobow

#### Acceptance Criteria

- Licencje mozna przypisac do uzytkownika lub konkretnego urzadzenia.
- Sprzet mozna przypisac do pracownika.

### US-06: Wlasciciel widzi dashboard

- **Given** wprowadzone licencje i sprzet
- **When** wlasciciel otwiera dashboard
- **Then** widzi laczny koszt subskrypcji oraz podstawowe podsumowanie sprzetu

#### Acceptance Criteria

- Dashboard pokazuje koszt miesieczny i roczny.
- Dashboard pokazuje podstawowe liczby sprzetu.

### US-07: System wysyla przypomnienie mailowe

- **Given** licencja ze zblizajaca sie data konca subskrypcji
- **When** termin odnowienia zbliza sie do ustalonego progu
- **Then** system wysyla email z przypomnieniem do wlasciciela firmy

#### Acceptance Criteria

- Przypomnienie dotyczy konkretnej licencji i jej daty odnowienia.
- Przypomnienia sa wysylane mailem, bez komunikatorow zespolowych.
- Odbiorca MVP jest jednoznaczny: email wlasciciela firmy.

### US-08: Wlasciciel tworzy i uzywa prostego szablonu onboardingowego

- **Given** firma ma powtarzalny pakiet zasobow dla nowej roli
- **When** wlasciciel tworzy szablon i uzywa go przy nowym pracowniku
- **Then** system pomaga szybko przypisac wymagany zestaw sprzetu i licencji

#### Acceptance Criteria

- Szablon jest recznie zdefiniowana lista zasobow lub licencji.
- Szablon nie wykonuje automatycznego nadawania dostepow w zewnetrznych systemach.

## Functional Requirements

### Accounts & Access

- FR-001: Owner can create a company account. Priority: must-have
- FR-002: Owner can sign in with email and password. Priority: must-have
- FR-003: System can isolate each company's data from other companies. Priority: must-have
- FR-004: System can enforce a single flat company role: owner. Priority: must-have

### People & Assets

- FR-005: Owner can create, edit, and view employee records. Priority: must-have
- FR-006: Owner can create, edit, delete, and view hardware records. Priority: must-have
- FR-007: Owner can create, edit, and view software license records. Priority: must-have
- FR-008: Owner can assign hardware to an employee. Priority: must-have
- FR-009: Owner can assign a license to an employee or a hardware device. Priority: must-have

### Renewals & Dashboard

- FR-010: System can track license renewal or subscription end dates. Priority: must-have
- FR-011: System can send email alerts for upcoming license renewals. Priority: must-have
- FR-012: Owner can view monthly and annual software subscription costs. Priority: must-have
- FR-013: Owner can view a basic hardware summary. Priority: must-have

### Onboarding Templates

- FR-014: Owner can create a simple onboarding template made of required hardware and licenses. Priority: must-have
- FR-015: Owner can apply an onboarding template to a new employee. Priority: must-have
- FR-016: System can keep onboarding templates manual and internal, without external provisioning. Priority: must-have

## Non-Functional Requirements

- Dane firm sa odseparowane: uzytkownik jednej firmy nigdy nie widzi danych innej firmy.
- Dashboard i listy zasobow odpowiadaja w czasie ponizej 2 sekund przy typowym uzyciu w malej firmie.
- Email alert dla licencji jest wyslany do wlasciciela firmy przed data odnowienia wedlug ustawionego progu.
- Aplikacja pozostaje uzywalna w aktualnych glownych wersjach przegladarek desktopowych.

## Business Logic

ITventory priorytetyzuje ryzyka operacyjne IT, zestawiajac daty odnowien licencji, koszty subskrypcji, przypisania pracownikow i szablony onboardingowe, aby wlasciciel wiedzial, ktore dzialania wymagaja uwagi jako pierwsze.

Regula korzysta z danych wprowadzonych przez uzytkownika: licencji, dat odnowien, kosztow, pracownikow, sprzetu, przypisan i szablonow onboardingowych. Wynikiem jest praktyczna kolejka uwagi: ktore licencje moga wygenerowac niechciany koszt, jakie zasoby sa przypisane do pracownika oraz jaki pakiet nalezy przygotowac przy onboardingu.

Uzytkownik spotyka te regule w dashboardzie, przypomnieniach mailowych, widoku przypisan oraz przy uzyciu szablonu onboardingowego.

## Access Control

Uzytkownicy loguja sie przez email i haslo.

Kazda firma ma osobna przestrzen danych. Licencje, sprzet, pracownicy, szablony onboardingowe, dashboard i przypisania sa widoczne tylko w ramach danej firmy.

Role MVP:

- `owner`: jedyna plaska rola firmy. Tworca firmy ma pelny dostep do wszystkich funkcji MVP w swojej firmie.

Nie ma zaproszen uzytkownikow, Menedzera, niestandardowego modelu uprawnien ani edytora rol w MVP.

## Non-Goals

- Brak zaproszen uzytkownikow do firmy w MVP.
- Brak roli Menedzera i granic Administrator/Menedzer.
- Brak automatycznego wykrywania licencji i sprzetu: MVP opiera sie na recznym wprowadzaniu danych.
- Brak integracji z zewnetrznymi systemami ewidencji, subskrypcji, chmury i pakietow biurowych: integracje nie sa wymagane do udowodnienia glownej wartosci.
- Brak agentow sieciowych do skanowania sprzetu: fizyczna i sieciowa inwentaryzacja pozostaje poza zakresem MVP.
- Brak niestandardowego modelu uprawnien: MVP obsluguje tylko jedna role `owner`.
- Brak wielowalutowosci: koszty sa obslugiwane w jednej glownej walucie firmy.
- Brak powiadomien w komunikatorach zespolowych: przypomnienia w MVP sa wysylane wylacznie mailem.
- Brak pelnego cyklu zycia sprzetu i serwisu: historia napraw, amortyzacja finansowa i fizyczne kody inwentaryzacyjne sa poza zakresem MVP.

## Open Questions

No open questions.
