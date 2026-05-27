---
project: "ITventory"
context_type: greenfield
created: 2026-05-25
updated: 2026-05-25
checkpoint:
  current_phase: 8
  phases_completed: [1, 2, 3, 4, 5, 6, 7]
  gray_areas_resolved:
    - topic: "primary persona"
      decision: "Menedzer IT jako glowna persona MVP."
    - topic: "pain category"
      decision: "Coordination overhead plus missing single source of truth."
    - topic: "domain insight"
      decision: "Produkt ma przypominac i priorytetyzowac ryzyka: wygasajace licencje, przypisania pracownikow i zestawy onboardingowe."
    - topic: "auth strategy"
      decision: "Email plus haslo."
    - topic: "tenant isolation"
      decision: "Kazda firma ma osobna przestrzen danych."
    - topic: "manager invitation"
      decision: "Tylko Administrator moze zapraszac Menedzerow."
    - topic: "mvp flow"
      decision: "MVP flow obejmuje konto firmy, zaproszenie Menedzera, pracownika, sprzet, licencje, przypisania, dashboard, mailowe przypomnienia i proste szablony onboardingowe."
    - topic: "timeline budget"
      decision: "Pierwsza wersja: 3 tygodnie after-hours."
    - topic: "mvp requirements priority"
      decision: "Wszystkie dziewiec historii MVP sa must-have; szablony onboardingowe zostaja w prostym zakresie bez provisioning'u i integracji."
    - topic: "business logic"
      decision: "ITventory priorytetyzuje ryzyka operacyjne IT, zestawiajac daty odnowien licencji, koszty subskrypcji, przypisania pracownikow i szablony onboardingowe."
    - topic: "non-functional requirements"
      decision: "Izolacja firm, akceptowalna responsywnosc dla malej firmy, terminowe alerty mailowe i wsparcie aktualnych przegladarek desktopowych."
    - topic: "product framing"
      decision: "Produkt typu web app, skala small, brak twardego deadline, praca after-hours."
    - topic: "non-goals"
      decision: "Brak automatycznego wykrywania, integracji zewnetrznych, agentow sieciowych, custom RBAC, wielowalutowosci, Slack/Teams notifications oraz cyklu zycia sprzetu."
  frs_drafted: 17
  quality_check_status: accepted
---

# Shape Notes

## Seed Idea

Source: `idea-notes.md`

```markdown
# ITventory - MVP

### Glowny problem
Manualne zarzadzanie licencjami oprogramowania i sprzetem (hardware) w firmie jest problematyczne i nieefektywne. Latwo zapomniec o przedluzeniu subskrypcji, ciezko kontrolowac stan licencji, a takze brakuje jednego zrodla prawdy o tym, jaki pracownik posiada dany sprzet i przypisany do niego software.

### Najmniejszy zestaw funkcjonalnosci
- Licencje: Przegladanie, edytowanie, tworzenie zapisu o licencji oraz wysylanie alertow e-mail o zblizajacym sie koncu subskrypcji.
- Hardware: Prosta kartoteka sprzetu (tworzenie, edycja, usuwanie) z polami: Typ (np. Laptop), Model, Numer Seryjny, Wlasciciel (Pracownik).
- Powiazania: Mozliwosc przypisania licencji do uzytkownika lub konkretnego urzadzenia.
- Dashboard: Laczny koszt subskrypcji oprogramowania (miesieczny/roczny) oraz podstawowe podsumowanie sprzetu.
- Szablony onboardingowe: Mozliwosc stworzenia zestawu startowego (np. Szablon "Developer" = Laptop + licencja JetBrains + licencja Slack). Umozliwia to szybkie przypisanie calego pakietu do nowego pracownika jednym kliknieciem.
- Role (Prosty podzial): System obsluguje dwa poziomy dostepu:
  - Administrator: Pelny dostep do systemu + mozliwosc zapraszania innych uzytkownikow do zarzadzania aplikacja.
  - Menedzer: Mozliwosc zarzadzania licencjami, sprzetem i szablonami, ale bez dostepu do ustawien konta firmy i zapraszania innych uzytkownikow.

### Co NIE wchodzi w zakres MVP
- Automatyczne wykrywanie licencji i sprzetu: Brak integracji z zewnetrznymi API (Microsoft 365, Google Workspace, AWS) oraz brak agentow sieciowych do skanowania sprzetu. Wszystko wprowadzane jest manualnie.
- Zaawansowany system uprawnien (Custom RBAC): Brak mozliwosci tworzenia wlasnych, niestandardowych rol lub edytowania pojedynczych uprawnien per uzytkownik. Dostepne sa tylko 2 sztywne role (Admin / Manager).
- Wielowalutowosc: Dashboard i koszty obslugiwane sa tylko w jednej, glownej walucie (np. PLN).
- Integracje z komunikatorami: Brak powiadomien na Slacku/Teamsach - wylacznie komunikacja mailowa.
- Cykl zycia sprzetu i serwis: Brak sledzenia historii napraw, amortyzacji finansowej sprzetu oraz generowania kodow QR do fizycznej inwentaryzacji.

### Kryteria sukcesu
- Redukcja "zapomnianych" oplat: Pierwsze skuteczne przypomnienie mailowe, ktore uchronilo firme przed automatycznym odnowieniem niechcianej subskrypcji.
- Blyskawiczny Offboarding/Onboarding: Administrator lub Menedzer IT jest w stanie w jednym miejscu wygenerowac pelna liste sprzetu i licencji przypisanych do pracownika w momencie jego odejscia, a przy zatrudnianiu - uzyc szablonu, skracajac czas przygotowania stanowiska do maksimum kilku minut.
- Delegacja pracy: Wlasciciel firmy (Admin) bez obaw zaprasza do systemu Menedzera, ktory samodzielnie uzupelnia baze danych, odciazajac Admina.
```

