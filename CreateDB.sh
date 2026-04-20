#! /usr/bin/env sh
rm "./Project/DataSources/project.db"

dotnet run --project "./CreateDB"
