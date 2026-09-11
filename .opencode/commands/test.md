---
name: test
description: Run tests for the College Admission Management System
category: test
command: ./gradlew test
args:
  - name: unit
    description: Run only unit tests
    type: boolean
    default: false
  - name: integration
    description: Run only integration tests
    type: boolean
    default: false
  - name: coverage
    description: Generate coverage report
    type: boolean
    default: false
  - name: class
    description: Run specific test class
    type: string
examples:
  - ./gradlew test
  - ./gradlew test --tests "com.college.admission.service.StudentServiceTest"
  - ./gradlew test jacocoTestReport
  - ./gradlew integrationTest