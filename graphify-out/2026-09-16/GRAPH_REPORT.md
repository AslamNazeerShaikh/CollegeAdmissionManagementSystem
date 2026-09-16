# Graph Report - CollegeAdmissionManagementSystem  (2026-09-16)

## Corpus Check
- 69 files · ~609,972 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 722 nodes · 894 edges · 74 communities (41 shown, 27 thin omitted)
- Extraction: 91% EXTRACTED · 9% INFERRED · 0% AMBIGUOUS · INFERRED: 81 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Community Hubs (Navigation)
- Android Java Imports
- Uno Registration Form
- Reference UI Design Tokens
- OpenCode Tool Config
- Android Registration Fields
- CRM Pipeline ViewModel
- Uno App Lifecycle
- NuGet Test Dependencies
- Agent Command Catalog
- Courses Page UI
- CRM Vision Spec
- Course Catalog Model
- Avalonia Shell Wiring
- Main Menu Navigation
- Pipeline Actions State
- Backend Architecture Patterns
- Design Reference Mapping
- BCA BSc Syllabus CBCS
- Android Asset Resources
- Avalonia App Bootstrap
- Avalonia Android Bootstrap
- Avalonia Shell Services
- Graphify Pipeline Docs
- Android Java Tests
- Uno Android Entry
- Android App Structure
- Avalonia Desktop Entry
- Uno Codebehind Files
- Uno Test Suite
- Launcher Navigation Services
- Reference Screenshots UI
- Admission Stage Enum
- CRM Models Seed
- MVVM ViewModel Base
- BBA Syllabus SRTMU
- App Illustrations Set
- Syllabus Launcher Command
- Uno Avalonia Brand Assets
- BSc Biotech Syllabus
- Gradle Wrapper Script
- Graphify Plugin Shim
- Legacy Android README
- Launcher Icon Set
- Graphify Export Options
- UI Recreation Prompt
- Platform Port Strategy
- BSc SE First Second Year
- MSc BT Years
- MSc CM Years
- MSc CS Years
- MSc SA Years
- Maintenance Activity
- Syllabus Command Overload
- Uno Getting Started
- Color Palette Override
- Run Config Notes
- REST API Principles
- MCP AI Integration
- MVVM Toolkit Pattern
- NativeAOT Code Patterns
- Uno Theme Integration
- CRM Reference Example
- BSc NT Third Year
- BSc SE Third Year
- Back Nav Icon
- App Singleton
- AppBuilder Setup
- STAThread Entry

## God Nodes (most connected - your core abstractions)
1. `CrmViewModel` - 53 edges
2. `Page` - 32 edges
3. `RegistrationViewModel` - 29 edges
4. `Avalonia CRM Vision — College Admission Management System (Full Feature Spec)` - 24 edges
5. `CrmShellView` - 17 edges
6. `Page` - 16 edges
7. `App` - 15 edges
8. `Applicant` - 14 edges
9. `9. Deep element-by-element analysis (second pass, all 8 images)` - 14 edges
10. `Reference UI Analysis — Brightway Carpet Care CRM (8 Screenshots)` - 14 edges

## Surprising Connections (you probably didn't know these)
- `CollegeAdmission` --references--> `Uno.Sdk`  [EXTRACTED]
  src/PlatformUno/CollegeAdmission/CollegeAdmission/CollegeAdmission.csproj → src/PlatformUno/CollegeAdmission/CollegeAdmission/ReadMe.md
- `Graphify Workflow Rules` --references--> `Graphify Knowledge Graph Pipeline`  [EXTRACTED]
  AGENTS.md → .opencode/skills/graphify/SKILL.md
- `MVVM with CommunityToolkit Pattern` --conceptually_related_to--> `AppShell with Sidebar and Detail Rail`  [INFERRED]
  .opencode/agents/csharp-uno-agent.md → docs/avalonia-crm-vision.md
