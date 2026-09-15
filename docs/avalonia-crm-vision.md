# Avalonia CRM Vision — College Admission Management System (Full Feature Spec)

**Status:** Vision approved + CRM shell built and verified on-device (see §6 as-built note, 2026-09-15) + Full admissions-CRM feature spec merged (2026-09-16) + Greenfield rebuild landed: classic views deleted, CrmShellView is the app root, ReferenceUi tokens live, launched on macOS (2026-09-16)
**Date:** 2026-09-14 (updated 2026-09-16)
**Scope:** `src/AvaloniaUi/` greenfield rebuild. Legacy Java (`src/AndroidJava/`) stays archived as behavior reference. Uno port (`src/PlatformUno/`) untouched.
**Audience:** Backend-strong developer (Java 2017-2020 native Android → ASP.NET Core WebAPI/MVC since mid-2020, plus Angular/React/TS/Python/shell/gRPC/WebSocket/REST/GraphQL/Azure/AWS/SQL/NoSQL/Docker/Linux + AI agents). Self-declared gap: UI craft — MVVM structure, look-and-feel, UX/DX, palettes/branding, components, responsiveness, animation/physics/realism, XML/AXAML/XAML.
**UI source of truth:** `docs/ReferenceUi/UiPrompt.md` + `docs/ReferenceUi/Reference-UI-Analysis.md` (8 screenshots). This doc defines WHAT to build; those docs define HOW it must look. No new palette, radius, shadow, or component style invented here.

This doc is the UI + feature contract. No new screens until this is agreed.

---

## 0. Core architectural decision (binding)

> **Do not design separate desktop and mobile products. Design one responsive information model and component system that changes layout based on screen size.**

- Desktop (macOS / Windows / Linux, landscape): 3-column admission workspace, tables, master-detail.
- Mobile (iOS / Android, portrait): same information as stacked cards + drill-down screens.
- One domain model, one API, one design language. Only presentation changes.

Target breakpoints (see §6 for as-built 900dp threshold):

```text
≥1280   full 3-pane (nav + content + rail)
768–1279 rail becomes sheet / stacked
<768    single-pane + drill-down (drawer/bottom-nav per §6 as-built)
```

Dashboard card reflow: desktop 5 across → tablet 3+2 → mobile 2+2+1. Never shrink cards until text is unreadable.

---

## 1. Where we are (honest baseline)

Old app (6-7 years old, built for HR showcase + GitHub Java proof):

- 4 views only: `SplashView` → `MainMenuView` (2 cards: Registration / Walkthrough) → `CoursesView` (2-col `UniformGrid`, 12 hard-coded `Course` cards, 170×190 fixed) → `RegistrationView` (19-field centered stacked form) + modal popup overlays.
- Current styling: `Background="White"`, hard-coded `#6200EE / #03DAC5 / #FF9800 / #F44336`, `#E4E4E4` borders, `CornerRadius="4"`, centered 12-14px text, image-per-card (`certificate_flat.jpg`, `table_chair.jpg`), back.png button, Lottie headers (`get_in_touch.json` 245px, `tutorials_online.json` 240px, `caution_anim.json` on missing FY).
- Data: `CollegeAdmission.Core/Models/Course.cs` — 12-item static `CourseCatalog.All` (`Id/CardTitle/CardSubtitle/PopupSubtitle + Fy/Sy/Ty urls`). Uno side already has 21 NEP courses. Online PDFs → external browser. Registration saves flat JSON locally. No tests on Avalonia side.
- What worked: small, shippable, smooth on-device (Avalonia 12 Debug beat Uno Release+AOT in 2026-09-14 device test).

What is missing for a "proper CRM": density, hierarchy, navigation, states, search/filter, master-detail, pipeline, persistence, roles.

**Decision: greenfield, not refactor.** Keep `CourseCatalog` URLs + registration field list as data reference only. New solution owns its design system from day one. Reuse: .NET 10, Avalonia 12.1.2, `CommunityToolkit.Mvvm 8.4.2`, `Avalonia.Labs.Lottie 12.0.2`, Inter font, DI/service split (`Core` models/viewmodels/services + head views/navigation/launcher) proven in current layered build.

---

## 2. UI skills loaded (via `ui-skills` MCP `https://www.ui-skills.com/mcp`)

Fetched live before writing this doc (registry = 303 skills). Applied subset below; full catalog searchable via `ui-skills list`.

