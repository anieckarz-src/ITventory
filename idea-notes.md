# ITventory - MVP

### Główny problem
Manualne zarządzanie licencjami oprogramowania i sprzętem (hardware) w firmie jest problematyczne i nieefektywne. Łatwo zapomnieć o przedłużeniu subskrypcji, ciężko kontrolować stan licencji, a także brakuje jednego źródła prawdy o tym, jaki pracownik posiada dany sprzęt i przypisany do niego software.

### Najmniejszy zestaw funkcjonalności
- **Licencje:** Przeglądanie, edytowanie, tworzenie zapisu o licencji oraz wysyłanie alertów e-mail o zbliżającym się końcu subskrypcji.
- **Hardware:** Prosta kartoteka sprzętu (tworzenie, edycja, usuwanie) z polami: Typ (np. Laptop), Model, Numer Seryjny, Właściciel (Pracownik).
- **Powiązania:** Możliwość przypisania licencji do użytkownika lub konkretnego urządzenia.
- **Dashboard:** Łączny koszt subskrypcji oprogramowania (miesięczny/roczny) oraz podstawowe podsumowanie sprzętu.
- **Szablony onboardingowe:** Możliwość stworzenia zestawu startowego (np. Szablon "Developer" = Laptop + licencja JetBrains + licencja Slack). Umożliwia to szybkie przypisanie całego pakietu do nowego pracownika jednym kliknięciem.
- **Role (Prosty podział):** System obsługuje dwa poziomy dostępu:
  - *Administrator:* Pełny dostęp do systemu + możliwość zapraszania innych użytkowników do zarządzania aplikacją.
  - *Menedżer:* Możliwość zarządzania licencjami, sprzętem i szablonami, ale bez dostępu do ustawień konta firmy i zapraszania innych użytkowników.

### Co NIE wchodzi w zakres MVP
- **Automatyczne wykrywanie licencji i sprzętu:** Brak integracji z zewnętrznymi API (Microsoft 365, Google Workspace, AWS) oraz brak agentów sieciowych do skanowania sprzętu. Wszystko wprowadzane jest manualnie.
- **Zaawansowany system uprawnień (Custom RBAC):** Brak możliwości tworzenia własnych, niestandardowych ról lub edytowania pojedynczych uprawnień per użytkownik. Dostępne są tylko 2 sztywne role (Admin / Manager).
- **Wielowalutowość:** Dashboard i koszty obsługiwane są tylko w jednej, głównej walucie (np. PLN).
- **Integracje z komunikatorami:** Brak powiadomień na Slacku/Teamsach – wyłącznie komunikacja mailowa.
- **Cykl życia sprzętu i serwis:** Brak śledzenia historii napraw, amortyzacji finansowej sprzętu oraz generowania kodów QR do fizycznej inwentaryzacji.

### Kryteria sukcesu
- **Redukcja "zapomnianych" opłat:** Pierwsze skuteczne przypomnienie mailowe, które uchroniło firmę przed automatycznym odnowieniem niechcianej subskrypcji.
- **Błyskawiczny Offboarding/Onboarding:** Administrator lub Menedżer IT jest w stanie w jednym miejscu wygenerować pełną listę sprzętu i licencji przypisanych do pracownika w momencie jego odejścia, a przy zatrudnianiu – użyć szablonu, skracając czas przygotowania stanowiska do maksimum kilku minut.
- **Delegacja pracy:** Właściciel firmy (Admin) bez obaw zaprasza do systemu Menedżera, który samodzielnie uzupełnia bazę danych, odciążając Admina.