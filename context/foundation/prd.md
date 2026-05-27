---
project: "ITventory"
version: 1
status: draft
created: 2026-05-25
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

Status quo nie daje jednego zrodla prawdy ani mechanizmu priorytetyzacji ryzyk. ITventory ma laczyc ewidencje zasobow z przypomnieniami i praktycznymi zestawami operacyjnymi, tak aby Menedzer IT szybciej widzial wygasajace licencje, przypisania pracownikow oraz gotowe pakiety onboardingowe.

## User & Persona

Primary persona: Menedzer IT, czyli osoba operacyjnie zarzadzajaca sprzetem, licencjami i przypisaniami pracownikow w firmie.

Menedzer IT siega po produkt, gdy musi sprawdzic aktualny stan licencji lub sprzetu, przygotowac stanowisko dla nowego pracownika, przeprowadzic offboarding albo upewnic sie, ze zblizajaca sie subskrypcja nie zostanie przeoczona.

## Success Criteria

### Primary

- Menedzer IT widzi w jednym miejscu sprzet, licencje, przypisania pracownikow oraz zblizajace sie odnowienia.
- System wysyla skuteczne przypomnienie mailowe o zblizajacym sie koncu subskrypcji, zanim firma poniesie niechciany koszt odnowienia.

### Secondary

- Administrator lub Menedzer IT moze w kilka minut przygotowac pakiet onboardingowy dla nowego pracownika przy uzyciu prostego szablonu.
- Administrator moze bezpiecznie delegowac uzupelnianie danych Menedzerowi bez dawania mu dostepu do ustawien firmy i zapraszania uzytkownikow.

### Guardrails

- MVP musi utrzymac zakres do przeplywu mozliwego do zbudowania w 3 tygodnie pracy after-hours.
- MVP nie moze wymagac integracji z zewnetrznymi systemami ani automatycznego wykrywania licencji lub sprzetu.
- Dane firm musza pozostac odseparowane miedzy organizacjami.

## User Stories

### US-01: Administrator zaklada konto firmy

- **Given** nowy Administrator bez konta firmy
- **When** zaklada konto i loguje sie przez email oraz haslo
- **Then** widzi pusta przestrzen danych swojej firmy

#### Acceptance Criteria

- Konto firmy jest oddzielone od danych innych firm.
- Administrator otrzymuje role z pelnym dostepem.

### US-02: Administrator zaprasza Menedzera

- **Given** zalogowany Administrator
- **When** zaprasza uzytkownika jako Menedzera
- **Then** zaproszona osoba moze dolaczyc do tej samej firmy z rola Menedzer

#### Acceptance Criteria

- Menedzer nie ma dostepu do ustawien konta firmy.
- Menedzer nie moze zapraszac innych uzytkownikow.

### US-03: Menedzer dodaje pracownika

- **Given** zalogowany Menedzer
- **When** dodaje pracownika do kartoteki
- **Then** pracownik jest dostepny jako wlasciciel sprzetu i odbiorca przypisan licencji

#### Acceptance Criteria

- Pracownik moze byc uzyty w przypisaniach sprzetu i licencji.
- Pracownik jest widoczny tylko w ramach firmy.

### US-04: Menedzer dodaje sprzet

- **Given** zalogowany Menedzer
- **When** tworzy wpis sprzetu z typem, modelem, numerem seryjnym i wlascicielem
- **Then** sprzet trafia do kartoteki firmy

#### Acceptance Criteria

- Sprzet mozna tworzyc, edytowac i usuwac.
- Numer seryjny pomaga odroznic egzemplarze sprzetu.

### US-05: Menedzer dodaje licencje

- **Given** zalogowany Menedzer
- **When** tworzy wpis licencji z kosztem i data konca subskrypcji
- **Then** licencja jest widoczna w ewidencji i moze wejsc do alertow oraz dashboardu

#### Acceptance Criteria

- Licencje mozna tworzyc, edytowac i przegladac.
- Licencja moze miec koszt miesieczny albo roczny.

### US-06: Menedzer przypisuje zasoby

- **Given** istniejacy pracownik, sprzet i licencja
- **When** Menedzer przypisuje sprzet lub licencje do pracownika albo urzadzenia
- **Then** system pokazuje aktualne powiazania zasobow

#### Acceptance Criteria

- Licencje mozna przypisac do uzytkownika lub konkretnego urzadzenia.
- Sprzet mozna przypisac do pracownika.

### US-07: Menedzer widzi dashboard

- **Given** wprowadzone licencje i sprzet
- **When** Menedzer otwiera dashboard
- **Then** widzi laczny koszt subskrypcji oraz podstawowe podsumowanie sprzetu

#### Acceptance Criteria

- Dashboard pokazuje koszt miesieczny i roczny.
- Dashboard pokazuje podstawowe liczby sprzetu.

### US-08: System wysyla przypomnienie mailowe

- **Given** licencja ze zblizajaca sie data konca subskrypcji
- **When** termin odnowienia zbliza sie do ustalonego progu
- **Then** system wysyla email z przypomnieniem do wlasciwej osoby w firmie

#### Acceptance Criteria

- Przypomnienie dotyczy konkretnej licencji i jej daty odnowienia.
- Przypomnienia sa wysylane mailem, bez komunikatorow zespolowych.

