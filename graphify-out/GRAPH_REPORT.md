# Graph Report - CollegeAdmissionManagementSystem  (2026-09-19)

## Corpus Check
- 184 files · ~479,973 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1172 nodes · 1472 edges · 128 communities (63 shown, 18 thin omitted)
- Extraction: 99% EXTRACTED · 1% INFERRED · 0% AMBIGUOUS · INFERRED: 17 edges (avg confidence: 0.86)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `0d75afea`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- App.tsx
- Registration
- INavigationService
- 9. Deep element-by-element analysis (second pass, all 8 images)
- crm_models.dart
- package.json
- Page
- CrmViewModel
- opencode.json
- Page
- RegistrationViewModel
- crm_state.dart
- Page
- Avalonia CRM Vision — College Admission Management System (Full Feature Spec)
- my_application.cc
- What You Must Do When Invoked
- FlutterMacOS
- crm_theme.dart
- CollegeAdmission.Tests
- AvaloniaUi/CollegeAdmission/CollegeAdmission/CollegeAdmission.csproj
- main.dart
- tauri.conf.json
- compilerOptions
- CrmShellView
- courses.dart
- CollegeAdmission.ViewModels
- applications.dart
- package:flutter/material.dart
- Opencode Configuration for College Admission Management System
- pipeline.dart
- Course
- RustPlugin.kt
- CoursesViewModel
- Applicant
- Application
- AppShell
- crm_button.dart
- Conventional Commits (v1.0.0) — adopted
- org.junit.Test
- MainActivity
- graphify reference: extra exports and benchmark
- dashboard.dart
- .BuildAvaloniaApp
- App
- CrmStage
- College Admissions CRM — Tauri 2 (React + Vite)
- compilerOptions
- graphify reference: query, path, explain
- README.md
- UnitTest1
- App
- AvaloniaLauncherService
- default.json
- CrmModels.cs
- Here is a cheat sheet
- .onCreate
- android/gradlew
- graphify reference: add a URL and watch a folder
- graphify reference: commit hook and native CLAUDE.md integration
- graphify reference: incremental update and cluster-only
- AndroidJava/gradlew
- layout_breakpoints.dart
- lib.rs
- graphify.js
- graphify reference: GitHub clone and cross-repo merge
- graphify reference: transcribe video and audio
- Legacy Java Android app (archived reference)
- college_admission
- AGENTS.md
- .Main
- _FirstOrNull
- extraction-spec.md
- Maintenance.java
- ReadMe.md
- ColorPaletteOverride.xaml
- Readme.md
- Applicant?
- List
- college-admission
- .OpenSyllabusAsync
- CrmStage

## God Nodes (most connected - your core abstractions)
1. `CrmViewModel` - 63 edges
2. `Page` - 32 edges
3. `RegistrationViewModel` - 29 edges
4. `Avalonia CRM Vision — College Admission Management System (Full Feature Spec)` - 24 edges
5. `useCrm()` - 18 edges
6. `CrmShellView` - 17 edges
7. `Page` - 16 edges
8. `compilerOptions` - 16 edges
9. `App` - 15 edges
10. `Reference UI Analysis — Brightway Carpet Care CRM (8 Screenshots)` - 14 edges

## Surprising Connections (you probably didn't know these)
- `CrmViewModel` --references--> `Course`  [EXTRACTED]
  src/AvaloniaUi/CollegeAdmission/CollegeAdmission.Core/ViewModels/CrmViewModel.cs → src/AvaloniaUi/CollegeAdmission/CollegeAdmission.Core/Models/Course.cs
- `CoursesViewModel` --references--> `Course`  [EXTRACTED]
  src/PlatformUno/CollegeAdmission/CollegeAdmission.Core/ViewModels/CoursesViewModel.cs → src/AvaloniaUi/CollegeAdmission/CollegeAdmission.Core/Models/Course.cs
- `CoursesPage` --references--> `Course`  [EXTRACTED]
  src/PlatformUno/CollegeAdmission/CollegeAdmission/CoursesPage.xaml.cs → src/AvaloniaUi/CollegeAdmission/CollegeAdmission.Core/Models/Course.cs
