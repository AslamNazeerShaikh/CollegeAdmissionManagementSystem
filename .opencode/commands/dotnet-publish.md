---
name: dotnet-publish
description: Publish the .NET Uno Platform application (NativeAOT for release)
category: build
command: dotnet publish
args:
  - name: project
    description: Project to publish (platform head)
    type: string
    default: src/CollegeAdmission.Platforms.Windows/CollegeAdmission.Platforms.Windows.csproj
    choices: [src/CollegeAdmission.Platforms.Windows/CollegeAdmission.Platforms.Windows.csproj, src/CollegeAdmission.Platforms.macOS/CollegeAdmission.Platforms.macOS.csproj, src/CollegeAdmission.Platforms.iOS/CollegeAdmission.Platforms.iOS.csproj, src/CollegeAdmission.Platforms.Android/CollegeAdmission.Platforms.Android.csproj, src/CollegeAdmission.Platforms.Linux/CollegeAdmission.Platforms.Linux.csproj]
  - name: configuration
    description: Build configuration
    type: string
    default: Release
    choices: [Debug, Release]
  - name: target-framework
    description: Target framework
    type: string
    default: net10.0-windows10.0.19041
    choices: [net10.0-windows10.0.19041, net10.0-macos14, net10.0-ios, net10.0-android36, net10.0-linux]
  - name: native-aot
    description: Enable NativeAOT publishing
    type: boolean
    default: true
  - name: runtime-identifier
    description: Runtime identifier
    type: string
    default: ""
  - name: output
    description: Output directory
    type: string
    default: ""
examples:
  - dotnet publish src/CollegeAdmission.Platforms.Windows -c Release -f net10.0-windows10.0.19041 -p:PublishAot=true
  - dotnet publish src/CollegeAdmission.Platforms.macOS -c Release -f net10.0-macos14 -p:PublishAot=true
  - dotnet publish src/CollegeAdmission.Platforms.iOS -c Release -f net10.0-ios -p:PublishAot=true
  - dotnet publish src/CollegeAdmission.Platforms.Android -c Release -f net10.0-android36 -p:PublishAot=true -p:AndroidNdkVersion=r27
  - dotnet publish src/CollegeAdmission.Platforms.Linux -c Release -f net10.0-linux -p:PublishAot=true -r linux-x64