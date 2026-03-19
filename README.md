# E-commerce Cassandra API

Uma API RESTful moderna construída com **.NET 8** e **Apache Cassandra** para um sistema de e-commerce escalável e robusto. Este projeto implementa padrões de arquitetura limpa com CQRS (Command Query Responsibility Segregation) utilizando MediatR.

## 📋 Índice

- [Visão Geral](#visão-geral)
- [Stack Tecnológico](#stack-tecnológico)
- [Arquitetura](#arquitetura)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Entidades do Domínio](#entidades-do-domínio)
- [Primeiros Passos](#primeiros-passos)
- [Instalação e Configuração](#instalação-e-configuração)
- [Rodando o Projeto](#rodando-o-projeto)
- [Endpoints da API](#endpoints-da-api)
- [Autenticação e Autorização](#autenticação-e-autorização)
- [Migrations](#migrations)
- [Estrutura de Pastas](#estrutura-de-pastas)
- [Padrões de Design](#padrões-de-design)
- [Contribuindo](#contribuindo)

---

## 🎯 Visão Geral

Este projeto é uma API RESTful de e-commerce que utiliza **Apache Cassandra** como banco de dados NoSQL, oferecendo:

✅ **Escalabilidade Horizontal** - Cassandra suporta distribuição em múltiplos servidores  
✅ **Alta Disponibilidade** - Replicação de dados garantindo consistência  
✅ **Performance** - Leitura e escrita rápidas em larga escala  
✅ **Arquitetura Limpa** - Separação clara de responsabilidades  
✅ **CQRS Pattern** - Separação entre commands e queries  
✅ **Autenticação JWT** - Segurança baseada em tokens  
✅ **API Versionada** - Suporte a múltiplas versões da API  
✅ **Documentação Swagger** - Documentação interativa dos endpoints  

---

## 💻 Stack Tecnológico

### Backend Framework
- **ASP.NET Core 8.0** - Framework web moderno e de alta performance
- **.NET 8** - Última versão do runtime .NET

### Banco de Dados
- **Apache Cassandra** - Banco de dados NoSQL distribuído
- **Cassandra.Fluent.Migrator** - Gerenciamento de migrations para Cassandra

### Bibliotecas Principais
- **MediatR** - Implementação do padrão CQRS
- **Mapster** - Mapeamento de objetos (alternativa ao AutoMapper)
- **Asp.Versioning** - API Versioning
- **Swashbuckle.AspNetCore** - Integração Swagger/OpenAPI
- **System.IdentityModel.Tokens.Jwt** - Autenticação JWT
- **MrP.FluentResult** - Tratamento de resultados fluentes

### Segurança
- **BCrypt.Net** - Hash de senhas
- **JWT (JSON Web Tokens)** - Autenticação e autorização

---

## 🏗️ Arquitetura

O projeto segue a **arquitetura em camadas limpa** com o padrão **CQRS**:

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

### Camadas

1. **WebApi** - Camada de apresentação com controllers REST
2. **Application** - Lógica de negócio com CQRS e MediatR
3. **Domain** - Entidades de negócio e interfaces
4. **Infrastructure** - Acesso a dados e implementações
5. **DependencyInjection** - Configuração do container IoC
6. **CrossCutting** - Utilidades compartilhadas (mensagens, constantes)
7. **MigrationJob** - Ferramenta CLI para executar migrations do Cassandra

---

## 📂 Estrutura do Projeto

```
ecom-cassandra/
├── ecom-cassandra.sln                          # Solução Visual Studio
│
├── ecom-cassandra.WebApi/                      # API REST Layer
│   ├── Program.cs                              # Configuração da aplicação
│   ├── appsettings.json                        # Configurações da aplicação
│   ├── Controllers/                            # Endpoints da API
│   │   ├── UsersController.cs
│   │   ├── ProductsController.cs
│   │   ├── CategoriesController.cs
│   │   ├── OrdersController.cs
│   │   └── OrderItemsController.cs
│   └── Properties/
│
├── ecom-cassandra.Application/                 # Lógica de Negócio
│   ├── Mapping/                                # Mapeamentos (Mapster)
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
│   └── ApplicationMarker.cs                    # Marker class para DI
│
├── ecom-cassandra.Domain/                      # Camada de Domínio
│   ├── Entities/                               # Modelos de Domínio
│   │   ├── User.cs
│   │   ├── Product.cs
│   │   ├── Category.cs
│   │   ├── Order.cs
│   │   ├── OrderItem.cs
│   │   ├── Review.cs
│   │   └── Address.cs
│   ├── Enums/                                  # Enumerações
│   │   ├── UserRoles.cs
│   │   └── OrderStatus.cs
│   └── Interfaces/                             # Contratos
│       ├── Repositories/                       # Interfaces de Repositórios
│       └── Security/                           # Interfaces de Segurança
│
├── ecom-cassandra.Infrastructure/              # Camada de Dados
│   ├── Session/                                # Cassandra Session
│   │   └── CassandraSession.cs
│   ├── Config/                                 # Configurações
│   ├── Repositories/                           # Implementação dos Repositórios
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
│   ├── Security/                               # Implementações de Segurança
│   │   ├── HashSecurity.cs
│   │   └── JwtSecurity.cs
│   ├── OperationBatch.cs                       # Batch operations para Cassandra
│   └── ecom-cassandra.Infrastructure.csproj
│
├── ecom-cassandra.DependencyInjection/         # Configuração IoC
│   ├── Ioc.cs                                  # Registro de dependências
│   ├── Cassandra.cs                            # Configuração Cassandra
│   ├── Jwt.cs                                  # Configuração JWT
│   ├── Swagger.cs                              # Configuração Swagger
│   ├── Mapster.cs                              # Configuração Mapster
│   ├── MediatR.cs                              # Configuração MediatR
│   ├── Versioning.cs                           # Configuração de Versionamento
│   └── ecom-cassandra.DependencyInjection.csproj
│
├── ecom-cassandra.CrossCutting/                # Utilidades Compartilhadas
│   ├── Constants/
│   │   ├── ErrorMessage.cs                     # Mensagens de erro
│   │   └── SuccessMessage.cs                   # Mensagens de sucesso
│   └── ecom-cassandra.CrossCutting.csproj
│
├── ecom-cassandra.MigrationJob/                # Job para executar Migrations
│   ├── Program.cs
│   ├── jobsettings.json
│   └── ecom-cassandra.MigrationJob.csproj
│
└── README.md                                    # Este arquivo
```

---

## 🗂️ Entidades do Domínio

### User
Representa um usuário da aplicação.

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

**Roles disponíveis:**
- `Admin` - Acesso completo ao sistema
- `User` - Usuário padrão

---

### Product
Representa um produto disponível para venda.

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
Representa uma categoria de produtos.

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
Representa um pedido realizado por um usuário.

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

**Status disponíveis:**
- `Pending` - Pedido aguardando processamento
- `Confirmed` - Pedido confirmado
- `Shipped` - Pedido enviado
- `Delivered` - Pedido entregue
- `Cancelled` - Pedido cancelado

---

### OrderItem
Representa um item dentro de um pedido.

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
Representa um endereço de um usuário.

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
Representa uma avaliação de um produto.

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

## 🚀 Primeiros Passos

### Pré-requisitos

- **.NET 8.0** ou superior
- **Apache Cassandra 3.11+** instalado e rodando
- **Visual Studio 2022** ou **Rider**
- **Windows PowerShell** ou terminal de sua preferência

### Verificar Instalação

```powershell
# Verificar versão do .NET
dotnet --version

# Verificar se Cassandra está rodando
# Cassandra geralmente roda na porta 9042
netstat -an | findstr :9042
```

---

## ⚙️ Instalação e Configuração

### 1. Clonar o Repositório

```powershell
git clone https://github.com/seu-usuario/ecom-cassandra.git
cd ecom-cassandra
```

### 2. Restaurar Dependências

```powershell
dotnet restore
```

### 3. Configurar Cassandra

Editar `appsettings.json` no projeto `ecom-cassandra.WebApi`:

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
    "Issuer": "seu-issuer",
    "Audience": "seu-audience",
    "SecretKey": "sua-chave-super-secreta-com-pelo-menos-32-caracteres",
    "TokenExpirationMinutes": 120
  }
}
```

### 4. Configurar User Secrets (Recomendado para produção)

```powershell
cd ecom-cassandra.WebApi
dotnet user-secrets set "ExternalServices:Cassandra:Username" "cassandra"
dotnet user-secrets set "ExternalServices:Cassandra:Password" "cassandra"
dotnet user-secrets set "Jwt:SecretKey" "sua-chave-super-secreta-com-pelo-menos-32-caracteres"
```

### 5. Criar Keyspace no Cassandra

Conecte-se ao Cassandra e execute:

```cql
CREATE KEYSPACE ecom_cassandra
WITH REPLICATION = {'class': 'SimpleStrategy', 'replication_factor': 1};
```

### 6. Executar Migrations

```powershell
cd ecom-cassandra.MigrationJob
dotnet run
```

Ou via dotnet:
```powershell
dotnet run --project ecom-cassandra.MigrationJob/ecom-cassandra.MigrationJob.csproj
```

---

## 📱 Rodando o Projeto

### Via Visual Studio

1. Abra `ecom-cassandra.sln` no Visual Studio
2. Defina `ecom-cassandra.WebApi` como projeto de inicialização
3. Pressione `F5` ou clique em **Debug > Start Debugging**
4. A API abrirá em `https://localhost:7123` (porta pode variar)

### Via CLI

```powershell
cd ecom-cassandra.WebApi
dotnet run
```

### Via Rider

1. Abra o projeto no JetBrains Rider
2. Clique no botão **Run** ou pressione `Shift + F10`

### Acessar Swagger

A documentação interativa estará disponível em:
```
https://localhost:7123/swagger/index.html
```

### 🔑 Credenciais Padrão do Admin

Após executar as migrations, um usuário admin padrão será criado automaticamente com as seguintes credenciais:

```
Email: admin@admin.com
Senha: admin1234admin
```

**Use essas credenciais para fazer login no endpoint de autenticação e obter um token JWT para acessar os endpoints protegidos.**

---

## 🔌 Endpoints da API

### Users (Usuários)

#### Registrar Novo Usuário
```
POST /api/v1/users/create
Content-Type: application/json

{
  "name": "João Silva",
  "email": "joao@example.com",
  "password": "senha@123"
}

Response: 200 OK
[
  "Usuário registrado com sucesso!"
]
```

#### Login
```
POST /api/v1/users/login
Content-Type: application/json

{
  "email": "joao@example.com",
  "password": "senha@123"
}

Response: 200 OK
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 7200
}
```

#### Listar Todos os Usuários (Admin Only)
```
GET /api/v1/users/get-all
Authorization: Bearer {token}

Response: 200 OK
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "João Silva",
    "email": "joao@example.com",
    "role": "User",
    "createdAt": "2026-03-17T10:30:00Z"
  }
]
```

#### Atualizar Role do Usuário (Admin Only)
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
  "Role atualizado com sucesso!"
]
```

---

### Products (Produtos)

#### Criar Produto
```
POST /api/v1/products/create
Authorization: Bearer {token}
Content-Type: application/json

{
  "categoryId": "550e8400-e29b-41d4-a716-446655440001",
  "name": "Notebook Dell",
  "description": "Notebook de alto desempenho",
  "price": 3500.00,
  "stockQuantity": 10
}

Response: 201 Created
```

#### Listar Produtos
```
GET /api/v1/products/get-all

Response: 200 OK
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440002",
    "categoryId": "550e8400-e29b-41d4-a716-446655440001",
    "name": "Notebook Dell",
    "description": "Notebook de alto desempenho",
    "price": 3500.00,
    "stockQuantity": 10,
    "createdAt": "2026-03-17T10:30:00Z"
  }
]
```

---

### Categories (Categorias)

#### Criar Categoria
```
POST /api/v1/categories/create
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Eletrônicos",
  "description": "Produtos eletrônicos em geral"
}

Response: 201 Created
```

#### Listar Categorias
```
GET /api/v1/categories/get-all

Response: 200 OK
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "name": "Eletrônicos",
    "description": "Produtos eletrônicos em geral",
    "createdAt": "2026-03-17T10:30:00Z"
  }
]
```

---

### Orders (Pedidos)

#### Criar Pedido
```
POST /api/v1/orders/create
Authorization: Bearer {token}
Content-Type: application/json

{
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "totalAmount": 3500.00,
  "shippingAddress": {
    "street": "Rua Principal, 123",
    "city": "São Paulo",
    "state": "SP",
    "postalCode": "01234-567",
    "country": "Brasil"
  }
}

Response: 201 Created
```

#### Listar Pedidos do Usuário
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

### Order Items (Itens de Pedido)

#### Adicionar Item ao Pedido
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

## 🔐 Autenticação e Autorização

### Como Funciona

1. **Login**: Envie suas credenciais (email e senha) para o endpoint de login
2. **Token JWT**: Se válidas, o servidor retorna um JWT token
3. **Autorização**: Inclua o token no header `Authorization: Bearer {token}`
4. **Validação**: O servidor valida o token antes de processar a requisição

### Exemplo de Uso

```bash
# 1. Fazer Login
curl -X POST https://localhost:7123/api/v1/users/login \
  -H "Content-Type: application/json" \
  -d '{"email": "admin@example.com", "password": "senha@123"}'

# 2. Usar o Token
curl -X GET https://localhost:7123/api/v1/users/get-all \
  -H "Authorization: Bearer seu-token-aqui"
```

### Roles e Permissões

| Endpoint | Público | User | Admin |
|----------|---------|------|-------|
| POST /users/create | ✅ | ✅ | ✅ |
| POST /users/login | ✅ | ✅ | ✅ |
| GET /users/get-all | ❌ | ❌ | ✅ |
| PATCH /users/update-role | ❌ | ❌ | ✅ |
| GET /products/get-all | ✅ | ✅ | ✅ |
| POST /products/create | ❌ | ❌ | ✅ |
| POST /orders/create | ❌ | ✅ | ✅ |

---

## 🔄 Migrations

As migrations são versionadas por data no formato `YYYYMMDD`.

### Migrations Existentes

| Data | Descrição |
|------|-----------|
| 20260202 | Criar tabelas User e Address |
| 20260203 | Criar tabelas Order, Product, OrderItem e Review |
| 20260203 | Adicionar coluna Roles em User |
| 20260215 | Adicionar índice de Email em User |
| 20260223 | Inserir usuário Admin padrão |

### Executar Migrations

```powershell
cd ecom-cassandra.MigrationJob
dotnet run
```

### Criar Nova Migration

1. Crie um novo arquivo em `ecom-cassandra.Infrastructure/Migrations/`
2. Implemente a interface `IMigration`
3. Execute novamente o MigrationJob

---

## 📋 Estrutura de Pastas Detalhada

### Application Layer
- **UseCases** - Commands e Queries (CQRS)
- **Mapping** - Configurações de mapeamento (Mapster)

### Domain Layer
- **Entities** - Modelos de negócio
- **Enums** - Tipos enumerados
- **Interfaces** - Contratos de repositórios e segurança

### Infrastructure Layer
- **Repositories** - Implementação do acesso a dados
- **Session** - Gerenciamento da conexão com Cassandra
- **Migrations** - Scripts de migração do banco de dados
- **Security** - Implementações de hash e JWT

### DependencyInjection Layer
- **Ioc.cs** - Registro de serviços
- **Cassandra.cs** - Configuração do Cassandra
- **Jwt.cs** - Configuração de autenticação
- **Swagger.cs** - Configuração da documentação
- **Mapster.cs** - Configuração de mapeamento
- **MediatR.cs** - Configuração do CQRS

---

## 🎨 Padrões de Design

### CQRS (Command Query Responsibility Segregation)
Separa operações de leitura (Queries) das de escrita (Commands).

### Repository Pattern
Abstrai o acesso a dados através de interfaces.

### Dependency Injection
Todos os serviços são registrados no container IoC.

### Data Transfer Objects (DTOs)
Separam modelos de domínio dos modelos da API.

### Fluent Results
Retorna resultados estruturados com mensagens.

---

## 🔧 Tecnologias e Versões

| Tecnologia | Versão |
|-----------|--------|
| .NET | 8.0 |
| ASP.NET Core | 8.0 |
| Cassandra.Client | Última |
| MediatR | Última |
| Mapster | Última |
| Swashbuckle.AspNetCore | 6.4+ |

---

## ⚠️ Troubleshooting

### Erro: "Unable to connect to Cassandra"

```powershell
# Verificar se Cassandra está rodando
netstat -an | findstr :9042

# Verificar as configurações em appsettings.json
```

### Erro: "Invalid token"

- Verifique se o token expirou
- Verifique se está no formato `Bearer {token}`
- Verifique a chave secreta

### Erro: "Unauthorized"

- Faça login para obter um token
- Inclua o token no header `Authorization: Bearer {token}`
- Verifique se seu role tem permissão

---

## 📝 Licença

Este projeto está sob a licença MIT.

---


## 🔗 Links Úteis

- [Documentação ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/core/)
- [Documentação Apache Cassandra](https://cassandra.apache.org/doc/latest/)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [Mapster Documentation](https://github.com/MapsterMapper/Mapster)
- [JWT.io](https://jwt.io/)

---

**Última atualização:** 17 de Março de 2026

Desenvolvido com ❤️ usando .NET 8 e Apache Cassandra
