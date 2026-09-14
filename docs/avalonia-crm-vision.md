# Avalonia CRM Vision — Brand-New College Management App

**Status:** Vision approved + CRM shell built and verified on-device (see §6 as-built note, 2026-09-15)
**Date:** 2026-09-14
**Scope:** `src/AvaloniaUi/` greenfield rebuild. Legacy Java (`src/AndroidJava/`) stays archived as behavior reference. Uno port (`src/PlatformUno/`) untouched.
**Audience:** Backend-strong developer (Java 2017-2020 native Android → ASP.NET Core WebAPI/MVC since mid-2020, plus Angular/React/TS/Python/shell/gRPC/WebSocket/REST/GraphQL/Azure/AWS/SQL/NoSQL/Docker/Linux + AI agents). Self-declared gap: UI craft — MVVM structure, look-and-feel, UX/DX, palettes/branding, components, responsiveness, animation/physics/realism, XML/AXAML/XAML.

This doc is the UI contract. No new screens until this is agreed.

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

**Color world (physical space):** deep indigo ink stamp · cream paper · maroon file covers · brass paper clips · green “paid” tick · red “pending” flag · graphite whiteboard.

**Signature (only-this-product element):** **Pipeline-as-merit-board** — admission stages rendered as a physical notice-board with pin-strip cards (applicant photo, merit %, doc ticks, fee chip). Not a generic SaaS kanban: cards carry FY/SY/TY syllabus status + doc icons. If product name is removed, the board + syllabus ticks still say "college admissions".

**Rejecting defaults:** (1) identical rounded cards everywhere → board-lane + dense table + detail rail, each with own density; (2) centered 12px italic hints → left-aligned 13/14px labels, tabular numbers, one focal number per view; (3) 5 accent colors competing → 60/30/10: neutral paper dominates, indigo = interactive only, green/red/amber = status only.

**Direction:** Warm-paper dense CRM, indigo ink interactive layer, notice-board pipeline as hero. Quiet surfaces, loud only on action + status.

---

## 4. Information architecture — from 3 screens to CRM

```
AppShell (left nav 264px + top bar + content + right detail rail 360px, collapsible)
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

## 5. Design system (Avalonia Fluent + custom tokens)

Map 1:1 to an Avalonia `ResourceDictionary` (`Themes/Tokens.axaml`). No raw hex in views — `{DynamicResource}` only.

**Color — ramps, not colors (`better-colors`):**

| Semantic token | Light | Dark | Job (one meaning only) |
|---|---|---|---|
| `AccentSolid` / `AccentFg` | #4F46E5 (indigo ink) / #FFFFFF | #818CF8 / #0B0B18 | Interactive ONLY (buttons, links, selected lane) |
| `PaperBg` / `PaperSurface` / `PaperRaised` | #F7F5F0 / #FFFFFF / #FFFFFF | #10131A / #161B26 / #1D2433 | Background ladder (+7/9/12% steps in dark) |
| `InkPrimary` / `InkSecondary` / `InkMuted` | #131720 / #4B5563 / #8A8FA3 | #E8EEF5 / #A6AEC2 / #6B7280 | 4-level text hierarchy |
| `LineStandard` / `LineSoft` | rgba(19,23,32,.12) / rgba(19,23,32,.06) | rgba(255,255,255,.10) / rgba(255,255,255,.06) | Borders disappear unless needed |
| `OkSolid` / `WarnSolid` / `BadSolid` | #15803D / #B45309 / #DC2626 | desat. -10% same hues | Status ONLY, never decoration |
| `FeePaid` = Ok, `FeeDue` = Warn, `DocMissing` = Bad | — | — | |

Old `#6200EE/#03DAC5/#FF9800/#F44336` retired except indigo lineage → `AccentSolid`. Teal `#03DAC5` on white fails 4.5:1 — never as text; use for focus ring only if re-measured.

**Typography (`better-typography`, Inter already referenced via `Avalonia.Fonts.Inter`):**

