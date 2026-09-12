---
name: dotnet-format
description: Format C# code using dotnet format
category: format
command: dotnet format
args:
  - name: verify-no-changes
    description: Verify no formatting changes needed (exit code 1 if changes needed)
    type: boolean
    default: false
  - name: include
    description: Include pattern for files to format
    type: string
    default: "**/*.cs"
  - name: exclude
    description: Exclude pattern for files
    type: string
    default: ""
  - name: verbosity
    description: Verbosity level
    type: string
    default: normal
    choices: [quiet, minimal, normal, detailed, diagnostic]
examples:
  - dotnet format
  - dotnet format --verify-no-changes
  - dotnet format --include "**/*.cs" --exclude "**/obj/**"
  - dotnet format --verbosity detailed