- `CrmViewModel` --references--> `CrmStage`  [EXTRACTED]
  src/AvaloniaUi/CollegeAdmission/CollegeAdmission.Core/ViewModels/CrmViewModel.cs → src/AvaloniaUi/CollegeAdmission/CollegeAdmission.Core/Models/CrmModels.cs
- `CrmSeed` --references--> `Applicant`  [EXTRACTED]
  src/AvaloniaUi/CollegeAdmission/CollegeAdmission.Core/ViewModels/CrmViewModel.cs → src/AvaloniaUi/CollegeAdmission/CollegeAdmission.Core/Models/CrmModels.cs

## Import Cycles
- None detected.

## Communities (128 total, 18 thin omitted)

### Community 0 - "App.tsx"
Cohesion: 0.12
Nodes (40): react, App(), Nav(), Shell(), TopBar(), Applicant, Course, COURSE_CATALOG (+32 more)

### Community 1 - "Registration"
Cohesion: 0.08
Nodes (22): android.os.Bundle, android.view.View, android.widget.Button, android.widget.EditText, androidx.appcompat.app.AppCompatActivity, com.github.barteksc.pdfviewer.PDFView, okhttp3.Response, okhttp3.WebSocket (+14 more)

### Community 2 - "INavigationService"
Cohesion: 0.17
Nodes (4): ILauncherService, INavigationService, Task, FrameNavigationService

### Community 3 - "9. Deep element-by-element analysis (second pass, all 8 images)"
Cohesion: 0.05
Nodes (39): 10. Second-order details easily missed, 11. UX principles reinforced by second pass, 12. DX — build tokens distilled from pixels, 13. What screenshots don't show (design ourselves, same language), 1. What the 8 images actually show, 2.1 Palette — warm neutral, not cold gray, 2.2 Typography — macOS native, medium not bold, 2.3 Spacing — 8px grid, generous (+31 more)

### Community 4 - "crm_models.dart"
Cohesion: 0.05
Nodes (39): bool get, int docsOk, docsTotal,, int filled,, Applicant, _base, copyWith, Course, courseCatalog (+31 more)

### Community 5 - "package.json"
Cohesion: 0.05
Nodes (37): @fontsource-variable/inter, react-dom, tailwindcss, @tailwindcss/vite, @tauri-apps/api, @tauri-apps/cli, @tauri-apps/plugin-opener, @types/react (+29 more)

### Community 6 - "Page"
Cohesion: 0.07
Nodes (30): BirthDay, BirthMonth, BirthYear, BloodGroup, CourseName, CurrentAddress, FirstName, Gender (+22 more)

### Community 7 - "CrmViewModel"
Cohesion: 0.05
Nodes (44): IEnumerable, ObservableCollection, CrmViewModel, ActionQueue, Applicants, CountApplied, CountEnquiry, CountEnrolled (+36 more)

### Community 8 - "opencode.json"
Cohesion: 0.05
Nodes (37): agents, default, list, chmod 777 *, rm -rf *, sudo *, commands, custom (+29 more)

### Community 9 - "Page"
Cohesion: 0.11
Nodes (15): CancellationTokenSource, NavigatingCancelEventArgs, Page, ExitMessage, ExitOverlay, Page, WelcomeAnim, MainMenuPage (+7 more)

### Community 10 - "RegistrationViewModel"
Cohesion: 0.06
Nodes (33): Dictionary, task, RegistrationViewModel, BirthDay, BirthMonth, BirthYear, BloodGroup, CourseName (+25 more)

### Community 11 - "crm_state.dart"
Cohesion: 0.07
Nodes (29): double get, int get, actionQueue, addApplication, addEnquiry, advanceStage, applicants, collectFee (+21 more)

### Community 12 - "Page"
Cohesion: 0.12
Nodes (20): Courses, ItemClickEventArgs, CautionAnim, FyButton, HeaderAnim, Page, PopupOverlay, PopupSubtitle (+12 more)

### Community 13 - "Avalonia CRM Vision — College Admission Management System (Full Feature Spec)"
Cohesion: 0.07
Nodes (27): 0. Core architectural decision (binding), 10.1 Multi-step application form (applicant-facing), 10.2 Save as draft, 10.3 Smart validation (inline, not on Submit), 10. Application management (heart of the product), 11. Applications list + filtering + search, 12. Enquiry / Lead management (pre-applicant CRM), 13. Documents + verification workflow (+19 more)

