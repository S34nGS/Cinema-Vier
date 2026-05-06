#! /usr/bin/env python3

import os
import subprocess

def CreateDB():
    if os.name == "nt":
        subprocess.run(r'dotnet run --project ".\CreateDB\\"', shell=True)
    else:
        subprocess.run("dotnet run --project './CreateDB'", shell=True)

if __name__ == "__main__":
    if os.path.exists("./Project/DataSources/project.db"):
        os.remove("./Project/DataSources/project.db")

    CreateDB()