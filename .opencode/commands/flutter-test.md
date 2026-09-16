---
name: flutter-test
description: Analyze and test the Flutter CollegeAdmission CRM port
category: test
command: flutter test
args:
  - name: analyze
    description: Run flutter analyze first
    type: boolean
    default: true
examples:
  - flutter analyze && flutter test