- `ApplicationController REST Endpoints` --conceptually_related_to--> `Spring Boot JPA Repository Service`  [INFERRED]
  .opencode/skills/api-design.md → .opencode/skills/java-development.md
- `Pact Contract Testing` --conceptually_related_to--> `ApplicationController REST Endpoints`  [INFERRED]
  .opencode/skills/testing-strategy.md → .opencode/skills/api-design.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **.NET Uno Platform toolchain workflow** — _opencode_commands_dotnet_build_dotnet_build, _opencode_commands_dotnet_clean_dotnet_clean, _opencode_commands_dotnet_format_dotnet_format, _opencode_commands_dotnet_lint_dotnet_lint, _opencode_commands_dotnet_publish_dotnet_publish, _opencode_commands_dotnet_run_dotnet_run, _opencode_commands_dotnet_test_dotnet_test [EXTRACTED 1.00]
- **Gradle toolchain workflow** — _opencode_commands_build_build, _opencode_commands_clean_clean, _opencode_commands_test_test, _opencode_commands_lint_lint, _opencode_commands_format_format, _opencode_commands_run_run [EXTRACTED 1.00]
- **Graphify build query update workflow** — _opencode_skills_graphify_skill_pipeline, _opencode_skills_graphify_references_extraction_spec, _opencode_skills_graphify_references_query_traversal, _opencode_skills_graphify_references_update_incremental [EXTRACTED 1.00]
- **Shared Reference Design System Consistency** — docs_referenceui_llm_chat_shared_design_system, docs_referenceui_reference_ui_analysis_design_tokens, docs_referenceui_uiprompt_component_architecture [EXTRACTED 1.00]
- **Android resource and asset guidance docs** — src_avaloniaui_collegeadmission_collegeadmission_android_resources_aboutresources_android_resources, src_platformuno_collegeadmission_collegeadmission_platforms_android_assets_aboutassets_android_only_assets, src_platformuno_collegeadmission_collegeadmission_platforms_android_resources_aboutresources_android_only_resources [INFERRED 0.75]
- **Admission selection to registration flow** — images_documents_1_course_catalog_ui, images_documents_1_course_detail_popup, images_documents_2_registration_form_ui [INFERRED 0.85]
- **College CRM Adaptation Of Reference UI** — docs_referenceui_reference_ui_analysis_college_mapping, docs_avalonia_crm_vision_pipeline_as_merit_board, docs_avalonia_crm_vision_admission_pipeline, docs_avalonia_crm_vision_application_drawer [INFERRED 0.85]
- **Opencode agent team** — _opencode_agents_college_admission_assistant_college_admission_assistant, _opencode_agents_code_reviewer_code_reviewer, _opencode_agents_csharp_code_reviewer_csharp_code_reviewer, opencode_agents_csharp_uno_agent_csharp_uno_agent, _opencode_agents_test_writer_test_writer, _opencode_agents_documentation_writer_documentation_writer, _opencode_agents_gradle_expert_gradle_expert [INFERRED 0.85]
- **Persistence migration and testing flow** — _opencode_skills_database_migration_flyway_config, _opencode_skills_database_migration_migration_scripts, _opencode_skills_testing_strategy_testcontainers, _opencode_skills_csharp_development_sqlite_abstraction [INFERRED 0.85]
- **BBA degree FY-SY-TY progression** — src_androidjava_app_src_main_assets_bba_fy_syllabus, src_androidjava_app_src_main_assets_bba_sy_syllabus, src_androidjava_app_src_main_assets_bba_ty_syllabus [INFERRED 0.85]
- **BCA degree FY-SY-TY progression** — src_androidjava_app_src_main_assets_cbcs_bca_fy_syllabus, src_androidjava_app_src_main_assets_cbcs_bca_sy_syllabus, src_androidjava_app_src_main_assets_cbcs_bca_ty_syllabus [INFERRED 0.85]
- **BSc Biotechnology degree FY-SY-TY progression** — src_androidjava_app_src_main_assets_cbcs_bsc_bt_fy_syllabus, src_androidjava_app_src_main_assets_cbcs_bsc_bt_sy_syllabus, src_androidjava_app_src_main_assets_cbcs_bsc_bt_ty_syllabus [INFERRED 0.85]
- **BSc Software Engineering degree** — src_androidjava_app_src_main_assets_cbcs_bsc_se_fy_bsc_software_engineering_first_year, src_androidjava_app_src_main_assets_cbcs_bsc_se_sy_bsc_software_engineering_second_year, src_androidjava_app_src_main_assets_cbcs_bsc_se_ty_bsc_software_engineering_third_year [INFERRED 0.85]
- **MSc Biotechnology degree** — src_androidjava_app_src_main_assets_cbcs_msc_bt_fy_msc_biotechnology_first_year, src_androidjava_app_src_main_assets_cbcs_msc_bt_sy_msc_biotechnology_second_year [INFERRED 0.85]
- **MSc Computer Science degree** — src_androidjava_app_src_main_assets_cbcs_msc_cs_fy_msc_computer_science_first_year, src_androidjava_app_src_main_assets_cbcs_msc_cs_sy_msc_computer_science_second_year [INFERRED 0.85]

