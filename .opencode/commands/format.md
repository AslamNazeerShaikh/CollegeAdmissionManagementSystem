---
name: format
description: Format code with ktlint
category: quality
command: ./gradlew ktlintFormat
args:
  - name: check
    description: Only check formatting, don't modify
    type: boolean
    default: false
examples:
  - ./gradlew ktlintFormat
  - ./gradlew ktlintCheck