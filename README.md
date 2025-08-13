# Admin Panel V0.1 Backend API
Bismillah.

This is the backend API for the Admin Panel system providing account management and service health monitoring capabilities.

## Features

### 1. Accounts Module
- Paginated, searchable, and filterable account management
- Account details with user profiles, subscriptions, and required actions
- Account actions: Ban/Unban, Force Disconnect, Subscription management

### 2. Services Health Module
- Real-time monitoring of backend services health status
- Visual status indicators for UserIdentity, Player, GameSettings, and Orders services

## Running the Project

To build and start the application:

```bash
docker compose up --build
```

The API will be available at: http://localhost:5001/swagger/index.html

## Database Migrations

Run migration:
```bash
dotnet ef migrations add InitialCreate --project UserIdentity.Infrastructure/UserIdentity.Infrastructure.csproj --startup-project UserIdentity.API/UserIdentity.API.csproj
```

Update database:
```bash
dotnet ef database update --project UserIdentity.Infrastructure/UserIdentity.Infrastructure.csproj --startup-project UserIdentity.API/UserIdentity.API.csproj
```

## API Endpoints

### Authentication
- `POST /api/signup` - User registration
- `POST /api/login` - User authentication

### Admin - Accounts Management
- `GET /api/admin/accounts` - Get paginated accounts list
- `GET /api/admin/accounts/{id}` - Get account details
- `POST /api/admin/accounts/{id}/ban` - Ban account
- `DELETE /api/admin/accounts/{id}/ban` - Unban account
- `POST /api/admin/accounts/{id}/disconnect` - Force disconnect
- `POST /api/admin/subscriptions` - Add subscription
- `DELETE /api/admin/subscriptions/{id}` - Cancel subscription

### Admin - Services Health
- `GET /api/admin/health/services` - Get all services health status
- `GET /api/admin/health/services/{serviceName}` - Get specific service health

## Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string | See appsettings.json |
| `Jwt__Key` | JWT signing key | Generate using `openssl rand -base64 32` |
| `Jwt__Issuer` | JWT issuer | UserIdentity |
| `Jwt__Audience` | JWT audience | UserIdentity |

## Generate JWT Key

```bash
openssl rand -base64 32
```
