# College Admission Management System

CollegeAdmissionManagementSystem is used by schools, colleges & institutions for admission processes and study material management. Light weight yet feature-rich, built for maximum performance and security. Flexible enough to customize on the basis of your needs so as to run a smooth and efficient system.

Three codebases live in `src/`:

| Codebase | Stack | Status | Targets today |
|---|---|---|---|
| `src/AvaloniaUi/` | .NET 10 + Avalonia 12 | Shipping | Android, Desktop (Linux/macOS/Windows) |
| `src/PlatformUno/` | .NET 10 + Uno Platform 6 | Retained port | Android, Desktop, bare `net10.0` |
| `src/AndroidJava/` | Java + Android SDK (archived, see `src/AndroidJava/README.md`) | Reference only | Android 4.4+ (API 19+) |

![CollegeAdmissionManagementSystem CoverPage](https://github.com/AslamNazeerShaikh/CollegeAdmissionManagementSystem/blob/development/Images%20&%20Documents/1.jpg)

# To run the app

**Avalonia (`src/AvaloniaUi/CollegeAdmission/`)** — primary app:
- Desktop: `dotnet run --project CollegeAdmission.Desktop -f net10.0`
- Android: open `CollegeAdmission.slnx` in Rider/VS and deploy the `CollegeAdmission.Android` head to device
- Needs internet for syllabus PDFs (online URLs) and no permissions otherwise
- Sensor: disable Auto Rotation for the original portrait layout feel

**Uno (`src/PlatformUno/CollegeAdmission/`)** — secondary port:
- Desktop: `dotnet run --project CollegeAdmission -f net10.0-desktop`
- Android: deploy the `net10.0-android` target to device
- Tests: `dotnet test CollegeAdmission.Tests/CollegeAdmission.Tests.csproj`

**Legacy Java (`src/AndroidJava/`)** — archived reference:
- Open `src/AndroidJava/` in Android Studio and Run, or `./gradlew installDebug` from that folder
- Runs on Android `4.4 KitKat (API 19) or above`; works fully offline (28 bundled PDFs)
- Permissions: none required; disable Auto Rotation

**Tested devices:**
- Samsung M51 — Android 12 (Avalonia + Uno Android heads, Java app)
- iPhone 16 Plus — iOS 26 (no iOS head configured yet, see pending)
- MacBook Pro M5 Pro — macOS 26 (Desktop heads)
- Lenovo Yoga X1 Gen 2 — Fedora 44 (Desktop heads, `dotnet` 10.0.401 verified)

![Code Structure](https://github.com/AslamNazeerShaikh/CollegeAdmissionManagementSystem/blob/development/Images%20&%20Documents/0.jpg)

# To develop the app

**.NET apps (Avalonia + Uno):**
- SDK: `.NET 10 (10.0.401 verified)` + Android workload for the mobile heads
- IDE: Rider, VS 2022+, or VS Code + C# Dev Kit
- Hardware: `8GB RAM + 30GB free storage`
- OS: Fedora 44, macOS 26, or Windows 10+ (Xcode 26 + Apple Dev account only when the iOS head lands)

**Legacy Java app:**
- IDE: `Android Studio v2022.1.1 or above`
- SDK: `Android SDK v33 or above` (Gradle 7.5 wrapper, AGP 7.4.1, `minSdk 19 / targetSdk 33`)
- Hardware: `8GB RAM + 30GB free storage`

# Project dependencies

**Avalonia (`Directory.Packages.props`, centrally managed):**
- Avalonia `: 12.1.2` (Themes.Fluent, Fonts.Inter, Desktop, Android, iOS)
- CommunityToolkit.Mvvm `: 8.4.2`
- Avalonia.Labs.Lottie `: 12.0.2`
- Xamarin.AndroidX.Core.SplashScreen `: 1.0.1.15`

**Uno (`global.json` → Uno.Sdk `: 6.7.22`, features: Material, Mvvm, SkiaRenderer, Lottie):**
- SkiaSharp.Views.Uno.WinUI / SkiaSharp.Skottie `: 4.152.0`
- CommunityToolkit.Mvvm `: 8.4.2`
- Tests: NUnit `: 4.1.0` + NUnit3TestAdapter + Microsoft.NET.Test.Sdk

**Legacy Java (`src/AndroidJava/app/build.gradle`):**
- androidx.appcompat:appcompat `: 1.6.1`
- com.google.android.material:material `: 1.8.0`
- androidx.constraintlayout:constraintlayout `: 2.1.4`
- junit : junit `: 4.13.2`
- androidx.test.ext : junit `: 1.1.5`
- androidx.test.espresso:espresso-core `: 3.5.1`
- com.daimajia.androidanimations : library `: 2.4@aar`
- com.airbnb.android:lottie `: 3.6.0`
- com.orhanobut:dialogplus `: 1.11@aar`
- com.github.barteksc:android-pdf-viewer `: 3.2.0-beta.1`
- com.squareup.okhttp3:okhttp `: 4.9.1`

# Pending (see `docs/avalonia-crm-vision.md`)

Unify the 21-course catalog across .NET apps, add Avalonia tests, decide the Phase 5 registration backend, restore offline/in-app PDFs, move drafts to SQLite, add iOS/Windows store heads + CI/signing.
