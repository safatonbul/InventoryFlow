# InventoryFlow

A RESTful Inventory Management API developed with C# and ASP.NET Core Web API.

InventoryFlow is a learning project built to practice backend development, object-oriented programming, layered architecture, database operations, and REST API development.

## Technologies

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- Repository Pattern
- Dependency Injection
- Async/Await

## Architecture

The project follows a layered architecture:

- **API:** Handles HTTP requests and responses.
- **Business:** Contains business logic and service operations.
- **DataAccess:** Handles database operations through repositories.
- **Entities:** Contains domain entities and their business rules.

## Features

- Create, read, update, and delete products.
- Increase and decrease product stock.
- Transfer stock between products.
- Entity validation and DTO validation.
- Asynchronous database operations.
- Global exception handling middleware.
- Transaction management with commit and rollback.
- SQL Server database integration using Entity Framework Core.

## Database Setup

1. Install SQL Server and configure your connection.
2. Update the connection string in `appsettings.json`.
3. Apply Entity Framework Core migrations.
4. Run the API.

Example connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=InventoryFlowDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

## Running the Project

Open the solution in Visual Studio 2022.

Set the API project as the startup project and run the application.

Swagger UI can be used to explore and test the API endpoints.

## Purpose

This project was developed as a practical exercise to improve my C# and backend development skills, understand layered architecture, and gain experience building RESTful APIs.

---

**Author:** Safa Tonbul

**Status:** Completed learning project
