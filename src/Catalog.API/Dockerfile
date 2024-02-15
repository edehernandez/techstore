#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["/Catalog.API/Catalog.API.csproj", "src/Catalog.API/"]
COPY ["/Catalog.Domain/Catalog.Domain.csproj", "src/Catalog.Domain/"]
COPY ["/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "src/Catalog.Infrastructure/"]
RUN dotnet restore "./src/Catalog.API/Catalog.API.csproj"
COPY . .

RUN dotnet build "src/Catalog.API/Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]