### Community 14 - "my_application.cc"
Cohesion: 0.09
Nodes (22): FlPluginRegistry, FlView, GApplication, gboolean, gchar, GObject, GtkApplication, MyApplicationClass (+14 more)

### Community 15 - "What You Must Do When Invoked"
Cohesion: 0.08
Nodes (24): For /graphify add and --watch, For /graphify query, For the commit hook and native CLAUDE.md integration, For --update and --cluster-only, /graphify, Honesty Rules, Interpreter guard for subcommands, Part A - Structural extraction for code files (+16 more)

### Community 16 - "FlutterMacOS"
Cohesion: 0.11
Nodes (15): Bool, Cocoa, FlutterAppDelegate, FlutterMacOS, FlutterPluginRegistry, Foundation, NSApplication, NSWindow (+7 more)

### Community 17 - "crm_theme.dart"
Cohesion: 0.09
Nodes (22): accent, accentInk, accentSoft, bad, badBg, CrmColors, crmTheme, info (+14 more)

### Community 18 - "CollegeAdmission.Tests"
Cohesion: 0.10
Nodes (17): net10.0-desktop, coverlet.collector, FluentAssertions, Microsoft.NET.Test.Sdk, NUnit, NUnit3TestAdapter, SkiaSharp.Skottie, SkiaSharp.Views.Uno.WinUI (+9 more)

### Community 19 - "AvaloniaUi/CollegeAdmission/CollegeAdmission/CollegeAdmission.csproj"
Cohesion: 0.10
Nodes (19): Avalonia, Avalonia.Android, Avalonia.Desktop, Avalonia.Fonts.Inter, Avalonia.Themes.Fluent, AvaloniaUI.DiagnosticsSupport, Xamarin.AndroidX.Core.SplashScreen, CollegeAdmission.Android (+11 more)

### Community 20 - "main.dart"
Cohesion: 0.10
Nodes (20): detail_rail.dart, package:flutter/foundation.dart, sections/applications.dart, sections/courses.dart, sections/dashboard.dart, sections/fees.dart, sections/pipeline.dart, _body (+12 more)

### Community 21 - "tauri.conf.json"
Cohesion: 0.10
Nodes (19): app, security, windows, build, beforeBuildCommand, beforeDevCommand, devUrl, frontendDist (+11 more)

### Community 22 - "compilerOptions"
Cohesion: 0.10
Nodes (19): compilerOptions, allowImportingTsExtensions, isolatedModules, jsx, lib, module, moduleResolution, noEmit (+11 more)

### Community 23 - "CrmShellView"
Cohesion: 0.18
Nodes (6): AvaloniaPropertyChangedEventArgs, CrmShellView, Vm, RoutedEventArgs, UserControl, VisualTreeAttachmentEventArgs

### Community 24 - "courses.dart"
Cohesion: 0.15
Nodes (15): ChangeNotifier, ../crm_button.dart, ../crm_state.dart, ../crm_theme.dart, package:url_launcher/url_launcher.dart, CrmState, build, DetailRail (+7 more)

### Community 25 - "CollegeAdmission.ViewModels"
Cohesion: 0.18
Nodes (6): CollegeAdmission.Models, CollegeAdmission.ViewModels, CollegeAdmission, CollegeAdmission.Services, UnoLauncherService, Task

### Community 26 - "applications.dart"
Cohesion: 0.14
Nodes (14): ../layout_breakpoints.dart, _Logo, _NavBody, ApplicationsSection, build, _cards, _chip, _H (+6 more)

### Community 27 - "package:flutter/material.dart"
Cohesion: 0.14
Nodes (12): dart:ui, package:college_admission/layout_breakpoints.dart, package:college_admission/main.dart, package:flutter/material.dart, package:flutter_test/flutter_test.dart, main, _boot, _hasHorizontalOurs (+4 more)

### Community 28 - "Opencode Configuration for College Admission Management System"
Cohesion: 0.15
Nodes (12): Agents, Check Permissions, Commands, Customization, Documentation, Opencode Configuration for College Admission Management System, Run Command, Skills (+4 more)

