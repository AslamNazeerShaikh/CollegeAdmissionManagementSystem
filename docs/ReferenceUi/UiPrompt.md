You are an expert product designer and senior frontend engineer specializing in
high-fidelity macOS desktop applications.

I am providing multiple screenshots of the SAME desktop CRM application.

Your task is to reverse-engineer the visual design system and reproduce the UI/UX
with extremely high visual fidelity.

IMPORTANT:
Do NOT treat each screenshot as an independent design.
Infer ONE shared design system and ONE reusable component system that explains
all screenshots.

The attached screenshots are the visual source of truth.

Ignore:
- the circular YouTube presenter/webcam overlay
- the mouse cursor
- video recording artifacts
- browser/YouTube artifacts
- anything that is clearly outside the application UI

Those must NOT be implemented.

==================================================
PRODUCT STYLE
==================================================

The application is a premium macOS desktop CRM for a field-service/carpet-cleaning
business.

Visual direction:

- native macOS feeling
- clean
- calm
- restrained
- premium
- spacious
- highly functional
- subtle SaaS aesthetic
- soft neutral backgrounds
- minimal shadows
- thin borders
- medium-weight typography
- rounded controls
- excellent information hierarchy

Do NOT use:
- Material Design styling
- Bootstrap styling
- generic admin-dashboard appearance
- excessive gradients
- glassmorphism
- neon colors
- excessive shadows
- giant rounded "AI SaaS" cards
- dark mode unless explicitly requested
- thick borders
- dense enterprise-table styling

The result should feel like a polished native macOS business application.

==================================================
DESIGN SYSTEM
==================================================

Use a neutral warm-white palette.

Approximate base palette:

App background:
#F7F6F3

Sidebar:
#F5F4F1

Primary surface:
#FFFFFF

Secondary surface:
#FBFBFA

Primary text:
#171717

Secondary text:
#666666

Tertiary text:
#8A8A8A

Border:
#DDDDD8

Divider:
#E7E7E3

Primary green:
#147A5A

Light green:
#E7F5ED

Danger:
#A94444

Light danger:
#FBECEC

Blue:
#3D73D9

Orange:
#D97A18

Use these as approximate starting values only.
Where possible, infer colors from the reference screenshots.

==================================================
TYPOGRAPHY
==================================================

Use a macOS-native system font stack:

-apple-system
BlinkMacSystemFont
"SF Pro Display"
"SF Pro Text"
system-ui
sans-serif

Typography should be restrained and medium-weight.

Approximate sizes:

Page title:
24–28px, font-weight 600

Section title:
15–18px, 600

Body:
14px, 400–500

Secondary:
13px

Metadata:
11–12px

KPI numbers:
24–30px, 600–700

Avoid excessive bold typography.

==================================================
SPACING
==================================================

Use an 8px spacing system:

4
8
12
16
20
24
32
40
48

Prefer generous whitespace over dense layouts.

Typical spacing:

Sidebar internal padding:
16–20px

Main page padding:
28–32px

Card padding:
18–22px

Form spacing:
16–24px

Input height:
40–44px

Button height:
38–42px

Table row height:
approximately 60–76px

==================================================
BORDERS
==================================================

Borders are subtle and approximately 1px.

Use very light neutral borders.

Never use heavy 2px dark borders unless required for a specific focus/error state.

Typical:
border: 1px solid rgba(0,0,0,0.08)

Avoid excessive visible outlines.

==================================================
CORNER RADIUS
==================================================

Use a clear hierarchy:

Application window:
18–22px

Large modal:
16–18px

Cards:
12–14px

Inputs:
8–10px

Buttons:
8–10px

Pills:
999px

Avatars:
50%

Do not make every component use the same radius.

==================================================
SHADOWS
==================================================

Shadows must be subtle.

Cards:
very subtle or none

Modal:
soft large elevation

Drawer:
soft lateral elevation

Example card:
0 1px 2px rgba(0,0,0,0.03)

Example modal:
0 20px 60px rgba(0,0,0,0.12)

Example drawer:
-12px 0 40px rgba(0,0,0,0.08)

Never use dark dramatic drop shadows.

==================================================
WINDOW
==================================================

Build this as a desktop-first macOS application.

Outer application window:
- rounded corners
- subtle shadow
- macOS traffic-light controls
- visually separated from desktop background

Do not make the layout look like a normal responsive website.

==================================================
SIDEBAR
==================================================

Create a fixed left sidebar approximately 280–310px wide.

