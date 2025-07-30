# CleanArchitectureBlazorInteractiveAuto

A modern .NET 9 solution using [Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/), featuring a Blazor WebAssembly client, Blazor Server, API, and automated SQL Server provisioning with clean architecture principles.

## Features

- **Blazor WebAssembly Client**: Interactive UI with MudBlazor.
- **Blazor Server**: Server-side rendering and API gateway.
- **API Project**: ASP.NET Core Web API for backend logic.
- **Automated SQL Server**: Local SQL Server container with automatic database creation and migrations using DbUp.
- **Service Defaults**: Health checks, OpenTelemetry, and service discovery.
- **Clean Architecture**: Clear separation of Application, Domain, and Infrastructure.

## Project Structure

•	Presentation.Client      # Blazor WebAssembly UI
•	Presentation.Server      # Blazor Server host & API gateway
•	Presentation.API         # ASP.NET Core Web API
•	WeatherDbBuilder         # Database migrations (DbUp)
•	Application, Domain, Infrastructure # Core business logic and data access
•	CleanArchitectureBlazorInteractiveAuto.AppHost # Aspire app host (orchestration)


## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/) (for local SQL Server)
- (Optional) Visual Studio 2022

### Run the Solution

1. **Restore & Build**
    ```sh
    dotnet restore
    dotnet build
    ```

2. **Start the Aspire App Host**
    ```sh
    dotnet run --project CleanArchitectureBlazorInteractiveAuto.AppHost
    ```
    This will:
    - Launch the SQL Server container
    - Run database migrations via `WeatherDbBuilder`
    - Start API, Server, and Client projects

3. **Browse the App**
    - Blazor Client: http://localhost:{PORT}
    - API: http://localhost:{PORT}/swagger
    - Health checks: `/health` and `/alive`

### Database

- SQL Server runs in a Docker container.
- Migrations are applied automatically at startup via `WeatherDbBuilder/scripts/*.sql`.

### Configuration

- Connection strings are managed by Aspire and do **not** need to be set in `appsettings.json`.
- For custom settings, use the appropriate `appsettings.*.json` files.

## Development

- **Business logic**: `Application`, `Domain`, `Infrastructure`
- **UI**: `Presentation.Client` (Blazor WASM), `Presentation.Server` (Blazor Server)
- **Database scripts**: `WeatherDbBuilder/scripts/`

## Health & Telemetry

- Health checks and OpenTelemetry are enabled by default.
- See `/health` and `/alive` endpoints.

## References

- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [Blazor WebAssembly](https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-9.0)
- [DbUp](https://dbup.readthedocs.io/en/latest/)