| # | Skill (`pathSlug`) | Role in this doc |
|---|---|---|
| 1 | `dammyjay93/interface-design` | **Primary.** Dashboards/admin/SaaS craft: intent-first, domain→signature, hierarchy, layering, token architecture, polish+motion essentials. Sections 3-6 follow its workflow. |
| 2 | `nextlevelbuilder/ui-ux-pro-max` | Palettes (192), font pairings (74), 119 UX rules, Avalonia/Uno stack guidance. Used for density dial + checklist. |
| 3 | `anthropics/frontend-design` | Anti-template rule: one memorable element, rest quiet. Used for signature + restraint. |
| 4 | `ibelick/create-design-md` | Shape of future `DESIGN.md` (tokens in YAML frontmatter, prose = intent only). Do not create it until new tokens exist. |
| 5 | `jakubkrehel/better-colors` | Ramp-not-colors, semantic tokens, hold hue, measure rendered pair. Section 5. |
| 6 | `jakubkrehel/better-typography` | Scale + weight + color hierarchy, 16px body floor, tabular-nums, balance/pretty wrap. Section 5. |
| 7 | `pbakaus/shape` | Discovery → brief → confirm-then-stop. Section 3 is the brief. |
| 8 | `pbakaus/layout` | Reading order, grouping by proximity, rhythm, density, adaptation. Section 6. |
| 9 | `pbakaus/typeset`, `pbakaus/colorize`, `pbakaus/distill`, `pbakaus/harden`, `pbakaus/impeccable`, `pbakaus/polish` | Applied during build passes (type system, strategic color, simplify, empty/error/edge states, flagship craft, launch pass). |
| 10 | `wshobson/interaction-design`, `emilkowalski/animate`, `raphaelsalaja/12-principles-of-animation` | Motion budget: <300ms, transform+opacity only, press `scale(0.97)`, reduced-motion. Section 5. |
| 11 | `vercel-labs/web-design-guidelines`, `wshobson/wcag-audit-patterns` | A11y floor: 4.5:1 text, 44×44 hits, keyboard, focus visible. Section 7. |

> For every future UI diff, state: `Intent / Hierarchy / Palette / Depth / Surfaces / Typography / Spacing` + why (per `interface-design`). If you can't say why, you're defaulting — stop.

---

## 3. Design brief (per `pbakaus/shape`)

**Job + audience:** A college admin clerk / counsellor at COCSIT Latur, mid-admission-season, 50+ enquiries/day, on Windows desktop (primary) + Android tablet (counselling desk). 5 min before: phone enquiry. 5 min after: merit-list check or fee follow-up. Needs: *who is stuck where, what needs my action now.*

**Outcome + proof:** Move an applicant one stage forward in <30s. Proof: pipeline count drops in current stage, appears in next; no re-typing (draft autosave, doc checklist carries over).

**Domain (product world, not features):** admission office desk · merit lists pinned on board · file folders with photos · fee receipts · syllabus booklets · counselling queue · stamp/signature · notice board.

**Color world:** governed by `docs/ReferenceUi/` (warm neutral surfaces, scarce status color). Do not invent a competing indigo/cream/maroon/brass world.

**Signature (only-this-product element):** **Pipeline-as-merit-board** — admission stages rendered as a physical notice-board with pin-strip cards (applicant photo, merit %, doc ticks, fee chip). Not a generic SaaS kanban: cards carry FY/SY/TY syllabus status + doc icons. If product name is removed, the board + syllabus ticks still say "college admissions". Rendered strictly in ReferenceUi tokens (see §5).

**Rejecting defaults:** (1) identical rounded cards everywhere → board-lane + dense table + detail rail, each with own density; (2) centered 12px italic hints → left-aligned 13/14px labels, tabular numbers, one focal number per view; (3) 5 accent colors competing → ReferenceUi scarcity rule: neutral warm-white dominates, green = primary CTA only, red/amber/blue = status only.

**Direction:** ReferenceUi visual identity (soft neutral surfaces + subtle borders + restrained shadows + 8px spacing + system typography + clean cards + excellent hierarchy). Quiet surfaces, loud only on action + status.

---

## 4. Information architecture — module structure

Full module tree (this replaces the earlier 10-item IA — all items below are in scope across MVP phases, §21):

```text
College Admission Management
│
├── Dashboard
│
├── Applications
│   ├── All Applications
│   ├── New
│   ├── Incomplete
│   ├── Submitted
│   ├── Under Review
│   ├── Approved
│   ├── Rejected
│   └── Waitlisted
│
├── Enquiries / Leads
│
├── Applicants / Students
│
├── Admission Process
│
├── Documents
│
├── Entrance / Merit
│
├── Counselling
│
├── Fees & Payments
│
├── Communication
│
├── Reports & Analytics
│
├── Academic Programs
│
├── Users & Roles
│
├── Notifications
│
└── Settings
```

