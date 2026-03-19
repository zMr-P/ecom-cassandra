# E-commerce Cassandra API

A modern RESTful API built with **.NET 8** and **Apache Cassandra** for a scalable and robust e-commerce system. This project implements clean architecture patterns with CQRS (Command Query Responsibility Segregation) using MediatR.

## 📋 Table of Contents

- [Overview](#overview)
- [Technology Stack](#technology-stack)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Domain Entities](#domain-entities)
- [Getting Started](#getting-started)
- [Installation and Configuration](#installation-and-configuration)
- [Running the Project](#running-the-project)
- [API Endpoints](#api-endpoints)
- [Authentication and Authorization](#authentication-and-authorization)
- [Migrations](#migrations)
- [Folder Structure](#folder-structure)
- [Design Patterns](#design-patterns)

---

## 🎯 Overview

This project is a RESTful e-commerce API that uses **Apache Cassandra** as a NoSQL database, offering:

✅ **Horizontal Scalability** - Cassandra supports distribution across multiple servers  
✅ **High Availability** - Data replication ensuring consistency  
✅ **Performance** - Fast read and write operations at scale  
✅ **Clean Architecture** - Clear separation of concerns  
✅ **CQRS Pattern** - Separation between commands and queries  
✅ **JWT Authentication** - Token-based security  
✅ **API Versioning** - Support for multiple API versions  
✅ **Swagger Documentation** - Interactive API documentation  

---

## 💻 Technology Stack

### Backend Framework
- **ASP.NET Core 8.0** - Modern, high-performance web framework
- **.NET 8** - Latest version of the .NET runtime

### Database
- **Apache Cassandra** - Distributed NoSQL database
- **Cassandra.Fluent.Migrator** - Migration management for Cassandra

### Main Libraries
- **MediatR** - CQRS pattern implementation
- **Mapster** - Object mapping (alternative to AutoMapper)
- **Asp.Versioning** - API Versioning
- **Swashbuckle.AspNetCore** - Swagger/OpenAPI integration
- **System.IdentityModel.Tokens.Jwt** - JWT authentication
- **MrP.FluentResult** - Fluent result handling

### Security
- **BCrypt.Net** - Password hashing
- **JWT (JSON Web Tokens)** - Authentication and authorization

---

## 🏗️ Architecture

The project follows **clean layered architecture** with the **CQRS** pattern:

```
┌─────────────────────────────────────┐
│       ecom-cassandra.WebApi         │
│    (Presentation Layer)             │
│  Controllers, Swagger, Versioning   │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│    ecom-cassandra.Application       │
│    (Business Logic Layer)           │
│  UseCases (Commands & Queries)      │
│  Mapping, Handlers (MediatR)        │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│    ecom-cassandra.Domain            │
│    (Domain Layer)                   │
│  Entities, Enums, Interfaces        │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│  ecom-cassandra.Infrastructure      │
│  (Data Access Layer)                │
│  Repositories, Cassandra Session    │
│  OperationBatch, Migrations         │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│ ecom-cassandra.DependencyInjection  │
│ (Configuration Layer)               │
│ IoC Container Setup, JWT, Swagger   │
└─────────────────────────────────────┘
```

### Layers

1. **WebApi** - Presentation layer with REST controllers
2. **Application** - Business logic with CQRS and MediatR
3. **Domain** - Business entities and interfaces
4. **Infrastructure** - Data access and implementations
5. **DependencyInjection** - IoC container configuration
6. **CrossCutting** - Shared utilities (messages, constants)
7. **MigrationJob** - CLI tool to execute Cassandra migrations

---

## 📂 Project Structure

```
ecom-cassandra/
├── ecom-cassandra.sln                          # Visual Studio Solution
│
├── ecom-cassandra.WebApi/                      # API REST Layer
│   ├── Program.cs                              # Application configuration
│   ├── appsettings.json                        # App settings
│   ├── Controllers/                            # API Endpoints
│   │   ├── UsersController.cs
│   │   ├── ProductsController.cs
│   │   ├── CategoriesController.cs
│   │   ├── OrdersController.cs
│   │   └── OrderItemsController.cs
│   └── Properties/
│
├── ecom-cassandra.Application/                 # Business Logic
│   ├── Mapping/                                # Mappings (Mapster)
│   │   ├── UserMapping.cs
│   │   ├── ProductsMapping.cs
│   │   ├── CategoriesMapping.cs
│   │   ├── OrdersMapping.cs
│   │   └── OrderItemsMapping.cs
│   ├── UseCases/                               # CQRS Commands & Queries
│   │   ├── Users/
│   │   │   ├── Create/                         # Create User Command
│   │   │   ├── GetAll/                         # Get All Users Query
│   │   │   ├── Login/                          # Login Command
│   │   │   └── UpdateRole/                     # Update Role Command
│   │   ├── Products/                           # Product Commands & Queries
│   │   ├── Categories/                         # Category Commands & Queries
│   │   ├── Orders/                             # Order Commands & Queries
│   │   └── OrderItems/                         # OrderItem Commands & Queries
│   └── ApplicationMarker.cs                    # Marker class for DI
│
├── ecom-cassandra.Domain/                      # Domain Layer
│   ├── Entities/                               # Domain Models
│   │   ├── User.cs
│   │   ├── Product.cs
│   │   ├── Category.cs
│   │   ├── Order.cs
│   │   ├── OrderItem.cs
│   │   ├── Review.cs
│   │   └── Address.cs
│   ├── Enums/                                  # Enumerations
│   │   ├── UserRoles.cs
│   │   └── OrderStatus.cs
│   └── Interfaces/                             # Contracts
│       ├── Repositories/                       # Repository Interfaces
│       └── Security/                           # Security Interfaces
│
├── ecom-cassandra.Infrastructure/              # Data Layer
│   ├── Session/                                # Cassandra Session
│   │   └── CassandraSession.cs
│   ├── Config/                                 # Configurations
│   ├── Repositories/                           # Repository Implementations
│   │   ├── UserRepository.cs
│   │   ├── ProductRepository.cs
│   │   ├── CategoryRepository.cs
│   │   ├── OrderRepository.cs
│   │   └── OrderItemRepository.cs
│   ├── Migrations/                             # Cassandra Migrations
│   │   ├── CreateUserAndAddress20260202.cs
│   │   ├── CreateOrderProductOrderItemReview20260203.cs
│   │   ├── AddColumnRolesOnUser20260203.cs
│   │   ├── AddEmailIndexOnUser20260215.cs
│   │   └── InsertAdminUser20260223.cs
│   ├── Security/                               # Security Implementations
│   │   ├── HashSecurity.cs
│   │   └── JwtSecurity.cs
│   ├── OperationBatch.cs                       # Batch operations for Cassandra
│   └── ecom-cassandra.Infrastructure.csproj
│
├── ecom-cassandra.DependencyInjection/         # IoC Configuration
│   ├── Ioc.cs                                  # Dependency registration
│   ├── Cassandra.cs                            # Cassandra configuration
│   ├── Jwt.cs                                  # JWT configuration
│   ├── Swagger.cs                              # Swagger configuration
│   ├── Mapster.cs                              # Mapster configuration
│   ├── MediatR.cs                              # MediatR configuration
│   ├── Versioning.cs                           # API Versioning configuration
│   └── ecom-cassandra.DependencyInjection.csproj
│
├── ecom-cassandra.CrossCutting/                # Shared Utilities
│   ├── Constants/
│   │   ├── ErrorMessage.cs                     # Error messages
│   │   └── SuccessMessage.cs                   # Success messages
│   └── ecom-cassandra.CrossCutting.csproj
│
├── ecom-cassandra.MigrationJob/                # Job to execute Migrations
│   ├── Program.cs
│   ├── jobsettings.json
│   └── ecom-cassandra.MigrationJob.csproj
│
└── README.md                                    # This file
```

---

## 🗂️ Domain Entities

### User
Represents an application user.

```csharp
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Address>? Addresses { get; set; }
}
```

**Available Roles:**
- `Admin` - Full system access
- `User` - Standard user

---

### Product
Represents a product available for sale.

```csharp
public class Product
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

---

### Category
Represents a product category.

```csharp
public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

---

### Order
Represents an order placed by a user.

```csharp
public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Address ShippingAddress { get; set; }
}
```

**Available Statuses:**
- `Pending` - Order awaiting processing
- `Confirmed` - Order confirmed
- `Shipped` - Order shipped
- `Delivered` - Order delivered
- `Cancelled` - Order cancelled

---

### OrderItem
Represents an item within an order.

```csharp
public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

---

### Address
Represents a user address.

```csharp
public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}
```

---

### Review
Represents a product review.

```csharp
public class Review
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

---

## 🚀 Getting Started

### Prerequisites

- **.NET 8.0** or higher
- **Apache Cassandra 3.11+** installed and running
- **Visual Studio 2022** or **Rider**
- **Windows PowerShell** or your preferred terminal

### Verify Installation

```powershell
# Check .NET version
dotnet --version

# Check if Cassandra is running
# Cassandra usually runs on port 9042
netstat -an | findstr :9042
```

---

## ⚙️ Installation and Configuration

### 1. Clone the Repository

```powershell
git clone https://github.com/your-user/ecom-cassandra.git
cd ecom-cassandra
```

### 2. Restore Dependencies

```powershell
dotnet restore
```

### 3. Configure Cassandra

Edit `appsettings.json` in the `ecom-cassandra.WebApi` project:

```json
{
  "ExternalServices": {
    "Cassandra": {
      "Username": "cassandra",
      "Password": "cassandra",
      "Host": "localhost",
      "Port": 9042,
      "Keyspace": "ecom_cassandra"
    }
  },
  "Jwt": {
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "SecretKey": "your-secret-key-with-at-least-32-characters",
    "TokenExpirationMinutes": 120
  }
}
```

### 4. Configure User Secrets (Recommended for production)

```powershell
cd ecom-cassandra.WebApi
dotnet user-secrets set "ExternalServices:Cassandra:Username" "cassandra"
dotnet user-secrets set "ExternalServices:Cassandra:Password" "cassandra"
dotnet user-secrets set "Jwt:SecretKey" "your-secret-key-with-at-least-32-characters"
```

### 5. Create Keyspace in Cassandra

Connect to Cassandra and execute:

```cql
CREATE KEYSPACE ecom_cassandra
WITH REPLICATION = {'class': 'SimpleStrategy', 'replication_factor': 1};
```

### 6. Run Migrations

```powershell
cd ecom-cassandra.MigrationJob
dotnet run
```

Or via dotnet:
```powershell
dotnet run --project ecom-cassandra.MigrationJob/ecom-cassandra.MigrationJob.csproj
```

---

## 📱 Running the Project

### Via Visual Studio

1. Open `ecom-cassandra.sln` in Visual Studio
2. Set `ecom-cassandra.WebApi` as the startup project
3. Press `F5` or click **Debug > Start Debugging**
4. The API will open at `https://localhost:7123` (port may vary)

### Via CLI

```powershell
cd ecom-cassandra.WebApi
dotnet run
```

### Via Rider

1. Open the project in JetBrains Rider
2. Click the **Run** button or press `Shift + F10`

### Access Swagger

The interactive documentation will be available at:
```
https://localhost:7123/swagger/index.html
```

### 🔑 Default Admin Credentials

After running the migrations, a default admin user will be created automatically with the following credentials:

```
Email: admin@admin.com
Password: admin1234admin
```

**Use these credentials to login on the authentication endpoint and get a JWT token to access protected endpoints.**

---

## 🔌 API Endpoints

### Users

#### Register New User
```
POST /api/v1/users/create
Content-Type: application/json

{
  "name": "John Silva",
  "email": "john@example.com",
  "password": "password@123"
}

Response: 200 OK
[
  "User registered successfully!"
]
```

#### Login
```
POST /api/v1/users/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "password@123"
}

Response: 200 OK
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 7200
}
```

#### List All Users (Admin Only)
```
GET /api/v1/users/get-all
Authorization: Bearer {token}

Response: 200 OK
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "John Silva",
    "email": "john@example.com",
    "role": "User",
    "createdAt": "2026-03-17T10:30:00Z"
  }
]
```

#### Update User Role (Admin Only)
```
PATCH /api/v1/users/update-role
Authorization: Bearer {token}
Content-Type: application/json

{
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "role": "Admin"
}

Response: 200 OK
[
  "Role updated successfully!"
]
```

---

### Products

#### Create Product
```
POST /api/v1/products/create
Authorization: Bearer {token}
Content-Type: application/json

{
  "categoryId": "550e8400-e29b-41d4-a716-446655440001",
  "name": "Dell Notebook",
  "description": "High-performance notebook",
  "price": 3500.00,
  "stockQuantity": 10
}

Response: 201 Created
```

#### List Products
```
GET /api/v1/products/get-all

Response: 200 OK
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440002",
    "categoryId": "550e8400-e29b-41d4-a716-446655440001",
    "name": "Dell Notebook",
    "description": "High-performance notebook",
    "price": 3500.00,
    "stockQuantity": 10,
    "createdAt": "2026-03-17T10:30:00Z"
  }
]
```

---

### Categories

#### Create Category
```
POST /api/v1/categories/create
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Electronics",
  "description": "Electronic products in general"
}

Response: 201 Created
```

#### List Categories
```
GET /api/v1/categories/get-all

Response: 200 OK
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "name": "Electronics",
    "description": "Electronic products in general",
    "createdAt": "2026-03-17T10:30:00Z"
  }
]
```

---

### Orders

#### Create Order
```
POST /api/v1/orders/create
Authorization: Bearer {token}
Content-Type: application/json

{
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "totalAmount": 3500.00,
  "shippingAddress": {
    "street": "Main Street, 123",
    "city": "New York",
    "state": "NY",
    "postalCode": "10001",
    "country": "USA"
  }
}

Response: 201 Created
```

#### List User Orders
```
GET /api/v1/orders/get-by-user/{userId}
Authorization: Bearer {token}

Response: 200 OK
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440003",
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "status": "Pending",
    "totalAmount": 3500.00,
    "createdAt": "2026-03-17T10:30:00Z"
  }
]
```

---

### Order Items

#### Add Item to Order
```
POST /api/v1/orderitems/create
Authorization: Bearer {token}
Content-Type: application/json

{
  "orderId": "550e8400-e29b-41d4-a716-446655440003",
  "productId": "550e8400-e29b-41d4-a716-446655440002",
  "quantity": 2,
  "unitPrice": 1750.00
}

Response: 201 Created
```

---

## 🔐 Authentication and Authorization

### How It Works

1. **Login**: Send your credentials (email and password) to the login endpoint
2. **JWT Token**: If valid, the server returns a JWT token
3. **Authorization**: Include the token in the `Authorization: Bearer {token}` header
4. **Validation**: The server validates the token before processing the request

### Usage Example

```bash
# 1. Login
curl -X POST https://localhost:7123/api/v1/users/login \
  -H "Content-Type: application/json" \
  -d '{"email": "admin@admin.com", "password": "admin1234admin"}'

# 2. Use the Token
curl -X GET https://localhost:7123/api/v1/users/get-all \
  -H "Authorization: Bearer your-token-here"
```

### Roles and Permissions

| Endpoint | Public | User | Admin |
|----------|--------|------|-------|
| POST /users/create | ✅ | ✅ | ✅ |
| POST /users/login | ✅ | ✅ | ✅ |
| GET /users/get-all | ❌ | ❌ | ✅ |
| PATCH /users/update-role | ❌ | ❌ | ✅ |
| GET /products/get-all | ✅ | ✅ | ✅ |
| POST /products/create | ❌ | ❌ | ✅ |
| POST /orders/create | ❌ | ✅ | ✅ |

---

## 🔄 Migrations

Migrations are versioned by date in `YYYYMMDD` format.

### Existing Migrations

| Date | Description |
|------|-------------|
| 20260202 | Create User and Address tables |
| 20260203 | Create Order, Product, OrderItem and Review tables |
| 20260203 | Add Roles column to User |
| 20260215 | Add Email index on User |
| 20260223 | Insert default Admin user |

### Run Migrations

```powershell
cd ecom-cassandra.MigrationJob
dotnet run
```

### Create New Migration

1. Create a new file in `ecom-cassandra.Infrastructure/Migrations/`
2. Implement the `IMigration` interface
3. Run the MigrationJob again

---

## 📋 Detailed Folder Structure

### Application Layer
- **UseCases** - Commands and Queries (CQRS)
- **Mapping** - Mapping configurations (Mapster)

### Domain Layer
- **Entities** - Business models
- **Enums** - Enumerated types
- **Interfaces** - Repository and security contracts

### Infrastructure Layer
- **Repositories** - Data access implementations
- **Session** - Cassandra connection management
- **Migrations** - Database migration scripts
- **Security** - Hash and JWT implementations

### DependencyInjection Layer
- **Ioc.cs** - Service registration
- **Cassandra.cs** - Cassandra configuration
- **Jwt.cs** - Authentication configuration
- **Swagger.cs** - Documentation configuration
- **Mapster.cs** - Mapping configuration
- **MediatR.cs** - CQRS configuration

---

## 🎨 Design Patterns

### CQRS (Command Query Responsibility Segregation)
Separates read operations (Queries) from write operations (Commands).

### Repository Pattern
Abstracts data access through interfaces.

### Dependency Injection
All services are registered in the IoC container.

### Data Transfer Objects (DTOs)
Separates domain models from API models.

### Fluent Results
Returns structured results with messages.

---

## 🔧 Technologies and Versions

| Technology | Version |
|-----------|---------|
| .NET | 8.0 |
| ASP.NET Core | 8.0 |
| Cassandra.Client | Latest |
| MediatR | Latest |
| Mapster | Latest |
| Swashbuckle.AspNetCore | 6.4+ |

---

## ⚠️ Troubleshooting

### Error: "Unable to connect to Cassandra"

```powershell
# Check if Cassandra is running
netstat -an | findstr :9042

# Check configurations in appsettings.json
```

### Error: "Invalid token"

- Check if the token has expired
- Check if it's in the format `Bearer {token}`
- Check if the secret key is correct

### Error: "Unauthorized"

- Login first to get a token
- Include the token in the `Authorization: Bearer {token}` header
- Check if your role has permission for the resource

---

## 📝 License

This project is licensed under the MIT License.

---

## 🔗 Useful Links

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Apache Cassandra Documentation](https://cassandra.apache.org/doc/latest/)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [Mapster Documentation](https://github.com/MapsterMapper/Mapster)
- [JWT.io](https://jwt.io/)

---

**Last Updated:** March 17, 2026

Developed with ❤️ using .NET 8 and Apache Cassandra
