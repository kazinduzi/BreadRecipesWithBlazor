# BreadRecipesWithBlazor

A modern bread recipe blog application built with Blazor WebAssembly and ASP.NET Core Web API.

## Architecture

- **Frontend**: Blazor WebAssembly (client-side)
- **Backend**: ASP.NET Core 9.0 Web API
- **Database**: SQLite (Entity Framework Core)
- **Logging**: NLog

## Prerequisites

- .NET 9.0 SDK
- A modern web browser

## Running the Application

### Option 1: Run Both Projects Separately

1. **Start the API Server**:
   ```bash
   cd AccountOwnerServer
   dotnet run
   ```
   The API will run on `http://localhost:5000`

2. **Start the Blazor Client** (in a new terminal):
   ```bash
   cd BreadRecipesWithWasmBlazor.Client
   dotnet run
   ```
   The client will run on `https://localhost:5001` or `http://localhost:5000` (check the output)

3. Open your browser and navigate to the client URL shown in the terminal.

### Option 2: Build and Run from Solution

```bash
# Build the solution
dotnet build BreadRecipesWithWasmBlazorApp.sln

# Run the API server
cd AccountOwnerServer
dotnet run

# In another terminal, run the client
cd BreadRecipesWithWasmBlazor.Client
dotnet run
```

## Configuration

- **API Base URL**: Configured in `BreadRecipesWithWasmBlazor.Client/wwwroot/appsettings.json`
- **Database**: SQLite database file is created automatically in the `AccountOwnerServer` directory
- **API Routes**: All API endpoints are prefixed with `/api/recipe`

## Features

- View all bread recipes
- Create new recipes with ingredients
- Edit existing recipes
- Search/filter recipes
- Health status indicators (Healthy/NotHealthy)

## API Endpoints

- `GET /api/recipe` - Get all recipes
- `GET /api/recipe/{id}` - Get recipe by ID
- `POST /api/recipe/create` - Create a new recipe
- `PUT /api/recipe/{id}` - Update a recipe
- `GET /health` - Health check endpoint
