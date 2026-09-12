---
name: dotnet-run
description: Run the .NET Uno Platform application
category: run
command: dotnet run
args:
  - name: project
    description: Project to run (platform head)
    type: string
    default: src/CollegeAdmission.Platforms.Windows/CollegeAdmission.Platforms.Windows.csproj
    choices: [src/CollegeAdmission.Platforms.Windows/CollegeAdmission.Platforms.Windows.csproj, src/CollegeAdmission.Platforms.macOS/CollegeAdmission.Platforms.macOS.csproj, src/CollegeAdmission.Platforms.iOS/CollegeAdmission.Platforms.iOS.csproj, src/CollegeAdmission.Platforms.Android/CollegeAdmission.Platforms.Android.csproj, src/CollegeAdmission.Platforms.Linux/CollegeAdmission.Platforms.Linux.csproj]
  - name: configuration
    description: Build configuration
    type: string
    default: Debug
    choices: [Debug, Release]
  - name: target-framework
    description: Target framework
    type: string
    default: net10.0-windows10.0.19041
    choices: [net10.0-windows10.0.19041, net10.0-macos14, net10.0-ios, net10.0-android36, net10.0-linux]
examples:
  - dotnet run --project src/CollegeAdmission.Platforms.Windows
  - dotnet run --project src/CollegeAdmission.Platforms.macOS -f net10.0-macos14
  - dotnet run --project src/CollegeAdmission.Platforms.Android -f net10.0-android36 -c Release