# ERP System - Backend RESTful API 🚀

A robust, enterprise-grade **ERP & Point of Sale (POS) Backend API** built with **.NET 8** following **Clean Architecture** principles. It manages inventory, multi-unit/group/warehouse product catalogs, transactional sales invoicing, financial profit analytics, and Role-Based Access Control (RBAC).

🔗 **Frontend Repository:** [ERP_System.Frontend](https://github.com/Dahoomshaheen/ERP_System.Frontend)

---

## 🏗️ Architecture & Design Patterns

The solution is structured into four decoupled layers following **Clean Architecture**:
* **`ERP_System.Domain`:** Core business entities (`Product`, `SalesHeader`, `SalesDetail`, `Unit`, `Group`, `Warehouse`, `User`) enriched with **Data Annotations** for strict schema and input validation.
* **`ERP_System.Application`:** Repository interfaces (`IGenericRepository<T>`, `IProductRepository`, `ISalesRepository`) defining contracts without infrastructure dependencies.
* **`ERP_System.Infrastructure`:** EF Core `ApplicationDbContext`, SQL Server migrations, generic and specialized repositories utilizing **Eager Loading** (`.Include()` / `.ThenInclude()`).
* **`ERP_System.API`:** RESTful controllers, JWT authentication/authorization pipeline, CORS configuration, and Swagger/OpenAPI documentation.

---

## ✨ Key Features

* **Authentication & RBAC (Admin / Cashier):**
  * Stateless **JWT Bearer Authentication** with an 8-hour shift token lifetime and embedded `ClaimTypes.Role`.
  * **Auto-Seeding:** Automatically provisions a default `Admin` account on startup if none exists.
  * Granular endpoint protection restricting financial reports, user registration, and product management to `Admin` users.
* **Data Integrity & Relational Validation:**
  * Verifies foreign key existence (`UnitId`, `GroupId`, `WarehouseId`, `ProductId`) before persisting records to prevent orphaned references.
  * **Business Logic Rules:** Enforces selling price validation (`Price >= CostPrice`) and non-empty transactional invoices.
  * **Safe Delete:** Prevents deleting products that are already linked to historical sales invoices.
* **Standardized API Responses & Serialization:**
  * Consistent JSON responses (`201 Created`, `400 Bad Request`, `401 Unauthorized`, `403 Forbidden`, `404 Not Found`) with descriptive messages.
  * Circular reference handling configured via `ReferenceHandler.IgnoreCycles` and `[JsonIgnore]`.

---

## 🛠️ Tech Stack

* **Framework:** .NET 8 (ASP.NET Core Web API)
* **ORM & Database:** Entity Framework Core & Microsoft SQL Server
* **Security:** JWT (JSON Web Tokens) & Role-Based Authorization
* **Documentation:** Swagger UI (OpenAPI)

---

## 📌 API Endpoints Overview

| Controller | Method | Endpoint | Access | Description |
| :--- | :--- | :--- | :--- | :--- |
| **Auth** | `POST` | `/api/Auth/login` | Public | Authenticates user and returns JWT + Role |
| **Auth** | `POST` | `/api/Auth/register` | **Admin** | Creates a new `Admin` or `Cashier` account |
| **Products** | `GET` | `/api/Products` | Authenticated | Retrieves all products with Unit, Group & Warehouse |
| **Products** | `GET` | `/api/Products/{id}` | Authenticated | Retrieves a single product by ID |
| **Products** | `POST` | `/api/Products` | **Admin** | Adds a new product with FK & price validation |
| **Products** | `DELETE` | `/api/Products/{id}` | **Admin** | Safely deletes a product if unlinked to sales |
| **Sales** | `GET` | `/api/Sales` | **Admin** | Retrieves all invoices with item details for profit analytics |
| **Sales** | `POST` | `/api/Sales` | Authenticated | Records a new POS invoice and calculates totals |
| **Lookups** | `GET/POST` | `/api/Units`, `/Group`, `/Warehouse` | Authenticated | Manages product classification lookups |

---

## 🚀 Getting Started

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/Dahoomshaheen/ERP_System.API.git](https://github.com/Dahoomshaheen/ERP_System.API.git)
   ```
2. **Configure Database Connection:**
   Update `"Defaultconnection"` in `ERP_System.API/appsettings.json` to point to your SQL Server instance.
3. **Apply EF Core Migrations:**
   Open **Package Manager Console** (set Default Project to `ERP_System.Infrastructure`) and run:
   ```powershell
   Update-Database
   ```
4. **Run the API:**
   Start the project in Visual Studio (default HTTPS port: `7104`).
   * **Default Admin Credentials:** Username: `admin` | Password: `123456`
