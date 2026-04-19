@echo off
del ".\Project\DataSources\project.db"
dotnet run --project ".\CreateDB"