#! /usr/bin/env python

import os
import subprocess

if os.path.exists("./Project/DataSources/project.db"):
    os.remove("./Project/DataSources/project.db")

subprocess.run("dotnet run --project './CreateDB'", shell=True)
