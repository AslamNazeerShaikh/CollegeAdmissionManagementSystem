# Reference UI Analysis — Brightway Carpet Care CRM (8 Screenshots)

Source: `docs/ReferenceUi/` — 8 YouTube screenshots + `UiPrompt.md` + `LLM-Chat.txt`
Date analyzed: 2026-09-15
Target: Define look / feel / layout / UX / DX we want for our **College Admission CRM** (Uno Platform / Avalonia).

Ignore in all screenshots: circular YouTube presenter overlay, blue cursor arrows, video compression, dimming from modal/drawer overlays. Those are recording artifacts, not product UI.

---

## 1. What the 8 images actually show

| # | File | Screen | Key learning |
|---|------|--------|--------------|
| 1 | `2.37.01` | **Today dashboard** | Full AppShell: sidebar + header + 5 KPI cards + Van-grouped schedule + Route map |
| 2 | `2.37.48` | **Job detail drawer** | Right drawer over dimmed dashboard. Status stepper, Record payment / Edit / Cancel, ADDRESS / CREW / CONTACT / WORK / INVOICE / ACTIVITY sections |
| 3 | `2.37.58` | **Edit job modal** | Large centered modal ~1000px. Customer / Property / Date-Start-Duration / Van-Crew segmented / WORK line editor / dual notes / Total footer |
| 4 | `2.38.35` | **New customer modal** | Same modal system. Residential/Commercial segmented, 2-col form, pill tags, Notes, PROPERTY ADDRESS |
| 5 | `2.38.45` | **Jobs / Open (107 jobs · £17,176.20)** | Data-table page: tabs + van filter + search + date-grouped rows, WHEN / CUSTOMER / ADDRESS / VAN·CREW / STATUS / TOTAL |
| 6 | `2.38.55` | **Jobs / All (350 jobs · £57,275.80)** | Same table component, different tab + counts. Proves tabs switch dataset, not layout |
| 7 | `2.39.09` | **Settings** | Business / Vans / Cleaners / Invoicing cards. Inline row editing (name, reg, phone, van assignment, Active checkbox, Save per row) + global Save changes |
| 8 | `2.39.29` | **Jobs / Open duplicate** | Confirms table is stable reusable pattern |

One app, 4 layout modes: **Dashboard, Drawer, Modal, Table, Settings-cards**. All share one token set.

---

## 2. Visual design system (steal verbatim)

### 2.1 Palette — warm neutral, not cold gray
```
App bg:       #F7F6F3
Sidebar:      #F5F4F1  (slightly darker than content)
Surface:      #FFFFFF
Surface 2:    #FBFBFA
Text:         #171717 / #666666 / #8A8A8A
Border:       #DDDDD8 / rgba(0,0,0,0.08)
Divider:      #E7E7E3
Green (CTA):  #147A5A, light #E7F5ED
Danger:       #A94444, light #FBECEC
Blue (Van 1): #3D73D9, light #EDF3FF
Orange(Van2): #D97A18, light #FFF3E4
```
Lesson: color is **scarce**. Only green CTA, red overdue/invoice badge, blue/orange identity dots. Everything else is warm gray + black text.

### 2.2 Typography — macOS native, medium not bold
Stack: `-apple-system, BlinkMacSystemFont, "SF Pro Display", "SF Pro Text", system-ui, sans-serif`

| Use | Size / weight |
|-----|---------------|
| Page title (Today / Jobs / Settings) | 24–28px, 600 |
| Section title | 15–18px, 600 |
| Body | 14px, 400–500 |
| Secondary (phone, address sub-line) | 13px, #666 |
| Metadata (WHEN sub, table group header, ADDRESS label) | 11–12px, uppercase + letter-spacing for group headers |
| KPI numbers | 24–30px, 600–700. Only red when Overdue |

### 2.3 Spacing — 8px grid, generous
`4 / 8 / 12 / 16 / 20 / 24 / 32 / 40 / 48`. Sidebar pad 16–20, page pad 28–32, card pad 18–22, input h 40–44, button h 38–42, table row h 60–76. Never cramped.

### 2.4 Borders / radius / shadow — subtle hierarchy
- Border: `1px solid rgba(0,0,0,0.08)` everywhere. No 2px dark borders.
- Radius ladder: window 18–22 → modal 16–18 → card 12–14 → input/button 8–10 → pill 999 → avatar 50%. Don't unify to one radius.
- Shadow: cards ~none / `0 1px 2px rgba(0,0,0,0.03)`; modal `0 20px 60px rgba(0,0,0,0.12)`; drawer `-12px 0 40px rgba(0,0,0,0.08)`. No dramatic shadows, no glassmorphism, no gradients, no neon.

