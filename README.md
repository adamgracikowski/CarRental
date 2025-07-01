# 🚗 Car Rental System

<p align="center">
  <img src="https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=.net&logoColor=white" alt="ASP.NET Core"/>
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#"/>
  <img src="https://img.shields.io/badge/ORM-EF%20Core-512BD4?style=for-the-badge&logo=code&logoColor=white" alt="EF Core"/>
  <img src="https://img.shields.io/badge/Database-SQL%20Server-CC2927?style=for-the-badge&logo=database&logoColor=white" alt="SQL Server"/>
  <img src="https://img.shields.io/badge/Cloud-Microsoft%20Azure-0089D6?style=for-the-badge&logo=cloud&logoColor=white" alt="Microsoft Azure"/>
  <img src="https://img.shields.io/badge/SendGrid-FF6C37?style=for-the-badge&logo=sendgrid&logoColor=white" alt="SendGrid"/>
  <img src="https://img.shields.io/badge/Blazor_WASM-512BD4?style=for-the-badge&logo=blazor&logoColor=white" alt="Blazor WebAssembly"/>
  <img src="https://img.shields.io/badge/Google_Maps-4285F4?style=for-the-badge&logo=google-maps&logoColor=white" alt="Google Maps"/>
</p>

**CarRental** is a full-stack car rental platform that aggregates rental offers from multiple third-party API providers 🚀. It features a price comparison tool, allowing users to find the best deals.

The project was developed as part of the _Web Application Development with .NET_ course during the winter semester of the 2024/2025 academic year.

## 📘 Table of Contents

