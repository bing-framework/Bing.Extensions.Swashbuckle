@echo off

echo =======================================================================
echo Bing.Extensions.Swashbuckle
echo =======================================================================

::create nuget_packages
if not exist nuget_packages (
    md nuget_packages
    echo Create nuget_packages folder.
)

::clear nuget_packages
for /R "nuget_packages" %%s in (*) do (
    del %%s
)
echo Cleaned up all nuget packages.
echo.

::start to package all projects

::Bing.Extensions.Swashbuckle
dotnet pack src/Bing.Extensions.Swashbuckle -c Release -o nuget_packages

for /R "nuget_packages" %%s in (*symbols.nupkg) do (
    del %%s
)

echo.
echo.

set /p key=input key:
set source=https://api.nuget.org/v3/index.json

for /R "nuget_packages" %%s in (*.nupkg) do (
    call dotnet nuget push %%s -k %key% -s %source% --skip-duplicate
    echo.
)

pause