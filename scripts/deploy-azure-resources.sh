#!/bin/bash
# Azure CLI script to deploy App Service with staging slot
# This script creates the necessary Azure resources for the application

set -e

# Variables - customize these for your environment
RESOURCE_GROUP_NAME="${RESOURCE_GROUP_NAME:-breadrecipes-rg}"
LOCATION="${LOCATION:-eastus}"
APP_SERVICE_NAME="${APP_SERVICE_NAME:-breadrecipes-appservice}"
APP_SERVICE_PLAN_NAME="${APP_SERVICE_PLAN_NAME:-breadrecipes-plan}"
APP_SERVICE_PLAN_SKU="${APP_SERVICE_PLAN_SKU:-S1}"
STAGING_SLOT_NAME="${STAGING_SLOT_NAME:-staging}"

echo "========================================="
echo "Azure App Service Deployment Script"
echo "========================================="
echo "Resource Group: $RESOURCE_GROUP_NAME"
echo "Location: $LOCATION"
echo "App Service: $APP_SERVICE_NAME"
echo "App Service Plan: $APP_SERVICE_PLAN_NAME"
echo "Plan SKU: $APP_SERVICE_PLAN_SKU"
echo "Staging Slot: $STAGING_SLOT_NAME"
echo "========================================="
echo ""

# Check if Azure CLI is installed
if ! command -v az &> /dev/null; then
    echo "Error: Azure CLI is not installed. Please install it from https://docs.microsoft.com/cli/azure/install-azure-cli"
    exit 1
fi

# Check if logged in
echo "Checking Azure login status..."
if ! az account show &> /dev/null; then
    echo "Please log in to Azure..."
    az login
fi

# Get current subscription
SUBSCRIPTION_ID=$(az account show --query id -o tsv)
echo "Using subscription: $SUBSCRIPTION_ID"
echo ""

# Create resource group
echo "Creating resource group: $RESOURCE_GROUP_NAME"
az group create \
    --name "$RESOURCE_GROUP_NAME" \
    --location "$LOCATION" \
    --output none

# Create App Service Plan
echo "Creating App Service Plan: $APP_SERVICE_PLAN_NAME"
az appservice plan create \
    --name "$APP_SERVICE_PLAN_NAME" \
    --resource-group "$RESOURCE_GROUP_NAME" \
    --location "$LOCATION" \
    --sku "$APP_SERVICE_PLAN_SKU" \
    --is-linux false \
    --output none

# Create App Service
echo "Creating App Service: $APP_SERVICE_NAME"
az webapp create \
    --name "$APP_SERVICE_NAME" \
    --resource-group "$RESOURCE_GROUP_NAME" \
    --plan "$APP_SERVICE_PLAN_NAME" \
    --runtime "DOTNET:9.0" \
    --output none

# Configure App Service settings for optimal performance and no cold starts
echo "Configuring App Service settings..."
az webapp config set \
    --name "$APP_SERVICE_NAME" \
    --resource-group "$RESOURCE_GROUP_NAME" \
    --always-on true \
    --http20-enabled true \
    --min-tls-version "1.2" \
    --output none

# Set health check path
echo "Setting health check path..."
az webapp config appsettings set \
    --name "$APP_SERVICE_NAME" \
    --resource-group "$RESOURCE_GROUP_NAME" \
    --settings WEBSITE_HEALTHCHECK_MAXPINGFAILURES=10 \
    --output none

# Create staging deployment slot
echo "Creating staging slot: $STAGING_SLOT_NAME"
az webapp deployment slot create \
    --name "$APP_SERVICE_NAME" \
    --resource-group "$RESOURCE_GROUP_NAME" \
    --slot "$STAGING_SLOT_NAME" \
    --configuration-source "$APP_SERVICE_NAME" \
    --output none

# Configure staging slot settings
echo "Configuring staging slot settings..."
az webapp config set \
    --name "$APP_SERVICE_NAME" \
    --resource-group "$RESOURCE_GROUP_NAME" \
    --slot "$STAGING_SLOT_NAME" \
    --always-on true \
    --http20-enabled true \
    --min-tls-version "1.2" \
    --output none

echo ""
echo "========================================="
echo "✓ Deployment completed successfully!"
echo "========================================="
echo ""
echo "App Service URL: https://$APP_SERVICE_NAME.azurewebsites.net"
echo "Staging Slot URL: https://$APP_SERVICE_NAME-$STAGING_SLOT_NAME.azurewebsites.net"
echo ""
echo "Next steps:"
echo "1. Configure your Azure DevOps pipeline variables:"
echo "   - azureSubscription: Your Azure service connection name"
echo "   - appServiceName: $APP_SERVICE_NAME"
echo "   - resourceGroupName: $RESOURCE_GROUP_NAME"
echo "   - stagingSlotName: $STAGING_SLOT_NAME"
echo ""
echo "2. Deploy your application using the Azure DevOps pipeline"
echo ""


