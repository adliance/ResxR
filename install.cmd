dotnet pack .\src\ResxDiffConsole\ResxDiffConsole.csproj -c Release

@ECHO Updating your local TogglR tool to the latest version ...
dotnet tool install --global --add-source .\src\ResxDiffConsole\nupkg\ ResxDiff --no-cache --ignore-failed-sources -v q
dotnet tool update --global --add-source .\src\ResxDiffConsole\nupkg\ ResxDiff --no-cache --ignore-failed-sources -v q
