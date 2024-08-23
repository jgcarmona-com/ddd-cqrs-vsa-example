#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Jgcarmona.Qna.Api.Web/Jgcarmona.Qna.Api.Web.csproj", "src/Jgcarmona.Qna.Api.Web/"]
COPY ["src/Jgcarmona.Qna.Api.Common/Jgcarmona.Qna.Api.Common.csproj", "src/Jgcarmona.Qna.Api.Common/"]
COPY ["src/Jgcarmona.Qna.Application/Jgcarmona.Qna.Application.csproj", "src/Jgcarmona.Qna.Application/"]
COPY ["src/Jgcarmona.Qna.Domain/Jgcarmona.Qna.Domain.csproj", "src/Jgcarmona.Qna.Domain/"]
COPY ["src/Jgcarmona.Qna.Domain.Abstract/Jgcarmona.Qna.Domain.Abstract.csproj", "src/Jgcarmona.Qna.Domain.Abstract/"]
COPY ["src/Jgcarmona.Qna.Common.Configuration/Jgcarmona.Qna.Common.Configuration.csproj", "src/Jgcarmona.Qna.Common.Configuration/"]
COPY ["src/Jgcarmona.Qna.Infrastructure.EventDispatchers/Jgcarmona.Qna.Infrastructure.EventDispatchers.csproj", "src/Jgcarmona.Qna.Infrastructure.EventDispatchers/"]
COPY ["src/Jgcarmona.Qna.Persistence.EntityFramework/Jgcarmona.Qna.Persistence.EntityFramework.csproj", "src/Jgcarmona.Qna.Persistence.EntityFramework/"]
COPY ["src/Jgcarmona.Qna.Infrastructure.Extensions/Jgcarmona.Qna.Infrastructure.Extensions.csproj", "src/Jgcarmona.Qna.Infrastructure.Extensions/"]
COPY ["src/Jgcarmona.Qna.Infrastructure.Messaging/Jgcarmona.Qna.Infrastructure.Messaging.csproj", "src/Jgcarmona.Qna.Infrastructure.Messaging/"]
RUN dotnet restore "./src/Jgcarmona.Qna.Api.Web/Jgcarmona.Qna.Api.Web.csproj"
COPY . .
WORKDIR "/src/src/Jgcarmona.Qna.Api.Web"
RUN dotnet build "./Jgcarmona.Qna.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Jgcarmona.Qna.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jgcarmona.Qna.Api.Web.dll"]