#! /usr/bin/env python3

import os
import subprocess

if os.path.exists("./Project/DataSources/project.db"):
    os.remove("./Project/DataSources/project.db")

if os.name == "nt":
    subprocess.run(r'dotnet run --project ".\CreateDB\\"', shell=True)
else: 
    subprocess.run("dotnet run --project './CreateDB'", shell=True)
