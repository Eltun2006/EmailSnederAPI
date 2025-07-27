# 1. Build mərhələsi üçün .NET 9 SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

COPY *.sln .
COPY EmailSnederAPI/*.csproj ./EmailSnederAPI/
RUN dotnet restore

COPY . .
WORKDIR /app/EmailSnederAPI
RUN dotnet publish -c Release -o out

# 2. Runtime üçün .NET 9 ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime
WORKDIR /app
COPY --from=build /app/EmailSnederAPI/out ./
ENTRYPOINT ["dotnet", "EmailSnederAPI.dll"]
