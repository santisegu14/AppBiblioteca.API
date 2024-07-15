#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
#ENV DB_CONNECTION_STRING=" Server=192.168.1.104,1433;Database=appBiblioteca2;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AppBiblioteca.API/AppBiblioteca.API.csproj", "AppBiblioteca.API/"]
RUN dotnet restore "./AppBiblioteca.API/AppBiblioteca.API.csproj"
COPY . .
WORKDIR "/src/AppBiblioteca.API"
RUN dotnet build "./AppBiblioteca.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AppBiblioteca.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppBiblioteca.API.dll"]