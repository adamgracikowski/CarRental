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
10. [👥 Authors](#-authors)

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

> 👇 **Click on any feature below to see it in action**

### 📱 User Views:

---

<details>
<summary> 🏠 Home Page </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/User/home.PNG" alt="Home Page" style="width: 80%;" />
</p>
</details>

<details>
<summary> 🚘 Vehicle Filters </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/User/car-filters.PNG" alt="Vehicle Filters" style="width: 80%;" />
</p>
</details>

<details>
<summary> 👤 Profile Management </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/User/profile-management.PNG" alt="Profile Management" style="width: 80%;" />
</p>
</details>

<details>
<summary> 🟢 Active Rentals </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/User/user-rentals-active.PNG" alt="Active Rentals" style="width: 80%;" />
</p>
</details>

<details>
<summary> ✅ Returned Rentals </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/User/user-rentals-returned.PNG" alt="Returned Rentals" style="width: 80%;" />
</p>
</details>

<details>
<summary> 🧾 Offer Generation </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/User/offers-user-details-form.PNG" alt="Offer Generation" style="width: 80%;" />
</p>
</details>

<details>
<summary> 🗺️ Google Maps Integration </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/User/offers-google-map.PNG" alt="Google Maps Integration" style="width: 80%;" />
</p>
</details>

### 🧑‍💼 Employee Views

---

<details>
<summary> 🟢 Active Rentals </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/Employee/dashboard-active-rentals.PNG" alt="Active Rentals - Employee" style="width: 80%;" />
</p>
</details>

<details>
<summary> ⏳ Rentals Pending Return </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/Employee/dashboard-confirm-rentals.PNG" alt="Rentals Pending Return" style="width: 80%;" />
</p>
</details>

<details>
<summary> ✅ Return Confirmation </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/Employee/dashboard-confirm-return-form.PNG" alt="Return Confirmation" style="width: 80%;" />
</p>
</details>

<details>
<summary> 📖 Rental History </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/Employee/dashboard-rental-history.PNG" alt="Rental History" style="width: 80%;" />
</p>
</details>

<details>
<summary> 📊 Excel Report Generation </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/Employee/excel-report-generate.PNG" alt="Report Generation - Initial View" style="width: 80%;" />
</p>
<p align="center">
  <img src="./CarRental.Docs/Images/Employee/excel-report-table.PNG" alt="Excel Report - Table" style="width: 80%;" />
</p>
<p align="center">
  <img src="./CarRental.Docs/Images/Employee/excel-report-pivot.PNG" alt="Excel Report - Pivot Table" style="width: 80%;" />
</p>
</details>

### 📧 Email Service

---

<details>
<summary> 📩 Rental Confirmation Email </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/Emails/email-confirm-rental.PNG" alt="Rental Confirmation Email" style="width: 80%;" />
</p>
</details>

<details>
<summary> 📨 Rental Approved Email </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/Emails/email-rental-confirmed.PNG" alt="Rental Approved Email" style="width: 80%;" />
</p>
</details>

<details>
<summary> ↩️ Return Started Email </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/Emails/email-return-started.PNG" alt="Return Started Email" style="width: 80%;" />
</p>
</details>

<details>
<summary> ✅ Rental Returned Email </summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/Emails/email-rental-returned.PNG" alt="Rental Returned Email" style="width: 80%;" />
</p>
</details>

## 🚀 Technologies Used

### 🌐 Frontend

- **Blazor WebAssembly**, **MudBlazor**, **Microsoft Entra Id**, **GoogleMaps**.

### ⚙️ Backend

- **ASP.NET Core**, **C#**, **Entity Framework Core**, **SQL Server**, **Azure Cache for Redis**, **SendGrid**, **Azure Blob Storage**, **Azure Key Vault**, **Application Insights**.

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

<!-- 
<br>

> The diagram illustrates the structure of how the projects are organized within the solution:

<br>

<p align="center">
  <img src="./CarRental.Docs/Diagrams/SolutionArchitecture/CarRental.png" 
       alt="Diagram of the projects" 
       style="width: 80%;"/>
</p>

```mermaid

```
-->

## ☁️ Cloud Services Architecture

> 👇 **Click to see the diagram**

<details>
<summary>
 🔍 Diagram illustrating the cloud services architecture
</summary>
<br>
<p align="center"> 
   <img src="./CarRental.Docs/Diagrams/AzureArchitecture/azure-architecture.png" 
        alt="Cloud Services Architecture Diagram" 
        style="width: 90%;"/> 
</p>
</details>

### 🧩 Resource Groups Breakdown

The system’s resources are organized into three **Resource Groups** for clarity and separation of responsibilities:

| 🔖 Resource Group | 🛠️ Service Name  | 📦 Type | 📝 Description |
|--------------------|------------------|---------|----------------|
| **`carrental-provider-prod-rg`** | `carrental-provider` | App Service | Hosts the Car Provider API (CRUD operations for vehicles, offers & reservations). |
| | `carrental-provider-kv` | Key Vault | Secure storage for keys, passwords and other secrets. |
| | `carrental-provider-ai` | Application Insights | Real‑time monitoring and diagnostics for the provider API. |
| | `CarRentalProviderDb` | SQL Database | Relational database storing car provider data. |
| **`carrental-comparer-prod-rg`** | `carrental-comparer` | App Service | Hosts the Price Comparer API (aggregates and compares rental offers). |
| | `carrental-comparer-kv` | Key Vault | Secure storage for keys, passwords and other secrets. |
| | `carrental-comparer-ai` | Application Insights | Monitoring and telemetry for the comparer API. |
| | `CarRentalComparerDb` | SQL Database | Relational database powering the price comparison engine. |
| | `carrental-comparer-web` | Static Web App | Frontend hosting for the price comparison UI. |
| **`carrental-common-prod-rg`** | `carrentalminisa` | Blob Storage | Static file storage (e.g., vehicle images, brand logos). |
| | `carrental-cache` | Azure Cache for Redis | Caching layer to accelerate read operations (e.g., search results). |

## 🗄️ Database Schema

The system uses **Entity Framework Core** to manage the database with a _code-first_ approach 🧩. This means the database schema is automatically generated from your C# model classes, ensuring consistency between the application and the database.

- In the **development** environment, we use **Microsoft SQL Server** running locally (`localhost`) 🖥️.  
- In **production**, the solution is deployed on two **Azure SQL Database** instances ☁️—one for the Car Provider API and one for the Price Comparer.

<!-- 
Below are the diagrams illustrating the database structures, table relationships, and key attributes:

<p align="center">
  <img src="./CarRental.Docs/Diagrams/Databases/carrental-provider-db.png" 
       alt="Database Schema for the Car Rental Provider" 
       style="width: 80%;"/>
</p>

<p align="center">
  <img src="./CarRental.Docs/Diagrams/Databases/carrental-comparer-db.png" 
       alt="Database Schema for the Car Rental Comparer" 
       style="width: 80%;"/>
</p>
-->

## 🧠 Key Patterns & Technologies

To ensure modularity and scalability, the system leverages the following design patterns and libraries:

### 🤝 Mediator Pattern

Using **[`MediatR`](https://github.com/jbogard/MediatR)**, we implemented a mediator to handle communication between components, which:

- 🔗 Reduces direct dependencies between modules  
- 🎯 Centralizes request/command handling logic  

### ⚖️ CQRS (Command Query Responsibility Segregation)

We separate operations into:

- **Commands**: Modify system state (e.g. generate offers, create/return rentals, update user data)  
- **Queries**: Read-only operations (e.g. search available vehicles, fetch rental details)  

This separation simplifies logic and improves code clarity.

### 🗃️ Repository & Specification Patterns

With **[`Ardalis.Specification`](https://specification.ardalis.com/)**, we introduced:

- **Repository Pattern**: Abstracts data operations, decoupling business logic from data access  
- **Specifications**: Encapsulate query criteria in reusable classes for consistent querying  

### 🏆 Result Pattern

Using **[`Ardalis.Result`](https://result.ardalis.com/)** for standardized operation results:

- ✅ Indicates success or failure  
- 📝 Provides error details (codes/messages)  
- 🚫 Minimizes exception usage in business logic for cleaner, more testable code  

### 🔐 User Authentication

Integrated **[`Microsoft Entra ID`](https://www.microsoft.com/en-us/security/business/identity-access/microsoft-entra-id)** for secure, single-sign‑on access:

- 🧑‍💼 Users log in via SSO
- 🔒 External service integration for robust access control  

#### 👥 Role-Based Access

Two main roles are defined:

- **`Employee`**: Manages vehicles, reservations, and system administration  
- **`User`**: Browses available cars, compares offers, and rents vehicles  

Each role has distinct permissions to enforce proper access control.

### ⏳ Background Jobs

Handled with **[`Hangfire`](https://www.hangfire.io/)** for asynchronous tasks such as:

- ⏰ Marking offers as expired based on business rules  
- 🔄 Checking rental status updates  

Hangfire offers easy scheduling, a web dashboard for monitoring, and seamless integration with Azure.

### 🧪 Data & Request Validation

Employed **[`FluentValidation`](https://docs.fluentvalidation.net/en/latest/)** to declare advanced validation rules:

- 🔍 Ensures every business operation starts with validated input  
- 📜 Improves readability and maintainability of validation logic  

### 🔄 Object Mapping

Used **[`AutoMapper`](https://docs.automapper.org/en/stable/)** for rapid object-to-object mapping:

- ✂️ Eliminates boilerplate conversion code  
- 🔄 Maintains consistent data structures across application layers  

### 📊 Excel Reporting

Utilized **[`ClosedXML`](https://docs.closedxml.io/en/latest/)** to generate periodic Excel reports:

- 🏗️ Quickly create and format complex spreadsheets  
- 📈 Build dynamic reports from database or runtime data  
- ➗ Support advanced formulas and multiple data types for detailed analysis  

## 🧪 Unit Testing

The solution includes two dedicated test projects to ensure code quality and reliability:

- 🔍 **[CarRental.Comparer.Tests](https://github.com/adamgracikowski/CarRental/tree/main/CarRental/CarRental.Comparer.Tests)**  
  Contains unit tests for the price comparison logic.

- ⚙️ **[CarRental.Provider.Tests](https://github.com/adamgracikowski/CarRental/tree/main/CarRental/CarRental.Provider.Tests)**  
  Contains unit tests for the Car Provider API logic.

The following libraries and frameworks are used for testing:

- 🧰 **[`xUnit`](https://xunit.net/)**: A streamlined testing framework for creating, running, and reporting unit tests.  
- ✅ **[`FluentValidation`](https://docs.fluentvalidation.net/en/latest/)**: Automatically validates input models in tests, enabling precise validation checks.  
- 🤖 **[`Moq`](https://github.com/devlooped/moq)**: A mocking library for creating fake objects, allowing isolated testing of business logic without external dependencies.

All tests follow the **Arrange-Act-Assert** pattern to maintain clarity and ease of debugging.  

## 📘 API Documentation

You can access the API docs for both the Car Provider and Price Comparer in your **development** environment:

- 🚗 **[CarRental.Provider.API](https://github.com/adamgracikowski/CarRental/tree/main/CarRental/CarRental.Provider.API)**: `https://localhost:7173/swagger/index.html`  
- 🔍 **[CarRental.Comparer.API](https://github.com/adamgracikowski/CarRental/tree/main/CarRental/CarRental.Comparer.API)**: `https://localhost:7016/swagger/index.html`  

<br>

> 👇 **Click to see the Swagger UI examples**

<br>

<details>
<summary>
 📑 Comparer API Swagger UI
</summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/provider-swagger.PNG" 
       alt="Comparer API Swagger UI" 
       style="width: 80%;"/>
</p>
</details>

<details>
<summary>
 📑 Provider API Swagger UI
</summary>
<br>
<p align="center">
  <img src="./CarRental.Docs/Images/comparer-swagger.PNG" 
       alt="Provider API Swagger UI" 
       style="width: 80%;"/>
</p>
</details>

## 👥 Authors

This project was created by:

- [Antonina Frąckowiak](https://github.com/tosiaf)
- [Adam Grącikowski](https://github.com/adamgracikowski)
- [Marcin Gronicki](https://github.com/gawxgd)

The course was taught by 🎓 [Marcin Sulecki](https://github.com/sulmar).