Main navigation (product-level grouping):

**Dashboard · Enquiries · Applications · Applicants · Documents · Counselling · Merit & Seats · Payments · Reports** with **Programs, Users, Workflow and Settings** under Management.

AppShell mapping (existing shell contract preserved):

```
AppShell (left nav 264–310px + top bar + content + right detail rail 360px, collapsible)
├── 01 Dashboard (focal: "action queue" — what needs me today)
├── 02 Pipeline / Admissions (focal: stage board; default view, not dashboard)
├── 03 Applications (= Leads/Enquiries table, dense DataGrid)
├── 04 Students (enrolled master-detail)
├── 05 Courses (21 NEP unified catalog: UG/CS/BCA/SE/SD/DS/AIML/NT/IT/CM/BVoc/BT/BBA + PG; FY/SY/TY syllabus links, seats, fees, eligibility)
├── 06 Documents (syllabus library + applicant doc checklist + in-app PDF viewer)
├── 07 Fees & Receipts (due list, collect, receipt)
├── 08 Communicate (SMS/WhatsApp/email templates + follow-up queue)
├── 09 Reports (intake vs enrolled, course fill %, fee collection, source-wise)
└── 10 Settings (themes, users/roles, academic year, backup)
```

**Flows (each <5 clicks, each state designed: loading/empty/error/success):**

1. `Enquiry → Application → Doc verification → Merit → Offer → Fee → Enrolled` (happy path; drag card or keyboard `→` advances).
2. `Application: incomplete docs → reminder → resubmit → verify` (checklist persists per applicant).
3. `Offer → fee pending → overdue → receipt` (fee chip flips green, receipt prints).
4. `Course browse → syllabus FY/SY/TY → apply pre-filled` (replaces old popup: detail rail, not modal).
5. `Search anything (Ctrl+K): applicant / course / receipt` → jump.
6. `New academic year rollover: duplicate course seats, archive enrolled`.

Density rule (`ui-ux-pro-max` dial `density=8`): dashboard/board = dense (8-32px scale, 12-16px card padding); forms = standard (16px); marketing-like headers = banned.

---

## 5. Design system — ReferenceUi is binding

All tokens, components, and interaction patterns come from `docs/ReferenceUi/UiPrompt.md` and `docs/ReferenceUi/Reference-UI-Analysis.md`. This section is an index + mapping, not a competing spec. On any conflict, ReferenceUi wins.

**Palette (verbatim from Reference-UI-Analysis §2.1):**

```
App bg:       #F7F6F3
Sidebar:      #F5F4F1
Surface:      #FFFFFF
Surface 2:    #FBFBFA
Text:         #171717 / #666666 / #8A8A8A
Border:       #DDDDD8 / rgba(0,0,0,0.08)
Divider:      #E7E7E3
Green (CTA):  #147A5A, light #E7F5ED
Danger:       #A94444, light #FBECEC
Blue:         #3D73D9, light #EDF3FF
Orange:       #D97A18, light #FFF3E4
```

Color scarcity rule: only green CTA, red overdue, blue/orange identity dots. Everything else warm gray + black text. Dark mode only if explicitly requested elsewhere — ReferenceUi default is light macOS-native.

**Typography:** macOS-native stack (`-apple-system, BlinkMacSystemFont, "SF Pro Display", "SF Pro Text", system-ui, sans-serif`; open-licensed bundle = Inter per Reference-UI-Analysis §2.6). Page title 24–28/600, section 15–18/600, body 14/400–500, secondary 13, metadata 11–12, KPI 24–30/600–700. Tabular numbers for money. No bold-everything.

**Spacing:** 8px grid (`4/8/12/16/20/24/32/40/48`). Sidebar pad 16–20, page pad 28–32, card pad 18–22, input h 40–44, button h 38–42, table row h 60–76.

**Borders / radius / shadow:** 1px `rgba(0,0,0,0.08)`; radius ladder window 18–22 → modal 16–18 → card 12–14 → input/button 8–10 → pill 999 → avatar 50%; card shadow `0 1px 2px rgba(0,0,0,0.03)`, modal `0 20px 60px rgba(0,0,0,0.12)`, drawer `-12px 0 40px rgba(0,0,0,0.08)`. No gradients, glassmorphism, neon, heavy shadows.

**Icons:** single thin outline set (Lucide-style), 16–20px, quiet gray.

**Motion:** 150–220ms ease-out for overlays (drawer fade + 8px slide, modal scale .98→1); button press `scale(.97)` 100–160ms; transform+opacity only; reduced-motion → opacity only. Lottie only for first-run/empty states (keep existing 3 JSONs).

