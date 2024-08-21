
# QnA Project

This is a .NET 6 project following a Domain-Driven Design (DDD) and CQRS architecture pattern.

## Getting Started

### Prerequisites

- .NET 6 SDK
- Docker
- SQL Server

### Running the Project

1. Clone this repository.
2. Make sure you have Docker installed and running.
3. Use the following command to start a SQL Server instance with Docker:

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong@Passw0rd' -p 1433:1433 --name sqlserver -v ~/docker-volumes/sqlserver-data:/var/opt/mssql -u root -d mcr.microsoft.com/mssql/server:2022-latest
```

4. Open the solution in Visual Studio or your preferred IDE.
5. Build the solution.
6. Run the project.

### Project Structure

This project follows a modular monolith approach with the following main layers:

- **Api.Web**: The main API project using a layered architecture.
- **Api.Extensions**: Extensions for configuring services like MediatR and global exception handling.
- **Application**: The core application logic, including CQRS commands and queries.
- **Domain**: The domain layer containing entities, value objects, and domain services.
- **Domain.Abstract**: Abstractions and interfaces for repositories and domain services.
- **Persistence.EntityFramework**: EF Core implementations for repository patterns and DB context.

### Features

- Modular Monolith with CQRS pattern.
- Clean architecture principles.
- Global exception handling with MediatR.
- Dependency injection setup for domain and application layers.

### Database Setup

Ensure the `appsettings.json` file is configured to point to your SQL Server instance. The connection string should look like this:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=QnADb;User Id=sa;Password=YourStrong@Passw0rd;"
}
```

### Contributing

Feel free to fork this project and create pull requests with your improvements.

### License

This project is licensed under the MIT License.
