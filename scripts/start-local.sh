#!/bin/bash

# Start the containers
docker-compose up -d

# Run database migrations (opcional)
docker exec -it sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" -Q "CREATE DATABASE [QnADB];"