## Communities (74 total, 27 thin omitted)

### Community 0 - "Android Java Imports"
Cohesion: 0.08
Nodes (22): android.os.Bundle, android.view.View, android.widget.Button, android.widget.EditText, androidx.appcompat.app.AppCompatActivity, com.github.barteksc.pdfviewer.PDFView, okhttp3.Response, okhttp3.WebSocket (+14 more)

### Community 1 - "Uno Registration Form"
Cohesion: 0.06
Nodes (32): Dictionary, RegistrationViewModel, BirthDay, BirthMonth, BirthYear, BloodGroup, CourseName, CurrentAddress (+24 more)

### Community 2 - "Reference UI Design Tokens"
Cohesion: 0.05
Nodes (39): 10. Second-order details easily missed, 11. UX principles reinforced by second pass, 12. DX — build tokens distilled from pixels, 13. What screenshots don't show (design ourselves, same language), 1. What the 8 images actually show, 2.1 Palette — warm neutral, not cold gray, 2.2 Typography — macOS native, medium not bold, 2.3 Spacing — 8px grid, generous (+31 more)

### Community 3 - "OpenCode Tool Config"
Cohesion: 0.05
Nodes (38): agents, default, list, chmod 777 *, rm -rf *, sudo *, commands, custom (+30 more)

### Community 4 - "Android Registration Fields"
Cohesion: 0.07
Nodes (30): BirthDay, BirthMonth, BirthYear, BloodGroup, CourseName, CurrentAddress, FirstName, Gender (+22 more)

### Community 5 - "CRM Pipeline ViewModel"
Cohesion: 0.06
Nodes (36): IEnumerable, ObservableCollection, CrmViewModel, ActionQueue, Applicants, CountApplied, CountEnquiry, CountEnrolled (+28 more)

### Community 6 - "Uno App Lifecycle"
Cohesion: 0.07
Nodes (16): LaunchActivatedEventArgs, NavigationFailedEventArgs, Application, App, Launcher, MainWindow, Navigation, RootFrame (+8 more)

### Community 7 - "NuGet Test Dependencies"
Cohesion: 0.08
Nodes (28): net10.0-android, net10.0-desktop, Avalonia, Avalonia.Fonts.Inter, Avalonia.Themes.Fluent, AvaloniaUI.DiagnosticsSupport, coverlet.collector, FluentAssertions (+20 more)

### Community 8 - "Agent Command Catalog"
Cohesion: 0.08
Nodes (30): code-reviewer agent, college-admission-assistant agent, csharp-code-reviewer agent, documentation-writer agent, gradle-expert agent, test-writer agent, build command (Gradle), clean command (Gradle) (+22 more)

