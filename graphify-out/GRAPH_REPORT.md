# Graph Report - CollegeAdmissionManagementSystem  (2026-09-14)

## Corpus Check
- 125 files · ~139,976 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 611 nodes · 808 edges · 48 communities (33 shown, 10 thin omitted)
- Extraction: 93% EXTRACTED · 7% INFERRED · 0% AMBIGUOUS · INFERRED: 59 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `4690af48`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- android.os.Bundle
- .RootOf
- CollegeAdmission.Views
- Registration
- opencode.json
- RegistrationViewModel
- ILauncherService
- .OpenUrlAsync
- RegistrationViewModel
- Page
- Page
- Opencode Configuration for College Admission Management System
- ApplicationController REST Endpoints
- AvaloniaServices.cs
- AvaloniaUi/CollegeAdmission/CollegeAdmission/CollegeAdmission.csproj
- MainViewModel
- CollegeAdmission.Tests
- CollegeAdmission.ViewModels
- gradlew
- Page
- UnitTest1
- CollegeAdmission
- Shared Assets
- Application
- App
- INavigationService
- Graphify Knowledge Graph Pipeline
- org.junit.Test
- Maintenance.java
- README.md
- MainActivity
- Course Catalog UI
- .BuildAvaloniaApp
- Course
- Uno Platform Rewrite Master Plan
- graphify.js
- Uno Platform Getting Started
- Uno Platform Theme Integration
- Extra Exports Wiki Neo4j SVG Benchmark
- ReadMe.md
- ColorPaletteOverride.xaml
- Readme.md
- REST API Design Principles

## God Nodes (most connected - your core abstractions)
1. `Page` - 32 edges
2. `RegistrationViewModel` - 30 edges
3. `RegistrationViewModel` - 28 edges
4. `MainViewModel` - 18 edges
5. `Page` - 16 edges
6. `App` - 15 edges
7. `Registration` - 13 edges
8. `CoursesPage` - 13 edges
9. `Opencode Configuration for College Admission Management System` - 13 edges
10. `RegistrationView` - 12 edges

## Surprising Connections (you probably didn't know these)
- `MCP Client AI Service Integration` --semantically_similar_to--> `Best Value Models Ranking`  [INFERRED] [semantically similar]
  .opencode/skills/csharp-development.md → docs/opencode-go-vs-commandcode-goat-analysis.md
- `MVVM with CommunityToolkit` --conceptually_related_to--> `Uno Platform Rewrite Master Plan`  [INFERRED]
  .opencode/skills/csharp-development.md → docs/modernization-master-plan.md
- `NativeAOT Compatible Code Patterns` --conceptually_related_to--> `Uno Platform Rewrite Master Plan`  [INFERRED]
  .opencode/skills/csharp-development.md → docs/modernization-master-plan.md
- `MCP Client AI Service Integration` --conceptually_related_to--> `AI Ready Architecture Voice Text Vision MCP`  [INFERRED]
  .opencode/skills/csharp-development.md → docs/modernization-master-plan.md
- `Cross-Platform SQLite Abstraction` --shares_data_with--> `SQLite Cross-Platform Strategy`  [INFERRED]
  .opencode/skills/csharp-development.md → docs/modernization-master-plan.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **.NET Uno Platform toolchain workflow** — _opencode_commands_dotnet_build_dotnet_build, _opencode_commands_dotnet_clean_dotnet_clean, _opencode_commands_dotnet_format_dotnet_format, _opencode_commands_dotnet_lint_dotnet_lint, _opencode_commands_dotnet_publish_dotnet_publish, _opencode_commands_dotnet_run_dotnet_run, _opencode_commands_dotnet_test_dotnet_test [EXTRACTED 1.00]
- **Gradle toolchain workflow** — _opencode_commands_build_build, _opencode_commands_clean_clean, _opencode_commands_test_test, _opencode_commands_lint_lint, _opencode_commands_format_format, _opencode_commands_run_run [EXTRACTED 1.00]
- **Graphify build query update workflow** — _opencode_skills_graphify_skill_pipeline, _opencode_skills_graphify_references_extraction_spec, _opencode_skills_graphify_references_query_traversal, _opencode_skills_graphify_references_update_incremental [EXTRACTED 1.00]
- **Cross-platform rewrite planning docs** — docs_modernization_master_plan_uno_rewrite, docs_modernization_audit_legacy_findings, docs_avalonia_migration_plan_path_b, docs_uno_platform_migration_plan_verdict [EXTRACTED 1.00]
- **Android resource and asset guidance docs** — src_avaloniaui_collegeadmission_collegeadmission_android_resources_aboutresources_android_resources, src_platformuno_collegeadmission_collegeadmission_platforms_android_assets_aboutassets_android_only_assets, src_platformuno_collegeadmission_collegeadmission_platforms_android_resources_aboutresources_android_only_resources [INFERRED 0.75]
- **Admission selection to registration flow** — images_documents_1_course_catalog_ui, images_documents_1_course_detail_popup, images_documents_2_registration_form_ui [INFERRED 0.85]
- **Opencode agent team** — _opencode_agents_college_admission_assistant_college_admission_assistant, _opencode_agents_code_reviewer_code_reviewer, _opencode_agents_csharp_code_reviewer_csharp_code_reviewer, _opencode_agents_csharp_uno_agent_csharp_uno_agent, _opencode_agents_test_writer_test_writer, _opencode_agents_documentation_writer_documentation_writer, _opencode_agents_gradle_expert_gradle_expert [INFERRED 0.85]
- **Persistence migration and testing flow** — _opencode_skills_database_migration_flyway_config, _opencode_skills_database_migration_migration_scripts, _opencode_skills_testing_strategy_testcontainers, _opencode_skills_csharp_development_sqlite_abstraction [INFERRED 0.85]

