---
name: lint
description: Run static analysis and linting
category: quality
command: ./gradlew detekt ktlintCheck
args:
  - name: fix
    description: Auto-fix linting issues
    type: boolean
    default: false
  - name: baseline
    description: Generate baseline for detekt
    type: boolean
    default: false
examples:
  - ./gradlew detekt ktlintCheck
  - ./gradlew detekt --baseline
  - ./gradlew ktlintFormat
  - ./gradlew detekt ktlintFormat