### 2.5 Iconography
Single thin outline set (Lucide-style), 16–20px, quiet gray. Nav icons left-aligned, consistent width. Brand mark: dark-green rounded square with sparkle.

### 2.6 Font files & sourcing (Nerd Fonts)
- Source: https://www.nerdfonts.com/font-downloads — pick a font → `Download` (zip from GitHub releases) → extract → use the contained `.otf` / `.ttf` files as needed.
- Current Android bundle (`src/AndroidJava/app/src/main/res/font/`): `sf_pro_display_regular.ttf` (400) + `sf_pro_display_bold.ttf` (700) only. Used as `@font/sf_pro_display_bold` (titles/labels/buttons) and `@font/sf_pro_display_regular` (body/secondary) across all layouts.
- Gaps to fill from Nerd Fonts as needed: **Medium/Semibold (500/600)** — the reference UI sets almost all emphasis at 500–600, full 700 bold renders heavier than spec; and **SF Pro Text** (or equivalent) for 11–13px metadata/small text where Display looks cramped.
- Android wiring: drop new `.ttf`/`.otf` into `res/font/` (lowercase underscores, e.g. `sf_pro_display_medium.ttf`), reference via `android:fontFamily="@font/sf_pro_display_medium"`. No code changes needed.
- If icon glyphs are needed in-app (drawer section icons, nav icons), prefer a Nerd-Font-patched variant (same download page, `*NerdFont*.ttf` inside the zip) so icons render as text glyphs instead of image assets.

---

## 3. Layout / components per screen

### 3.1 AppShell + Sidebar (280–310px fixed)
```
[●●●] Brightway Carpet Care / CRM
[Search ⌘K]
Today, Calendar, Jobs, Customers, Quotes, Invoices (badge 8 on Quotes, red pill 12 on Invoices)
BUSINESS label → Settings
Footer: "2 vans · 3 cleaners" + "Saturday 5 September"
```
- Active nav = soft neutral fill + 8px radius, **not** colored block.
- Search has ⌘K hint. Badges are quiet except red invoice count.
- Sidebar is the anchor — same across all 8 shots.

### 3.2 Today dashboard (Img 1)
Header: `Today` + `Saturday 5 September 2026 · 4 jobs · 3 done` left; `< Today > [Week] [+ New job]` right.
5 KPI cards in one row: Today's value £847.40 / This week £5,252 / Awaiting £6,258.80 / Overdue £2,616.20 (red) / Quotes out £1,134.60. Each: white, 12–14px radius, big value + small label + small sub-line.
Below: left column = VanSection cards; right column = Route map card (white, header + Van1 blue / Van2 orange legend, map placeholder).

VanSection header: `[van-icon] Van 1  WX68 KLP / Marcus | 8:30am–2:30pm  2 stops  £557.40`. Van color only on icon/dot/number.
Job row: `8:30am / 4h | (1) | Bishopston Nurs… / 71 Kings Drive… / £477.40 · note | [● Done] [£ Paid / ✓ Done]`. Time left, stop-number circle tinted by van, 2-line title/address, status pill + action button right.

### 3.3 Job detail drawer (Img 2)
- Right drawer ~45–50vw, white, rounded outer corners, own scroll, dims background.
- Header: building icon + `Bishopston Nursery` + `J-1241 · Saturday 5 Sep 2026 · 8:30am–12:30pm` + `● Done` pill + X.
- Stepper: `✓ Booked — ✓ In progress — ◉ Done — ○ Paid`.
- Actions: green `£ Record payment` + white `✎ Edit`, quiet text `Cancel job` far right (restraint for destructive).
- Body cards: ADDRESS (street / city-postcode / Bishopston·office / Access / Parking) + CREW (● Van1 WX68 KLP / Marcus / 4h allowed) side-by-side; CONTACT; WORK (ITEM list); INVOICE (INV-2234 + status); ACTIVITY (Add a note… input, Booked 25 Aug note, £477.40 footer).

### 3.4 Edit Job modal (Img 3) + New Customer modal (Img 4) — same system
Modal: centered, ~950–1050px, 16–18px radius, dim overlay `rgba(0,0,0,0.18–0.28)`.
- Header: `Edit job / New customer + X`.
- Labels above inputs (13–14px medium), 6–8px gap, input h 40–44, 8–10px radius, subtle focus ring (no blue glow).
- Edit Job: Customer combo (`Bishopston Nursery  71 Kings Drive…  x`), Property text, Date/Start/Duration 3-col, Van `[● Van1][● Van2]` + Crew `[Marcus][Priya][Jake]` segmented (selected = soft fill), WORK table `ITEM | QTY | UNIT` (Office carpet | 217 | 2.2) + `+ Add service`, dual textareas (Notes for crew / Office notes), footer `Total £477.40 · 4h` + Cancel/Save.
- New Customer: `[⌂ Residential][🏢 Commercial]` segmented, 2-col `First|Last, Mobile|Alt phone, Email|How found (Google)`, Tags pills `[regular][pets][landlord][VIP][key holder][referrer]`, Notes (`Pets, preferences…`), PROPERTY ADDRESS (`12 Cranbrook Road…`).

