# 1. Build mərhələsi
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Layihə fayllarını kopyala və bərpa et
COPY *.sln .
COPY EmailSnederAPI/*.csproj ./EmailSnederAPI/
RUN dotnet restore

# Qalan faylları da əlavə et və build et
COPY . .
WORKDIR /app/EmailSnederAPI
RUN dotnet publish -c Release -o out

# 2. Run mərhələsi
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/EmailSnederAPI/out ./
ENTRYPOINT ["dotnet", "EmailSnederAPI.dll"]
