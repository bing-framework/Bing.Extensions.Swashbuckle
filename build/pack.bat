@echo off
echo %1
cd ..

rem clear old packages
del output\* /q/f/s

rem build
dotnet build Bing.Extensions.Swashbuckle.sln -c Release

rem pack
dotnet pack ./src/Bing.Extensions.Swashbuckle/Bing.Extensions.Swashbuckle.csproj

rem push
for %%i in (output\release\*.nupkg) do dotnet nuget push %%i -k %1 -s https://www.nuget.org/api/v2/package