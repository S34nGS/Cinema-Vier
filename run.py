#! /usr/bin/env python
import subprocess
import os

versions = subprocess.run("dotnet --list-sdks", shell=True, capture_output=True, text=True)

versions_output = versions.stdout

if versions_output == "":
    raise SystemError(".NET Framework isn't installed, please install .NET 8")

if "8.0" not in versions_output:
    raise SystemError(".NET is installed, however version 8 was not found")

if os.path.exists("./Project/DataSources/project.db") == False:
    subprocess.run("dotnet run --project './CreateDB'", shell=True)

subprocess.run("dotnet run --project './Project'", shell=True)