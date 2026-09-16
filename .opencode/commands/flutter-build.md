---
name: flutter-build
description: Build the Flutter CollegeAdmission CRM port
category: build
command: flutter build
args:
  - name: target
    description: Build target platform
    type: string
    default: linux
    choices: [linux, apk, appbundle, web]
  - name: mode
    description: Build mode
    type: string
    default: release
    choices: [debug, profile, release]
examples:
  - flutter build linux --release
  - flutter build apk --release