### Community 29 - "pipeline.dart"
Cohesion: 0.18
Nodes (12): CrmApp, _CrmAppState, build, createState, initState, PipelineSection, _PipelineSectionState, _row (+4 more)

### Community 30 - "Course"
Cohesion: 0.11
Nodes (18): Course, HasFy, HasSy, HasTy, CourseCatalog, All, IReadOnlyList, Course (+10 more)

### Community 31 - "RustPlugin.kt"
Cohesion: 0.31
Nodes (6): DefaultTask, Plugin, Project, BuildTask, Config, RustPlugin

### Community 32 - "CoursesViewModel"
Cohesion: 0.17
Nodes (9): ObservableObject, ILauncherService, Task, ViewModelBase, CoursesViewModel, Courses, IReadOnlyList, RelayCommand (+1 more)

### Community 33 - "Applicant"
Cohesion: 0.19
Nodes (7): Applicant, DocsLabel, DocsPending, FeeLabel, StageName, Applicant, RelayCommand

### Community 34 - "Application"
Cohesion: 0.20
Nodes (7): AvaloniaAndroidApplication, AvaloniaMainActivity, CollegeAdmission.Android, Application, App, AppBuilder, MainActivity

### Community 35 - "AppShell"
Cohesion: 0.50
Nodes (4): Control, AppShell, Crm, ShellView

### Community 36 - "crm_button.dart"
Cohesion: 0.20
Nodes (9): EdgeInsetsGeometry, build, CrmButton, CrmButtonKind, kind, label, onPressed, padding (+1 more)

### Community 37 - "Conventional Commits (v1.0.0) — adopted"
Cohesion: 0.29
Nodes (6): Conventional Commits (v1.0.0) — adopted, Examples, Format, Notes, Scopes (this repo), Types we use

### Community 38 - "org.junit.Test"
Cohesion: 0.33
Nodes (5): androidx.test.ext.junit.runners.AndroidJUnit4, org.junit.runner.RunWith, org.junit.Test, ExampleInstrumentedTest, ExampleUnitTest

### Community 39 - "MainActivity"
Cohesion: 0.22
Nodes (6): ApplicationActivity, CollegeAdmission.Droid, NativeApplication, Application, MainActivity, Bundle

### Community 40 - "graphify reference: extra exports and benchmark"
Cohesion: 0.22
Nodes (8): graphify reference: extra exports and benchmark, Step 6b - Wiki (only if --wiki flag), Step 7 - Neo4j export (only if --neo4j or --neo4j-push flag), Step 7a - FalkorDB export (only if --falkordb or --falkordb-push flag), Step 7b - SVG export (only if --svg flag), Step 7c - GraphML export (only if --graphml flag), Step 7d - MCP server (only if --mcp flag), Step 8 - Token reduction benchmark (only if total_words > 5000)

### Community 41 - "dashboard.dart"
Cohesion: 0.29
Nodes (6): ../crm_models.dart, _actionCard, build, _funnel, _stat, state

### Community 42 - ".BuildAvaloniaApp"
Cohesion: 0.29
Nodes (5): CollegeAdmission.Desktop, Program, App, AppBuilder, STAThread

### Community 43 - "App"
Cohesion: 0.16
Nodes (10): LaunchActivatedEventArgs, NavigationFailedEventArgs, Application, App, Launcher, MainWindow, Navigation, RootFrame (+2 more)

### Community 44 - "CrmStage"
Cohesion: 0.25
Nodes (8): CrmStage, Applied, Enquiry, Enrolled, FeePaid, Merit, Offered, Verified

### Community 45 - "College Admissions CRM — Tauri 2 (React + Vite)"
Cohesion: 0.25
Nodes (7): College Admissions CRM — Tauri 2 (React + Vite), Dev / build / test, Recommended IDE Setup, Required toolchain (macOS), Runtime footprint (measured 2026-09-19, macOS release build), Vertical layout rules, Window (macOS landscape, resizable)

### Community 46 - "compilerOptions"
Cohesion: 0.25
Nodes (7): compilerOptions, allowSyntheticDefaultImports, composite, module, moduleResolution, skipLibCheck, include