## Vision & Problem Statement

Manualne zarzadzanie licencjami oprogramowania i sprzetem firmowym tworzy kosztowny narzut koordynacyjny dla osoby odpowiedzialnej za IT. Bol pojawia sie szczegolnie przy konczacych sie subskrypcjach, onboardingu, offboardingu oraz przy pytaniu "kto ma jaki sprzet i software".

Status quo nie daje jednego zrodla prawdy ani mechanizmu priorytetyzacji ryzyk. ITventory ma laczyc ewidencje zasobow z przypomnieniami i praktycznymi zestawami operacyjnymi, tak aby Menedzer IT szybciej widzial wygasajace licencje, przypisania pracownikow oraz gotowe pakiety onboardingowe.

## User & Persona

Primary persona: Menedzer IT, czyli osoba operacyjnie zarzadzajaca sprzetem, licencjami i przypisaniami pracownikow w firmie.

Menedzer IT siega po produkt, gdy musi sprawdzic aktualny stan licencji lub sprzetu, przygotowac stanowisko dla nowego pracownika, przeprowadzic offboarding albo upewnic sie, ze zblizajaca sie subskrypcja nie zostanie przeoczona.

## Access Control

Uzytkownicy loguja sie przez email i haslo.

Kazda firma ma osobna przestrzen danych. Licencje, sprzet, pracownicy, szablony onboardingowe, dashboard i przypisania sa widoczne tylko w ramach danej firmy.

Role MVP:

- Administrator moze zarzadzac licencjami, sprzetem, szablonami, przypisaniami, ustawieniami konta firmy oraz zapraszac innych uzytkownikow do zarzadzania aplikacja.
- Menedzer moze zarzadzac licencjami, sprzetem, szablonami i przypisaniami, ale nie ma dostepu do ustawien konta firmy i nie moze zapraszac innych uzytkownikow.
- Tylko Administrator moze zaprosic Menedzera.

Nie ma custom RBAC w MVP. System obsluguje tylko dwie sztywne role: Administrator i Menedzer.

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

## MVP Flow

1. Administrator zaklada konto firmy.
2. Administrator zaprasza Menedzera IT.
3. Menedzer dodaje pracownika.
4. Menedzer dodaje sprzet i licencje.
5. Menedzer przypisuje sprzet i licencje do pracownika lub urzadzenia.
6. Menedzer widzi dashboard kosztow i zblizajace sie odnowienia.
7. System wysyla mailowe przypomnienie o wygasajacej licencji.
8. Menedzer uzywa prostego szablonu onboardingowego, zeby przypisac pakiet zasobow pracownikowi.

## Timeline Budget

- MVP weeks: 3
- Hard deadline: null
- After-hours only: true

## User Stories

### US-01: Administrator zaklada konto firmy

- Given nowy Administrator bez konta firmy
- When zaklada konto i loguje sie przez email oraz haslo
- Then widzi pusta przestrzen danych swojej firmy

#### Acceptance Criteria

- Konto firmy jest oddzielone od danych innych firm.
- Administrator otrzymuje role z pelnym dostepem.

### US-02: Administrator zaprasza Menedzera

- Given zalogowany Administrator
- When zaprasza uzytkownika jako Menedzera
- Then zaproszona osoba moze dolaczyc do tej samej firmy z rola Menedzer

#### Acceptance Criteria

- Menedzer nie ma dostepu do ustawien konta firmy.
- Menedzer nie moze zapraszac innych uzytkownikow.

### US-03: Menedzer dodaje pracownika

- Given zalogowany Menedzer
- When dodaje pracownika do kartoteki
- Then pracownik jest dostepny jako wlasciciel sprzetu i odbiorca przypisan licencji

#### Acceptance Criteria

- Pracownik moze byc uzyty w przypisaniach sprzetu i licencji.
- Pracownik jest widoczny tylko w ramach firmy.

### US-04: Menedzer dodaje sprzet

- Given zalogowany Menedzer
- When tworzy wpis sprzetu z typem, modelem, numerem seryjnym i wlascicielem
- Then sprzet trafia do kartoteki firmy

#### Acceptance Criteria

- Sprzet mozna tworzyc, edytowac i usuwac.
- Numer seryjny pomaga odroznic egzemplarze sprzetu.

