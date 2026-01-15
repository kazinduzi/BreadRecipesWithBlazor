# Azure App Service Deployment Guide

This guide explains how to deploy the BreadRecipes application to Azure App Service using Azure DevOps with slot swapping to prevent cold starts.

## Architecture Overview

The deployment strategy uses:

- **Azure App Service** with deployment slots
- **Staging Slot** for pre-production testing and warm-up
- **Slot Swapping** to eliminate cold starts
- **Health Checks** to verify application readiness
- **Azure DevOps Pipeline** for CI/CD automation

## Prerequisites

1. Azure Subscription
2. Azure DevOps Organization and Project
3. Azure CLI installed (for manual setup)
4. Service Connection configured in Azure DevOps

## Setup Instructions

### 1. Create Azure Resources

#### Option A: Using Azure CLI Script

```bash
# Set environment variables
export RESOURCE_GROUP_NAME="breadrecipes-rg"
export APP_SERVICE_NAME="breadrecipes-appservice"
export APP_SERVICE_PLAN_NAME="breadrecipes-plan"
export LOCATION="eastus"

# Run the deployment script
bash scripts/deploy-azure-resources.sh
```

#### Option B: Using Bicep Template

```bash
# Login to Azure
az login

# Deploy using Bicep template (recommended)
az deployment group create \
  --resource-group breadrecipes-rg \
  --template-file azure-deploy.bicep \
  --parameters @azure-deploy.parameters.json

# Or using inline parameters
az deployment group create \
  --resource-group breadrecipes-rg \
  --template-file azure-deploy.bicep \
  --parameters appServiceName=breadrecipes-appservice resourceGroupName=breadrecipes-rg appServicePlanName=breadrecipes-plan
```

**Note:** The Bicep template (`azure-deploy.bicep`) is the preferred method as it's more readable and easier to maintain than JSON ARM templates. Bicep automatically compiles to ARM JSON when deploying. Bicep uses JSON parameter files (e.g., `azure-deploy.parameters.json`) for providing parameter values, which maintains compatibility with existing tooling.

### 2. Configure Azure DevOps Service Connection

1. Go to your Azure DevOps project
2. Navigate to **Project Settings** > **Service Connections**
3. Click **New service connection**
4. Select **Azure Resource Manager**
5. Choose **Service principal (automatic)** or **Service principal (manual)**
6. Configure the connection with appropriate permissions:
   - Subscription scope
   - Contributor role (minimum)

### 3. Configure Pipeline Variables

In your Azure DevOps pipeline, set the following variables:

| Variable            | Description                                  | Example                   |
| ------------------- | -------------------------------------------- | ------------------------- |
| `azureSubscription` | Name of your Azure DevOps service connection | `Azure-Connection-Name`   |
| `appServiceName`    | Name of your Azure App Service               | `breadrecipes-appservice` |
| `resourceGroupName` | Resource group containing the App Service    | `breadrecipes-rg`         |
| `stagingSlotName`   | Name of the staging deployment slot          | `staging`                 |

**To set variables:**

1. Open your pipeline
2. Click **Edit**
3. Click **Variables** tab
4. Add each variable

### 4. Update Pipeline YAML

The `azure-pipelines.yml` file is already configured. If you need to customize:

- Update the `vmImage` if you prefer Linux
- Adjust retry counts and delays in the warm-up script
- Modify the build steps if needed

## Deployment Flow

The pipeline follows these stages:

1. **Build Stage**

   - Restores NuGet packages
   - Builds the solution
   - Publishes the web app
   - Creates deployment artifact

2. **Deploy to Staging Stage**

   - Deploys to the staging slot
   - Waits for deployment initialization
   - Performs health checks
   - Warms up the application (database connections, etc.)
   - Verifies readiness

3. **Swap Slots Stage**
   - Swaps staging slot with production
   - Verifies production slot health
   - Ensures zero-downtime deployment

## Warm-Up Strategy

The application includes several endpoints to prevent cold starts:

- `/health` - Basic health check
- `/api/health/warmup` - Comprehensive warm-up (tests database connectivity)
- `/api/health/ready` - Readiness probe

The pipeline automatically:

1. Checks the health endpoint until it returns 200
2. Calls the warmup endpoint to initialize database connections
3. Calls the ready endpoint to verify full readiness
4. Only proceeds with slot swap after successful warm-up

## Monitoring and Troubleshooting

### Check Deployment Logs

View logs in Azure DevOps pipeline run details or in Azure Portal:

- App Service → Deployment Center → Logs

### Verify Slot Swap

```bash
az webapp deployment slot list \
  --name breadrecipes-appservice \
  --resource-group breadrecipes-rg
```

### Manual Warm-Up (if needed)

```powershell
# Using PowerShell
.\scripts\warmup-staging.ps1 -AppServiceName "breadrecipes-appservice" -StagingSlotName "staging"
```

### Common Issues

1. **Cold Start Still Occurs**

   - Ensure "Always On" is enabled: `az webapp config set --always-on true`
   - Verify health check path is set correctly
   - Check that staging slot is warmed up before swap

2. **Deployment Fails**

   - Check Azure service connection permissions
   - Verify app service name and resource group are correct
   - Review pipeline logs for specific error messages

3. **Health Check Fails**
   - Verify database connection string is configured
   - Check application logs in Azure Portal
   - Ensure health endpoint is accessible

## App Service Configuration

Key settings configured for optimal performance:

- **Always On**: Enabled (prevents cold starts)
- **HTTP/2**: Enabled
- **TLS Version**: 1.2 minimum
- **Health Check Path**: `/health`
- **Deployment Slot**: `staging` for zero-downtime deployments

## Database Configuration

For production, consider:

1. **Azure SQL Database** instead of SQLite
2. **Connection String** stored in Azure Key Vault
3. **Connection Pooling** for better performance

To update connection string:

```bash
az webapp config connection-string set \
  --name breadrecipes-appservice \
  --resource-group breadrecipes-rg \
  --connection-string-type SQLite \
  --settings DefaultConnection="YourConnectionString" \
  --slot-settings DefaultConnection="YourConnectionString"
```

## Additional Resources

- [Azure App Service Deployment Slots](https://docs.microsoft.com/azure/app-service/deploy-staging-slots)
- [Azure DevOps Pipelines](https://docs.microsoft.com/azure/devops/pipelines/)
- [Zero-Downtime Deployment Best Practices](https://docs.microsoft.com/azure/app-service/deploy-best-practices)
- [Bicep Documentation](https://docs.microsoft.com/azure/azure-resource-manager/bicep/)
- [Bicep Language Specification](https://github.com/Azure/bicep/blob/main/docs/spec/bicep.md)

## Support

For issues or questions:

1. Check Azure DevOps pipeline logs
2. Review Azure App Service logs
3. Verify all configuration variables are set correctly
