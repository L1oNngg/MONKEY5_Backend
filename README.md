# MONKEY5 Backend - Local Setup Guide

This README provides instructions for setting up and running the MONKEY5 Backend application locally, particularly if cloud deployment encounters issues.

## Prerequisites

- .NET 9 SDK
- SQL Server (Developer or Express edition)
- Visual Studio or Visual Studio Code
- Git

## Getting Started

### 1. Clone the Repository

```sh
git clone https://github.com/L1oNngg/MONKEY5_Backend.git
cd MONKEY5_Backend
```

### 2. Configure the Database Connection

Open `MONKEY5_API/appsettings.json` and update the connection string to point to your local SQL Server instance:

For SQL Server Authentication:

```json
{
  "ConnectionStrings": {
    "MyDB": "server=YOUR_SERVER_NAME; database=MONKEY5; uid=YOUR_USERNAME; pwd=YOUR_PASSWORD; TrustServerCertificate=True"
  }
}
```

### 3. Install Entity Framework Core Tools

If you haven't already installed the EF Core tools, run:

```sh
dotnet tool install --global dotnet-ef
```

Or update them if already installed:

```sh
dotnet tool update --global dotnet-ef
```

### 4. Create and Apply Database Migrations

Run these commands from the root directory of the project:

```sh
# Navigate to DataAccessObjects project to create the migration
cd .\DataAccessObjects\
dotnet ef migrations add InitialCreate

# Return to the root directory
cd ..

# Navigate to the API project to apply the migration
cd .\MONKEY5_API\
dotnet ef database update --project "..\DataAccessObjects\DataAccessObjects.csproj" --startup-project ".\MONKEY5_API.csproj"
```

This will:

- Create a migration called `InitialCreate` that includes all your entity configurations
- Apply the migration to create the database schema
- Run any seed data methods defined in the `MyDbContext.OnModelCreating` method

### 5. Run the Application

From the `MONKEY5_API` directory:

```sh
dotnet run
```

Or from the root directory:

```sh
dotnet run --project .\MONKEY5_API\MONKEY5_API.csproj
```

The application should start and be accessible at:

- **API:** `https://localhost:7000` (or the port specified in your `launchSettings.json`)
- **Swagger UI:** `https://localhost:7000/swagger`

## Troubleshooting

### Migration Issues

If you encounter migration errors:

Delete existing migrations and start fresh:

```sh
cd .\DataAccessObjects\
del /s /q Migrations\*
dotnet ef migrations add InitialCreate
cd ..\MONKEY5_API\
dotnet ef database update --project "..\DataAccessObjects\DataAccessObjects.csproj" --startup-project ".\MONKEY5_API.csproj"
```

Drop and recreate the database:

```sh
cd .\MONKEY5_API\
dotnet ef database drop --project "..\DataAccessObjects\DataAccessObjects.csproj" --startup-project ".\MONKEY5_API.csproj" --force
dotnet ef database update --project "..\DataAccessObjects\DataAccessObjects.csproj" --startup-project ".\MONKEY5_API.csproj"
```

### Connection String Issues

If you can't connect to the database:

- Verify SQL Server is running
- Check that the server name is correct
- Ensure the user has appropriate permissions
- Try using `localhost` or `(localdb)\MSSQLLocalDB` for the server name

### Missing NuGet Packages

If you're missing required packages:

```sh
dotnet restore
```

## Database Schema

The MONKEY5 database includes the following main entities:

- `Users` (base class with inheritance)
  - `Customers`
  - `Staff`
  - `Managers`
- `Locations`
- `Services`
- `Bookings`
- `Payments`
- `Refunds`
- `Reviews`
- `CompletionReports`
- `ReportImages`

The relationships and configurations are defined in the `MyDbContext.OnModelCreating` method.

## API Documentation

Once the application is running, you can access the Swagger UI documentation at `/swagger` to explore and test all available API endpoints.
