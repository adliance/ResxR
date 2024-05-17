dotnet format ResxDiff.sln -v normal

if %ERRORLEVEL% NEQ 0 (
   echo [91mFormat failed[0m %errorlevel%
   exit /b %errorlevel%
)

dotnet build ResxDiff.sln

if %ERRORLEVEL% NEQ 0 (
   echo [91mBuild failed[0m %errorlevel%
   exit /b %errorlevel%
)

dotnet test ResxDiff.sln

if %ERRORLEVEL% EQU 0 (
   echo [92mBuild succeeded[0m %errorlevel%
)