1. [📖 Overview](#-overview)
2. [🎯 Features](#-features)
3. [🚀 Technologies Used](#-technologies-used)
4. [🏗️ Solution Architecture](#-solution-architecture)
5. [☁️ Cloud Services Architecture](#-cloud-services-architecture)
6. [🗄️ Database Schema](#-database-schema)
7. [🧠 Key Patterns & Technologies](#-key-patterns--technologies)
8. [🧪 Unit Testing](#-unit-testing)
9. [📘 API Documentation](#-api-documentation)
10. [📦 Installation](#-installation)
11. [👥 Authors](#-authors)

## 📖 Overview

The goal of this project is to build a comprehensive 🚗 **Car Rental System** composed of two main components:

- 🌐 **Web Application**  
  Allows users to browse, filter, and compare rental prices from multiple car providers in one place.

- 🔌 **Car Provider API**  
  Integrates with external vehicle‑rental services to manage available cars, reservations, and rental details.

The system is designed for **flexibility** and **extensibility**:

- ➕ New car providers can be added at any time via dedicated APIs, ensuring smooth **scalability**.
- 🤝 Supports collaboration with other teams offering their own provider APIs, opening the door to future feature enhancements.

## 🎯 Features

## 🚀 Technologies Used

## 🏗️ Solution Architecture

The solution consists of **11** modular projects, organized to ensure:

- ✔️ Clear code structure  
- 🔧 Easy maintenance  
- 🚀 Expandability without breaking existing functionality  

Below is an overview of the logical layers and their responsibilities:

| 🔨 Layer         | 📝 Description                                                                                     |
|------------------|---------------------------------------------------------------------------------------------------|
| **Core**         | 📦 Domain models and enums for car providers and the rental price comparer.                       |
| **Persistence**  | 💾 Data access via **Entity Framework Core** and the repository pattern.                          |
| **Infrastructure** | 🔗 Business logic and integrations with external services (e.g. **Azure Cache for Redis**, **Azure Blob Storage**, **Twilio SendGrid**). |
| **API**          | 🌐 Exposes REST endpoints, handles authentication & authorization, and validates requests.         |
| **Web**          | 🎨 Frontend built with **Blazor WebAssembly**, hosted as a static app on **Azure Static Web Apps**. |
| **Tests**        | 🧪 Unit tests to verify correctness of business logic and data access layers.                     |


## ☁️ Cloud Services Architecture

## 🗄️ Database Schema

## 🧠 Key Patterns & Technologies

## 🧪 Unit Testing

## 📘 API Documentation

## 📦 Installation

## 👥 Authors



Diagram przedstawia strukturę podziału na projekty w ramach rozwiązania:

<p align="center">
  <img src="./CarRental.Docs/Diagrams/SolutionArchitecture/CarRental.png" 
       alt="Diagram projektów w solucji" 
       style="width: 80%;"/>
</p>

## Architektura Usług Chmurowych

System został wdrożony w środowisku **Microsoft Azure**, co zapewnia:

- Skalowalność,
- Wysoki poziom bezpieczeństwa,
- Łatwość utrzymania i monitorowania zasobów.

### Diagram Architektury

Poniżej przedstawiono diagram prezentujący architekturę usług chmurowych:

<p align="center"> 
   <img src="./CarRental.Docs/Diagrams/AzureArchitecture/azure-architecture.png" 
        alt="Diagram Architektury Usług Chmurowych" 
        style="width: 90%;"/> 
</p>

### Podział Zasobów

Zasoby systemu zostały podzielone na trzy grupy zasobów (**Resource Groups**), aby zapewnić przejrzystość i separację odpowiedzialności.

1. `carrental-provider-prod-rg`: Zasoby obsługujące API dostawcy samochodów.

   - `carrental-provider` (**App Service**): Usługa hostująca API dla operacji CRUD na pojazdach, ofertach i rezerwacjach.
   - `carrental-provider-kv` (**Key Vault**): Bezpieczne przechowywanie kluczy, haseł oraz innych tajnych danych.
   - `carrental-provider-ai` (**Application Insights**): Monitorowanie i diagnostyka działania aplikacji w czasie rzeczywistym.
   - `CarRentalProviderDb` (**SQL Database**): Relacyjna baza danych przechowująca informacje potrzebne dostawcy samochodów.

2. `carrental-comparer-prod-rg`: Zasoby obsługujące API porównywarki cen, obsługujące poszczególne wypożyczalnie samochodów.

   - `carrental-comparer` (**App Service**): Usługa hostująca API odpowiedzialne za porównywanie ofert.
   - `carrental-comparer-kv` (**Key Vault**): Bezpieczne przechowywanie kluczy, haseł oraz innych tajnych danych.
   - `carrental-comparer-ai` (**Application Insights**): Monitorowanie działania aplikacji i zbieranie metryk diagnostycznych.
   - `CarRentalComparerDb` (**SQL Database**): Relacyjna baza danych obsługująca funkcjonalności porównywania ofert.
   - `carrental-comparer-web` (**Static Apps**): Hostowanie aplikacji frontowej, która udostępnia interfejs użytkownika końcowego.

3. `carrental-common-prod-rg`: Zadoby wspólne dla obu części rozwiązania, takie jak przechowywanie plików oraz pamięć podręczna.
   - `carrentalminisa` (**Blob Storage**): Przechowywanie plików statycznych, takich jak zdjęcia pojazdów oraz logo marek samochodów.
   - `carrental-cache` (**Azure Cache for Redis**): Pamięć podręczna wykorzystywana do przyspieszenia operacji odczytu, np. przechowywanie wyników wyszukiwania.

## Schemat Bazy Danych

System wykorzystuje **Entity Framework Core** do zarządzania bazą danych przy użyciu podejścia _code-first_.
Dzięki temu struktura baz danych jest generowana na podstawie klas modelowych w kodzie, co ułatwia utrzymanie spójności między aplikacją a bazą danych.

W środowisku developerskim używano **Microsoft SQL Server** uruchomionego lokalnie (`localhost`), natomiast w środowisku produkcyjnym wdrożono rozwiązanie oparte na dwóch instancjach **Azure SQL Database** – jednej dla dostawcy samochodów i jednej dla porównywarki cen.

Poniżej przedstawiono diagramy ilustrujące strukturę baz danych, relacje między tabelami oraz kluczowe atrybuty.

### Diagram Bazy Danych Dostawcy Samochodów:

<p align="center">
  <img src="./CarRental.Docs/Diagrams/Databases/carrental-provider-db.png" 
       alt="Diagram Bazy Danych Dostawcy Samochodów" 
       style="width: 80%;"/>
</p>

### Diagram Bazy Danych Porównywarki Cen:

<p align="center">
  <img src="./CarRental.Docs/Diagrams/Databases/carrental-comparer-db.png" 
       alt="Diagram Bazy Danych Porównywarki Cen" 
       style="width: 80%;"/>
</p>

## Kluczowe Wzorce i Technologie

Aby zapewnić modularność i skalowalność, wykorzystano m.in. następujące wzorce projektowe i biblioteki:

### Wzorzec Mediatora (Mediator Pattern)

Za pomocą pakietu **MediatR** zaimplementowano mechanizm pośredniczący w komunikacji między komponentami systemu. Dzięki temu:

- Zredukowano zależności między modułami,
- Umożliwiono centralizację logiki obsługi zapytań i poleceń.

### Wzorzec CQRS (Command Query Responsibility Segregation)

Zastosowano wzorzec CQRS, który dzieli operacje na:

- **Commands** (polecenia): Odpowiadają za operacje modyfikujące stan systemu, takie jak generowanie ofert, tworzenie i zwrot wypożyczenia czy aktualizacja danych użytkowników.
- **Queries** (zapytania): Służą do odczytu danych, np. wyszukiwanie dostępnych pojazdów czy pobieranie szczegółów wypożyczenia.

Taki podział upraszcza zarządzanie logiką i zwiększa przejrzystość kodu.

### Wzorzec Repozytorium i Specyfikacji (Repository and Specification Patterns)

Dzięki bibliotece **Ardalis.Specification** wdrożono:

- **Wzorzec repozytorium**: Abstrakcję dla operacji na danych, która pozwala oddzielić logikę biznesową od szczegółów implementacji dostępu do danych.
- **Specyfikacje**: Określenie precyzyjnych kryteriów zapytań w formie zdefiniowanych klas. Pozwala to na wielokrotne użycie tych samych reguł w różnych częściach systemu.

### Wzorzec Wynikowy (Result Pattern)

W celu ustandaryzowania obsługi wyników operacji, zarówno pomyślnych, jak i zakończonych błędami, zastosowano bibliotekę **Ardalis.Result**.
Wzorzec ten pozwala na zwracanie obiektów, które:

- Zawierają informację o sukcesie bądź błędzie operacji.
- Dostarczają szczegóły błędów, np. w postaci kodów błędów lub komunikatów.
- Upraszczają obsługę wyjątków i komunikację między warstwami systemu.

Dzięki **Ardalis.Result** ograniczono użycie wyjątków w logice biznesowej, co poprawiło czytelność kodu i ułatwiło testowanie operacji.

### Logowanie Użytkowników

Do obsługi logowania użytkowników w systemie wykorzystano **Microsoft Entra ID**. Dzięki temu:

- Użytkownicy mogą logować się za pomocą uwierzytelnienia jednokrotnego.
- System integruje się z zewnętrznymi usługami, co pozwala na bezpieczne zarządzanie dostępem do zasobów.

#### Zastosowanie Ról w Systemie

Komponenty systemu oraz dostępne endpointy zostały przystosowane do pracy z użytkownikami o różnych rolach.
Wprowadzono dwie główne role:

- **Pracownik** (`Employee`): Osoba odpowiedzialna za zarządzanie samochodami, rezerwacjami, oraz administrację systemem.
- **Użytkownik** (`User`): Klient, który korzysta z aplikacji do przeglądania dostępnych pojazdów, porównywania ofert wypożyczalni oraz wypożyczania samochodów.

Każda z ról ma przypisane konkretne uprawnienia, co umożliwia segregację funkcji oraz zapewnia odpowiedni poziom dostępu w zależności od charakteru użytkownika.

### Wykonywanie Zadań w Tle

Wykorzystano bibliotekę **Hangfire** do obsługi zadań wykonywanych asynchronicznie w tle, takich jak:

- Oznaczanie oferty jako przeterminowanej po upływie czasu określonego przez reguły biznesowe.
- Sprawdzanie statusu wypozyczenia.

**Hangfire** umożliwia:

- Łatwą konfigurację harmonogramów zadań,
- Wizualizację i monitorowanie zadań w dashboardzie webowym,
- Skalowalność dzięki integracji z Azure oraz innymi platformami chmurowymi.

### Walidacja Danych i Zapytań

W projekcie zastosowano **FluentValidation**, który umożliwia zdefiniowanie zaawansowanych reguł walidacji dla obiektów. Dzięki temu:

- Każda operacja biznesowa jest poprzedzona dokładną weryfikacją poprawności danych wejściowych,
- Walidacja jest przeprowadzana w sposób deklaratywny, co zwiększa czytelność kodu i ułatwia jej modyfikację.

### Mapowanie Obiektów

Do mapowania obiektów wykorzystano **AutoMapper**, który pozwala na szybkie konwertowanie danych między modelami.
**AutoMapper** ułatwia:

- Unikanie powielania logiki konwersji, co poprawia przejrzystość kodu.
- Utrzymanie spójności struktury danych między różnymi warstwami aplikacji.

### Generowanie Raportów w Excelu

Do generowania okresowych raportów w formacie Excel wykorzystano bibliotekę **ClosedXML**. Dzięki niej możliwe jest:

- Szybkie tworzenie i formatowanie zaawansowanych arkuszy kalkulacyjnych.
- Tworzenie dynamicznych raportów opartych na danych z bazy lub generowanych w czasie rzeczywistym.
- Obsługa wielu typów danych i zaawansowanych formuł, co ułatwia analizę i prezentację wyników.

## Testy Jednostkowe

W projekcie wykorzystano dwa osobne projekty testowe:

- `CarRental.Comparer.Tests`: Zawiera testy jednostkowe dla logiki związanej z porównywarką cen wypożyczeń samochodów.
- `CarRental.Provider.Tests`: Zawiera testy jednostkowe dla logiki związanej z obsługą API dostawcy samochodów.

Do pisania testów jednostkowych użyto:

- **XUnit**: Framework testowy, który zapewnia prosty i czytelny sposób na tworzenie, uruchamianie oraz raportowanie wyników testów jednostkowych.
- **FluentValidation**: Dzięki tej bibliotece możliwe jest automatyczne sprawdzanie poprawności danych wejściowych w testach, co pozwala na precyzyjne testowanie walidacji.
- **Moq**: Narzędzie do tworzenia fałszywych obiektów (mocków), które umożliwia łatwe i izolowane testowanie logiki aplikacji, bez potrzeby korzystania z zależności zewnętrznych.

Wszystkie testy są zorganizowane modularnie i korzystają z podejścia **Arrange-Act-Assert**, co zapewnia klarowność i łatwość w identyfikacji błędów.

## Dokumentacja API

Dokumentację dla API dostawcy samochodów oraz porównywarki cen wypożyczeń można znaleźć pod następującymi adresami (w środowisku deweloperskim):

- `CarRental.Provider.API`: `https://localhost:7173/swagger/index.html`
- `CarRental.Comparer.API`: `https://localhost:7016/swagger/index.html`

Dokumentacja zawiera:

- Szczegółowy opis dostępnych endpointów,
- Wymagane parametry dla każdego żądania,
- Przykłady odpowiedzi w formacie `JSON`.

### Przykłady widoków Swagger

Poniżej znajdują się zrzuty ekranu interfejsu dokumentacji dla obu API:

#### Dokumentacja dla `CarRental.Comparer.API`

<p align="center">
  <img src="./CarRental.Docs/Images/provider-swagger.PNG" 
       alt="Dokumentacja dla CarRental.Comparer.API" 
       style="width: 80%;"/>
</p>

#### Dokumentacja dla `CarRental.Provider.API`

<p align="center">
  <img src="./CarRental.Docs/Images/comparer-swagger.PNG" 
       alt="Dokumentacja dla CarRental.Provider.API" 
       style="width: 80%;"/>
</p>

## Frontend Aplikacji

Frontend aplikacji został zbudowany w oparciu o **Blazor WebAssembly** – framework do tworzenia nowoczesnych aplikacji webowych z użyciem **C#** i **Razor**.

### Zastosowane Rozwiązania

- **MudBlazor**: Umożliwiająca stworzenie spójnego i nowoczesnego interfejsu użytkownika, który jest responsywny, estetyczny oraz przyjazny w nawigacji.
- **Blazor.Geolocation.WebAssembly**: Umożliwia automatyczne pobieranie lokalizacji użytkownika i wypełnaienie pól adresowych.
- **BlazorGoogleMaps**: Umożliwia zintegrowanie mapy Google w celu wizualizacji lokalizacji użytkownika oraz dostępnych samochodów.

### Zrzuty Ekrany Aplikacji

#### Widoki Użytkownika

##### Strona Główna

<p align="center">
  <img src="./CarRental.Docs/Images/User/home.PNG" 
       alt="Strona Główna" 
       style="width: 80%;"/>
</p>

##### Filtry Pojazdów

<p align="center">
  <img src="./CarRental.Docs/Images/User/car-filters.PNG" 
       alt="Filtry Pojazdów" 
       style="width: 80%;"/>
</p>

##### Zarządzanie Profilem

<p align="center">
  <img src="./CarRental.Docs/Images/User/profile-management.PNG" 
       alt="Zarządzanie Profilem" 
       style="width: 80%;"/>
</p>

##### Aktywne Wypożyczenia

<p align="center">
  <img src="./CarRental.Docs/Images/User/user-rentals-active.PNG" 
       alt="Aktywne Wypożyczenia" 
       style="width: 80%;"/>
</p>

##### Zwrócone Wypożyczenia

<p align="center">
  <img src="./CarRental.Docs/Images/User/user-rentals-returned.PNG" 
       alt="Zwrócone Wypożyczenia" 
       style="width: 80%;"/>
</p>

##### Generowanie Ofert

<p align="center">
  <img src="./CarRental.Docs/Images/User/offers-user-details-form.PNG" 
       alt="Generowanie Ofert" 
       style="width: 80%;"/>
</p>

##### Integracja z Google Maps

<p align="center">
  <img src="./CarRental.Docs/Images/User/offers-google-map.PNG" 
       alt="Integracja z Google Maps" 
       style="width: 80%;"/>
</p>

#### Widoki Pracownika

##### Aktywne Wypożyczenia

<p align="center">
  <img src="./CarRental.Docs/Images/Employee/dashboard-active-rentals.PNG" 
       alt="Aktywne Wypożyczenia" 
       style="width: 80%;"/>
</p>

##### Wypożyczenia Do Zwortu

<p align="center">
  <img src="./CarRental.Docs/Images/Employee/dashboard-confirm-rentals.PNG" 
       alt="Wypożyczenia Do Zwortu" 
       style="width: 80%;"/>
</p>

##### Akceptacja Zwrotu

<p align="center">
  <img src="./CarRental.Docs/Images/Employee/dashboard-confirm-return-form.PNG" 
       alt="Akceptacja Zwrotu" 
       style="width: 80%;"/>
</p>

##### Wypożyczenia Zwrócone

<p align="center">
  <img src="./CarRental.Docs/Images/Employee/dashboard-rental-history.PNG" 
       alt="Wypożyczenia Zwrócone" 
       style="width: 80%;"/>
</p>

##### Generowanie Raportu

<p align="center">
  <img src="./CarRental.Docs/Images/Employee/excel-report-generate.PNG" 
       alt="Generowanie Raportu" 
       style="width: 80%;"/>
</p>

<br></br>

<p align="center">
  <img src="./CarRental.Docs/Images/Employee/excel-report-table.PNG" 
       alt="Generowanie Raportu" 
       style="width: 80%;"/>
</p>

<br></br>

<p align="center">
  <img src="./CarRental.Docs/Images/Employee/excel-report-pivot.PNG" 
       alt="Generowanie Raportu" 
       style="width: 80%;"/>
</p>

#### Serwis Emailowy

<p align="center">
  <img src="./CarRental.Docs/Images/Emails/email-confirm-rental.PNG" 
       alt="Serwis Emailowy" 
       style="width: 80%;"/>
</p>

<br></br>

<p align="center">
  <img src="./CarRental.Docs/Images/Emails/email-rental-confirmed.PNG" 
       alt="Serwis Emailowy" 
       style="width: 80%;"/>
</p>

<br></br>

<p align="center">
  <img src="./CarRental.Docs/Images/Emails/email-return-started.PNG" 
       alt="Serwis Emailowy" 
       style="width: 80%;"/>
</p>

<br></br>

<p align="center">
  <img src="./CarRental.Docs/Images/Emails/email-rental-returned.PNG" 
       alt="Serwis Emailowy" 
       style="width: 80%;"/>
</p>

## Autorzy

Projekt został wykonany przez 3-osobowy zespół:

- [Antonina Frąckowiak](https://github.com/tosiaf)
- [Adam Grącikowski](https://github.com/adamgracikowski)
- [Marcin Gronicki](https://github.com/gawxgd)

Przedmiot prowadził pan [Marcin Sulecki](https://github.com/sulmar).
