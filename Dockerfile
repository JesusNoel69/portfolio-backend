FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /webapp

EXPOSE 80
EXPOSE 5024

COPY /Backend/Backend.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o publish

FROM  mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /webapp
COPY --from=build /webapp/publish .
ENTRYPOINT [ "dotnet", "Backend.dll"]
