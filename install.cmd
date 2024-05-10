dotnet pack .\src\Adliance.ResxR\Adliance.ResxR.csproj -c Release

@ECHO Updating your local ResxR tool to the latest version ...
dotnet tool install --global --add-source .\src\Adliance.ResxR\nupkg\ Adliance.ResxR --no-cache --ignore-failed-sources -v q
dotnet tool update --global --add-source .\src\Adliance.ResxR\nupkg\ Adliance.ResxR --no-cache --ignore-failed-sources -v q