**Components (names from UiPrompt.md — reuse, do not rename):** `AppShell, Sidebar, SidebarNavItem, PageHeader, SummaryCard, VanSection→CounsellorSection, JobCard/JobRow→ApplicationRow, StatusPill, Avatar, SegmentedControl, SearchField, FilterSelect, DataTable, TableGroupHeader, JobDetailDrawer→ApplicationDrawer, StatusStepper, Modal, FormField, ServiceLineEditor→DocumentChecklistEditor, Tag, TextArea, RouteMapCard→FunnelCard, Button, IconButton` + central `DesignTokens`.

**Reference → College mapping (from Reference-UI-Analysis §6, binding):** Today→Dashboard, KPI cards→collections/admissions/awaiting/overdue/enquiries, Van blue/orange→counsellor teams/departments (dot/icon only), job rows→application rows, route map→funnel/counselling schedule, job drawer+stepper→application drawer+stepper (`Enquiry → Counselling → Admitted → Fees Paid`), Edit Job→Edit Application, New Customer+tags→New Student+tags (`[hostel][bus][scholarship][VIP][referrer][sports]`, `[UG][PG]` segmented), Jobs table→Applications table, Settings Vans/Cleaners→Departments/Counsellors/Rooms/Fee heads.

---

## 6. Screen contracts (compact, info-dense — what "more on screen" means)

**AppShell:** left nav (icon + label, section groups: Work / Manage / System; badge counts on Pipeline/Fees), top bar (global search `Ctrl+K`/`⌘K`, academic-year switcher, theme toggle, user), content, right rail (context detail of selected row/card — replaces ALL old modal popups). Keyboard: `↑↓` move, `→` advance stage, `E` edit, `/` search. Sidebar 280–310px per ReferenceUi; active nav = soft neutral fill + 8px radius; search has ⌘K hint; footer = operational meta (e.g. programs + counsellors + date).

**02 Pipeline (hero, default landing):** lanes = Enquiry / Applied / Verified / Merit / Offered / Fee-paid / Enrolled. Card = photo initials, name, course tag, merit %, doc ticks (3 dots), fee chip, days-in-stage. Lane header = count + WIP warning (amber if >X days). Interactions: drag + button advance, filter by course/counsellor, sort by merit/wait. Empty lane = illustration + "Add enquiry" CTA. This is the signature — spend craft here.

**01 Dashboard:** NOT a stat-tile parking lot. One focal: `Needs action today (n)` list (fee overdue, docs missing, merit pending) → each row jumps to Pipeline/Fees. Secondary: intake funnel bar, course fill % (progress, not numbers alone), fee collected vs due sparkline, counsellor queue. Tertiary: recent activity feed. KPI row follows ReferenceUi Today pattern: header `Dashboard + cycle + date meta` left, `< Today > [Week] [+ New]` right; 5 `SummaryCard`s (Enquiries / Applied / Review / Approved / Fees Collected) white 12–14px radius, big value + small sub-line, only overdue in red.

Admission funnel (dashboard widget, same data as pipeline):

```text
Enquiry → Application Started → Application Submitted → Documents Verified
→ Eligible → Merit / Entrance → Selected → Fee Paid → Admission Confirmed
```

Dashboard widget checklist: total enquiries · applications received · incomplete applications · awaiting document verification · awaiting review · entrance-test applicants · shortlisted · confirmed admissions · fee pending · today's counselling appointments · admission deadlines · recent activity. Mobile: widgets become stacked cards.

**03 Applications (DataGrid):** 12+ columns, virtualized, sticky header, inline status pill edit, bulk select → bulk message/offer. Saved views (e.g. "BCA merit >75% docs pending"). Tabs switch dataset, never layout (per ReferenceUi Jobs proof): `All / New / Incomplete / Submitted / Under Review / Approved / Rejected / Waitlisted`. Filter + search + date-grouped rows per §11.

**05 Courses:** unified 21-course `Course` record (`Id/Name/Level/Dept/Eligibility/Duration/Seats/FeePerYear/Fy/Sy/Ty urls`) — resolves master-plan parity gap. Card → rail shows eligibility/fees/seats + FY/SY/TY buttons (collapse when missing + `caution_anim` only then, same trigger as legacy `Courses.java:193`) + `Apply` pre-fills registration.

**Registration (was `RegistrationView`):** wizard in rail/page, not one long centered stack: 1 Personal → 2 Academic (marks/year per level) → 3 Contact+Address → 4 Course+Docs → 5 Review+Pay. Left step list, auto-save draft to SQLite on every field (debounced), validation inline under field (not top), `REGISTER` single primary per step. Confirm dialog = WinUI/Avalonia `Dialog`, no custom overlay divs. See §10 for full 10-step applicant-facing form.

