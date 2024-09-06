
# EF Core Migrations Guide

This guide provides instructions on how to manage Entity Framework Core (EF Core) migrations in this multi-project solution. The example assumes the usage of the `dotnet ef` CLI to manage database schema changes through migrations.

## Adding a New Migration

To add a new migration, use the following command:

```bash
dotnet ef migrations add <MigrationName> --startup-project ../Jgcarmona.Qna.Api --output-dir Persistence/Sql/Migrations
```

### Explanation of the Command:

- `<MigrationName>`: Replace this placeholder with a meaningful name that reflects the changes in your migration (e.g., `InitialCreate`, `AddNewTable`).
- `--startup-project ../Jgcarmona.Qna.Api`: Specifies the startup project, which contains the `Program.cs` file and configuration for your API.
- `--output-dir Persistence/Sql/Migrations`: Defines where the migration files will be placed. In this case, it is under the `Persistence/Sql/Migrations` folder of the project.

## Applying Migrations

To apply the migrations to your database, use the following command:

```bash
dotnet ef database update --startup-project ../Jgcarmona.Qna.Api
```

This will apply all pending migrations to the database specified in your `DbContext`.

## Removing a Migration

If you made a mistake in the last migration, you can remove it using:

```bash
dotnet ef migrations remove --startup-project ../Jgcarmona.Qna.Api
```

This command removes the last migration that has not yet been applied to the database.

## Updating Database After Code Changes

After making changes to your domain models or configurations, follow these steps:

1. Add a new migration using the `dotnet ef migrations add` command described above.
2. Apply the migration to the database using `dotnet ef database update`.

## Common Issues

- **Project not found**: Ensure that the `--startup-project` option correctly points to the project containing your `Program.cs` file.
- **Migrations folder not found**: If the specified `--output-dir` does not exist, make sure to create the folder or correct the path.
- **Out of sync migrations**: If there are issues with migrations not being in sync with the database, you may need to revert or remove migrations using the `dotnet ef migrations remove` command.

## Best Practices

- Always name migrations meaningfully to reflect the changes introduced (e.g., `AddCustomerTable` or `UpdateOrderSchema`).
- Review generated migration files to ensure they reflect the intended changes before applying them to the database.
- Use version control to track migration files.

For more details on using EF Core migrations, refer to the [official documentation](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/).