### 3.5 Jobs table (Img 5/6/8)
Header: `Jobs` + `107 jobs · £17,176.20` (or 350 / £57k in All). Green `+ New job` top-right.
Toolbar: tab strip in soft container `[Upcoming|Today|Open|AWAITING PAYMENT|Paid|Cancelled|All]` (active = white card), `All vans ▾` filter, search `Customer, address, pos…` ~250–300px.
Table: headers `WHEN | CUSTOMER | ADDRESS | VAN·CREW | STATUS | TOTAL` (11px uppercase gray). **No vertical gridlines**, only thin horizontal separators. Date group rows: `SATURDAY 26 SEPTEMBER 2026`, `FRIDAY 25 SEPTEMBER 2026`.
Row cells: WHEN = `12:15pm–2:15pm / J-1348`; CUSTOMER = 32–36px colored initials avatar (JJ/PR/NS/KH/MW/AR/PD/FF, deterministic muted colors) + name + phone gray; ADDRESS = street + `Neighborhood · POSTCODE` gray; VAN·CREW = `● Van 1 / Marcus`; STATUS = `● Booked` light-blue pill; TOTAL = right-aligned semibold `£145.00`.

### 3.6 Settings (Img 7)
Header: `Settings` + `Business details, vans, crew and price list` + muted-green `Save changes` top-right.
Cards: Business (Business name, Tagline, Owner|Phone, Email|Website, Address), Vans (color square + Van1/Van2 + reg + ✅ Active + per-row Save, `+ Add van`), Cleaners (name + phone + van + Active + Save, `+ Add cleaner`), Invoicing (Payment terms days, Invoice footer textarea). Same inputs/cards/tokens as everywhere else.

---

## 4. UX learnings

1. **One design language, four modes.** Dashboard / drawer / modal / table all reuse tokens. Tabs change dataset, never layout (Open vs All proof).
2. **Hierarchy: title → quiet meta → actions.** Every page: big title, gray sub-line with counts, controls right. No competing headings.
3. **Progressive disclosure.** Dashboard row → drawer (read) → Edit modal (write). Cancel/Record payment are one click from drawer; destructive stays quiet text.
4. **Status as language.** Pills everywhere (Done green, In progress amber, Booked blue, Paid neutral, Overdue red). Stepper in drawer mirrors pills. Color + text, never color alone.
5. **Identity accents, not color floods.** Van blue/orange only on dots/icons/numbers. Avatars give per-customer color without noise.
6. **Two-line cells.** Primary + gray secondary in every cell (name/phone, street/area-postcode, van/crew, time/job-id). Dense info without density feeling.
7. **Grouping > pagination.** Date-group headers chunk long lists. KPI cards answer "how are we doing" before scrolling.
8. **Forgiving forms.** 2-col grids, clear labels, segmented single-selects instead of dropdowns for ≤3 options, pill tags for multi, inline `+ Add service/van/cleaner`, sticky total/footer so cost is always visible.
9. **Quiet destructive.** Cancel job = text link, not red button. Save per row + global Save in Settings avoids modal-for-everything.
10. **Desktop-first.** 1440×900 / 1600×1000 / 1920×1080. Fixed sidebar, content breathes. No mobile reflow in refs — collapse secondary (map) first if narrow.

## 5. DX / architecture learnings

Reusable primitives to copy (names from `UiPrompt.md`):
`AppShell, Sidebar, SidebarNavItem, PageHeader, SummaryCard, VanSection, JobCard/JobRow, StatusPill, Avatar, SegmentedControl, SearchField, FilterSelect, DataTable, TableGroupHeader, JobDetailDrawer, StatusStepper, Modal, FormField, ServiceLineEditor, Tag, TextArea, RouteMapCard, Button, IconButton` + central `DesignTokens` (colors/spacing/radii/shadows/type/heights/borders).

Rules: tokens centralized, no scattered hex; 150–220ms ease-out for overlays; keyboard-friendly (⌘K, Esc closes, focus trap in modal); type-safe models (Job/Customer/Van/Crew/Invoice); no new deps for what stdlib/XAML styles do.

---

## 6. Mapping to our College Admission CRM