**Fees / Docs / Reports / Settings:** standard dense tables + detail rail; receipts printable; doc viewer = embedded PDF (WebView2/PDF.js spike per master plan Phase 3 — do not open external browser in new app).

Responsive (`pbakaus/layout` adaptation): ≥1280 full 3-pane; 768-1279 rail becomes sheet; <768 single-pane + bottom nav (5 max: Pipeline, Apps, Fees, Courses, More), inputs 16px, board → horizontal lane pager.

**As-built desktop rebuild (2026-09-16, `CrmShellView` as root, macOS 1440×900, build 0 warn/0 err, launched):** classic `Splash/MainMenu/Courses/Registration/MainView` views, `MainViewModel`, `RegistrationViewModel`, `INavigationService`, 9:16 portrait ratio lock, 6 classic images, 3 Lottie JSONs + package dep all deleted. `AppShell` exposes `CrmViewModel` directly; `MainWindow`/Android single-view host `CrmShellView`. Tokens are ReferenceUi verbatim (warm-white `#F7F6F3`, sidebar white, green CTA `#147A5A`, neutral active-nav fill). Sidebar 300px with brand block + ⌘K search + MANAGEMENT section; Dashboard has 5-card KPI row; `Apply →` / `+ New application` / rail button create real Enquiry applicants (`AddApplicationCommand`). Still open: status pills, 10-step wizard (quick-add covers it), ⌘K palette, Core tests.

**As-built responsive (2026-09-15, `CrmShellView`, verified on Galaxy M51 1080×2400):** single width threshold 900dp (`ApplyLayout()` in code-behind, no VM changes). Wide (desktop, `MinWidth 1024`): unchanged 3-pane (300 nav + content + 340 rail). Narrow (portrait 19.5:9/20:9 phones, landscape phones): nav → `SplitView` overlay drawer + hamburger (drawn bars — `☰` font glyph renders blank on-device), top bar → search/actions row + scrollable stats strip (fixed 116dp height), rail → stacked below content (capped 360dp), funnel → `WrapPanel`, Applications 6-col table → stacked cards, pipeline lanes already horizontally scrollable. Deviations from plan: drawer instead of bottom nav, stacked rail instead of sheet — revisit if counsellor testing prefers sheet.

---

## 7. Non-functional floors (build these in, not later)

- A11y: contrast measured on rendered pair, focus visible, full keyboard map, screen-reader names on all icon buttons, no emoji-as-icon in UI chrome (current `👦🩸📞` placeholders → real labels + helper text).
- Perf: virtualized lists, lazy Lottie/PDF, cold start <500ms desktop (AOT later), no layout thrash (no width/height animation).
- Data: SQLite (`Microsoft.Data.Sqlite` desktop/Android; `sqlite-net` if iOS head lands) — tables `Applicants/Stages/Courses/Docs/Fees/Comms`; drafts autosave; TLS 1.3 + no cleartext when backend (Phase 5 REST, OpenAPI → Kiota/NSwag, `System.Text.Json` source-gen for AOT) lands.
- Tests: Core unit (catalog, stage machine, fee math) + headless UI smoke per screen contract. Avalonia currently 0 tests — new app starts with them.
- Security baseline (see §19): auth, RBAC, TLS, encryption at rest, secure doc storage (no permanent local-fs copies without an offline-storage design), sessions, audit log, password policy, optional MFA, device/session management, backups, retention.

---

## 8. Greenfield build plan (Avalonia, from scratch)

1. **Scaffold:** new `src/AvaloniaCrm/` (slnx + `Crm.Core` net10.0 + `Crm` head + `Crm.Tests`). Reference `Avalonia 12.1.2 (Fluent), CommunityToolkit.Mvvm, Avalonia.Labs.Lottie`. Copy zero AXAML — only `CourseCatalog` URLs + field names as seed JSON (`Assets/Data/courses.seed.json` → SQLite).
2. **Tokens first:** `Themes/Tokens.axaml` (§5 + ReferenceUi tokens) + light/dark + high-contrast; `DESIGN.md` after (per `create-design-md`).
3. **Shell + nav + empty states** → Pipeline board with mock data → Dashboard queue → Applications grid → Courses+rail → Registration wizard → Fees/Docs/Comms/Reports/Settings → search/launchers → persistence → PDF spike → backend seam.
4. **Per-screen gate:** states (loading/empty/error) + keyboard + measure (contrast/hit/size) + device screenshot (desktop + Android) before next screen.
5. Old `src/AvaloniaUi/` classic views archived 2026-09-16 (parity reached: Pipeline + Courses + Fees + quick-add in shell; full wizard still open, see §6).

