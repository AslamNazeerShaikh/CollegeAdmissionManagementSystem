---
name: dotnet-test
description: Run tests for the .NET Uno Platform solution
category: test
command: dotnet test
args:
  - name: configuration
    description: Build configuration
    type: string
    default: Debug
    choices: [Debug, Release]
  - name: target-framework
    description: Target framework to test
    type: string
    default: net10.0
    choices: [net10.0, net10.0-windows10.0.19041, net10.0-macos14, net10.0-ios, net10.0-android36, net10.0-linux]
  - name: filter
    description: Test filter expression
    type: string
    default: ""
  - name: collect-coverage
    description: Collect code coverage
    type: boolean
    default: false
  - name: verbosity
    description: Verbosity level
    type: string
    default: normal
    choices: [quiet, minimal, normal, detailed, diagnostic]
examples:
  - dotnet test
  - dotnet test -c Release
  - dotnet test --filter "FullyQualifiedName~CoursesViewModel"
  - dotnet test --collect:"XPlat Code Coverage"
  - dotnet test -f net10.0 --logger "console;verbosity=detailed"