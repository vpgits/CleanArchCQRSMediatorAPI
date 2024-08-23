#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CleanArchCQRSMediatorAPI.API/CleanArchCQRSMediatorAPI.API.csproj", "CleanArchCQRSMediatorAPI.API/"]
COPY ["CleanArchCQRSMediatorAPI.Application/CleanArchCQRSMediatorAPI.Application.csproj", "CleanArchCQRSMediatorAPI.Application/"]
COPY ["CleanArchCQRSMediatorAPI.Domain/CleanArchCQRSMediatorAPI.Domain.csproj", "CleanArchCQRSMediatorAPI.Domain/"]
COPY ["CleanArchCQRSMediatorAPI.Persistence/CleanArchCQRSMediatorAPI.Persistence.csproj", "CleanArchCQRSMediatorAPI.Persistence/"]
RUN dotnet restore "./CleanArchCQRSMediatorAPI.API/CleanArchCQRSMediatorAPI.API.csproj"
COPY . .
WORKDIR "/src/CleanArchCQRSMediatorAPI.API"
RUN dotnet build "./CleanArchCQRSMediatorAPI.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArchCQRSMediatorAPI.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchCQRSMediatorAPI.API.dll"]