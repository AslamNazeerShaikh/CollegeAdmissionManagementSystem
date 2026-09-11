---
name: run
description: Run the College Admission Management System application
category: run
command: ./gradlew bootRun
args:
  - name: profile
    description: Spring profile to activate
    type: string
    default: "dev"
  - name: port
    description: Server port
    type: integer
    default: 8080
  - name: args
    description: Additional arguments
    type: string
examples:
  - ./gradlew bootRun
  - ./gradlew bootRun --args='--spring.profiles.active=prod'
  - ./gradlew bootRun --args='--server.port=9090'
  - SPRING_PROFILES_ACTIVE=prod ./gradlew bootRun