Contents:

Brand:
- icon/logo
- "Brightway Carpet Care"
- "CRM"

Search field.

Navigation:

Today
Calendar
Jobs
Customers
Quotes
Invoices

Section label:
BUSINESS

Settings

Bottom metadata:
"2 vans · 3 cleaners"
date text

Sidebar styling:
- slightly darker than main content
- soft neutral background
- subtle active navigation background
- icons aligned consistently
- generous vertical rhythm

Active navigation:
- soft neutral fill
- rounded approximately 8px
- no strong color block

==================================================
TODAY DASHBOARD
==================================================

Header:

Title:
Today

Subtitle:
Saturday 5 September 2026 · 4 jobs · 3 done

Top-right controls:
previous date
Today
next date
Week button
+ New job

Create five summary KPI cards.

Cards must:
- white
- subtle border
- 12–14px radius
- generous padding
- little/no shadow
- large primary value
- small supporting text

Examples:

Today's value
£847.40
4 jobs across 2 vans

This week
£5,252.00
33 jobs booked

Awaiting payment
£6,258.80
33 invoices

Overdue
£2,616.20
12 invoices past due

Quotes out
£1,134.60
8 waiting on customers

==================================================
VAN / JOB SCHEDULE
==================================================

Create reusable VanSection components.

Van header:

icon
Van 1
vehicle registration
cleaner
time range
stop count
total amount

Van colors:

Van 1 = blue accent
Van 2 = orange accent

Use the accent only for:
- icon
- dot
- numbering
- tiny identity accents

Do NOT color the entire card.

Each job row contains:

time
duration
stop number
customer/job title
address
amount
small note
status
action

Status pills:

Done = green
In progress = amber
Booked = blue
Paid = neutral/green

Status pills:
- pill shaped
- 999px radius
- 11–12px text
- 4–9px horizontal padding

==================================================
ROUTE MAP
==================================================

Dashboard contains a large map panel to the right of the job schedule.

Map container:
- white card
- subtle border
- 12–14px radius
- no excessive shadow
- header with "Route map"
- small legend for Van 1 / Van 2

Use a map placeholder if real maps are not required.

==================================================
JOB DETAIL DRAWER
==================================================

Clicking a job opens a right-side detail drawer.

Background page becomes dimmed.

Drawer:
- white
- approximately 45–50vw
- rounded outer corners
- subtle shadow
- independent scrolling

Header:
job icon
job/customer name
job ID
date
time
close button

Status progression:

Booked
In progress
Done
Paid

Render as a horizontal status stepper.

Primary actions:
Record payment
Edit

Secondary destructive action:
Cancel job

Sections:

ADDRESS
address
city/postcode
location notes
parking/access notes

CREW
van
vehicle registration
cleaner
hours allowed

CONTACT
phone/email

WORK
item/service details

INVOICE
invoice identifier
payment status

ACTIVITY
notes
add-note input

==================================================
EDIT JOB MODAL
==================================================

Large centered modal.

Background:
dim overlay

Modal:
white
16–18px radius
~950–1050px width
soft large shadow

Header:
Edit job
close icon

Form sections:

Customer
Property

Date
Start
Duration

Van
Crew

WORK

Service item table/editor:
Item
Qty
Unit

Example:
Office carpet | 217 | 2.2

Button:
+ Add service

Notes for crew
Office notes

Footer:
Total £477.40 · 4h

Use a two-column grid where appropriate.

Inputs:
40–44px high
subtle 1px border
8–10px radius
12px horizontal padding

Focus states should be subtle and premium.

==================================================
SEGMENTED CONTROLS
==================================================

For customer type, van selection and crew selection use segmented/pill controls.

Example:

[ Residential ] [ Commercial ]

[ Van 1 ] [ Van 2 ]

[ Marcus ] [ Priya ] [ Jake ]

Selected:
soft filled background

Unselected:
white/neutral surface with subtle border

Do NOT use generic browser buttons.

==================================================
NEW CUSTOMER MODAL
==================================================

Same modal system as Edit Job.

Header:
New customer

Customer type:
Residential / Commercial segmented control

Two-column form:

First name
Last name

Mobile / phone
Alternative phone

Email
How did they find us?

Tags:

regular
pets
landlord
VIP
key holder
referrer

Use pill tags.

Notes textarea.

PROPERTY ADDRESS section.

Fields should have:
- clear labels
- generous spacing
- soft borders
- consistent widths
- same typography as rest of app

