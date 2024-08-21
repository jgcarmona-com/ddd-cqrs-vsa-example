
# Proyecto de Ejemplo - Arquitectura DDD con CQRS y Mediator

Este proyecto es una implementación de una arquitectura basada en DDD (Domain-Driven Design) con patrones de CQRS (Command Query Responsibility Segregation) y Mediator. Es una réplica sencilla de un sistema de preguntas y respuestas, similar a Stack Overflow, usando .NET Core, EF Core y otras herramientas modernas.

## Estructura del Proyecto

El proyecto sigue un diseño modular organizado en los siguientes ensamblados:

- **Jgcarmona.Qna.Domain**: Contiene las entidades de dominio y las reglas de negocio.
- **Jgcarmona.Qna.Domain.Abstract**: Define las interfaces para los servicios y repositorios del dominio.
- **Jgcarmona.Qna.Application**: Contiene la lógica de la aplicación, incluyendo los comandos y consultas (CQRS).
- **Jgcarmona.Qna.Persistence.EntityFramework**: Implementación concreta de los repositorios usando Entity Framework Core.
- **Jgcarmona.Qna.Api.Web**: La API principal para exponer los endpoints.
- **Jgcarmona.Qna.Api.Extensions**: Contiene extensiones y configuraciones globales reutilizables.

## Requisitos

- **Docker**: Para lanzar una instancia local de SQL Server.
- **.NET SDK 6+**: Para compilar y ejecutar el proyecto.
- **Visual Studio Code o Visual Studio**: Recomendado para el desarrollo.

## Configuración del Entorno

1. **Clonar el repositorio**:

    ```bash
    git clone https://github.com/tu-usuario/tu-repositorio.git
    cd tu-repositorio
    ```

2. **Lanzar SQL Server usando Docker**:

    Ejecuta el siguiente comando para iniciar una instancia de SQL Server en Docker:

    ```bash
    docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong@Passw0rd' -p 1433:1433 --name sqlserver -v ~/docker-volumes/sqlserver-data:/var/opt/mssql -u root -d mcr.microsoft.com/mssql/server:2022-latest
    ```

    Esto lanzará una instancia de SQL Server en el puerto 1433. Asegúrate de que la contraseña proporcionada sea segura y complazca las políticas de seguridad de SQL Server.

3. **Configuración de la Base de Datos**:

    El proyecto está configurado para utilizar esta instancia de SQL Server local. Asegúrate de que la cadena de conexión en `appsettings.json` de la API Web esté correctamente configurada:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost,1433;Database=QnADb;User Id=sa;Password=YourStrong@Passw0rd;"
    }
    ```

4. **Aplicar las Migraciones**:

    Antes de ejecutar la aplicación, aplica las migraciones para crear la base de datos:

    ```bash
    dotnet ef database update --project src/Jgcarmona.Qna.Persistence.EntityFramework
    ```

5. **Ejecutar la Aplicación**:

    Inicia la aplicación:

    ```bash
    dotnet run --project src/Jgcarmona.Qna.Api.Web
    ```

    Esto iniciará la API en `https://localhost:5001`.

## Características Principales

- **Diseño Modular**: Separación clara entre capas de dominio, aplicación e infraestructura.
- **CQRS con Mediator**: Uso de MediatR para manejar comandos y consultas de forma desacoplada.
- **Entity Framework Core**: Para la persistencia de datos usando un contexto de SQL Server.
- **Soporte para Docker**: Uso de SQL Server en un contenedor para facilitar la configuración y despliegue.

## Próximos Pasos

El proyecto sigue en desarrollo y se añadirán nuevas funcionalidades, así como una mayor cobertura de pruebas. Se espera añadir soporte para autenticación con JWT, validaciones avanzadas y mejoras en la documentación.
