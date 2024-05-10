dotnet format Adliance.ResxR.sln -v normal

if %ERRORLEVEL% NEQ 0 (
   echo [91mFormat failed[0m %errorlevel%
   exit /b %errorlevel%
)

dotnet build Adliance.ResxR.sln -warnAsError

if %ERRORLEVEL% NEQ 0 (
   echo [91mBuild failed[0m %errorlevel%
   exit /b %errorlevel%
)

dotnet test Adliance.ResxR.sln

if %ERRORLEVEL% EQU 0 (
   echo [92mBuild succeeded[0m %errorlevel%
)
