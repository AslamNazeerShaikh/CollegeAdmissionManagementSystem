---
name: clean
description: Clean build artifacts
category: build
command: ./gradlew clean
args:
  - name: all
    description: Clean all subprojects
    type: boolean
    default: true
examples:
  - ./gradlew clean
  - ./gradlew clean build