- Scale (ratio 1.25, base 14 desktop / 16 mobile inputs — iOS zoom floor): `caption 11/500 · body 14/400 · label 13/500 · h4 16/600 · h3 18/600 · h2 22/650 · h1 28/700 · display 36/700` (tight tracking on h1+, `line-height` body 1.5, headings 1.15).
- Hierarchy = size + weight + color, never size alone. Numbers that change: `font-variant-numeric: tabular-nums` (Avalonia: `FontVariantNumeric` / `Typography.NumeralVariant="Tabular"` where available; else Inter tabular feature).
- Wrap: headings `TextWrapping` + balance intent (short titles), body pretty (no orphans), badges `NoWrap`, long IDs `Wrap + BreakWord`, clamp syllabus titles to 2 lines with tooltip/full title in detail rail.

**Spacing / radius / depth (`interface-design` craft foundations):**

- Base 4px, multiples only. Component 8/12/16, section 24, major 32+. Sidebar 264px (serves content), rail 360px (peer to content), nav item 40px min hit, all hits ≥40 (WCAG 44 target).
- Radius scale: inputs/buttons 6, cards 8, dialogs/sheets 12. Concentric: `outer = inner + padding`.
- Depth = ONE strategy: **borders-only light + ring dark.** Light lift: `0 0 0 1px rgba(0,0,0,.06), 0 1px 2px rgba(0,0,0,.06)`; dark: single `rgba(255,255,255,.08)` ring, no drop shadows. Inputs *darker/inset* than surface. Sidebars same bg as canvas + 1px line, not a second color.
- Motion: button press 100-160ms `scale(.97)`; popovers 150-250ms `easeOut cubic-bezier(.23,1,.32,1)` from trigger origin; page/drawer 200-300ms; Lottie only for first-run/empty states (keep existing 3 JSONs, `AutoPlay=false` except headers); `prefers-reduced-motion` → opacity only. Animate transform+opacity only, stagger 30-80ms.

---

## 6. Screen contracts (compact, info-dense — what "more on screen" means)

**AppShell:** left nav (icon + label, section groups: Work / Manage / System; badge counts on Pipeline/Fees), top bar (global search `Ctrl+K`, academic-year switcher, theme toggle, user), content, right rail (context detail of selected row/card — replaces ALL old modal popups). Keyboard: `↑↓` move, `→` advance stage, `E` edit, `/` search.

**02 Pipeline (hero, default landing):** lanes = Enquiry / Applied / Verified / Merit / Offered / Fee-paid / Enrolled. Card = photo initials, name, course tag, merit %, doc ticks (3 dots), fee chip, days-in-stage. Lane header = count + WIP warning (amber if >X days). Interactions: drag + button advance, filter by course/counsellor, sort by merit/wait. Empty lane = illustration + "Add enquiry" CTA. This is the signature — spend craft here.

**01 Dashboard:** NOT a stat-tile parking lot. One focal: `Needs action today (n)` list (fee overdue, docs missing, merit pending) → each row jumps to Pipeline/Fees. Secondary: intake funnel bar, course fill % (progress, not numbers alone), fee collected vs due sparkline, counsellor queue. Tertiary: recent activity feed.

**03 Applications (DataGrid):** 12+ columns, virtualized, sticky header, inline status pill edit, bulk select → bulk message/offer. Saved views (e.g. "BCA merit >75% docs pending").

**05 Courses:** unified 21-course `Course` record (`Id/Name/Level/Dept/Eligibility/Duration/Seats/FeePerYear/Fy/Sy/Ty urls`) — resolves master-plan parity gap. Card → rail shows eligibility/fees/seats + FY/SY/TY buttons (collapse when missing + `caution_anim` only then, same trigger as legacy `Courses.java:193`) + `Apply` pre-fills registration.

**Registration (was `RegistrationView`):** wizard in rail/page, not one long centered stack: 1 Personal → 2 Academic (marks/year per level) → 3 Contact+Address → 4 Course+Docs → 5 Review+Pay. Left step list, auto-save draft to SQLite on every field (debounced), validation inline under field (not top), `REGISTER` single primary per step. Confirm dialog = WinUI/Avalonia `Dialog`, no custom overlay divs.

**Fees / Docs / Reports / Settings:** standard dense tables + detail rail; receipts printable; doc viewer = embedded PDF (WebView2/PDF.js spike per master plan Phase 3 — do not open external browser in new app).

