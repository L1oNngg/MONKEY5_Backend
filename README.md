# MONKEY5 Backend

This repository contains the backend implementation for the **MONKEY5** application, built with a 3-layer architecture using C# and PostgreSQL.

---

## 🏗 Architecture Overview

The application follows a clean 3-layer architecture:

- **Presentation Layer**: `MONKEY5_API` (REST API endpoints)
- **Business Layer**: `Services`, `Repositories`
- **Data Layer**: `DataAccessObjects`, `BusinessObjects`

---

## ⚙️ Technology Stack

- **.NET 9**: Core framework
- **Entity Framework Core**: ORM for database operations
- **PostgreSQL**: Primary database (deployed on Render)
- **Swagger/OpenAPI**: API documentation

---

## ✅ Prerequisites

- .NET 9 SDK
- PostgreSQL (for local development)
- Visual Studio or Visual Studio Code
- Git

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/L1oNngg/MONKEY5_Backend.git
cd MONKEY5_Backend
```

---

### 2. Configure the Database Connection

Open `MONKEY5_API/appsettings.json` and update the connection string:

#### For local PostgreSQL:

```json
{
  "ConnectionStrings": {
    "MyDB": "Host=localhost;Database=MONKEY5;Username=your_username;Password=your_password;"
  }
}
```

#### For Render PostgreSQL:

```json
{
  "ConnectionStrings": {
    "MyDB": "Host=your-postgres-instance.render.com;Database=MONKEY5;Username=your_username;Password=your_password;SSL Mode=Require;Trust Server Certificate=true;"
  }
}
```

---

### 3. Install Entity Framework Core Tools

```bash
dotnet tool install --global dotnet-ef
```

Or update if already installed:

```bash
dotnet tool update --global dotnet-ef
```

---

### 4. Create and Apply Database Migrations

```bash
# Navigate to DataAccessObjects project
cd .\DataAccessObjects\
dotnet ef migrations add InitialCreate

# Return to the root directory
cd ..

# Apply migrations from the API project
cd .\MONKEY5_API\
dotnet ef database update --project "..\DataAccessObjects\DataAccessObjects.csproj" --startup-project ".\MONKEY5_API.csproj"
```

---

### 5. Run the Application

```bash
dotnet run --project .\MONKEY5_API\MONKEY5_API.csproj
```

> The API will be accessible at:
>
> - API: https://localhost:7000
> - Swagger UI: https://localhost:7000/swagger

---

## 📁 Project Structure

- **BusinessObjects**: Contains domain entities and models
- **DataAccessObjects**: Contains database context and configurations
- **Repositories**: Contains repository implementations for data access
- **Services**: Contains business logic and service implementations
- **MONKEY5_API**: Contains API controllers and application configuration

---

## 🗃 Database Schema

The **MONKEY5** database includes the following main entities:

- Users (base class with inheritance)
- Customers
- Staff
- Managers
- Locations
- Services
- Bookings
- Payments
- Refunds
- Reviews
- CompletionReports
- ReportImages

---

## ☁️ Deployment

The application is configured to be deployed on **Render** with a PostgreSQL database.

### Render Deployment Steps

1. Create a PostgreSQL database service on Render
2. Create a Web Service for the API
3. Set the following environment variables:
   - `ConnectionStrings__MyDB`: Your PostgreSQL connection string
   - `ASPNETCORE_ENVIRONMENT`: Production

---

## 🧰 Troubleshooting

### Migration Issues

If you encounter migration errors:

```bash
# Delete existing migrations
cd .\DataAccessObjects\
del /s /q Migrations\*
dotnet ef migrations add InitialCreate

# Apply fresh migrations
cd ..\MONKEY5_API\
dotnet ef database update --project "..\DataAccessObjects\DataAccessObjects.csproj" --startup-project ".\MONKEY5_API.csproj"
```

---

### PostgreSQL Connection Issues

- Ensure PostgreSQL service is running
- Verify connection string parameters
- Check network connectivity to Render (for cloud deployment)
- Ensure SSL settings are correct for Render connections

---

### Missing NuGet Packages

```bash
dotnet restore
```

---

## 📖 API Documentation

Once the application is running, access the **Swagger UI** documentation at `/swagger` to explore and test all available API endpoints.
