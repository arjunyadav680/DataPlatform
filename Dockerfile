# --- Build stage: dotnet SDK + install Node (needed so dotnet publish can build Angular) ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# install curl (if missing) and Node 20 (Node required for dotnet publish to build Angular)
RUN apt-get update && apt-get install -y curl ca-certificates gnupg \
  && curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
  && apt-get install -y nodejs \
  && node --version && npm --version

# copy solution and project files for restore
COPY *.sln ./
COPY DataPlatform.API/*.csproj ./DataPlatform.API/
COPY DataPlatform.Application/*.csproj ./DataPlatform.Application/
COPY DataPlatform.Infrastructure/*.csproj ./DataPlatform.Infrastructure/
COPY DataPlatform.Shared/*.csproj ./DataPlatform.Shared/

RUN dotnet restore

# copy all source files
COPY . .

# run dotnet publish (this will invoke your configured Angular build and require node)
WORKDIR /src/DataPlatform.API
RUN dotnet publish -c Release -o /app/publish

# --- Runtime stage: smaller image ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Set default port (can be overridden by docker-compose)
ENV APP_INTERNAL_PORT=80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_PATHBASE=""

COPY --from=build /app/publish .

# Expose default port (docker-compose will override this)
EXPOSE 80

ENTRYPOINT ["dotnet","DataPlatform.API.dll"]