| Reference | College CRM equivalent | Notes |
|-----------|------------------------|-------|
| Today → 4 jobs · 3 done | Dashboard → `Today · 24 follow-ups · 18 done` | Keep same header pattern |
| KPI cards (value/week/awaiting/overdue/quotes) | `Today's collections / This week admissions / Fees awaiting / Overdue fees (red) / Enquiries open` | Keep 5-card row, same styling |
| Van 1 blue / Van 2 orange | Counsellor teams or Departments (Science blue / Commerce orange) — dot/icon only | Don't flood cards |
| Job rows (time/duration/stop) | Application rows (slot/duration/token no.) | Keep time-left + numbered circle + 2-line title |
| Route map | Funnel chart / counselling-room schedule map | Same card container, legend |
| Job drawer + stepper Booked→Paid | Application drawer + stepper `Enquiry → Counselling → Admitted → Fees Paid` | Same actions: Record fees / Edit / Cancel admission |
| Edit Job modal | Edit Application / Edit Student | Same 2-col + segmented (Shift: Morning/Evening, Counsellor: names) + document checklist instead of service lines |
| New Customer + tags | New Student + tags `[hostel][bus][scholarship][VIP][referrer][sports]` | Same Residential/Commercial → `[UG][PG]` segmented |
| Jobs table WHEN/CUSTOMER/ADDRESS/VAN/STATUS/TOTAL | Applications table `SLOT / STUDENT(+phone) / COURSE / COUNSELLOR / STATUS / FEES` + date groups + avatar initials + search | Copy verbatim |
| Settings Vans/Cleaners | Settings Departments/Counsellors/Rooms/Fee heads | Same card + per-row Save + Active toggle |
| Invoices 12 badge, Quotes 8 | Fees Due 12, Enquiries 8 | Same sidebar badge language |

Do NOT copy: carpet-cleaning nouns, van regs, £ values, map geography, presenter overlay.

## 7. What to avoid (explicit non-goals)

Material/Bootstrap/admin-dashboard look, gradients, glassmorphism, neon, heavy shadows, 20px-everywhere radius, dark mode, thick borders, vertical table gridlines, bold-everything type, generic browser buttons/focus rings, giant red destructive buttons, one-off screen styles.

When uncertain: lighter border, softer shadow, more whitespace, smaller icon, less bold.

## 8. Implementation checklist for our CRM

- [ ] Centralize tokens (palette above + 8px spacing + radius ladder + shadows + type scale) before any screen.
- [ ] Build AppShell + Sidebar (300px, search ⌘K, badges, BUSINESS section, footer meta).
- [ ] Build primitives: StatusPill, Avatar (deterministic colors), SegmentedControl, SearchField, SummaryCard, DataTable + TableGroupHeader.
- [ ] Dashboard: PageHeader + 5 KPI cards + grouped schedule + side panel (funnel/schedule).
- [ ] Drawer: status stepper + Record fees / Edit / Cancel + ADDRESS/CREW→STUDENT/COUNSELLOR/CONTACT/DOCUMENTS/FEES/ACTIVITY.
- [ ] Modals: Edit Application + New Student sharing Modal/FormField/Tag/ServiceLine→DocumentChecklist.
- [ ] Table page: tabs (Upcoming/Today/Open/Awaiting fees/Paid/Cancelled/All) + filter + search + grouped rows.
- [ ] Settings cards: Institute / Departments / Counsellors / Fee heads with per-row Save + global Save.
- [ ] Verify against Img 1–8: proportions, alignment, density, pill/avatar/button sizes. Iterate on mismatches, don't invent new patterns.

---

## 9. Deep element-by-element analysis (second pass, all 8 images)

This section dissects *every* visible control. Measurements are visual estimates from 1440px-wide screenshots, not pixel samples — use as build targets.

### 9.1 Window chrome (all images)
- macOS traffic lights top-left: red/yellow/green ~12px circles, 8px gap, 16px top/left inset inside sidebar, not in content area.
- Outer window: 18–22px radius, thin 1px stroke + soft shadow separating it from desktop/video background. Content never bleeds to edge.
- No browser bar, no top nav bar. Sidebar *is* the navigation. This is what makes it feel native, not web.

