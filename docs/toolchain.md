# Toolchain — required versions (verified 2026-09-15)

All versions below were verified working together on macOS arm64. Bump only as a set; workload 36.x rejects any JDK except 21.

| Tool | Version | Notes |
|---|---|---|
| .NET SDK | 10.0.400 | `/usr/local/share/dotnet`, macOS arm64 |
| `android` workload | 36.1.69 / 10.0.100 | `sudo dotnet workload install android` |
| JDK | Microsoft OpenJDK 21.0.12.1 LTS | `~/java/jdk-21.0.12.1+1/Contents/Home`. Workload 36.x requires **exactly JDK 21** (XA0030 rejects Studio JBR 25). Pass per-build: `-p:JavaSdkDirectory="$HOME/java/jdk-21.0.12.1+1/Contents/Home"` |
| adb | 1.0.41, v37.0.1 | `~/platform-tools/adb` |
| Avalonia | 12.1.2 (+ Fluent, Inter, Labs.Lottie) | Floating refs in `.csproj`; resolved in `obj/project.assets.json` |
| `CommunityToolkit.Mvvm` | 8.4.2 | ViewModels live in UI-agnostic `CollegeAdmission.Core` |
| csharp-ls | 0.27.0 | `~/.dotnet/tools/csharp-ls`; opencode uses built-in `csharp` server (`"lsp": true`), needs only the .NET SDK |
| App target | `net10.0-android`, min API 23 | `ApplicationId in.org.cocsit.collegeadmission`, APK via `AndroidPackageFormat=apk` |
| Test device | Galaxy M51 SM-M515F, Android 12 (API 31), 1080×2400 | Wireless debugging |

## Android deploy (M51, wireless)

```sh
# 1. Pair once: phone → Developer options → Wireless debugging → Pair with code
adb connect 192.168.1.6:39831   # IP:port shown on phone

# 2. Build + FastDeploy install (NOT plain `adb install` of the APK alone —
#    Debug assemblies live in .__override__, bare-APK install SIGABRTs on launch)
dotnet build -c Debug -t:Install \
  -p:JavaSdkDirectory="$HOME/java/jdk-21.0.12.1+1/Contents/Home"

# 3. Launch (use `am start`, NOT `monkey` — monkey injects random taps/typing)
adb shell am start -n in.org.cocsit.collegeadmission/crc640dda22ef3cc81aef.MainActivity

# 4. Logs / screenshot / hierarchy
adb logcat -c; adb logcat -d | grep -iE "monodroid|avalonia|FATAL"
adb exec-out screencap -p > /tmp/m51.png
adb shell uiautomator dump /sdcard/ui.xml
```

## Gotchas hit so far

- `/etc/paths.d/dotnet-cli-tools` contained literal `~/.dotnet/tools` (no tilde expansion via `path_helper`) → `csharp-ls` invisible. Fixed to `$HOME`-expanded path.
- `☰` (U+2630) renders blank in the bundled Inter font on Android → hamburger icon is drawn with three `Border` bars instead.
- Avalonia a11y `uiautomator` bounds are unreliable (14px heights); screenshots are 1:1 device pixels — trust those for tap coordinates.