Open decisions (need owner call): 21-course fee/seat source of truth; backend (REST/Firebase/Supabase); PDF engine; roles; Marathi/Hindi scope.

---

## 9. What to ask before coding

1. Default landing: Pipeline board (recommended) vs Dashboard?
2. Which 3 lanes cause the most daily pain?
3. Fee/Seat numbers per course — real data source?
4. Roles: single clerk vs counsellor/office/admin?

Reply with these 4 answers (or "proceed with recommendations") and build starts at §8.1.

---

## 10. Application management (heart of the product)

Every application gets a unique ID, e.g. `ADM-2026-004872` (prefix + cycle year + zero-padded sequence; searchable globally, see §15).

Application profile (single Applicant Workspace — the central object of the system):

```text
Applicant
├── Personal Information
├── Contact Information
├── Parent / Guardian
├── Address
├── Academic History
├── Program Preference
├── Category / Reservation
├── Entrance Examination
├── Documents
├── Merit Information
├── Counselling
├── Fees
├── Communication
└── Activity Timeline
```

**Applicant Workspace rule:** Personal + Academic + Documents + Eligibility + Merit + Counselling + Fees + Communication + Timeline are all tied to one applicant/application ID. Desktop renders it as tabs in the detail rail / 3-pane (§22); mobile renders the same sections as stacked cards / drill-down.

### 10.1 Multi-step application form (applicant-facing)

Never one 100-field page. Fixed step order:

```text
Personal → Family → Address → Education → Program → Entrance → Documents → Declaration → Review → Submit
```

Progress indicator on every step: bar (e.g. `████████████████░░░ 82%`) + `7 of 9 sections completed`. Critical on mobile.

### 10.2 Save as draft

Students can start → close → return → continue. Draft stores: current step, completed sections, uploaded documents, validation errors, timestamp. Autosave debounced to SQLite on every field (see §7).

### 10.3 Smart validation (inline, not on Submit)

Validate per field with instant feedback, e.g. DOB → computed age `18 ✅`; marks → auto total/percentage (`Physics 78 + Maths 82 = 160, 80%`). Errors render under the field in ReferenceUi form language (red 12px text + 1px `#A94444` border per Reference-UI-Analysis §13).

---

## 11. Applications list + filtering + search

Desktop list (table, ReferenceUi Jobs-table language):

```text
┌───────┬──────────────┬──────────┬─────────┬────────┐
│ ID    │ Applicant    │ Program  │ Status  │ Fee    │
├───────┼──────────────┼──────────┼─────────┼────────┤
│ 00231 │ Rahul Sharma │ B.Tech   │ Review  │ ₹20K   │
│ 00232 │ Priya Shah   │ BCA      │ Paid    │ ₹30K   │
└───────┴──────────────┴──────────┴─────────┴────────┘
```

Same data on mobile as cards:

```text
Rahul Sharma / ADM-00231 / B.Tech Computer Science / 🟡 Under Review / ₹20,000 / [View]
```

Applicant profile desktop vs mobile (same sections, different presentation):

```text
Desktop: header (name + ID + status pill) + tabs [Personal|Academic|Documents|Fees|Timeline] + 2-col details
Mobile:  header + status + horizontal scroll tabs / segmented nav + stacked sections
```

Advanced filters (desktop filter rail; mobile = Filter button → bottom sheet): Status · Program · Campus · Admission cycle · Category · Application date · Document status · Eligibility · Payment status · Counsellor · Entrance score.

Global search (one box, `⌘K/Ctrl+K`): applicants, application ID (`ADM-2026-004872`), phone (`+91 98XXXXXXXX`), email, course, city — direct open. Command palette entries: Go to Applications, Search applicant, Create application, Create enquiry, Schedule counselling, Open reports.

---

## 12. Enquiry / Lead management (pre-applicant CRM)

Store per enquiry: name, phone, email, city, course interested in, campus, academic background, enquiry source, counsellor assigned, enquiry date, follow-up date, status.

Sources: Website · Walk-in · Phone · WhatsApp · Education fair · Referral · Advertisement · Social media.

Lead stages: `New → Contacted → Interested → Application Started → Application Submitted → Converted / Lost`.

---

## 13. Documents + verification workflow

Document types: Aadhaar/identity, passport, 10th marksheet, 12th marksheet, transfer certificate, migration certificate, caste certificate, income certificate, domicile, entrance score card, photograph, signature.

