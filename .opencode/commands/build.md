---
name: build
description: Build the College Admission Management System
category: build
command: ./gradlew build
args:
  - name: clean
    description: Clean before building
    type: boolean
    default: false
  - name: skip-tests
    description: Skip running tests
    type: boolean
    default: false
  - name: parallel
    description: Enable parallel execution
    type: boolean
    default: true
examples:
  - ./gradlew build
  - ./gradlew clean build
  - ./gradlew build -x test
  - ./gradlew build --parallel