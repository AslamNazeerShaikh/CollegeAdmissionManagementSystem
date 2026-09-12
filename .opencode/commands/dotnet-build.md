---
name: dotnet-build
description: Build the .NET Uno Platform solution
category: build
command: dotnet build
args:
  - name: configuration
    description: Build configuration
    type: string
    default: Debug
    choices: [Debug, Release]
  - name: target-framework
    description: Target framework to build
    type: string
    default: net10.0
    choices: [net10.0, net10.0-windows10.0.19041, net10.0-macos14, net10.0-ios, net10.0-android36, net10.0-linux]
  - name: no-restore
    description: Skip implicit restore
    type: boolean
    default: false
  - name: verbosity
    description: Verbosity level
    type: string
    default: minimal
    choices: [quiet, minimal, normal, detailed, diagnostic]
examples:
  - dotnet build
  - dotnet build -c Release
  - dotnet build -f net10.0-ios -c Release
  - dotnet build --no-restore -v normal