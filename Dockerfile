# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia el archivo .csproj y restaura las dependencias
COPY Backend/Backend.csproj ./Backend/
WORKDIR /app/Backend
RUN dotnet restore

# Copia el resto del código fuente y publica la aplicación
WORKDIR /app
COPY . .  # Copia el resto del código fuente
RUN dotnet publish Backend/Backend.csproj -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]
