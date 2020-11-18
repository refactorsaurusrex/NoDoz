param (
  [string]$Version = '0.0.0'
)

$proj = Get-ChildItem -Filter '*.csproj' -Recurse -Path $PSScriptRoot | Select-Object -ExpandProperty FullName
Remove-Item "$PSScriptRoot\publish" -Recurse -Force -ErrorAction SilentlyContinue
dotnet publish $proj -c Release -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o "$PSScriptRoot\publish" -p:PublishTrimmed=true -r win-x64 -p:DebugSymbols=false -p:DebugType=None
Compress-Archive -Path "$PSScriptRoot\publish\NoDoz.exe" -DestinationPath "$PSScriptRoot\publish\NoDoz-v$Version.zip"