==================================================
JOBS PAGE
==================================================

Create a reusable data-table page.

Header:

Jobs
107 jobs · £17,176.20

Tabs:

Upcoming
Today
Open
Awaiting payment
Paid
Cancelled
All

Filter:
All vans

Search:
Customer, address, postcode...

Table columns:

WHEN
CUSTOMER
ADDRESS
VAN · CREW
STATUS
TOTAL

Do not use heavy vertical table borders.

Use:
- subtle horizontal separators
- generous whitespace
- grouped date rows
- clean alignment

Date group headers:
SATURDAY 26 SEPTEMBER 2026
FRIDAY 25 SEPTEMBER 2026

Date group headers:
- uppercase
- muted
- small
- slightly letter-spaced

Customer cell:
colored circular initials avatar
primary customer name
secondary phone number

Address:
primary address
secondary neighborhood/postcode

Van/crew:
colored van dot
van name
cleaner name

Status:
small pill

Total:
right aligned
semibold

==================================================
AVATARS
==================================================

Circular initials avatars.

Examples:
JJ
PR
NS
KH
MW
AR
PD
FF

Approx:
32–36px

Use muted deterministic colors.

==================================================
ICONOGRAPHY
==================================================

Use a consistent modern line-icon system.

Icons should be:
- thin
- simple
- approximately 16–20px
- visually quiet

Do not mix multiple icon styles.

Suggested libraries:
Lucide or another coherent outline icon system.

==================================================
INTERACTION
==================================================

Preserve the interaction hierarchy shown in the screenshots.

Examples:

Click a job:
→ open detail drawer

Click Edit:
→ open Edit Job modal

Click New Job:
→ open job creation workflow

Click New Customer:
→ open New Customer modal

Click Done:
→ update job status

Click Record payment:
→ update payment status

Click tabs:
→ change dataset without changing overall layout

All overlays:
- dim background
- animate subtly
- never use aggressive animation

Preferred animation:
150–220ms
ease-out

==================================================
RESPONSIVENESS
==================================================

This is a desktop application.

Optimize first for:
1440×900
1600×1000
1920×1080

Maintain the visual composition of the reference screenshots.

Do not redesign the application when width changes.

At smaller desktop widths:
- preserve hierarchy
- collapse secondary content intelligently
- avoid horizontal page scrolling where possible

==================================================
COMPONENT ARCHITECTURE
==================================================

Create reusable components rather than one giant page component.

Suggested components:

AppShell
Sidebar
SidebarNavItem
PageHeader
SummaryCard
VanSection
JobCard
JobRow
StatusPill
Avatar
SegmentedControl
SearchField
FilterSelect
DataTable
TableGroupHeader
JobDetailDrawer
StatusStepper
Modal
FormField
ServiceLineEditor
Tag
TextArea
RouteMapCard
Button
IconButton

The same design tokens must be reused by every component.

==================================================
DESIGN TOKENS
==================================================

Centralize:

colors
spacing
radii
shadows
typography
component heights
border colors

Do NOT scatter random values throughout the implementation.

==================================================
HIGH FIDELITY REQUIREMENT
==================================================

Do not merely create a UI that is "similar."

Recreate:

- relative proportions
- alignment
- spacing
- visual hierarchy
- card dimensions
- sidebar width
- modal width
- drawer width
- text density
- border subtlety
- radius hierarchy
- shadow softness
- button proportions
- status-pill appearance
- table row height
- icon placement
- typography hierarchy

Use the screenshots as the source of truth.

When uncertain, prefer:
- less visual noise
- lighter borders
- softer shadows
- more whitespace
- smaller icons
- restrained typography

rather than inventing new visual patterns.

==================================================
IMPLEMENTATION RULE
==================================================

Before coding:

1. Analyze ALL screenshots.
2. Infer the common design system.
3. Create design tokens.
4. Create reusable components.
5. Implement the shared application shell.
6. Implement each screen/state using the same components.
7. Compare output against the reference screenshots.
8. Correct visual mismatches iteratively.

Do NOT start by coding one screenshot and then copy-pasting styles into the others.

The goal is ONE coherent application whose different states reproduce the
reference screenshots.

==================================================
TECHNICAL QUALITY
==================================================

The implementation must be:

- production-quality
- accessible
- keyboard friendly
- componentized
- maintainable
- type-safe
- responsive on desktop
- free of unnecessary dependencies
- visually consistent

Do not sacrifice visual accuracy for generic framework conventions.

The screenshots are the visual specification.