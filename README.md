# ERP Sales Management System API 🛒

A  scalable Enterprise Resource Planning (ERP) Web API built to manage products, inventory, and sales operations securely. 

## 🏗️ Architecture & Patterns
This project strictly follows **Clean Architecture** principles to ensure separation of concerns, maintainability, and scalability. It is divided into four main layers:
* **Domain Layer:** Contains Core Entities (`Product`, `Unit`, `Group`, `Warehouse`, `SalesHeader`, `SalesDetail`).
* **Application Layer:** Contains Interfaces and Business Logic Rules.
* **Infrastructure Layer:** Implements Data Access, EF Core `DbContext`, and Migrations.
* **API / Presentation Layer:** Contains API Controllers securely exposing endpoints.

**Design Patterns Used:**
* **Repository Pattern:** Both Generic (for basic CRUD operations) and Custom Repositories (for complex operations like Invoice generation and Eager Loading).
* **Dependency Injection (DI):** For loose coupling and managing object lifecycles (`Scoped`).

## 🛠️ Tech Stack
* **Framework:** .NET 8 Web API
* **Database:** SQL Server
* **ORM:** Entity Framework Core 8 (Code-First Approach)
* **Security:** JWT (JSON Web Tokens) Authentication & Authorization
* **API Documentation:** Swagger UI

## 🔐 Key Features
* **Full CRUD Operations:** Manage Units, Groups, Warehouses, and Products.
* **Transactional Sales Invoicing:** Create complete sales invoices (Header & Details) in a single, robust database transaction.
* **JWT Authentication:** Secure API endpoints requiring valid Bearer Tokens for access.
* **Dynamic Calculations:** Automatic total calculation for sales invoices based on product quantities and unit prices.