### US-05: Menedzer dodaje licencje

- Given zalogowany Menedzer
- When tworzy wpis licencji z kosztem i data konca subskrypcji
- Then licencja jest widoczna w ewidencji i moze wejsc do alertow oraz dashboardu

#### Acceptance Criteria

- Licencje mozna tworzyc, edytowac i przegladac.
- Licencja moze miec koszt miesieczny albo roczny.

### US-06: Menedzer przypisuje zasoby

- Given istniejacy pracownik, sprzet i licencja
- When Menedzer przypisuje sprzet lub licencje do pracownika albo urzadzenia
- Then system pokazuje aktualne powiazania zasobow

#### Acceptance Criteria

- Licencje mozna przypisac do uzytkownika lub konkretnego urzadzenia.
- Sprzet mozna przypisac do pracownika.

### US-07: Menedzer widzi dashboard

- Given wprowadzone licencje i sprzet
- When Menedzer otwiera dashboard
- Then widzi laczny koszt subskrypcji oraz podstawowe podsumowanie sprzetu

#### Acceptance Criteria

- Dashboard pokazuje koszt miesieczny i roczny.
- Dashboard pokazuje podstawowe liczby sprzetu.

### US-08: System wysyla przypomnienie mailowe

- Given licencja ze zblizajaca sie data konca subskrypcji
- When termin odnowienia zbliza sie do ustalonego progu
- Then system wysyla email z przypomnieniem do wlasciwej osoby w firmie

#### Acceptance Criteria

- Przypomnienie dotyczy konkretnej licencji i jej daty odnowienia.
- Przypomnienia sa wysylane mailem, bez Slacka i Teams.

### US-09: Menedzer tworzy i uzywa prostego szablonu onboardingowego

- Given firma ma powtarzalny pakiet zasobow dla nowej roli
- When Menedzer tworzy szablon i uzywa go przy nowym pracowniku
- Then system pomaga szybko przypisac wymagany zestaw sprzetu i licencji

#### Acceptance Criteria

- Szablon jest recznie zdefiniowana lista zasobow lub licencji.
- Szablon nie wykonuje automatycznego provisioning'u w zewnetrznych systemach.

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

## Business Logic

ITventory priorytetyzuje ryzyka operacyjne IT, zestawiajac daty odnowien licencji, koszty subskrypcji, przypisania pracownikow i szablony onboardingowe, aby Menedzer IT wiedzial, ktore dzialania wymagaja uwagi jako pierwsze.

Regula korzysta z danych wprowadzonych przez uzytkownika: licencji, dat odnowien, kosztow, pracownikow, sprzetu, przypisan i szablonow onboardingowych. Wynikiem jest praktyczna kolejka uwagi: ktore licencje moga wygenerowac niechciany koszt, jakie zasoby sa przypisane do pracownika oraz jaki pakiet nalezy przygotowac przy onboardingu.

Uzytkownik spotyka te regule w dashboardzie, przypomnieniach mailowych, widoku przypisan oraz przy uzyciu szablonu onboardingowego.

## Non-Functional Requirements

- Dane firm sa odseparowane: uzytkownik jednej firmy nigdy nie widzi danych innej firmy.
- Dashboard i listy zasobow odpowiadaja w czasie ponizej 2 sekund przy typowym uzyciu w malej firmie.
- Email alert dla licencji jest wyslany przed data odnowienia wedlug ustawionego progu.
- Aplikacja pozostaje uzywalna w aktualnych glownych wersjach przegladarek desktopowych.

## Product Framing

- Product type: web-app
- Target scale:
  - users: small
  - qps: low
  - data_volume: small
- Timeline budget:
  - mvp_weeks: 3
  - hard_deadline: null
  - after_hours_only: true

Scale note: przy 100x wiekszej skali regula priorytetyzacji ryzyk musialaby silniej grupowac alerty, odrozniac pilne od informacyjnych i unikac zalewania Menedzera IT powiadomieniami.

## Non-Goals

- Brak automatycznego wykrywania licencji i sprzetu: MVP opiera sie na recznym wprowadzaniu danych.
- Brak integracji z Microsoft 365, Google Workspace, AWS i podobnymi systemami: integracje nie sa wymagane do udowodnienia glownej wartosci.
- Brak agentow sieciowych do skanowania sprzetu: fizyczna i sieciowa inwentaryzacja pozostaje poza zakresem MVP.
- Brak custom RBAC: MVP obsluguje tylko dwie sztywne role, Administrator i Menedzer.
- Brak wielowalutowosci: koszty sa obslugiwane w jednej glownej walucie firmy.
- Brak powiadomien Slack/Teams: przypomnienia w MVP sa wysylane wylacznie mailem.
- Brak pelnego cyklu zycia sprzetu i serwisu: historia napraw, amortyzacja finansowa i QR inventory sa poza zakresem MVP.

## Quality Cross-Check

- Access Control: present.
- Business Logic: present.
- Project artifacts: present.
- Timeline-cost acknowledgement: present.
- Non-Goals: present.
- Preserved behavior: n/a for greenfield.