### Community 9 - "Courses Page UI"
Cohesion: 0.10
Nodes (22): Courses, ItemClickEventArgs, CautionAnim, FyButton, HeaderAnim, Page, PopupOverlay, PopupSubtitle (+14 more)

### Community 10 - "CRM Vision Spec"
Cohesion: 0.07
Nodes (27): 0. Core architectural decision (binding), 10.1 Multi-step application form (applicant-facing), 10.2 Save as draft, 10.3 Smart validation (inline, not on Submit), 10. Application management (heart of the product), 11. Applications list + filtering + search, 12. Enquiry / Lead management (pre-applicant CRM), 13. Documents + verification workflow (+19 more)

### Community 11 - "Course Catalog Model"
Cohesion: 0.11
Nodes (18): Course, HasFy, HasSy, HasTy, CourseCatalog, All, IReadOnlyList, Course (+10 more)

### Community 12 - "Avalonia Shell Wiring"
Cohesion: 0.17
Nodes (6): AvaloniaPropertyChangedEventArgs, CrmShellView, Vm, RoutedEventArgs, UserControl, VisualTreeAttachmentEventArgs

### Community 13 - "Main Menu Navigation"
Cohesion: 0.17
Nodes (9): Page, ExitOverlay, Page, MainMenuPage, RoutedEventArgs, Border, Page, SplashPage (+1 more)

### Community 14 - "Pipeline Actions State"
Cohesion: 0.24
Nodes (6): Applicant, DocsLabel, DocsPending, FeeLabel, StageName, RelayCommand

### Community 15 - "Backend Architecture Patterns"
Cohesion: 0.17
Nodes (13): ApplicationController REST Endpoints, Standardized Error Response Format, OpenAPI Configuration with SpringDoc, Cross-Platform SQLite Abstraction, Flyway Configuration, Versioned SQL Migration Scripts, Gradle Kotlin DSL Build Configuration, Gradle Version Catalog (+5 more)

### Community 16 - "Design Reference Mapping"
Cohesion: 0.20
Nodes (11): Avalonia CRM Vision Spec, Status Pill Language, LLM Chat UI Recreation Guide, Reference to College Mapping, Reference Design Tokens, Reference UI Analysis File, Inter Font Choice over SF Pro, UiPrompt Design System Spec File (+3 more)

### Community 17 - "BCA BSc Syllabus CBCS"
Cohesion: 0.29
Nodes (11): BCA Program CBCS (SRTMU), BCA First Year Syllabus CBCS (Sem I-II, SRTMU), BCA Second Year Syllabus CBCS (Sem III-IV, SRTMU), BCA Third Year Syllabus CBCS (Sem V-VI, SRTMU), BSc Computer Science Program CBCS (SRTMU), BSc Computer Science First Year Syllabus CBCS (Sem I-II), BSc Computer Science Second Year Syllabus CBCS (Sem III-IV), BSc Computer Science Third Year Syllabus CBCS (Sem V-VI) (+3 more)

### Community 18 - "Android Asset Resources"
Cohesion: 0.20
Nodes (10): Android Resources, Android R Class, Asset Scale Variants, Examples, Here is a cheat sheet, Shared Assets, Table of scales, Android-Only Assets (+2 more)

### Community 19 - "Avalonia App Bootstrap"
Cohesion: 0.22
Nodes (5): Application, CollegeAdmission.Views, App, MainWindow, Window

### Community 20 - "Avalonia Android Bootstrap"
Cohesion: 0.20
Nodes (7): AvaloniaAndroidApplication, AvaloniaMainActivity, CollegeAdmission.Android, Application, App, AppBuilder, MainActivity

### Community 21 - "Avalonia Shell Services"
Cohesion: 0.20
Nodes (8): Control, Func, ILauncher, AppShell, Crm, ShellView, AvaloniaLauncherService, Task

### Community 22 - "Graphify Pipeline Docs"
Cohesion: 0.22
Nodes (9): URL Ingest and Watch Mode, Semantic Extraction Specification, GitHub Clone and Cross-Repo Merge, Commit Hook and Claude Integration, BFS DFS Traversal Query, Whisper Video Audio Transcription, Incremental Update and Cluster Only, Graphify Knowledge Graph Pipeline (+1 more)

