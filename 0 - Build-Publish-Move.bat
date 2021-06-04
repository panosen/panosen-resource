@echo off

dotnet restore

dotnet build --no-restore -c Release

dotnet nuget push Panosen.Resource\bin\Release\Panosen.Resource.*.nupkg -s https://package.savory.cn/v3/index.json --skip-duplicate

move /Y Panosen.Resource\bin\Release\Panosen.Resource.*.nupkg D:\LocalSavoryNuget\

pause