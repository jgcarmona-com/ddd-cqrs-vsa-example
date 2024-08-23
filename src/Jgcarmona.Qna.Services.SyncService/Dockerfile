FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Jgcarmona.Qna.Services.SyncService/Jgcarmona.Qna.Services.SyncService.csproj", "src/Jgcarmona.Qna.Services.SyncService/"]
COPY ["src/Jgcarmona.Qna.Common.Configuration/Jgcarmona.Qna.Common.Configuration.csproj", "src/Jgcarmona.Qna.Common.Configuration/"]
COPY ["src/Jgcarmona.Qna.Infrastructure.Extensions/Jgcarmona.Qna.Infrastructure.Extensions.csproj", "src/Jgcarmona.Qna.Infrastructure.Extensions/"]
COPY ["src/Jgcarmona.Qna.Domain.Abstract/Jgcarmona.Qna.Domain.Abstract.csproj", "src/Jgcarmona.Qna.Domain.Abstract/"]
COPY ["src/Jgcarmona.Qna.Domain/Jgcarmona.Qna.Domain.csproj", "src/Jgcarmona.Qna.Domain/"]
COPY ["src/Jgcarmona.Qna.Infrastructure.EventDispatchers/Jgcarmona.Qna.Infrastructure.EventDispatchers.csproj", "src/Jgcarmona.Qna.Infrastructure.EventDispatchers/"]
COPY ["src/Jgcarmona.Qna.Infrastructure.Messaging/Jgcarmona.Qna.Infrastructure.Messaging.csproj", "src/Jgcarmona.Qna.Infrastructure.Messaging/"]
RUN dotnet restore "./src/Jgcarmona.Qna.Services.SyncService/Jgcarmona.Qna.Services.SyncService.csproj"
COPY . .
WORKDIR "/src/src/Jgcarmona.Qna.Services.SyncService"
RUN dotnet build "./Jgcarmona.Qna.Services.SyncService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Jgcarmona.Qna.Services.SyncService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jgcarmona.Qna.Services.SyncService.dll"]