### Community 47 - "graphify reference: query, path, explain"
Cohesion: 0.33
Nodes (5): For /graphify explain, For /graphify path, graphify reference: query, path, explain, Step 0 — Constrained query expansion (REQUIRED before traversal), Step 1 — Traversal

### Community 48 - "README.md"
Cohesion: 0.29
Nodes (6): College Admission Management System, Pending (see `docs/avalonia-crm-vision.md`), Project dependencies, Screenshots (Linux, 2026-09-19 — all 4 apps running, verified no crashes), To develop the app, To run the app

### Community 49 - "UnitTest1"
Cohesion: 0.29
Nodes (4): CollegeAdmission.Tests, SetUp, UnitTest1, Test

### Community 50 - "App"
Cohesion: 0.25
Nodes (5): Application, CollegeAdmission.Views, App, MainWindow, Window

### Community 51 - "AvaloniaLauncherService"
Cohesion: 0.40
Nodes (4): Func, ILauncher, AvaloniaLauncherService, Task

### Community 52 - "default.json"
Cohesion: 0.33
Nodes (5): description, identifier, permissions, $schema, windows

### Community 53 - "CrmModels.cs"
Cohesion: 0.33
Nodes (5): CourseFill, Pct, PctLabel, CrmAction, CrmSeed

### Community 54 - "Here is a cheat sheet"
Cohesion: 0.40
Nodes (4): Examples, Here is a cheat sheet, Shared Assets, Table of scales

### Community 55 - ".onCreate"
Cohesion: 0.60
Nodes (3): Bundle, MainActivity, TauriActivity

### Community 56 - "android/gradlew"
Cohesion: 0.70
Nodes (4): gradlew script, die(), save(), warn()

### Community 57 - "graphify reference: add a URL and watch a folder"
Cohesion: 0.50
Nodes (3): For /graphify add, For --watch, graphify reference: add a URL and watch a folder

### Community 58 - "graphify reference: commit hook and native CLAUDE.md integration"
Cohesion: 0.50
Nodes (3): For git commit hook, For native CLAUDE.md integration, graphify reference: commit hook and native CLAUDE.md integration

### Community 59 - "graphify reference: incremental update and cluster-only"
Cohesion: 0.50
Nodes (3): For --cluster-only, For --update (incremental re-extraction), graphify reference: incremental update and cluster-only

### Community 60 - "AndroidJava/gradlew"
Cohesion: 0.83
Nodes (3): gradlew script, die(), warn()

### Community 61 - "layout_breakpoints.dart"
Cohesion: 0.50
Nodes (3): applicationsUseTable, ShellLayout, shellLayoutForWidth

### Community 68 - "AGENTS.md"
Cohesion: 0.50
Nodes (3): graphify, response style, toolchain installs

## Knowledge Gaps
- **538 isolated node(s):** `$schema`, `version`, `name`, `description`, `type` (+533 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 706 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **18 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `CrmViewModel` connect `CrmViewModel` to `CoursesViewModel`, `Applicant`, `AppShell`, `CrmStage`, `CrmModels.cs`, `CrmShellView`, `.OpenSyllabusAsync`, `Course`?**
  _High betweenness centrality (0.054) - this node is a cross-community bridge._
- **Why does `ILauncherService` connect `CoursesViewModel` to `CrmViewModel`, `RegistrationViewModel`, `App`, `AvaloniaLauncherService`, `CollegeAdmission.ViewModels`?**
  _High betweenness centrality (0.027) - this node is a cross-community bridge._
- **Why does `RegistrationPage` connect `Page` to `CollegeAdmission.ViewModels`, `RegistrationViewModel`, `Page`?**
  _High betweenness centrality (0.022) - this node is a cross-community bridge._
- **What connects `$schema`, `version`, `name` to the rest of the system?**
  _538 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `App.tsx` be split into smaller, more focused modules?**
  _Cohesion score 0.11949685534591195 - nodes in this community are weakly interconnected._
- **Should `Registration` be split into smaller, more focused modules?**
  _Cohesion score 0.0815686274509804 - nodes in this community are weakly interconnected._
- **Should `9. Deep element-by-element analysis (second pass, all 8 images)` be split into smaller, more focused modules?**
  _Cohesion score 0.05 - nodes in this community are weakly interconnected._