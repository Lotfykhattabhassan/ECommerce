# MiniECommerce

A modular monolith e-commerce backend built with **ASP.NET Core** and **Clean Architecture** principles.

The project was designed as a practical backend project to demonstrate how a medium-sized e-commerce system can be structured using independent business modules while keeping clear separation of concerns and module boundaries.

---

## 🏗️ Architecture

MiniECommerce follows a **Modular Monolith** architecture.

Each business module is internally separated into:

- API
- Application
- Domain
- Infrastructure

The project also contains shared **Building Blocks** and module-specific **Contracts** for controlled communication between modules.

```text
MiniECommerce
│
├── BuildingBlocks
│   ├── Application
│   ├── Domain
│   └── Infrastructure
│
├── Contracts
│   ├── Cart
│   └── Order
│
├── Modules
│   ├── Identity
│   ├── Reviews
│   ├── Notifications
│   ├── Catalog
│   ├── Inventory
│   ├── Cart
│   ├── Orders
│   └── Payments
│
└── Host
```

### Module Structure

Each module follows the same general structure:

```
```

```
Module
├── API
├── Application
├── Domain
└── Infrastructure
```

This keeps business logic isolated and prevents modules from directly depending on each other's internal implementation details.

---

## 📦 Modules

### Identity

Responsible for authentication and authorization related concepts.

Includes:

- Users
- User Credentials
- Roles
- User Roles
- Permissions / Claims
- Credential management
- Login security and lockout behavior

---

### Catalog

Responsible for product and category management.

Includes:

- Products
- Categories
- Product status
- Product pricing
- Product validation
- Product and category persistence

The module exposes contracts for other modules instead of exposing its internal entities.

---

### Inventory

Responsible for product stock management.

Includes:

- Product inventory
- Available quantity
- Reserved quantity
- Stock addition/removal
- Stock reservation and release
- Inventory availability checks

Inventory exposes a contract that can be consumed by other modules such as Cart and Orders.

---

### Cart

Responsible for the customer's shopping cart.

Includes:

- Carts
- Cart items
- Quantity management
- Product availability checks
- Cart ownership
- Cart item pricing snapshots

The Cart module communicates with Catalog and Inventory through contracts rather than depending directly on their domain entities.

---

### Orders

Responsible for the order lifecycle.

Includes:

- Orders
- Order items
- Order status
- Product and price snapshots
- Order ownership
- Order state transitions

Order items store the relevant product information at the time of purchase so that historical orders are not affected by future catalog changes.

---

### Payments

Responsible for payment records and payment lifecycle management.

Includes:

- Payment
- Payment method
- Payment status
- Transaction information
- Payment state transitions

The Payment module remains independent from the internal implementation of the Orders module and communicates through defined contracts.

---

### Reviews

Responsible for product reviews.

Includes:

- Product reviews
- Ratings
- Optional comments
- Review updates
- Validation rules

---

### Notifications

Responsible for notification-related functionality.

The module is kept independent from the business modules that trigger notifications.

---

## 🔗 Module Communication

Modules do not directly reference each other's Domain or Infrastructure layers.

Instead, communication is handled through explicit contracts.

For example:

```
```

```
Cart
 │
 ├──→ Catalog Contract
 │
 └──→ Inventory Contract
```

And:

```
```

```
Payments
 │
 └──→ Order Contract
```

This keeps the internal implementation of each module isolated and makes the boundaries between business capabilities explicit.

---

## 🧱 Building Blocks

The project contains shared building blocks for functionality that is common across modules.

Examples include:

- Base entities

These components are intentionally kept small so that modules remain focused on their own business responsibilities.

---

## 🛠️ Technologies

- **C#**
- **.NET / ASP.NET Core**
- **Entity Framework Core**
- **SQL Server**
- **AutoMapper**
- **FluentValidation**
- **Swagger / OpenAPI**
- **Dependency Injection**
- **Clean Architecture**
- **Modular Monolith Architecture**

---

## 🎯 Design Goals

The main goals of the project are:

