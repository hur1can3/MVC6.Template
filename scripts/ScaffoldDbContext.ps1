. ./util.ps1

Push-Location -Path "$webProjectFolder"

$contextDirectory = "../$projectName.Data/Core"
$contextName = "Context"
$connectionString = Get-Content -Path "configuration.json" |
  ConvertFrom-Json |
  Select-Object -ExpandProperty ConnectionStrings |
  Select-Object -ExpandProperty Connection

dotnet ef dbcontext scaffold "$connectionString" Microsoft.EntityFrameworkCore.SqlServer --force --context-dir "$contextDirectory" --context "$contextName" --output-dir "../$dataModelsFolder"

Write-Host "Within $dataModelsFolder, update namespaces."
Write-Host "Within $contextName, add using for models namespace."
Write-Host "Within $contextName, update namespace. and add using for models namespace."
Write-Host "Within $contextName, remove the OnConfiguring method as it contains sensitive information."
Write-Host "Within $contextName, add a call to MapViews(modelBuilder); to OnModelCreating."

Pop-Location
