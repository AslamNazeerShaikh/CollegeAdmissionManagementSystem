---
name: default
description: LSP configuration for the College Admission Management System
version: 1.0.0
enabled: true
servers:
  # Kotlin Language Server
  - name: kotlin-language-server
    command: kotlin-language-server
    args: []
    filetypes:
      - kotlin
      - kts
    rootPatterns:
      - settings.gradle.kts
      - settings.gradle
      - build.gradle.kts
      - build.gradle
      - pom.xml
    initializationOptions:
      storagePath: ".opencode/lsp/kotlin"
  
  # Gradle Language Server (if available)
  - name: gradle-language-server
    command: gradle-language-server
    args: []
    filetypes:
      - gradle
      - kts
    rootPatterns:
      - settings.gradle.kts
      - settings.gradle
      - build.gradle.kts
      - build.gradle
  
  # TypeScript/JavaScript (for any frontend)
  - name: typescript-language-server
    command: typescript-language-server
    args:
      - --stdio
    filetypes:
      - typescript
      - typescriptreact
      - javascript
      - javascriptreact
    rootPatterns:
      - package.json
      - tsconfig.json
  
  # YAML Language Server
  - name: yaml-language-server
    command: yaml-language-server
    args:
      - --stdio
    filetypes:
      - yaml
      - yml
    rootPatterns:
      - .github
      - docker-compose.yml
      - docker-compose.yaml
  
  # JSON Language Server
  - name: json-language-server
    command: vscode-json-language-server
    args:
      - --stdio
    filetypes:
      - json
    rootPatterns:
      - package.json
  
  # Dockerfile Language Server
  - name: dockerfile-language-server
    command: docker-langserver
    args:
      - --stdio
    filetypes:
      - dockerfile
    rootPatterns:
      - Dockerfile
      - docker-compose.yml
  
  # SQL Language Server (for migration files)
  - name: sql-language-server
    command: sql-language-server
    args:
      - up
      - --method
      - stdio
    filetypes:
      - sql
    rootPatterns:
      - db/migration
      - src/main/resources/db/migration

  # C# Language Server (csharp-ls - modern, fast)
  - name: csharp-ls
    command: csharp-ls
    args: []
    filetypes:
      - csharp
    rootPatterns:
      - "*.sln"
      - "*.csproj"
      - "global.json"
      - "Directory.Build.props"
      - "Directory.Build.targets"

  # Dart Language Server (ships with the Flutter SDK)
  - name: dart-language-server
    command: dart
    args:
      - language-server
      - --protocol=lsp
    filetypes:
      - dart
    rootPatterns:
      - pubspec.yaml

settings:
  # Kotlin specific settings
  kotlin:
    completion:
      autoImport: true
      snippets: true
    formatting:
      ktlintEnabled: true
    diagnostics:
      enabled: true
  
  # Gradle specific settings
  gradle:
    wrapper:
      enabled: true
    build:
      offline: false
  
  # General settings
  completion:
    triggerCharacters: [".", ":", "<", "@", "#"]
    resolveTimeout: 5000
  
  diagnostics:
    enable: true
    debounce: 300

  # C# specific settings
  csharp:
    formatting:
      enable: true
      indentSize: 4
      tabSize: 4
      useTabs: false
      newLine: "\n"
    completion:
      triggerCharacters: [".", "(", "<", "@", "#", "?"]
      provideRegexCompletion: true
    diagnostics:
      enable: true
      enableSuppress: true
    semanticTokens:
      enable: true
    inlayHints:
      enable: true
      parameterNames: true
      typeAnnotations: true
    navigation:
      enable: true