- Keep business logic inside the Domain layer.
- Keep application orchestration inside the Application layer.
- Keep persistence concerns inside Infrastructure.
- Keep HTTP concerns inside API.
- Keep modules independent from each other's internal implementation.
- Communicate between modules through explicit contracts.
- Keep the codebase understandable and maintainable.
- Avoid unnecessary architectural complexity.

---

## 🔐 Authentication & Authorization

The Identity module separates user information from credential information.

Users are not responsible for storing password information directly.

Credentials are handled through a dedicated `UserCredential` entity which manages concepts such as:

- Password hash
- Password changes
- Failed login attempts
- Account lockout
- Credential status

Authorization is based on roles and permissions/claims.

---

## 🗃️ Persistence

Each module owns its own persistence concerns.

The project uses:

- Entity Framework Core
- DbContexts
- Entity configurations
- Repositories
- Unit of Work

The modules avoid sharing their EF Core entities directly with other modules.

---

## 📐 Domain-Driven Design Concepts

The project applies several DDD concepts where they provide practical value:

- Entities
- Entity behavior
- Domain validation
- Domain events
- Value/state transitions
- Module boundaries
- Explicit contracts between modules

The project intentionally avoids introducing complex patterns unless they provide a clear business or architectural benefit.

---

## 🚀 Getting Started

### Prerequisites

Make sure you have installed:

- .NET SDK
- SQL Server
- Visual Studio / Rider / VS Code

### Clone the repository

```
```

```
git clone <repository-url>
cd MiniECommerce
```

### Configure the database

Update the required connection strings in the application's configuration.

Example:

```
```

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MiniECommerce;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### Apply migrations

Run the required Entity Framework Core migrations for the modules.

```
```

```
dotnet ef database update
```

> Migration commands may need to be executed against the corresponding module DbContext depending on the project configuration.

### Run the application

```
```

```
dotnet run
```

After running the application, Swagger/OpenAPI can be used to explore and test the available endpoints.

---

## 📁 Project Structure

A simplified view of the solution:

```
```

```
MiniECommerce
│
├── BuildingBlocks
│
├── Contracts
│
├── Modules
│   │
│   ├── Identity
│   │   ├── API
│   │   ├── Application
│   │   ├── Domain
│   │   └── Infrastructure
│   │
│   ├── Catalog
│   ├── Inventory
│   ├── Cart
│   ├── Orders
│   ├── Payments
│   ├── Reviews
│   └── Notifications
│
└── Host
```

---

## 🔄 Example Business Flow

A simplified checkout flow looks like:

```
```

```
Customer
   │
   ▼
Cart
   │
   ├── Product information ──→ Catalog
   │
   └── Availability ──────────→ Inventory
   │
   ▼
Order
   │
   ▼
Payment
```

Each module remains responsible for its own business rules while the Application layer coordinates interactions between modules.

---

## 📌 Architectural Principles

The project follows these core principles:

### Separation of Concerns

Each layer has a clear responsibility.

### Dependency Direction

Dependencies point toward abstractions and business logic rather than infrastructure details.

### Encapsulation

Domain entities control their own state through behaviors instead of exposing unrestricted setters.

### Module Isolation

A module should not depend on another module's internal implementation.

### Explicit Communication

Cross-module communication is done through contracts.

### Simplicity

Patterns are introduced when they solve an actual problem, rather than adding abstraction for its own sake.

---

## 📚 Project Purpose

This project was built as a practical backend exercise to apply concepts such as:

- Modular Monolith Architecture
- Clean Architecture
- Domain-Driven Design
- Entity Framework Core
- Authentication & Authorization
- Repository and Unit of Work patterns
- DTOs
- Validation
- Cross-module contracts
- Dependency Injection
- RESTful APIs

The focus is on building a realistic, maintainable backend while keeping the architecture understandable.

---

## 👨‍💻 Author

**[Lotfy Khattab]**

Backend Developer focused on:

- C#
- .NET
- ASP.NET Core
- Clean Architecture
- Modular Monoliths
- Backend Development

---

## 📄 License

This project is available for educational and portfolio purposes.

```
```

```