### US-09: Menedzer tworzy i uzywa prostego szablonu onboardingowego

- **Given** firma ma powtarzalny pakiet zasobow dla nowej roli
- **When** Menedzer tworzy szablon i uzywa go przy nowym pracowniku
- **Then** system pomaga szybko przypisac wymagany zestaw sprzetu i licencji

#### Acceptance Criteria

- Szablon jest recznie zdefiniowana lista zasobow lub licencji.
- Szablon nie wykonuje automatycznego nadawania dostepow w zewnetrznych systemach.

## Functional Requirements

### Accounts & Access

- FR-001: Administrator can create a company account. Priority: must-have
- FR-002: User can sign in with email and password. Priority: must-have
- FR-003: Administrator can invite a Manager to the company workspace. Priority: must-have
- FR-004: System can isolate each company's data from other companies. Priority: must-have
- FR-005: System can enforce the Administrator and Manager role boundaries. Priority: must-have

### People & Assets

- FR-006: Manager can create, edit, and view employee records. Priority: must-have
- FR-007: Manager can create, edit, delete, and view hardware records. Priority: must-have
- FR-008: Manager can create, edit, and view software license records. Priority: must-have
- FR-009: Manager can assign hardware to an employee. Priority: must-have
- FR-010: Manager can assign a license to an employee or a hardware device. Priority: must-have

### Renewals & Dashboard

- FR-011: System can track license renewal or subscription end dates. Priority: must-have
- FR-012: System can send email alerts for upcoming license renewals. Priority: must-have
- FR-013: Manager can view monthly and annual software subscription costs. Priority: must-have
- FR-014: Manager can view a basic hardware summary. Priority: must-have

### Onboarding Templates

- FR-015: Manager can create a simple onboarding template made of required hardware and licenses. Priority: must-have
- FR-016: Manager can apply an onboarding template to a new employee. Priority: must-have
- FR-017: System can keep onboarding templates manual and internal, without external provisioning. Priority: must-have

## Non-Functional Requirements

- Dane firm sa odseparowane: uzytkownik jednej firmy nigdy nie widzi danych innej firmy.
- Dashboard i listy zasobow odpowiadaja w czasie ponizej 2 sekund przy typowym uzyciu w malej firmie.
- Email alert dla licencji jest wyslany przed data odnowienia wedlug ustawionego progu.
- Aplikacja pozostaje uzywalna w aktualnych glownych wersjach przegladarek desktopowych.

## Business Logic

ITventory priorytetyzuje ryzyka operacyjne IT, zestawiajac daty odnowien licencji, koszty subskrypcji, przypisania pracownikow i szablony onboardingowe, aby Menedzer IT wiedzial, ktore dzialania wymagaja uwagi jako pierwsze.

Regula korzysta z danych wprowadzonych przez uzytkownika: licencji, dat odnowien, kosztow, pracownikow, sprzetu, przypisan i szablonow onboardingowych. Wynikiem jest praktyczna kolejka uwagi: ktore licencje moga wygenerowac niechciany koszt, jakie zasoby sa przypisane do pracownika oraz jaki pakiet nalezy przygotowac przy onboardingu.

Uzytkownik spotyka te regule w dashboardzie, przypomnieniach mailowych, widoku przypisan oraz przy uzyciu szablonu onboardingowego.

## Access Control

Uzytkownicy loguja sie przez email i haslo.

Kazda firma ma osobna przestrzen danych. Licencje, sprzet, pracownicy, szablony onboardingowe, dashboard i przypisania sa widoczne tylko w ramach danej firmy.

Role MVP:

- Administrator moze zarzadzac licencjami, sprzetem, szablonami, przypisaniami, ustawieniami konta firmy oraz zapraszac innych uzytkownikow do zarzadzania aplikacja.
- Menedzer moze zarzadzac licencjami, sprzetem, szablonami i przypisaniami, ale nie ma dostepu do ustawien konta firmy i nie moze zapraszac innych uzytkownikow.
- Tylko Administrator moze zaprosic Menedzera.

Nie ma niestandardowego modelu uprawnien w MVP. System obsluguje tylko dwie sztywne role: Administrator i Menedzer.

## Non-Goals

- Brak automatycznego wykrywania licencji i sprzetu: MVP opiera sie na recznym wprowadzaniu danych.
- Brak integracji z zewnetrznymi systemami ewidencji, subskrypcji, chmury i pakietow biurowych: integracje nie sa wymagane do udowodnienia glownej wartosci.
- Brak agentow sieciowych do skanowania sprzetu: fizyczna i sieciowa inwentaryzacja pozostaje poza zakresem MVP.
- Brak niestandardowego modelu uprawnien: MVP obsluguje tylko dwie sztywne role, Administrator i Menedzer.
- Brak wielowalutowosci: koszty sa obslugiwane w jednej glownej walucie firmy.
- Brak powiadomien w komunikatorach zespolowych: przypomnienia w MVP sa wysylane wylacznie mailem.
- Brak pelnego cyklu zycia sprzetu i serwisu: historia napraw, amortyzacja finansowa i fizyczne kody inwentaryzacyjne sa poza zakresem MVP.

## Open Questions

No open questions.
