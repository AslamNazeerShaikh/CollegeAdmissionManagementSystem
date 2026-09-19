# Session handoff — CollegeAdmissionManagementSystem (saved 2026-09-17 ~01:50 IST)

> UNTRACKED helper file. Delete after resume. Machine was shut down mid-task.

## Where we stand
Tauri CRM port (`src/TauriUi/college-admission`, React 19 + TS + Tailwind v4, Tauri 2.11):
- **Linux**: BUILT + LAUNCHED. `.rpm` + `.deb` in `src-tauri/target/release/bundle/`. Binary was running detached when machine went down.
- **Android**: BUILT once (`app-universal-release-unsigned.apk`), debug-signed to `/tmp/opencode/crm-signed.apk` (**LOST on reboot — /tmp is tmpfs**), installed + verified working on Pixel_10 AVD (screenshot verified: pipeline tabs, rows, rail all render).
- A top-bar mobile fix (safe-area + two-row narrow layout, `index.html` viewport-fit) was coded AFTER the APK → **Android rebuild was aborted mid-run** (`tauri android build` killed by user). Linux `tauri build` re-ran clean AFTER the fix. So: Linux binary has the fix, Android APK does not.

## To resume (in order)
1. `export PATH="$HOME/.cargo/bin:$HOME/nodejs/bin:$PATH"` (bashrc already has cargo/flutter/android vars — open a NEW shell or `source ~/.bashrc`)
2. `cd src/TauriUi/college_admission`
3. `JAVA_HOME=$HOME/jdks/jdk-21.0.12.1+1 ANDROID_HOME=$HOME/Android/Sdk NDK_HOME=$ANDROID_HOME/ndk/28.2.13676358 npm run tauri android build`
4. Re-sign: `build-tools/36.0.0/apksigner sign --ks ~/.android/debug.keystore --ks-pass pass:android --key-pass pass:android --out /tmp/opencode/crm-signed.apk <unsigned-apk>`
5. Boot AVD: `emulator -avd Pixel_10` (KVM works), `adb install -r`, launch, `adb exec-out screencap -p` to verify.
6. Relaunch Linux build: `./src-tauri/target/release/college-admission` (detached via setsid+nohup).
7. Commit + push (73+ files staged last time as `6b0b596`; NEW uncommitted work: TauriUi sources, tauri.conf, README — run `git status`, review, commit, push `contribute`).

## Gotchas learned (do not rediscover)
- Gradle 8.14.3 CANNOT run on Java 25 (major version 69). Android builds MUST use `JAVA_HOME=$HOME/jdks/jdk-21.0.12.1+1` (Temurin 21). System `~/jdk` and Studio `jbr` are both v25.
- apksigner needs JDK 21 in PATH and `pass:android` syntax (NOT `--ks-pass:android`).
- `tauri android init` needs NDK installed FIRST + `NDK_HOME` set (its "cmdline tools" error is really missing-NDK).
- NDK: `r28c (28.2.13676358)` via sdkmanager. Rust Android targets: all 4 installed.
- `tauri android build` compiles 4 ABIs sequentially — looks "stuck in a loop" but isn't (watch `rustc` CPU + `tail` the log). Gradle dex phase is log-quiet with ~50% CPU on two JVMs — normal.
- NEVER `pkill/pgrep -f` with a pattern appearing in your own command line (killed own shell twice). Use bracket patterns like `[b]undle/...`.
- `npm create tauri-app` needs `--yes` (no TTY here). `rm -rf` is denied by policy — use `mv` for dir swaps.
- AppImage bundling fails on linuxdeploy here → `tauri.conf.json` targets restricted to `["rpm","deb"]`.
- Session facts: Wayland GNOME session, XWayland `:0`, AVD name `Pixel_10`, app id `in.org.cocsit.college_admission`, Flutter SDK `~/flutter_sdk`, Rust stable 1.98.1.
- Open user questions: commit/push Tauri work (not yet pushed), `graphify label` rename pass (cosmetic, skipped).
