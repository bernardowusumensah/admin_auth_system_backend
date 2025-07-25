FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything first to see where files actually end up
COPY . .

# Find all csproj files to see where they are
RUN echo "Finding all csproj files:" && find /src -name "*.csproj" 

# Determine the correct working directory dynamically
RUN PROJECT_PATH=$(find /src -name "UserIdentity.API.csproj" | head -1) && \
    if [ -n "$PROJECT_PATH" ]; then \
        PROJECT_DIR=$(dirname "$PROJECT_PATH") && \
        echo "Found project at: $PROJECT_PATH" && \
        echo "Setting working directory to: $PROJECT_DIR" && \
        mkdir -p /working_dir && \
        echo $PROJECT_DIR > /working_dir/path.txt; \
    else \
        echo "ERROR: UserIdentity.API.csproj not found anywhere!" && \
        exit 1; \
    fi

# Install EF Core tools
RUN dotnet tool install --global dotnet-ef --version 8.0.8

# Navigate to the API project directory
WORKDIR /src
RUN PROJECT_DIR=$(cat /working_dir/path.txt) && cd "$PROJECT_DIR"

# Create the migration (but don't update the database yet)
RUN PROJECT_DIR=$(cat /working_dir/path.txt) && \
    cd "$PROJECT_DIR" && \
    /root/.dotnet/tools/dotnet-ef migrations add InitialCreate \
    --project ../UserIdentity.Infrastructure/UserIdentity.Infrastructure.csproj \
    --startup-project .

# Set working directory and build from the found project path
WORKDIR /working_dir
RUN PROJECT_DIR=$(cat /working_dir/path.txt) && \
    cd "$PROJECT_DIR" && \
    dotnet restore && \
    dotnet build -c Release -o /app/build

FROM build AS publish
RUN PROJECT_DIR=$(cat /working_dir/path.txt) && \
    cd "$PROJECT_DIR" && \
    dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create a startup script that runs migrations then starts the app
COPY --from=build /root/.dotnet/tools/dotnet-ef /usr/local/bin/
RUN echo '#!/bin/sh \n\
echo "Waiting for database to be ready..." \n\
sleep 10 \n\
echo "Running migrations..." \n\
dotnet UserIdentity.Infrastructure.dll -- --migrations \n\
echo "Starting application..." \n\
dotnet UserIdentity.API.dll' > /app/entrypoint.sh && \
chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]