### Community 23 - "Android Java Tests"
Cohesion: 0.33
Nodes (5): androidx.test.ext.junit.runners.AndroidJUnit4, org.junit.runner.RunWith, org.junit.Test, ExampleInstrumentedTest, ExampleUnitTest

### Community 24 - "Uno Android Entry"
Cohesion: 0.22
Nodes (6): ApplicationActivity, CollegeAdmission.Droid, NativeApplication, Application, MainActivity, Bundle

### Community 25 - "Android App Structure"
Cohesion: 0.28
Nodes (9): Android Studio Project Structure, App Source Classes, Syllabus PDF Assets, Course Catalog UI, Course Detail Popup B.Sc. Software Engineering, Exit Confirmation Dialog, M.Sc. Software Engineering Detail, Registration Form UI (+1 more)

### Community 26 - "Avalonia Desktop Entry"
Cohesion: 0.29
Nodes (5): App, AppBuilder, CollegeAdmission.Desktop, Program, STAThread

### Community 27 - "Uno Codebehind Files"
Cohesion: 0.32
Nodes (3): CollegeAdmission.Models, CollegeAdmission.ViewModels, CollegeAdmission

### Community 28 - "Uno Test Suite"
Cohesion: 0.25
Nodes (4): CollegeAdmission.Tests, SetUp, UnitTest1, Test

### Community 29 - "Launcher Navigation Services"
Cohesion: 0.29
Nodes (4): CollegeAdmission.Services, ILauncherService, UnoLauncherService, Task

### Community 30 - "Reference Screenshots UI"
Cohesion: 0.25
Nodes (8): Today dashboard - KPI cards, Van 1/2 job timeline with Done/In progress status, route map, left nav Today/Calendar/Jobs/Customers/Quotes/Invoices/Settings, Job detail drawer - Bishopston Nursery J-1241, Booked-In progress-Done-Paid stepper, Record payment/Edit actions, address/crew/contact/work/INV-2234/activity, Edit job modal - customer/property, date/start/duration, Van 1/2 selector, crew Marcus/Priya/Jake, work item Office carpet qty/unit, crew vs office notes, total £477.40, New customer modal - Residential/Commercial type, first/last name, mobile/alternative phone, email, lead source Google, tags regular/pets/landlord/VIP/key holder/referrer, property address, Jobs list Open filter - 107 jobs table WHEN/CUSTOMER/ADDRESS/VAN-CREW/STATUS/TOTAL grouped by date, Booked badges, van color dots, search and All vans filter, Jobs list All filter - 350 jobs same table layout, sidebar nav highlight, New job CTA, shared card/table design language with Open view, Settings screen - Business details, Vans Van1 WX68KLP Van2 BD21TRV Active, Cleaners Marcus/Priya/Jake, Invoicing payment terms and footer, Save changes, Jobs list address focus - same 107-job Open table, cursor on address column, demonstrates customer-address-crew-status pattern for scheduling

### Community 31 - "Admission Stage Enum"
Cohesion: 0.25
Nodes (8): CrmStage, Applied, Enquiry, Enrolled, FeePaid, Merit, Offered, Verified

### Community 32 - "CRM Models Seed"
Cohesion: 0.33
Nodes (5): CourseFill, Pct, PctLabel, CrmAction, CrmSeed

### Community 33 - "MVVM ViewModel Base"
Cohesion: 0.33
Nodes (5): ObservableObject, ViewModelBase, CoursesViewModel, Courses, IReadOnlyList

### Community 34 - "BBA Syllabus SRTMU"
Cohesion: 0.60
Nodes (5): BBA Program (SRTMU), BBA First Year Syllabus (Sem I-II, SRTMU), Swami Ramanand Teerth Marathwada University Nanded (SRTMU), BBA Second Year Syllabus (Sem III-IV, SRTMU), BBA Third Year Syllabus (Sem V-VI, SRTMU)

