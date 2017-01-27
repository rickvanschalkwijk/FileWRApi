#!/usr/bin/env bash
dotnet restore && dotnet build **/project.json
dotnet test ./Source/FileWR.Tests/
dotnet test ./Source/FileWR.Business.Tests/