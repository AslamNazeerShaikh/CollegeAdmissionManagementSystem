---
name: flutter-run
description: Run the Flutter CollegeAdmission CRM port on Linux desktop
category: run
command: flutter run -d linux
args:
  - name: flavor
    description: Build mode for the run
    type: string
    default: debug
    choices: [debug, profile, release]
examples:
  - flutter run -d linux
