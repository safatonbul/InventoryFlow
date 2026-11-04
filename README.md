# InventoryFlow

A RESTful inventory management API built with **C#** and **ASP.NET Core (.NET 8)**. It covers product CRUD, stock adjustments, and **transactional stock transfers** between products, with a layered architecture and consistent error handling.

> **Status:** v1 completed

---

## Highlights

- **Layered architecture** (API / Business / DataAccess / Entities) with dependencies pointing inward: the Business layer depends on repository interfaces, not on EF Core.
- **Transactional stock transfer:** decrease source stock, increase target stock, then commit. If anything fails, the whole operation is rolled back and both products keep their original stock.
- **Business rules inside entities:** stock can never go negative.
- **Global exception handling middleware** returning consistent JSON error responses (400 / 500).
- **Async/await** across the whole data access path.

---

## Tech Stack

| Area | Technology |
|---|---|
| Language | C# |
| Framework | .NET 8, ASP.NET Core Web API |
| ORM | Entity Framework Core (Code First + Migrations) |
| Database | Microsoft SQL Server |
| Querying | LINQ |
| Patterns | Repository Pattern, Dependency Injection, DTOs |
| Validation | Data Annotations + entity-level business rules |
| API Docs / Testing | Swagger (OpenAPI) |
| IDE | Visual Studio 2022 |

---

## Architecture

```
InventoryFlow
├── InventoryFlow.API          -> Controllers, middleware, DTOs, HTTP responses
├── InventoryFlow.Business     -> Services and business logic (depends on repository interfaces)
├── InventoryFlow.DataAccess   -> DbContext, repositories, migrations (EF Core)
└── InventoryFlow.Entities     -> Domain entities and domain rules
```


| Layer | Responsibility |
|---|---|
| **API** | Receives HTTP requests, validates DTOs, returns responses |
| **Business** | Orchestrates operations such as `ProductService` and stock transfer |
| **DataAccess** | Talks to SQL Server through repositories and EF Core |
| **Entities** | Holds the `Product` model and its rules (e.g. no negative stock) |

---

## Features

**Product management**
- Create, list, get by ID, update, and delete products

**Stock management**
- Increase stock
- Decrease stock
- Transfer stock from one product to another
- Negative stock prevention

**Reliability**
- Transaction management with commit / rollback
- DTO validation with Data Annotations
- Global exception handling middleware with JSON error messages

---

## API Endpoints



| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/products` | List all products |
| GET | `/api/products/{id}` | Get a product by ID |
| POST | `/api/products` | Create a product |
| PUT | `/api/products/{id}` | Update a product |
| DELETE | `/api/products/{id}` | Delete a product |
| POST | `/api/products/{id}/increase-stock` | Increase stock |
| POST | `/api/products/{id}/decrease-stock` | Decrease stock |
| POST | `/api/products/transfer` | Transfer stock between two products |

---

## Stock Transfer and Rollback

The transfer runs inside a single database transaction:

1. Decrease stock of the source product
2. Increase stock of the target product
3. Both succeed -> **Commit**
4. Any failure (e.g. insufficient stock) -> **Rollback**, so neither product is changed

**Successful transfer**

```http
POST /api/products/transfer
Content-Type: application/json

{
  "fromProductId": 1,
  "toProductId": 2,
  "quantity": 5
}
```

Result: product 1 stock decreases by 5, product 2 stock increases by 5.

**Insufficient stock**

```json
{
  "statusCode": 400,
  "message": "Insufficient stock."
}
```

Result: the transaction is rolled back and **both products keep their original stock**.




| Swagger endpoint list | Successful transfer | Insufficient stock (rollback) |
|---|---|---|
| ![Swagger](docs/swagger.png) | ![Transfer](docs/transfer-success.png) | ![Rollback](docs/transfer-rollback.png) |

---

## Getting Started

### Requirements

- .NET 8 SDK
- SQL Server (or SQL Server Express)
- Visual Studio 2022 (optional)

### Setup

1. Clone the repository
   ```bash
   git clone https://github.com/safatonbul/InventoryFlow.git
   cd InventoryFlow
   ```

2. Update the connection string in `appsettings.json`
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.\\SQLEXPRESS;Database=InventoryFlowDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. Apply the migrations
   ```bash
   dotnet ef database update --project InventoryFlow.DataAccess --startup-project InventoryFlow.API
   ```
   

4. Run the API
   ```bash
   dotnet run --project InventoryFlow.API
   ```
   Or open the solution in Visual Studio 2022, set the API project as the startup project, and press F5.

5. Open Swagger UI (usually `https://localhost:<port>/swagger`) to explore and test the endpoints.

---

## Future Improvements

- Supplier management
- Low-stock alerts
- Sales records and reporting
- JWT authentication
- Unit tests (xUnit)
- SQL index optimization

---

## Author

**Safa Tonbul**
GitHub: [github.com/safatonbul](https://github.com/safatonbul)