### 9.2 Sidebar (280–310px, #F5F4F1)
1. **Brand block** (~64px tall): dark-green 36px rounded-square (10px radius) with white sparkle glyph left; two-line bold `Brightway Carpet` / `Care` 15px #171717 + `CRM` 12px #8A8A8A below. Left-aligned, 16–20px padding.
2. **Search**: full-width white input h~40px, 9px radius, magnifier 16px gray left, `Search` 14px #8A8A8A, right kbd pill `⌘K` 11px on #F0EFEC bg with 6px radius + thin border. Present on every screen — global search affordance.
3. **Nav items** (h~40px, 8px radius, 14px label, 18px outline icon, 12px left pad): `Today (sun) / Calendar / Jobs (briefcase) / Customers (people) / Quotes (doc, count 8 right-aligned gray) / Invoices (doc, red pill 12)`. Active = white/very-light fill (#FFF or #ECEBE8) + semibold text, no colored left bar. In Img 5/6/8 `Jobs` is active; Img 7 `Settings` is active; Img 1/2 `Today` active.
4. **Badges**: Quotes count = plain gray `8` text (quiet). Invoices = solid muted-red pill (#A94444 bg, white 12px bold, 999px radius, ~24×20px). Lesson: only interrupt for money overdue.
5. **Section label** `BUSINESS` 11px uppercase #8A8A8A letter-spacing ~0.8px, 24px margin above, then `Settings (gear)` nav item same style.
6. **Footer** pinned bottom: `🚚 2 vans · 3 cleaners` 13px #666 + `Saturday 5 September` 13px #8A8A8A, 16px padding. Operational context always visible.

### 9.3 Page header cluster
- **Title** 26px/600 left (`Today` / `Jobs` / `Settings`). Subtitle 13–14px #666 directly under (4px gap): `Saturday 5 September 2026 · 4 jobs · 3 done` uses middle-dot separators; `107 jobs · £17,176.20` / `350 jobs · £57,275.80`; `Business details, vans, crew and price list`.
- **Right actions** vertically centered to title: icon-only pager `< Today >` (36px square white buttons, 8px radius, 1px border), `[📅 Week]` outline button, `[+ New job]` solid green (#147A5A, white 14px/500, 38–42px h, 8–10px radius, 16px h-pad). In Settings this slot is `[Save changes]` sage-muted green (disabled-look until dirty — important nuance). In Jobs it's only `[+ New job]`.

### 9.4 KPI cards (Img 1, 5 across)
- White, 12–14px radius, 1px border, ~0 shadow, 18–20px padding, equal width, 12px gap.
- Inside top→bottom: label 13px #666 (`Today's value`), value 26px/700 #171717 (`£847.40`, comma + 2 decimals always), sub 13px #8A8A8A (`4 jobs across 2 vans`). Only `Overdue £2,616.20` value is #A94444. No icons, no charts — restraint is the pattern.

### 9.5 VanSection + JobRow (Img 1)
- **Van header row** (white card top, 16px pad, bottom divider): left 32px rounded-square van icon (blue #3D73D9 for Van 1, orange #D97A18 for Van 2, white van glyph, 8px radius) + `Van 1` 15px/600 + `WX68 KLP` 13px #8A8A8A inline + `Marcus` 13px #666 below; right `8:30am–2:30pm  2 stops  £557.40` — time 13px #666, stops 13px #666, total 14px/600 #171717.
- **JobRow** (white, horizontal divider #E7E7E3, 16–18px pad, ~76px tall):
  - Col 1 time: `8:30am` 14px/600 + `4h` / `1h 15m` / `2h 30m` 12px #8A8A8A below. Fixed ~80px width.
  - Col 2 stop number: 28px circle, tinted bg (light blue for Van 1, light peach for Van 2), 13px/600 number in van color. `1, 2, 1, 2` sequence.
  - Col 3 main: title 14px/600 truncated with `…` (`Bishopston Nurs…`, `Derek Chap…`) + tiny building icon for commercial; line 2 address 13px #666 truncated (`71 Kings Drive, BS7 4GD · Bishop…`); line 3 `£477.40 · Sign in at reception` 13px (#171717 amount + #8A8A8A note, middle dot). Max 3 lines, tight 2px leading.
  - Col 4 right: status pill top + action button bottom? Actually side-by-side: `● Done` pill + `[£ Paid]` outline button (38px h, 8px radius, £ icon + 13px label). Exception: `In progress` row shows solid green `[✓ Done]` CTA instead — the only row with a completion affordance. This is intentional: only actionable state gets solid button.

### 9.6 Route map card (Img 1 right)
- White card same radius/border as KPI. Header 14px/600 `📍 Route map` left + legend right: `● Van 1` blue dot + `● Van 2` orange dot, 12px #666.
- Body: very light gray grid + faint suburb labels (FILTON, SOUTHMEAD, WESTBURY-ON-TRYM, HENLE…) + pale blue river stroke. No pins visible in shot (likely under overlay) — placeholder map is acceptable. Card is ~35% width, full height of schedule. Collapse first on narrow windows.

### 9.7 Status pills + action buttons (all images)
- Pill spec: 999px radius, 11–12px/600, 6px v-pad / 10px h-pad, dot 6px + 6px gap + text:
  - `● Done` = #2B7D5A text on #E7F5ED bg (Img 1/2)
  - `● In progress` = brown/amber text on #FFF3E4 / #FEF0D5 (Img 1/2)
  - `● Booked` = #3D73D9 text on #EDF3FF (Img 5/6/8 table)
  - `● Paid` implied neutral/green (drawer stepper end state)
- Buttons: primary solid green h38–42px 8–10px radius 14px/500 white (`+ New job`, `✓ Done`, `£ Record payment`); secondary white with 1px border #DDDDD8 dark text (`£ Paid`, `✎ Edit`, `Week`, `< >`, `Today`); tertiary text-only (`Cancel job` 14px #171717 right-aligned). Danger is never a red button.

### 9.8 Job detail drawer (Img 2, right ~45–50vw)
- Overlays content, dims page (`rgba(0,0,0,0.18)` behind), white, left corners rounded 16px, right flush, `-12px 0 40px` shadow, independent scroll, X top-right 20px gray.
- Header: 44px dark-gray circle with white building icon + `Bishopston Nursery` 18px/600 + `● Done` pill inline + sub `J-1241 · Saturday 5 September 2026 · 8:30am–12:30pm` 13px #666 (middle dots, J-ID prefix).
- Action row: solid `[£ Record payment]` + outline `[✎ Edit]` left, `Cancel job` text right, 12px gap, 16px below stepper.
- Body: single bordered card with two columns inside (ADDRESS left, CREW right, vertical divider) + stacked sections below. Section label pattern: `📍 ADDRESS` / `🚚 CREW` / `📞 CONTACT` / `WORK` / `🧾 INV-2234` / `ACTIVITY` — 11–12px uppercase #8A8A8A letter-spaced, 16px icon, 16px margin.
  - ADDRESS: `71 Kings Drive` 15px/600 + `Bristol BS7 4GD` 14px + `Bishopston · office` 13px #666 + `Access: Sign in at reception` / `Parking: Loading bay / visitor space` 13px (#8A8A8A label + #171717 value).
  - CREW: `■ Van 1  WX68 KLP` (blue square 10px + 14px/600 + 13px gray reg) + `Marcus` 14px + `4h allowed` 13px #666.
  - WORK: `ITEM` micro-label then `Office carpet · per …` 14px.
  - INVOICE pill row: `🧾 INV-2234` + status dot + amount.
  - ACTIVITY: `[Add a note…]` full-width input h40px 8px radius placeholder #8A8A8A, timeline `Booked 25 Aug, 08:30` 12px gray, footer total `£477.40` 16px/700 right-aligned.

### 9.9 Status stepper (drawer top)
- Horizontal 4 nodes: `✓ Booked — ✓ In progress — ◉ Done — ○ Paid`. Completed = solid green circle white check + 14px/600 #171717 label; current = green ring + green dot + bold; future = gray hollow circle + #8A8A8A label. Connectors 2px solid green for done segments, light gray for todo. ~20px nodes, 8px gap to label. Reusable component, not image.

### 9.10 Edit Job modal (Img 3, ~1000px centered)
- Overlay dims + blurs background. Modal white 16–18px radius, `0 20px 60px` shadow, header `🧳 Edit job` 18px/600 left + `×` right, 20px pad, bottom divider.
- Form: labels 13–14px/500 #333 above inputs (8px gap), inputs h40–44px white 1px #DCDDD8 8–10px radius 12px h-pad 14px text.
  - Customer combo: building icon in gray circle + `Bishopston Nursery` 14px/600 + `71 Kings Drive, BS7 4GD` 13px gray + `×` clear right — single tokenized selector, not plain text.
  - Property: static text `71 Kings Drive, BS7 4GD` 14px (derived, not editable here).
  - Date/Start/Duration 3-col grid: `05/09/2026 📅` + `08:30 am 🕐` (native date/time inputs with trailing icons) + `4h ▾` dropdown + tiny `⚡` magic-fill icon right of duration (easy to miss — auto-estimate affordance).
  - Van/Crew segmented: `Van` label then `[● Van 1][● Van 2]` pills (selected = pale green/blue tint bg + dark text + colored dot; unselected = white + border); `Crew` then `[Marcus][Priya][Jake]` same (Marcus selected tinted). Not radio buttons, not dropdowns.
  - WORK section: `WORK` uppercase gray label + column heads `ITEM | QTY | UNIT` 11px gray + row inputs (`Office carpet` flex + `217` 80px + `2.2` 80px) + `[+ Add service]` dashed/outline small button. Lightweight table-form hybrid.
  - Notes: two textareas side-by-side (`Notes for the crew` placeholder `What to focus on, what the customer said…` vs `Office notes` empty), ~90px tall, resize handle bottom-right.
  - Footer: sticky bottom bar with top divider: `Total £477.40 · 4h` left (Total 13px gray + amount 15px/700) + `[Cancel]` + solid `[Save]` right (Cancel partially hidden under overlay in shot).

### 9.11 New Customer modal (Img 4, same shell)
- `Customer type` label + segmented `[⌂ Residential][🏢 Commercial]` in a joined control (container #F0EFEC 8px radius, selected segment white with shadow + icon + 14px/500). Residential selected.
- 2-col grid rows: `First name | Last name` (First focused: 2px green-gray focus ring, others 1px border), `Mobile/phone (07… placeholder) | Alternative phone`, `Email | How did they find us? (Google prefilled)`.
- Tags: label + wrap row of outline pills `[regular][pets][landlord][VIP][key holder][referrer]` 13px, 999px radius, 8px h-pad, toggle-select (none selected in shot — default off).
- Notes textarea full width (`Pets, preferences, anything the crew should know…`).
- `PROPERTY ADDRESS` uppercase section header + `Address line 1` label + `12 Cranbrook Road` input. Signals address is a grouped sub-form, same input style.
- Footer `Cancel` outline visible; Save hidden below fold — modal scrolls internally.

### 9.12 Jobs table (Img 5/6/8 — most information-dense)
- Tabs: pill container #F0EFEC 10px radius, 4px pad, items `Upcoming | Today | Open | Awaiting payment (2-line) | Paid | Cancelled | All` 14px #666; active (`Open` in Img 5/8, `All` in Img 6) = white bg + 8px radius + shadow-sm + #171717/600. Two-line tab wraps centered. Clicking changes count line only (`107 jobs · £17,176.20` vs `350 jobs · £57,275.80`) — layout identical.
- Filter `All vans ▾` white 40px h 8px radius border, ~150px wide; search `🔍 Customer, address, pos…` truncated placeholder (field too narrow — keep min 250px in our build), same height.
- Column header row: `WHEN | CUSTOMER | ADDRESS | VAN·CREW | STATUS | TOTAL` 11px/600 #8A8A8A uppercase, 12px pad, bottom divider, no verticals. Implied widths: WHEN ~15%, CUSTOMER ~22%, ADDRESS ~25%, VAN ~14%, STATUS ~12%, TOTAL ~12% right-aligned.
- Group header: full-width row `SATURDAY 26 SEPTEMBER 2026` / `FRIDAY 25 SEPTEMBER 2026` 12px/600 #8A8A8A letter-spacing 0.6px, 16px top pad, faint divider below. Groups descending date.
- Body rows (~68px, horizontal divider only, 12px v-pad):
  - WHEN: `12:15pm–2:15pm` 14px/600 (en-dash range, no spaces) + `J-1348` 12px #8A8A8A below. J-IDs sequential-ish (J-1342…J-1350).
  - CUSTOMER: 32px circle initials 12px/700 white on deterministic muted bg (JJ teal-dark, PR royal-blue, NS green, KH olive, MW teal-green, AR blue, PD olive-brown, FF dark-green) + `Joseph Jenkins` 14px/500 + `07849 506710` 13px #666 (UK mobile with space). Avatar vertically centered to two-line text.
  - ADDRESS: `108 Egerton Road` 14px + `Bishopston · BS7 4LR` 13px #666 (neighborhood + postcode, middle dot). Wraps to 2 lines max.
  - VAN·CREW: `● Van 1` 14px (8px dot blue/orange) + `Marcus` / `Priya` / `Marcus & Jake` 13px #666 below. Joint crew uses `&`.
  - STATUS: `● Booked` pill (blue on pale blue, 12px). No other status in these two shots — Open/All filtered to Booked-heavy set.
  - TOTAL: `£145.00` / `£215.00` / `£70.00` 14px/600 right-aligned. Always 2 decimals.
- Last visible row `Fiona Foster … £70.00` confirms bottom edge has no pagination in view — infinite scroll or paged below fold.

### 9.13 Settings (Img 7)
- Two-column card grid: left narrow (Business + Invoicing stacked), right wide (Vans + Cleaners stacked). Cards white 12–14px radius border, header 15px/600 + top-right small outline `[+ Add van]` / `[+ Add cleaner]` buttons.
- Vans rows: 28px color square (blue/orange, 6px radius, white border + gray outer border) + `Van 1` input + `WX68 KLP` reg input (uppercase mono-ish) + `✅ Active` (dark-green checkbox 18px + 14px label) + `[Save]` small outline right. Same pattern for Cleaners: `Marcus Reid` + `07700 900341` + `Van 1 ▾` assignment + Active + Save.
- Business card: stacked `Business name` (`Brightway Carpet Care`), `Tagline` (`Carpet, rug & upholstery clear` — truncated, field too narrow), 2-col `Owner (Dean Whitlc — truncated) | Phone (0117 496 02 — truncated)`, `Email (hello@brigh — truncated) | Website (brightwayca — truncated)`, `Address (Unit 4, Ashton Vale Trading Es — truncated)`. Truncation everywhere signals inputs need wider cards or smaller text in our build — don't replicate the clipping, replicate the intent.
- Invoicing: `Payment terms (days)` label 2-line + `14` small input (~100px), `Invoice footer` textarea (`Thank you for choosing Brightway. Payment by bank…`) with visible scrollbar. Global `[Save changes]` top-right is sage/muted green (low-contrast until dirty) — distinct from vivid CTA green.

---

## 10. Second-order details easily missed

- **Truncation language**: `…` on titles/addresses, `pos…` on search placeholder, clipped inputs in Settings. Design for 300px min widths to avoid this in ours.
- **Separators**: middle dot `·` joins meta (counts, areas, notes); en-dash `–` joins time ranges; `·` never `|` or `/` for meta. Copy exactly.
- **Numbers**: £ always, comma thousands, 2 decimals even for round (`£70.00`); time `8:30am` no space, no leading zero; durations `4h`, `1h 15m`, `2h 30m`; dates `Saturday 5 September 2026` long-form in headers vs `05/09/2026` numeric in inputs vs `SATURDAY 26 SEPTEMBER 2026` uppercase in groups.
- **IDs**: `J-1241…J-1350` jobs, `INV-2234` invoices — prefix + number, gray sub-line, never the title.
- **Phones**: `07849 506710` / `07700 900341` / `0117 496 02…` — UK spacing, gray secondary, never bold.
- **Icons carry labels**: every section label has a small outline icon (📍🧳🚚⚙️🔍📅); icon + uppercase label is the section signature.
- **Overlays dim, never blur heavily**: drawer/modal backgrounds are flat dark overlay ~18–28% black; content behind is visible but non-interactive.
- **Focus**: only one focused input at a time (First name in Img 4) with subtle green-gray ring — no blue glow.
- **Empty states absent**: no empty-table, no-error, no-loading shots — we must design those ourselves in same language (don't invent new colors).
- **Presenter/cursor artifacts**: ignore large circular video, purple/blue arrows, `What it needs to do: customers…` text at very top (YouTube title bar bleed).

## 11. UX principles reinforced by second pass

1. Money is always visible: KPI cards, row totals, drawer footer, modal footer, table header aggregate. No screen without a £ figure.
2. One primary action per context: Today = +New job; actionable row = ✓Done; drawer = Record payment; modal = Save; table = +New job; settings = Save changes. Secondary actions stay outline/text.
3. Selection without dropdowns when N≤3: Van (2), Crew (3), Customer type (2) all segmented. Dropdowns only for long lists (Duration, All vans, How found).
4. Read → edit separation: drawer never has text inputs except Add-a-note; all structured editing lives in modal. Prevents half-edited states.
5. Batch + atomic saves coexist: per-row Save (Vans/Cleaners) for quick fixes + global Save changes for drafts. Matches mental model of fleet vs business profile.

## 12. DX — build tokens distilled from pixels

```text
sidebarW: 300px; pagePad: 32px; cardPad: 20px; cardGap: 16px; rowH: 68px
inputH: 42px; btnH: 40px; btnPadX: 16px
radius: window 20 / modal 16 / card 12 / input 9 / navActive 8 / pill 999 / avatar 50%
border: 1px solid rgba(0,0,0,.08); divider: #E7E7E3
shadowCard: 0 1px 2px rgba(0,0,0,.03); modal: 0 20px 60px rgba(0,0,0,.12); drawer: -12px 0 40px rgba(0,0,0,.08)
font: -apple-system…; title 26/600; label 13–14/500; body 14/400; meta 12–13/#666; micro 11/uppercase
ctaGreen: #147A5A; ctaHover: darken 5%; danger: #A94444; infoBlue: #3D73D9; warnOrange: #D97A18
pillPad: 6px 10px; pillFont: 12/600; avatar: 32px / 12.5/700 white
anim: 180ms ease-out (overlay fade + 8px slide for drawer, scale .98→1 for modal)
```

Components must accept `density: comfortable` only — no compact mode in refs. Uno/WinUI: implement as shared `ResourceDictionary` + `UserControls` mirroring the 23 names in §5.

## 13. What screenshots don't show (design ourselves, same language)

Loading skeletons (use card-shimmer in #F0EFEC), empty search/table (centered 16px icon + 14px gray text + outline action), form validation (red 12px text below input + 1px #A94444 border, no toast for inline), delete confirmation (small modal reusing shell, `Cancel` + solid-red `Delete` only exception to quiet-danger), mobile (not in scope — desktop min 1280px, collapse map column first).

