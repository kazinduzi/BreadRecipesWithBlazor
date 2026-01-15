@description('Name of the App Service')
param appServiceName string

@description('Name of the Resource Group')
param resourceGroupName string

@description('Location for all resources')
param location string = resourceGroup().location

@description('Name of the App Service Plan')
param appServicePlanName string

@description('The pricing tier for the App Service Plan')
@allowed([
  'F1'
  'D1'
  'B1'
  'B2'
  'B3'
  'S1'
  'S2'
  'S3'
  'P1'
  'P2'
  'P3'
  'P1V2'
  'P2V2'
  'P3V2'
  'P1V3'
  'P2V3'
  'P3V3'
])
param appServicePlanSku string = 'S1'

@description('Name of the staging deployment slot')
param stagingSlotName string = 'staging'

@description('Enable Always On to prevent cold starts')
param enableAlwaysOn bool = true

@description('Enable HTTP/2')
param enableHttp2 bool = true

@description('Minimum TLS version')
@allowed([
  '1.0'
  '1.1'
  '1.2'
])
param minTlsVersion string = '1.2'

@description('Health check path for App Service')
param healthCheckPath string = '/health'

// App Service Plan
resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: appServicePlanName
  location: location
  kind: 'app'
  sku: {
    name: appServicePlanSku
  }
}

// App Service
resource appService 'Microsoft.Web/sites@2021-02-01' = {
  name: appServiceName
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      alwaysOn: enableAlwaysOn
      http20Enabled: enableHttp2
      minTlsVersion: minTlsVersion
      netFrameworkVersion: 'v9.0'
      healthCheckPath: healthCheckPath
      cors: {
        allowedOrigins: ['*']
        supportCredentials: false
      }
    }
    httpsOnly: true
  }
}

// Staging Deployment Slot
resource stagingSlot 'Microsoft.Web/sites/slots@2021-02-01' = {
  name: '${appServiceName}/${stagingSlotName}'
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      alwaysOn: enableAlwaysOn
      http20Enabled: enableHttp2
      minTlsVersion: minTlsVersion
      netFrameworkVersion: 'v9.0'
      healthCheckPath: healthCheckPath
      cors: {
        allowedOrigins: ['*']
        supportCredentials: false
      }
    }
    httpsOnly: true
  }
}

// Outputs
output appServiceName string = appServiceName
output appServiceDefaultHostName string = appService.properties.defaultHostName
output stagingSlotHostName string = stagingSlot.properties.defaultHostName
output appServicePlanName string = appServicePlanName