Per document: Document · Status · Uploaded date · Verified by · Verified date · Remarks. Statuses: `Not uploaded / Uploaded / Under verification / Verified / Rejected / Expired`.

Capture: mobile = camera → crop → upload → verify (plus biometric/device auth where available); desktop = drag & drop.

Verifier view:

```text
✓ Photo / ✓ 10th Marksheet / ✓ 12th Marksheet / ⚠ Income Certificate / ✕ Domicile
```

Click → preview (embedded viewer, not external browser). Actions: Approve · Reject · Request re-upload · Add remark.

---

## 14. Eligibility engine + Programs + Cycles + Merit + Seats

**Eligibility:** admin configures per-program rules (e.g. B.Tech CS: 12th ≥ 60%, Physics required, Mathematics required, Entrance required). System auto-evaluates `✅ Eligible / ⚠ Conditionally eligible / ❌ Not eligible` and explains why (e.g. "Mathematics satisfied · Aggregate satisfied · Entrance NOT satisfied").

**Programs:** B.Tech CS/IT, BCA, MCA, MBA, BBA, … each with duration, intake, eligibility rules, fees, campuses, quota, entrance requirements, required documents, application dates, counselling dates.

**Admission cycles:** first-class object, never hardcoded `2026-27`. Each cycle (`2026–27`, `2027–28`, …) has opening/closing dates, programs, seat allocation, fee structure, rules.

**Merit/ranking:** rank by entrance score, 12th %, 10th %, interview, category, program preference. Outputs: provisional list, final list, waitlist. Example row: `1 · Rahul Sharma · 96.4 · Selected`.

**Seats:** per-program quotas (e.g. B.Tech CSE 120 = General 60 / OBC 30 / SC 15 / ST 10 / EWS 5). Track Available / Reserved / Allocated / Confirmed / Remaining. Multiple rounds supported.

---

## 15. Counselling management

Features: counsellor assignment, appointment scheduling, time slots, room/counter assignment, applicant queue, counselling notes, outcome, next action.

Desktop queue example: `10:00 Rahul B.Tech CSE Waiting · 10:15 Priya MBA In progress · 10:30 Aman BCA Confirmed`. Mobile: `Today's Counselling` cards with `[Open]`, plus quick call / quick WhatsApp actions.

---

## 16. Fees & payments + Communication + Notifications + Timeline

**Fees:** heads = application, admission, tuition, hostel, examination, misc. Statuses: `Pending / Partially Paid / Paid / Refunded / Failed`. Receipt + invoice + payment history; online gateway where relevant. Money is always visible (per ReferenceUi principle): KPI cards, row totals, drawer footer, modal footer.

**Communication center:** Email · SMS · WhatsApp · Push. Templates: application received, documents rejected, counselling scheduled, shortlisted, fee pending, admission confirmed. Per-applicant timeline (e.g. `12 Sep 10:23 Email sent · 10:24 SMS sent · 13 Sep 09:10 Applicant replied · 14 Sep 14:00 Counsellor called`).

**Notifications — staff:** documents awaiting verification, applications awaiting review, fees pending, today's counselling appointments. **Student:** application incomplete, document rejected, counselling scheduled, payment successful, admission confirmed.

**Applicant timeline (binding record, newest last in this example):**

```text
16 Sep Application submitted · 16 Sep Documents uploaded · 17 Sep Documents verified
· 18 Sep Entrance result added · 20 Sep Shortlisted · 23 Sep Counselling completed
· 24 Sep Admission confirmed · 24 Sep ₹50,000 received
```

---

## 17. Reports, roles, audit, import/export, duplicates

**Reports:** applications by program/city/source, funnel, conversion rate, fee collection, pending fees, rejected, demographics, category distribution, merit stats, counsellor performance, daily trend. Export Excel/CSV/PDF.

**Roles:** Super Admin · Admission Admin · Admission Counselor · Document Verifier · Accounts · Department Staff · Reviewer · Read Only. Permissions: view/edit applications, verify documents, approve admissions, manage fees, generate reports, manage users.

**Audit logs:** who changed what, when, old value, new value (e.g. "Aslam changed status Under Review → Approved"). Mandatory with multiple staff.

**Import/export:** Excel/CSV/PDF both directions (e.g. import 2,000 applicants from legacy DB).

**Duplicate detection:** on create, warn with existing record (name + phone + email + `ADM-2026-002381`). Keys: phone, email, DOB, government ID, name similarity (+ AI probability in §20).

---

## 18. Student-facing portal (same backend, mobile-first)