Responsive (`pbakaus/layout` adaptation): ≥1280 full 3-pane; 768-1279 rail becomes sheet; <768 single-pane + bottom nav (5 max: Pipeline, Apps, Fees, Courses, More), inputs 16px, board → horizontal lane pager.

**As-built responsive (2026-09-15, `CrmShellView`, verified on Galaxy M51 1080×2400):** single width threshold 900dp (`ApplyLayout()` in code-behind, no VM changes). Wide (desktop, `MinWidth 1024`): unchanged 3-pane (232 nav + content + 340 rail). Narrow (portrait 19.5:9/20:9 phones, landscape phones): nav → `SplitView` overlay drawer + hamburger (drawn bars — `☰` font glyph renders blank on-device), top bar → search/actions row + scrollable stats strip (fixed 116dp height), rail → stacked below content (capped 360dp), funnel → `WrapPanel`, Applications 6-col table → stacked cards, pipeline lanes already horizontally scrollable. Deviations from plan: drawer instead of bottom nav, stacked rail instead of sheet — revisit if counsellor testing prefers sheet.

---

## 7. Non-functional floors (build these in, not later)

- A11y: contrast measured on rendered pair, focus visible, full keyboard map, screen-reader names on all icon buttons, no emoji-as-icon in UI chrome (current `👦🩸📞` placeholders → real labels + helper text).
- Perf: virtualized lists, lazy Lottie/PDF, cold start <500ms desktop (AOT later), no layout thrash (no width/height animation).
- Data: SQLite (`Microsoft.Data.Sqlite` desktop/Android; `sqlite-net` if iOS head lands) — tables `Applicants/Stages/Courses/Docs/Fees/Comms`; drafts autosave; TLS 1.3 + no cleartext when backend (Phase 5 REST, OpenAPI → Kiota/NSwag, `System.Text.Json` source-gen for AOT) lands.
- Tests: Core unit (catalog, stage machine, fee math) + headless UI smoke per screen contract. Avalonia currently 0 tests — new app starts with them.

---

## 8. Greenfield build plan (Avalonia, from scratch)

1. **Scaffold:** new `src/AvaloniaCrm/` (slnx + `Crm.Core` net10.0 + `Crm` head + `Crm.Tests`). Reference `Avalonia 12.1.2 (Fluent), CommunityToolkit.Mvvm, Avalonia.Labs.Lottie`. Copy zero AXAML — only `CourseCatalog` URLs + field names as seed JSON (`Assets/Data/courses.seed.json` → SQLite).
2. **Tokens first:** `Themes/Tokens.axaml` (§5) + light/dark + high-contrast; `DESIGN.md` after (per `create-design-md`).
3. **Shell + nav + empty states** → Pipeline board with mock data → Dashboard queue → Applications grid → Courses+rail → Registration wizard → Fees/Docs/Comms/Reports/Settings → search/launchers → persistence → PDF spike → backend seam.
4. **Per-screen gate:** states (loading/empty/error) + keyboard + measure (contrast/hit/size) + device screenshot (desktop + Android) before next screen.
5. Keep old `src/AvaloniaUi/` building until Pipeline+Registration parity, then archive.

Open decisions (need owner call): 21-course fee/seat source of truth; backend (REST/Firebase/Supabase); PDF engine; roles; Marathi/Hindi scope.

---

## 9. What to ask before coding

1. Default landing: Pipeline board (recommended) vs Dashboard?
2. Which 3 lanes cause the most daily pain?
3. Fee/Seat numbers per course — real data source?
4. Roles: single clerk vs counsellor/office/admin?

Reply with these 4 answers (or "proceed with recommendations") and build starts at §8.1.

---

*Sources: `src/AvaloniaUi/.../CoursesView.axaml`, `MainMenuView.axaml`, `RegistrationView.axaml`, `CollegeAdmission.Core/Models/Course.cs`, `docs/modernization-master-plan.md`, `README.md`; ui-skills MCP registry (303) + fetched: `interface-design`, `ui-ux-pro-max`, `frontend-design`, `create-design-md`, `better-colors`, `better-typography`, `shape`, `layout`.*
