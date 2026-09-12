---
name: dotnet-clean
description: Clean the .NET Uno Platform solution
category: build
command: dotnet clean
args:
  - name: configuration
    description: Build configuration
    type: string
    default: Debug
    choices: [Debug, Release]
  - name: target-framework
    description: Target framework to clean
    type: string
    default: ""
examples:
  - dotnet clean
  - dotnet clean -c Release
  - dotnet clean -f net10.0-ios