---
name: dotnet-lint
description: Run static code analysis on the .NET solution
category: lint
command: dotnet build
args:
  - name: configuration
    description: Build configuration
    type: string
    default: Debug
    choices: [Debug, Release]
  - name: target-framework
    description: Target framework
    type: string
    default: net10.0
    choices: [net10.0, net10.0-windows10.0.19041, net10.0-macos14, net10.0-ios, net10.0-android36, net10.0-linux]
  - name: analyzers
    description: Run analyzers during build
    type: boolean
    default: true
examples:
  - dotnet build -c Release --tl:off
  - dotnet build -f net10.0 --tl:off
  - dotnet build -c Release -f net10.0-ios --tl:off