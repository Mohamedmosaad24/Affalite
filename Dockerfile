FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first for layer caching
COPY AffalitePL.sln ./
COPY AffalitePL/AffalitePL.csproj AffalitePL/
COPY AffaliteBL/AffaliteBL.csproj AffaliteBL/
COPY AffaliteDAL/AffaliteDAL.csproj AffaliteDAL/

# Restore dependencies
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish AffalitePL/AffalitePL.csproj -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Railway provides PORT env var; ASP.NET must bind to it
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}
EXPOSE 8080

ENTRYPOINT ["dotnet", "AffalitePL.dll"]