## Communities (48 total, 10 thin omitted)

### Community 0 - "android.os.Bundle"
Cohesion: 0.13
Nodes (14): android.os.Bundle, android.view.View, android.widget.Button, androidx.appcompat.app.AppCompatActivity, com.github.barteksc.pdfviewer.PDFView, OnClickListener, Courses, Override (+6 more)

### Community 1 - ".RootOf"
Cohesion: 0.08
Nodes (14): ViewNavigationService, Vm, CoursesView, RoutedEventArgs, MainMenuView, RoutedEventArgs, Control, RegistrationView (+6 more)

### Community 2 - "CollegeAdmission.Views"
Cohesion: 0.13
Nodes (8): Application, CollegeAdmission.Views, App, MainView, Root, VisualTreeAttachmentEventArgs, MainWindow, Window

### Community 3 - "Registration"
Cohesion: 0.20
Nodes (8): android.widget.EditText, okhttp3.Response, okhttp3.WebSocket, okhttp3.WebSocketListener, okio.ByteString, Override, Registration, SocketListener

### Community 4 - "opencode.json"
Cohesion: 0.06
Nodes (35): agents, default, list, chmod 777 *, rm -rf *, sudo *, commands, custom (+27 more)

### Community 5 - "RegistrationViewModel"
Cohesion: 0.08
Nodes (26): RegistrationViewModel, BirthDay, BirthMonth, BirthYear, BloodGroup, CourseName, CurrentAddress, FirstName (+18 more)

### Community 6 - "ILauncherService"
Cohesion: 0.33
Nodes (3): ILauncherService, UnoLauncherService, Task

### Community 7 - ".OpenUrlAsync"
Cohesion: 0.25
Nodes (5): Task, RelayCommand, Task, RelayCommand, Task

### Community 8 - "RegistrationViewModel"
Cohesion: 0.06
Nodes (32): RegistrationViewModel, BirthDay, BirthMonth, BirthYear, BloodGroup, CourseName, CurrentAddress, FirstName (+24 more)

### Community 9 - "Page"
Cohesion: 0.07
Nodes (30): BirthDay, BirthMonth, BirthYear, BloodGroup, CourseName, CurrentAddress, FirstName, Gender (+22 more)

### Community 10 - "Page"
Cohesion: 0.11
Nodes (20): Courses, ItemClickEventArgs, CautionAnim, FyButton, HeaderAnim, Page, PopupOverlay, PopupSubtitle (+12 more)

### Community 11 - "Opencode Configuration for College Admission Management System"
Cohesion: 0.11
Nodes (23): code-reviewer agent, college-admission-assistant agent, csharp-code-reviewer agent, csharp-uno-agent, documentation-writer agent, gradle-expert agent, test-writer agent, build command (Gradle) (+15 more)

### Community 12 - "ApplicationController REST Endpoints"
Cohesion: 0.12
Nodes (18): ApplicationController REST Endpoints, Standardized Error Response Format, OpenAPI Configuration with SpringDoc, MCP Client AI Service Integration, Cross-Platform SQLite Abstraction, Flyway Configuration, Versioned SQL Migration Scripts, Gradle Kotlin DSL Build Configuration (+10 more)

### Community 13 - "AvaloniaServices.cs"
Cohesion: 0.20
Nodes (8): Func, ILauncher, AppShell, Main, ShellView, AvaloniaLauncherService, Control, Task

### Community 14 - "AvaloniaUi/CollegeAdmission/CollegeAdmission/CollegeAdmission.csproj"
Cohesion: 0.10
Nodes (20): Avalonia, Avalonia.Android, Avalonia.Desktop, Avalonia.Fonts.Inter, Avalonia.Labs.Lottie, Avalonia.Themes.Fluent, AvaloniaUI.DiagnosticsSupport, Xamarin.AndroidX.Core.SplashScreen (+12 more)

### Community 15 - "MainViewModel"
Cohesion: 0.21
Nodes (5): INavigationService, MainViewModel, Courses, Registration, IReadOnlyList

### Community 16 - "CollegeAdmission.Tests"
Cohesion: 0.10
Nodes (17): net10.0-desktop, coverlet.collector, FluentAssertions, Microsoft.NET.Test.Sdk, NUnit, NUnit3TestAdapter, SkiaSharp.Skottie, SkiaSharp.Views.Uno.WinUI (+9 more)

