# Admin Authentication System Backend

## Running the Project

To build and start the application:

```bash
docker-compose up --build
```
Run migration
```bash
dotnet ef migrations add InitialCreate --project UserIdentity.Infrastructure/UserIdentity.Infrastructure.csproj --startup-project UserIdentity.API/UserIdentity.API.csproj
```

Update database
```bash
dotnet ef database update --project UserIdentity.Infrastructure/UserIdentity.Infrastructure.csproj --startup-project UserIdentity.API/UserIdentity.API.csproj
```

The API will be available at: http://localhost:5001/swagger/index.html