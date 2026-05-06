#! /usr/bin/env python3
import subprocess
import os

versions = subprocess.run("dotnet --list-sdks", shell=True, capture_output=True, text=True)

versions_output = versions.stdout

if versions_output == "":
    raise SystemError(".NET Framework isn't installed, please install .NET 8")

if "8.0" not in versions_output:
    raise SystemError(".NET is installed, however version 8 was not found")

if os.path.exists("./Project/DataSources/project.db") == False:
    if os.name == "nt":
        subprocess.run(r'dotnet run --project ".\CreateDB\\"', shell=True)
    else: 
        subprocess.run("dotnet run --project './CreateDB'", shell=True)

try:
    if os.name == "nt":
        subprocess.run(r'dotnet run --project ".\Project\\"', shell=True)
    else:
        subprocess.run("dotnet run --project './Project'", shell=True)
except:
    exit()