# PowerShell script to warm up the staging slot before slot swap
# This prevents cold starts by ensuring the application is fully initialized

param(
    [Parameter(Mandatory=$true)]
    [string]$AppServiceName,
    
    [Parameter(Mandatory=$false)]
    [string]$StagingSlotName = "staging",
    
    [Parameter(Mandatory=$false)]
    [int]$MaxRetries = 10,
    
    [Parameter(Mandatory=$false)]
    [int]$RetryDelaySeconds = 10
)

$ErrorActionPreference = "Stop"

# Construct staging URL
$stagingUrl = "https://$AppServiceName-$StagingSlotName.azurewebsites.net"
$healthUrl = "$stagingUrl/health"
$warmupUrl = "$stagingUrl/api/health/warmup"
$readyUrl = "$stagingUrl/api/health/ready"

Write-Host "Starting warm-up process for staging slot: $stagingUrl" -ForegroundColor Cyan

# Function to test HTTP endpoint
function Test-HttpEndpoint {
    param(
        [string]$Url,
        [int]$TimeoutSeconds = 30
    )
    
    try {
        $response = Invoke-WebRequest -Uri $Url -Method Get -TimeoutSec $TimeoutSeconds -UseBasicParsing
        return @{
            Success = $true
            StatusCode = $response.StatusCode
            Content = $response.Content
        }
    }
    catch {
        return @{
            Success = $false
            StatusCode = $_.Exception.Response.StatusCode.value__
            Error = $_.Exception.Message
        }
    }
}

# Wait for deployment to complete
Write-Host "Waiting 30 seconds for deployment to initialize..." -ForegroundColor Yellow
Start-Sleep -Seconds 30

# Health check loop
Write-Host "`nChecking health endpoint: $healthUrl" -ForegroundColor Cyan
$retryCount = 0
$healthCheckPassed = $false

while ($retryCount -lt $MaxRetries) {
    $retryCount++
    Write-Host "Attempt $retryCount of $MaxRetries..." -ForegroundColor Yellow
    
    $result = Test-HttpEndpoint -Url $healthUrl
    
    if ($result.Success -and $result.StatusCode -eq 200) {
        Write-Host "✓ Health check passed! (Status: $($result.StatusCode))" -ForegroundColor Green
        $healthCheckPassed = $true
        break
    }
    else {
        $statusCode = if ($result.StatusCode) { $result.StatusCode } else { "N/A" }
        Write-Host "✗ Health check failed (Status: $statusCode). Retrying in $RetryDelaySeconds seconds..." -ForegroundColor Red
        
        if ($retryCount -lt $MaxRetries) {
            Start-Sleep -Seconds $RetryDelaySeconds
        }
    }
}

if (-not $healthCheckPassed) {
    Write-Host "`n✗ Warm-up failed after $MaxRetries attempts. Deployment may not be ready." -ForegroundColor Red
    exit 1
}

# Warm up database connections and critical paths
Write-Host "`nWarming up application endpoints..." -ForegroundColor Cyan

Write-Host "Calling warmup endpoint: $warmupUrl" -ForegroundColor Yellow
$warmupResult = Test-HttpEndpoint -Url $warmupUrl -TimeoutSeconds 60
if ($warmupResult.Success) {
    Write-Host "✓ Warmup endpoint responded successfully" -ForegroundColor Green
}
else {
    Write-Host "⚠ Warmup endpoint warning: $($warmupResult.Error)" -ForegroundColor Yellow
}

Write-Host "Calling readiness endpoint: $readyUrl" -ForegroundColor Yellow
$readyResult = Test-HttpEndpoint -Url $readyUrl -TimeoutSeconds 60
if ($readyResult.Success) {
    Write-Host "✓ Readiness endpoint responded successfully" -ForegroundColor Green
}
else {
    Write-Host "⚠ Readiness endpoint warning: $($readyResult.Error)" -ForegroundColor Yellow
}

Write-Host "`n✓ Warm-up process completed successfully!" -ForegroundColor Green
Write-Host "Staging slot is ready for slot swap." -ForegroundColor Green


