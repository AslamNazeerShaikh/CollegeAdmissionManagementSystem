# Legacy Java Android app (archived reference)

This folder holds the original Android-native Java app (`src/Java/app/` plus the
root Gradle wrapper: `build.gradle`, `settings.gradle`, `gradle.properties`,
`gradlew`, `gradle/`).

- Active development moved to `src/AvaloniaUi/` (shipping) and `src/PlatformUno/` (retained port).
- Keep this folder as the behavior/reference spec: offline bundled PDFs
  (`app/src/main/assets/*.pdf`), course popup logic (`Courses.java`), and the
  legacy registration WebSocket protocol (`Registration.java`,
  `ws://<host>:9999`, `$`-delimited fields).
- Gradle still builds it from the repo root (`settings.gradle` remaps `:app`
  to `src/Java/app`); do not delete until Phase 5 backend + offline-PDF
  replacements land in the .NET apps.