### Community 35 - "App Illustrations Set"
Cohesion: 0.40
Nodes (5): Onboarding illustration - flat person in green pointing at document with placeholder lines, empty-state or guide art, Certificate art - white diploma with teal ribbon header and red/blue seal on yellow circle, completion/achievement illustration, Course card art - white card with gray lines and gold medal red ribbon on green, course listing badge, Legacy college logo - Royal Education Society Latur circular seal ESTD 1982 with globe computer DNA trees, old branding, Classroom art - flat orange desk with bench and dark legs, furniture/room illustration

### Community 36 - "Syllabus Launcher Command"
Cohesion: 0.40
Nodes (3): Task, RelayCommand, Task

### Community 37 - "Uno Avalonia Brand Assets"
Cohesion: 0.50
Nodes (4): Avalonia app icon, Shared illustration set mirroring Android drawables, Splash screen, Uno app icon

### Community 38 - "BSc Biotech Syllabus"
Cohesion: 0.83
Nodes (4): BSc Biotechnology Program CBCS (SRTMU), BSc Biotechnology First Year Syllabus CBCS (Sem I-II), BSc Biotechnology Second Year Syllabus CBCS (Sem III-IV), BSc Biotechnology Third Year Syllabus CBCS (Sem V-VI)

### Community 39 - "Gradle Wrapper Script"
Cohesion: 0.83
Nodes (3): gradlew script, die(), warn()

### Community 42 - "Launcher Icon Set"
Cohesion: 0.67
Nodes (3): launcher icon set, foreground layer, round variant

## Ambiguous Edges - Review These
- `Certificate art - white diploma with teal ribbon header and red/blue seal on yellow circle, completion/achievement illustration` → `Legacy college logo - Royal Education Society Latur circular seal ESTD 1982 with globe computer DNA trees, old branding`  [AMBIGUOUS]
  src/AndroidJava/app/src/main/res/drawable/old_logo.png · relation: conceptually_related_to

## Knowledge Gaps
- **292 isolated node(s):** `Maintenance`, `DocsLabel`, `DocsPending`, `FeeLabel`, `StageName` (+287 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 365 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **27 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **What is the exact relationship between `Certificate art - white diploma with teal ribbon header and red/blue seal on yellow circle, completion/achievement illustration` and `Legacy college logo - Royal Education Society Latur circular seal ESTD 1982 with globe computer DNA trees, old branding`?**
  _Edge tagged AMBIGUOUS (relation: conceptually_related_to) - confidence is low._
- **Why does `CrmViewModel` connect `CRM Pipeline ViewModel` to `CRM Models Seed`, `MVVM ViewModel Base`, `Course Catalog Model`, `Avalonia Shell Wiring`, `Pipeline Actions State`, `Syllabus Command Overload`, `Avalonia Shell Services`, `Launcher Navigation Services`?**
  _High betweenness centrality (0.079) - this node is a cross-community bridge._
- **Why does `RegistrationPage` connect `Android Registration Fields` to `Uno Registration Form`, `Uno Codebehind Files`, `Main Menu Navigation`?**
  _High betweenness centrality (0.047) - this node is a cross-community bridge._
- **Why does `CoursesPage` connect `Courses Page UI` to `Course Catalog Model`, `MVVM ViewModel Base`, `Uno Codebehind Files`, `Main Menu Navigation`?**
  _High betweenness centrality (0.045) - this node is a cross-community bridge._
- **What connects `Maintenance`, `DocsLabel`, `DocsPending` to the rest of the system?**
  _292 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Android Java Imports` be split into smaller, more focused modules?**
  _Cohesion score 0.0815686274509804 - nodes in this community are weakly interconnected._
- **Should `Uno Registration Form` be split into smaller, more focused modules?**
  _Cohesion score 0.06341463414634146 - nodes in this community are weakly interconnected._