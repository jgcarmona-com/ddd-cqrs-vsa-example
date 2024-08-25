#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Jgcarmona.Qna.Services.StatsService/Jgcarmona.Qna.Services.StatsService.csproj", "src/Jgcarmona.Qna.Services.StatsService/"]
COPY ["src/Jgcarmona.Qna.Common/Jgcarmona.Qna.Common.csproj", "src/Jgcarmona.Qna.Common/"]
COPY ["src/Jgcarmona.Qna.Infrastructure.Extensions/Jgcarmona.Qna.Infrastructure.Extensions.csproj", "src/Jgcarmona.Qna.Infrastructure.Extensions/"]
COPY ["src/Jgcarmona.Qna.Domain.Abstract/Jgcarmona.Qna.Domain.Abstract.csproj", "src/Jgcarmona.Qna.Domain.Abstract/"]
COPY ["src/Jgcarmona.Qna.Domain/Jgcarmona.Qna.Domain.csproj", "src/Jgcarmona.Qna.Domain/"]
COPY ["src/Jgcarmona.Qna.Infrastructure.Messaging/Jgcarmona.Qna.Infrastructure.Messaging.csproj", "src/Jgcarmona.Qna.Infrastructure.Messaging/"]
COPY ["src/Jgcarmona.Qna.Infrastructure.Persistence.MongoDB/Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.csproj", "src/Jgcarmona.Qna.Infrastructure.Persistence.MongoDB/"]
COPY ["src/Jgcarmona.Qna.Infrastructure.Persistence.Sql/Jgcarmona.Qna.Infrastructure.Persistence.Sql.csproj", "src/Jgcarmona.Qna.Infrastructure.Persistence.Sql/"]
RUN dotnet restore "./src/Jgcarmona.Qna.Services.StatsService/Jgcarmona.Qna.Services.StatsService.csproj"
COPY . .
WORKDIR "/src/src/Jgcarmona.Qna.Services.StatsService"
RUN dotnet build "./Jgcarmona.Qna.Services.StatsService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Jgcarmona.Qna.Services.StatsService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jgcarmona.Qna.Services.StatsService.dll"]