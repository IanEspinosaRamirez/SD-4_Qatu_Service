# Backend Application (ASP.NET Core 8)

This is a backend application built using .NET 8, utilizing Entity Framework Core for database access, with an architecture structured around layers including Infrastructure, Domain, Application, and Web.Api.

## Prerequisites

To run this project, ensure you have the following tools installed:

- **.NET 8 SDK**  
   Install from: [https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

- **Entity Framework Core Tools**  
   Install with the following command:

  ```bash
  dotnet tool install --global dotnet-ef
  ```

- **Database Provider**  
   Ensure the database provider you are using (MySQL) is correctly set up in the connection string in your `appsettings.json`.

## Getting Started

### Clone the Repository

Clone the project to your local machine using SSH:

```bash
git clone git@gitlab.com:jala-university1/cohort-1/oficial-es-desarrollo-de-software-4-iso-223.ga.t2.24.m2/secci-n-b/group-c/qatu-service.git

cd qatu-service
```

Or using HTTPS:

```bash
git https://gitlab.com/jala-university1/cohort-1/oficial-es-desarrollo-de-software-4-iso-223.ga.t2.24.m2/secci-n-b/group-c/qatu-service.git

cd qatu-service
```

### Setup Environment

Navigate to the Web.Api directory:

```bash
cd Web.Api
```

Install the required packages:

```bash
dotnet restore
```

Configure the necessary environment variables or update the `appsettings.json` file with your database connection string.

## DB Configuration

Ensure your `appsettings.json` file in the [Web.Api](Web.Api/appsettings.json) directory is configured correctly. Below is an example configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=qatu;Uid=[username];Pwd=[password];"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Jwt": {
    "Key": "1883d62ffed6f8c663d90d0f3579d4942c7f0ef8908c1c8a2ac33777dbe5732fe3294da1008d16c8f204b71988e2fe7481de14a2947999e8de954043c089234f44b1fecf3f793aa22eef1d53c24905aa03176326389d3200142a56ceb7704bdb5d445ebf5321cf5f0697e8f12801bd7face1ac63d511fa31db6a4bdea26791d4321ae344edbe028e1532ed91ba66882694b28d5d4978cb24950d29e35caa4d45b128c3202822cb41da3c94d12068e293285a3a74e71ac3d91da225feab4ac1da69504a7bfc6acc447f3a93e2d6def3807b3c826be223d44856a9c9b34eb92aa837698df0a9e27b232fcf91e615f3aeab2d164aa2ffd431b91407f4a14164bef8",
    "Issuer": "Qatu",
    "Audience": "SD4 - Finnisimo"
  },
  "AllowedHosts": "*"
}
```

### Install Entity Framework Core CLI

You will need to install the `dotnet-ef` tool globally to manage migrations:

Install `dotnet-ef`:

```bash
dotnet tool install --global dotnet-ef
```

Verify the installation:

```bash
dotnet ef --version
```

### Install Entity Framework Core CLI

You will need to install the `dotnet-ef` tool globally to manage migrations:

Install `dotnet-ef`:

```bash
dotnet tool install --global dotnet-ef
```

Verify the installation:

```bash
dotnet ef --version
```

### Running the Application

To run the backend application, execute the following command from the Web.Api directory:

```bash
dotnet run
```

This will start the application and make it available on `http://localhost:{PORT}`.

## Running Migrations

To set up your database and run migrations, follow these steps:

1. **Navigate to the project root (or stay in the current directory):**

   ```bash
   cd qatu-service
   ```

2. **Add a migration:**

   ```bash
   dotnet ef migrations add initialMigration -p Infrastructure -s Web.Api -o Persistence/Migrations
   ```

3. **Apply the migration to your database:**
   ```bash
   dotnet ef database update -p Infrastructure -s Web.Api
   ```

## Additional Commands

- **Build the application:**

  ```bash
  dotnet build
  ```

- **Run tests:** Ensure that all tests pass before pushing changes:
  ```bash
  dotnet test
  ```

## Project Structure

The application follows a Hexagonal Architecture with the following layers:

- **Web.Api**: The entry point for the application (controllers, endpoints).
- **Application**: Contains the business logic (CQRS commands, handlers).
- **Domain**: Core domain entities, aggregates, and domain services.
- **Infrastructure**: Database, external services integration (repositories, Entity Framework, etc.).
