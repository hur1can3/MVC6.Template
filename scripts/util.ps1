function Stop-OnError {
  if ($LASTEXITCODE -ne 0) {
    Pop-Location
    Exit $LASTEXITCODE
  }
}

$projectName = "MvcTemplate"

$dataModelsFolder = "../src/$projectName.Objects/Models/"
$testProjectFolder = "../test/$projectName.Tests/"

$webProjectFolder = "../src/$projectName.Web"

$testProjectFolder = "../test/$projectName.Tests/"

$iisDirectoryProduction = "\\server1\c$\inetpub\wwwroot\$($projectName)"
$settingsDirectoryProduction = "\\server1\appSettings\$($projectName)"

$iisDirectoryStaging = "\\server1\c$\inetpub\wwwroot\$($projectName)Staging"
$settingsDirectoryStaging = "$settingsDirectoryProduction"
