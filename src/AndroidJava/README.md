# Legacy Java Android app (archived reference)

This folder holds the original Android-native Java app (`app/` plus its
self-contained Gradle project: `build.gradle`, `settings.gradle`,
`gradle.properties`, `gradlew`, `gradle/`). Build it from here, not repo root.

- Active development moved to `src/AvaloniaUi/` (shipping) and `src/PlatformUno/` (retained port).
- Keep this folder as the behavior/reference spec: offline bundled PDFs
  (`app/src/main/assets/*.pdf`), course popup logic (`Courses.java`), and the
  legacy registration WebSocket protocol (`Registration.java`,
  `ws://<host>:9999`, `$`-delimited fields).
- Gradle builds it from this folder (`settings.gradle` maps `:app`
  to `app/`); do not delete until Phase 5 backend + offline-PDF
  replacements land in the .NET apps.