`Create account → Search programs → Start application → Upload documents → Pay application fee → Track application → View merit status → Book counselling → Pay admission fee → Download receipt → Download admission letter`. This flow is the future mobile app; staff flows (§6–§16) are the desktop priority.

---

## 19. Platform matrix: mobile-first vs desktop-first, offline, sync, security

**Mobile priority (staff/student away from desk):** status, doc upload/verify, search, notifications, counselling schedule, quick call/WhatsApp, payment confirmation, KPI dashboard, approve/reject, camera capture, biometric auth. Do NOT port every admin config page to mobile.

**Desktop priority:** complex workflows in master-detail:

```text
Filters | Applicant list | Applicant details (Personal/Academic/Documents/Eligibility/Fees/Timeline)
```

Mobile renders the same three panes as `Applications → list → detail → section` drill-down.

**Shell:** desktop = sidebar (§6); mobile = bottom nav (`Home | Applications | Tasks | More`) or as-built overlay drawer — follow §6 as-built until counsellor testing says otherwise. Dashboard quick actions: `+ New Application / + New Enquiry / + Add Applicant / Upload Documents / Schedule Counselling / Record Payment`; mobile = `[+]` quick-action sheet.

**Offline (tolerant, moving to first):** cache applicant search, drafts, forms, notes, queued uploads/updates. Flow: `Offline → save locally → connection restored → sync`. **Sync/conflicts** (Mac/Win/Linux/iPhone/Android): server timestamps, version numbers, optimistic concurrency (`ConcurrencyToken` per master plan), sync queue, conflict resolution.

**Security (from paste §40, binding minimum):** authentication, RBAC, encryption in transit + at rest, secure document storage, session management, audit logging, password policies, optional MFA, device/session management, secure backups, configurable retention. No permanent client-fs document copies without a deliberate offline-storage design.

---

## 20. AI features (value-only, no chatbot-for-show)

- **Document extraction:** marksheet photo → populated fields (Name, Physics 82, Chemistry 79, Maths 91, 84%).
- **Document classification:** detect "this is a 12th marksheet".
- **Completeness check:** "2 required documents missing".
- **Duplicate detection:** "92% probability duplicate".
- **Counsellor assistant:** one-line brief ("Eligible B.Tech CSE, prefers Pune, verification pending").
Architecture per master plan Phase 7 (voice/text/vision/MCP, on-device-first, opt-in).

---

## 21. Workflow builder + MVP phasing

**Admission Workflow Builder (flagship configurability):** admin composes stages instead of hardcoded `New → Verify → Eligibility → Entrance → Merit → Counselling → Payment → Admission`, with rules like `IF program=B.Tech → Entrance required; IF MBA → Interview required; IF international → Visa document required`. Makes the product reusable across colleges.

**MVP (binding order):**

- Phase 1 — Core: Dashboard, Applications, Applicant profiles, Programs, Application forms, Documents, Application workflow, Search/filter, Users/Roles.
- Phase 2: Enquiries, Counselling, Merit/ranking, Seat management, Payments, Notifications, Reports.
- Phase 3: AI extraction/duplicates/eligibility, WhatsApp integration, Advanced analytics, Offline sync, Workflow builder.

---

## 22. Screen inventory + next step

Desktop shell (binding composition):

```text
┌─────────────────────────────────────────────────────────────┐
│ Sidebar │ Top bar                                           │
│         ├───────────────────────────────────────────────────┤
│         │                Main content                       │
└─────────────────────────────────────────────────────────────┘
```

Applicant management (desktop 3-pane → mobile drill-down, §19):

```text
Desktop: Filters | Applicant list | Applicant details
Mobile:  Applications → list → details → section
```

Next step (agreed in source paste): define the **complete screen-by-screen UI/UX spec (~25–40 screens)** — exactly what appears on desktop vs mobile, navigation, tables, cards, forms, drawers, modals, states, responsive breakpoints — before handing to a coding agent. That spec must cite ReferenceUi tokens per screen.

---

*Sources: `src/AvaloniaUi/.../CoursesView.axaml`, `MainMenuView.axaml`, `RegistrationView.axaml`, `CollegeAdmission.Core/Models/Course.cs`, `README.md`, `docs/ReferenceUi/UiPrompt.md`, `docs/ReferenceUi/Reference-UI-Analysis.md`; ui-skills MCP registry (303) + fetched: `interface-design`, `ui-ux-pro-max`, `frontend-design`, `create-design-md`, `better-colors`, `better-typography`, `shape`, `layout`; pasted admissions-CRM vision (45 sections, 2026-09-16) merged without loss into §§0,4–6,10–22.*