### Community 17 - "CollegeAdmission.ViewModels"
Cohesion: 0.19
Nodes (8): CollegeAdmission.Models, CollegeAdmission.ViewModels, CollegeAdmission.Services, ObservableObject, ViewModelBase, CoursesViewModel, Courses, IReadOnlyList

### Community 18 - "gradlew"
Cohesion: 0.83
Nodes (3): gradlew script, die(), warn()

### Community 19 - "Page"
Cohesion: 0.13
Nodes (13): Page, ExitMessage, ExitOverlay, Page, WelcomeAnim, MainMenuPage, RoutedEventArgs, AnimatedVisualPlayer (+5 more)

### Community 20 - "UnitTest1"
Cohesion: 0.29
Nodes (4): CollegeAdmission.Tests, SetUp, UnitTest1, Test

### Community 22 - "Shared Assets"
Cohesion: 0.17
Nodes (11): Android Resources, Android R Class, Asset Scale Variants, Examples, Here is a cheat sheet, Shared Assets, Table of scales, Android-Only Assets (+3 more)

### Community 24 - "Application"
Cohesion: 0.20
Nodes (7): AvaloniaAndroidApplication, AvaloniaMainActivity, CollegeAdmission.Android, Application, App, AppBuilder, MainActivity

### Community 25 - "App"
Cohesion: 0.09
Nodes (13): LaunchActivatedEventArgs, NavigationFailedEventArgs, Application, App, Launcher, MainWindow, Navigation, RootFrame (+5 more)

### Community 26 - "INavigationService"
Cohesion: 0.20
Nodes (3): ILauncherService, INavigationService, Task

### Community 27 - "Graphify Knowledge Graph Pipeline"
Cohesion: 0.22
Nodes (9): URL Ingest and Watch Mode, Semantic Extraction Specification, GitHub Clone and Cross-Repo Merge, Commit Hook and Claude Integration, BFS DFS Traversal Query, Whisper Video Audio Transcription, Incremental Update and Cluster Only, Graphify Knowledge Graph Pipeline (+1 more)

### Community 28 - "org.junit.Test"
Cohesion: 0.33
Nodes (5): androidx.test.ext.junit.runners.AndroidJUnit4, org.junit.runner.RunWith, org.junit.Test, ExampleInstrumentedTest, ExampleUnitTest

### Community 32 - "MainActivity"
Cohesion: 0.22
Nodes (6): ApplicationActivity, CollegeAdmission.Droid, NativeApplication, Application, MainActivity, Bundle

### Community 34 - "Course Catalog UI"
Cohesion: 0.28
Nodes (9): Android Studio Project Structure, App Source Classes, Syllabus PDF Assets, Course Catalog UI, Course Detail Popup B.Sc. Software Engineering, Exit Confirmation Dialog, M.Sc. Software Engineering Detail, Registration Form UI (+1 more)

### Community 38 - ".BuildAvaloniaApp"
Cohesion: 0.29
Nodes (5): CollegeAdmission.Desktop, Program, App, AppBuilder, STAThread

### Community 39 - "Course"
Cohesion: 0.11
Nodes (18): Course, HasFy, HasSy, HasTy, CourseCatalog, All, IReadOnlyList, Course (+10 more)

### Community 40 - "Uno Platform Rewrite Master Plan"
Cohesion: 0.38
Nodes (7): MVVM with CommunityToolkit, NativeAOT Compatible Code Patterns, Avalonia Path B Rewrite Plan, Legacy Android Audit Findings, Uno Platform Rewrite Master Plan, Uno vs Avalonia Verdict, College Admission Android App Overview

### Community 47 - "Uno Platform Getting Started"
Cohesion: 0.67
Nodes (3): Rider IDE Support, Uno Platform Getting Started, Uno.Sdk

## Knowledge Gaps
- **196 isolated node(s):** `$schema`, `version`, `name`, `description`, `type` (+191 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 272 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **10 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainViewModel` connect `MainViewModel` to `.RootOf`, `CollegeAdmission.Views`, `ILauncherService`, `.OpenUrlAsync`, `RegistrationViewModel`, `Course`, `AvaloniaServices.cs`, `CollegeAdmission.ViewModels`?**
  _High betweenness centrality (0.089) - this node is a cross-community bridge._
- **Why does `RegistrationViewModel` connect `RegistrationViewModel` to `CollegeAdmission.ViewModels`, `Page`, `.RootOf`, `MainViewModel`?**
  _High betweenness centrality (0.085) - this node is a cross-community bridge._
- **Why does `RegistrationPage` connect `Page` to `RegistrationViewModel`, `Page`, `CollegeAdmission`?**
  _High betweenness centrality (0.071) - this node is a cross-community bridge._
- **What connects `$schema`, `version`, `name` to the rest of the system?**
  _196 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `android.os.Bundle` be split into smaller, more focused modules?**
  _Cohesion score 0.12643678160919541 - nodes in this community are weakly interconnected._
- **Should `.RootOf` be split into smaller, more focused modules?**
  _Cohesion score 0.08292682926829269 - nodes in this community are weakly interconnected._
- **Should `CollegeAdmission.Views` be split into smaller, more focused modules?**
  _Cohesion score 0.1323529411764706 - nodes in this community are weakly interconnected._