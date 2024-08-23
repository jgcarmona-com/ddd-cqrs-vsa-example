#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Jgcarmona.Qna.Services.StatService/Jgcarmona.Qna.Services.StatService.csproj", "src/Jgcarmona.Qna.Services.StatService/"]
RUN dotnet restore "./src/Jgcarmona.Qna.Services.StatService/Jgcarmona.Qna.Services.StatService.csproj"
COPY . .
WORKDIR "/src/src/Jgcarmona.Qna.Services.StatService"
RUN dotnet build "./Jgcarmona.Qna.Services.StatService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Jgcarmona.Qna.Services.StatService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jgcarmona.Qna.Services.StatService.dll"]