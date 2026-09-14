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

## Toolchain (modernized 2026-09-14, mirrors `MyDemoApp` reference)

- Gradle `9.6.0`, Android Gradle Plugin `9.4.0` (`gradle/libs.versions.toml`), JDK 25 toolchain
- `minSdk 31` (Android 12), `targetSdk 37`, `compileSdk 37`, Java 11
- Version catalog (`gradle/libs.versions.toml`); repos `google()` + `mavenCentral()` only (`jcenter()` removed)
- PDF viewer is the maintained `com.github.mhiew:android-pdf-viewer:3.2.0-beta.1` fork (same API; `barteksc` artifact only existed on dead jcenter)
- `switch` on `R.id` converted to `if-else` (required since AGP 8 non-final res IDs)

Build from this folder: `./gradlew :app:assembleDebug` (or `:app:assembleRelease`, `:app:testDebugUnitTest`).
