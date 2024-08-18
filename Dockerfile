FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Backend/Backend.csproj ./Backend/
WORKDIR /app/Backend
RUN dotnet restore

WORKDIR /app
COPY . . 
RUN dotnet publish Backend/Backend.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]
