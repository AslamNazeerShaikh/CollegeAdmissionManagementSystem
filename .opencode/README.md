# Opencode Configuration for College Admission Management System

This directory contains all opencode configuration files for agents, skills, commands, permissions, and LSP settings.

## Structure

```
.opencode/
├── opencode.json              # Main configuration
├── agents/                    # Agent definitions
│   ├── college-admission-assistant.md
│   ├── code-reviewer.md
│   ├── test-writer.md
│   ├── documentation-writer.md
│   └── gradle-expert.md
├── skills/                    # Skill definitions
│   ├── gradle-build.md
│   ├── java-development.md
│   ├── kotlin-development.md
│   ├── database-migration.md
│   ├── api-design.md
│   └── testing-strategy.md
├── commands/                  # Custom commands
│   ├── build.md
│   ├── test.md
│   ├── lint.md
│   ├── format.md
│   ├── run.md
│   └── clean.md
├── permissions/               # Permission rules
│   └── default.md
└── lsp/                       # Language Server Protocol
    └── default.md
```

## Agents

| Agent | Purpose |
|-------|---------|
| `college-admission-assistant` | Main development assistant |
| `code-reviewer` | Code review specialist |
| `test-writer` | Test engineering expert |
| `documentation-writer` | Technical documentation |
| `gradle-expert` | Gradle build specialist |

## Skills

| Skill | Description |
|-------|-------------|
| `gradle-build` | Gradle/Kotlin DSL expertise |
| `java-development` | Spring Boot, JPA, testing |
| `kotlin-development` | Idiomatic Kotlin, coroutines |
| `database-migration` | Flyway/Liquibase migrations |
| `api-design` | REST, OpenAPI, versioning |
| `testing-strategy` | Unit, integration, contract testing |

## Commands

| Command | Description |
|---------|-------------|
| `build` | Build project (`./gradlew build`) |
| `test` | Run tests (`./gradlew test`) |
| `lint` | Static analysis (`./gradlew detekt ktlintCheck`) |
| `format` | Code formatting (`./gradlew ktlintFormat`) |
| `run` | Run application (`./gradlew bootRun`) |
| `clean` | Clean artifacts (`./gradlew clean`) |

## Usage

### Switch Agent
```
> agent code-reviewer
```

### Use Skill
```
> skill gradle-build
```

### Run Command
```
> build
> test --coverage
> lint --fix
```

### Check Permissions
```
> permissions
```

## Customization

Edit `opencode.json` to:
- Change default agent
- Enable/disable skills
- Add custom commands
- Modify permissions
- Configure LSP servers

## Documentation

- [Opencode Agents](https://opencode.ai/docs/agents/)
- [Opencode Skills](https://opencode.ai/docs/skills/)
- [Opencode Commands](https://opencode.ai/docs/commands/)
- [Opencode Permissions](https://opencode.ai/docs/permissions/)
- [Opencode LSP](https://opencode.ai/docs/lsp/)