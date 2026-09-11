---
name: default
description: Default permissions for the College Admission Management System
version: 1.0.0
allow:
  # File operations
  - tool: read
    description: Read any file in the project
  - tool: write
    description: Write new files
  - tool: edit
    description: Edit existing files
  - tool: glob
    description: Find files by pattern
  - tool: grep
    description: Search file contents
  
  # Task operations
  - tool: task
    description: Launch sub-agents for complex tasks
  
  # Bash operations (safe commands)
  - tool: bash
    args:
      - ./gradlew
      - ./gradlew *
      - git status
      - git diff
      - git log *
      - git branch
      - git show *
      - cat *
      - ls *
      - find * -name *
      - mkdir -p *
      - rm -f *.tmp *.log
      - cp * *
      - mv * *
      - chmod +x *
      - docker compose *
      - docker ps
      - docker logs *
  
  # Web operations
  - tool: webfetch
    description: Fetch documentation and resources

deny:
  # Dangerous bash commands
  - tool: bash
    args:
      - rm -rf *
      - rm -rf /
      - sudo *
      - chmod 777 *
      - chown -R *
      - dd *
      - mkfs *
      - fdisk *
      - shutdown *
      - reboot *
      - kill -9 *
      - pkill -9 *
      - git push --force *
      - git reset --hard HEAD~*
      - curl * | bash
      - wget * | bash
  
  # File operations on sensitive files
  - tool: write
    args:
      - ".env*"
      - "*.key"
      - "*.pem"
      - "*.p12"
      - "*.jks"
      - "secrets.*"
      - "credentials.*"
  
  - tool: read
    args:
      - ".env*"
      - "*.key"
      - "*.pem"
      - "*.p12"
      - "*.jks"
      - "secrets.*"
      - "credentials.*"