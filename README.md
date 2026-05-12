<div align="center">

# TrendyFusion E-Shop

### An enterprise-grade ASP.NET Core MVC platform built with clean architecture, SOLID principles, and modern .NET best practices.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-5C2D91?style=for-the-badge&logo=.net&logoColor=white)](https://docs.microsoft.com/aspnet/core)
[![EF Core](https://img.shields.io/badge/EF_Core-8.0-512BD4?style=for-the-badge&logo=microsoft&logoColor=white)](https://docs.microsoft.com/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![C#](https://img.shields.io/badge/C%23-12-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://docs.microsoft.com/dotnet/csharp/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](LICENSE)

</div>

---

## Overview

**TrendyFusion E-Shop** is a full-stack ASP.NET Core MVC application engineered around a layered, domain-driven architecture. The project demonstrates production-ready patterns — Repository, Unit of Work, Service Layer, and DTO mapping — wrapped around an ASP.NET Core Identity authentication system with email-based password recovery.

It is built as a foundation for a modern e-commerce platform, currently shipping a fully-featured **Employee & Department management module** that showcases CRUD operations, file uploads, search, and role-aware authorization.

---

## Highlights

- **Clean 3-Tier Architecture** — strict separation between `Presentation`, `BusinessLogic`, and `DataAccess` layers, each compiled as an isolated project.
- **Repository + Unit of Work Pattern** — generic repository with strongly-typed specializations, coordinated through a single transactional `IUnitOfWork`.
- **Service Layer with DTOs** — controllers stay thin; business rules live in dedicated services with AutoMapper-powered DTO translation.
- **ASP.NET Core Identity** — secure user registration, sign-in, sign-out, and SMTP-based **Forget / Reset Password** flow.
- **Entity Framework Core 8** — code-first migrations, lazy loading proxies, and fluent API entity configurations.
- **CSRF Protection** — global `AutoValidateAntiforgeryToken` filter applied across the MVC pipeline.
- **File / Image Uploads** — dedicated `IAttachementService` handles employee profile images with safe storage.
- **Modern C# 12 / .NET 8** — primary constructors, nullable reference types, file-scoped namespaces, and implicit usings throughout.

---

## Tech Stack

| Layer | Technology |
| --- | --- |
| **Framework** | ASP.NET Core MVC 8.0 |
| **Language** | C# 12 |
| **ORM** | Entity Framework Core 8 (Code-First) |
| **Database** | Microsoft SQL Server |
| **Authentication** | ASP.NET Core Identity |
| **Object Mapping** | AutoMapper |
| **Front-End** | Razor Views, Bootstrap 5, jQuery, jQuery Validation Unobtrusive |
| **Email** | System.Net.Mail (SMTP) |
| **IDE** | Visual Studio 2022 / JetBrains Rider |

---

## Solution Architecture

```
DemoMvcSolution.sln
│
├── Demo.Presentation              # ASP.NET Core MVC — UI, Controllers, ViewModels, Razor Views
│   ├── Controllers/               # Account, Home, Employees, Departments
│   ├── ViewModels/                # Login, Register, ForgetPassword, ResetPassword, Employee, Department
│   ├── Views/                     # Razor pages + _Layout, _ValidationScriptsPartial
│   ├── Utilities/                 # Email helper + SMTP settings
│   ├── wwwroot/                   # Static assets (Bootstrap, jQuery, images, uploads)
│   └── Program.cs                 # DI registration + middleware pipeline
│
├── Demo.BusinessLogic             # Service layer — business rules & DTOs
│   ├── Services/                  # IDepartmentService, IEmployeeService, IAttachementService
│   ├── DataTransferObjects/       # Created / Updated / Details / List DTOs
│   ├── Profiles/                  # AutoMapper mapping profiles
│   └── Factories/                 # Object factories
│
└── Demo.DataAccess                # Data layer — EF Core, repositories, entities
    ├── Models/                    # Employee, Department, ApplicationUser, BaseEntity
    ├── Data/
    │   ├── DbContexts/            # ApplicationDbContext (Identity + domain)
    │   ├── Configurations/        # Fluent API IEntityTypeConfiguration<T>
    │   └── Migrations/            # EF Core migration history
    └── Repositories/              # IGenericRepository<T>, IUnitOfWork + concrete impls
```

---

## Key Features

### Authentication & Identity
- User registration with email uniqueness enforcement
- Cookie-based sign-in with "Remember Me" support
- Account lockout and not-allowed states surfaced as friendly validation errors
- **Password reset via email** — token-based, link sent through SMTP
- Global `[Authorize]` protection on management controllers

### Employee Management
- List, create, view, edit, and soft-delete employees
- Search-by-name filtering
- Profile image upload via attachment service
- Department relationship with navigation properties
- Gender & EmployeeType enums, hiring date, salary, address, contact info

### Department Management
- Full CRUD with unique code enforcement
- One-to-many relationship to employees
- Cascade-safe deletion logic

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/) (LocalDB, Express, or full edition)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) **or** the [.NET CLI](https://docs.microsoft.com/dotnet/core/tools/)
- An SMTP account (Gmail, SendGrid, etc.) for the password-reset feature

### Installation

```bash
# 1. Clone the repository
git clone https://github.com/Arsany-Naim/TrendyFusion_E-Shop.git
cd "TrendyFusion E-Shop - .NET (MVC)"

# 2. Restore NuGet packages
dotnet restore

# 3. Build the solution
dotnet build
```

### Configuration

Update `Demo.Presentation/appsettings.json` with your local values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TrendyFusionDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "EmailSettings": {
    "From": "your-email@example.com",
    "Password": "your-app-password",
    "Host": "smtp.gmail.com",
    "Port": 587
  }
}
```

### Database Setup

Apply the Entity Framework Core migrations to create the schema:

```bash
dotnet ef database update --project Demo.DataAccess --startup-project Demo.Presentation
```

### Run the Application

```bash
dotnet run --project Demo.Presentation
```

The site will be available at **https://localhost:5001** (or the port shown in the console).

---

## Design Patterns & Principles

| Pattern / Principle | Where to Find It |
| --- | --- |
| **Repository Pattern** | `Demo.DataAccess/Repositories/Classes/GenericRepository.cs` |
| **Unit of Work** | `Demo.DataAccess/Repositories/Classes/UnitOfWork.cs` |
| **Dependency Injection** | `Demo.Presentation/Program.cs` |
| **DTO Pattern** | `Demo.BusinessLogic/DataTransferObjects/` |
| **AutoMapper Profiles** | `Demo.BusinessLogic/Profiles/MappingProfiles.cs` |
| **SOLID Principles** | Applied across services, repositories, and controllers |
| **Code-First Migrations** | `Demo.DataAccess/Data/Migrations/` |
| **Fluent API Configurations** | `Demo.DataAccess/Data/Configurations/` |

---

## Roadmap

- [ ] Product catalog and category management
- [ ] Shopping cart and checkout flow
- [ ] Payment gateway integration (Stripe / PayPal)
- [ ] Order tracking & customer profile
- [ ] Role-based authorization (Admin / Customer)
- [ ] Unit and integration tests with xUnit + Moq
- [ ] CI/CD pipeline via GitHub Actions
- [ ] Containerization with Docker
- [ ] Migration to ASP.NET Core 9 with Minimal APIs

---

## Contributing

Contributions are welcome. To propose a change:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m "Add your feature"`)
4. Push to your fork (`git push origin feature/your-feature`)
5. Open a Pull Request describing your changes

---

## License

Distributed under the **MIT License**. See [`LICENSE`](LICENSE) for details.

---

## Author

**Arsany Naim** — Full-Stack .NET Developer

- GitHub: [@Arsany-Naim](https://github.com/Arsany-Naim)
- Repository: [TrendyFusion_E-Shop](https://github.com/Arsany-Naim/TrendyFusion_E-Shop)

<div align="center">

If you find this project useful or interesting, please consider giving it a star.

</div>
