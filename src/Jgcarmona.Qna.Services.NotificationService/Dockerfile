#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Jgcarmona.Qna.Services.NotificationService/Jgcarmona.Qna.Services.NotificationService.csproj", "src/Jgcarmona.Qna.Services.NotificationService/"]
COPY ["src/Jgcarmona.Qna.Domain/Jgcarmona.Qna.Domain.csproj", "src/Jgcarmona.Qna.Domain/"]
COPY ["src/Jgcarmona.Qna.Infrastructure/Jgcarmona.Qna.Infrastructure.csproj", "src/Jgcarmona.Qna.Infrastructure/"]
COPY ["src/Jgcarmona.Qna.Common/Jgcarmona.Qna.Common.csproj", "src/Jgcarmona.Qna.Common/"]
RUN dotnet restore "./src/Jgcarmona.Qna.Services.NotificationService/Jgcarmona.Qna.Services.NotificationService.csproj"
COPY . .
WORKDIR "/src/src/Jgcarmona.Qna.Services.NotificationService"
RUN dotnet build "./Jgcarmona.Qna.Services.NotificationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Jgcarmona.Qna.Services.NotificationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jgcarmona.Qna.Services.NotificationService.dll"]