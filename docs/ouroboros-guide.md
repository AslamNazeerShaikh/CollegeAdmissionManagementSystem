# Ouroboros — Detailed Guide for This Repo

> Source-verified 2026-09-14 against docs (`ouroboros.page/learn/en/`, 12 chapters) and code (`Q00/ouroboros`, v0.51.5).
> Official links: Guide <https://ouroboros.page/learn/en/> · Roadmap <https://ouroboros.page/roadmap/> · Repo <https://github.com/Q00/ouroboros> · CLI ref <https://github.com/Q00/ouroboros/blob/main/docs/cli-reference.md> · Architecture <https://github.com/Q00/ouroboros/blob/main/docs/architecture.md> · OpenCode runtime <https://github.com/Q00/ouroboros/blob/main/docs/runtime-guides/opencode.md>

## 1. What it is (30 seconds)

Ouroboros is **not** a code writer. It is a spec-first management layer around your AI coding runtime:

```
You (idea + answers + approval)
  → Ouroboros (Interview → Seed → manage → Evaluate → record)
    → runtime (OpenCode / Claude Code / Codex…) writes files, runs commands
```

Loop: `Interview → Seed → Execute → Evaluate → Evolve (→ Ralph)`. Each loop leaves reusable assets: Seed, workflow, evaluation record, lineage.

## 2. All pages / nodes map

Landing: <https://ouroboros.page/> · EN index: <https://ouroboros.page/learn/en/>

| Ch | Page | What you learn | Key output |
|---|---|---|---|
| 01 | [What is Ouroboros](https://ouroboros.page/learn/en/what-is-ouroboros/) | Roles, inputs (one-liner + answers + optional repo), 5 artifacts | Seed in `~/.ouroboros/seeds/`, events in `~/.ouroboros/data/ouroboros.db` |
| 02 | [Why use it](https://ouroboros.page/learn/en/why-ouroboros/) | Solo-tool failure modes → fixes; fit vs unfit | Use for multi-requirement / test-proven / multi-revision / long work. Skip for typos |
| 03 | [Components](https://ouroboros.page/learn/en/architecture/) | `ooo` → Core → runtime (MCP); EventStore + Ledger + Evaluator | Storage map under `~/.ouroboros/` |
| 04 | [Install](https://ouroboros.page/learn/en/install/) | login → plugin → `ooo setup` → verify | `ooo help` + `/mcp` connected |
| 05 | [Interview](https://ouroboros.page/learn/en/interview/) | Socratic Q&A, ambiguity gate | `session_id`, score ≤ 0.2; `ooo auto` shortcut; `ooo tutorial` for practice |
| 06 | [Seed](https://ouroboros.page/learn/en/seed/) | Immutable YAML work-spec, grade A/B/C, review + edit | Grade-A Seed, `ambiguity_score ≤ 0.2` |
| 07 | [Execute](https://ouroboros.page/learn/en/execute/) | `ooo run`, Double Diamond, IDs, status/cancel | `session_id` for next step |
| 08 | [Evaluate](https://ouroboros.page/learn/en/evaluate/) | 3-stage gate + manual check + `qa` distinction | Pass verdict |
| 09 | [Iterate](https://ouroboros.page/learn/en/evolve/) | `evolve`, lineage, auto-stop, `ralph` | `lineage_id`, converged Seed |
| 10 | [Existing projects](https://ouroboros.page/learn/en/existing-project/) | `brownfield`, `pm` (PRD), `publish` (→ GitHub Issues) | Grounded questions, PRD, team issues |
| 11 | [Troubleshooting](https://ouroboros.page/learn/en/troubleshooting/) | Symptom → check → command → expected | Recovery (see §8) |
| 12 | [Glossary](https://ouroboros.page/learn/en/glossary/) | Canonical terms | Shared vocabulary |

Roadmap: <https://ouroboros.page/roadmap/> — evidence-gated, no dates. Verified = OSS engine + 14 runtimes. Planned = public proof / ChatGPT Hosted. Conditional = Kiro/AWS pilot, enterprise, marketplace. No token/crypto (explicit disclaimer in README).

## 3. How it works, end to end

### 3.1 Interview (ch.5) — expose hidden assumptions

```bash
ooo interview "a simple web app to add today's to-dos and mark them done"
# or first-timer practice: ooo tutorial
# or one-shot: ooo auto "…" [--skip-run] [--resume <auto_session_id>]
```

- Answer in plain sentences ("survive refresh", "strike through, don't delete", "single user").
- Score: `Ambiguity = 1 − (goal×0.4 + constraints×0.3 + success×0.3)`, 0–1, lower = clearer.
- Exit at `≤ 0.2`. Keep the printed `session_id`.

### 3.2 Seed (ch.6) — freeze the spec

```bash
ooo seed <session_id>
```

Filed under `~/.ouroboros/seeds/*.yaml`:

```yaml
goal: Build a web app that adds today's to-dos and marks them done
constraints: [Single user on single device, Store in browser localStorage]
acceptance_criteria:
  - An empty title is not added
  - An added to-do survives a page refresh
  - Checking a to-do shows and stores the done state immediately
ontology_schema: {name: TodoList, fields: [{name: title, type: string}, {name: completed, type: boolean}]}
evaluation_principles: [{name: completeness, description: every acceptance criterion actually works}]
exit_conditions: [{name: all_criteria_met, description: all acceptance criteria pass}]
metadata: {ambiguity_score: 0.15}
```

| Field | Review rule |
|---|---|
| Goal | States what a user can do, no "pretty app" vagueness |
| Constraints | Matches your answers (tech/env/scope) |
| Acceptance Criteria | Each reproducible on screen or by test |
| Ontology | Field names/types match the feature |
| Evaluation Principles / Exit Conditions | Emphasis + finish-state correct |

Grades: **A** = no blockers, all thresholds pass → may run (`ooo auto` requires A). **B** = fix findings + regrade. **C** = blockers, may not run. Non-goals/assumptions live in the **Ledger**, not the Seed (`ouroboros auto --show-ledger` to view).

### 3.3 Execute (ch.7) — Double Diamond

```bash
ooo run
ooo status <session_id>        # + `ouroboros monitor` (live TUI) in another terminal
ooo cancel                     # pick active execution or pass ID
```

Phases: **Discover** (repo/env/needs) → **Define** (in/out) → **Design** (structure/order) → **Deliver** (files+tests). First run asks `efficient` vs `quality_first`; pin with `ouroboros config set execution.default_policy quality_first` (`ask` = default).

IDs:

| ID | Shape | Use in |
|---|---|---|
| `session_id` | `orch_…` | `status`, `evaluate`, resume |
| `job_id` | `job_…` | bg-job status/result |
| `execution_id` | `exec_…` | `cancel` |
| `auto_session_id` | `auto_…` | `auto --resume` |
| `lineage_id` | `lin_…` | `evolve --status`, `ralph` |

### 3.4 Evaluate (ch.8) — 3-stage gate

```bash
ooo evaluate <session_id>
ooo qa   # separate: one-shot file/text check, no session needed (pass ≥0.80, fail <0.40)
```

| Stage | What | Pass |
|---|---|---|
| 1 Mechanical | Auto-detect lang → lint/build/test/coverage, no LLM | All exit 0, coverage ≥ 70% |
| 2 Semantic | LLM vs Acceptance Criteria + score | Criteria met + score ≥ 0.8 |
| 3 Consensus | Multi-model vote, only if triggered | ≥ 2/3 approve |

Consensus triggers (first match wins): Seed modified → ontology changed → goal reinterpreted → drift > 0.3 → stage-2 uncertainty > 0.3 → alternative approach adopted. Direct request forces it. **Drift** = distance from Seed intent (goal 50% + constraints 30% + ontology 20%); low drift ≠ correct — read with criteria verdict. Always manually reproduce criteria (e.g. empty-title rejected, refresh persists, done-state immediate).

### 3.5 Iterate (ch.9) — evolve + ralph

```bash
ooo evolve "to-do app: keep the done state after a refresh" [--no-execute]
ooo evolve --status <lineage_id>
ooo ralph --lineage-id <lineage_id>   # background until stop
```

One generation = Wonder (unknowns?) → Reflect (Seed diff) → execute → evaluate. Chain = **lineage**. Auto-stop (first hit): convergence (ontology similarity ≥ 0.95, needs ≥ 2 gens) → stagnation (≥ 0.95 × 3 gens) → oscillation (N ≈ N−2) → cap 30 gens. Human stop when: criteria met, same failure repeats, change < run cost, direction needs a person.

### 3.6 Existing projects (ch.10) — brownfield / pm / publish

```bash
ooo brownfield                       # pick default repos/worktrees for interview context
ouroboros setup scan [DIR]           # CLI equivalents: scan / list / default
ouroboros setup list
ouroboros setup default
ooo pm                               # product-level Q&A → PRD (then feed PRD into `interview`)
ooo publish <seed_path>              # Seed → GitHub Issues (needs `gh` auth; confirms first)
```

## 4. Code internals (for contributors)

`src/ouroboros/`: `bigbang/` (interview, ambiguity, seed_gen) · `routing/` PAL router (Frugal 1x <0.4 / Standard 10x <0.7 / Frontier 30x; escalate after 2 fails, downgrade after 5 wins; complexity = 0.3·tokens/4000 + 0.3·tools/5 + 0.4·depth/5) · `orchestrator/` (14 adapters: Claude/Codex/OpenCode/Hermes/Gemini/Kiro/Copilot/Pi/OMP/GJC/Goose/Antigravity/Grok/Zcode + `parallel_executor.py` recursive AC fan-out, depth 0–4 durable) · `resilience/` (SPINNING/OSCILLATION/NO_DRIFT/DIMINISHING_RETURNS → hacker/researcher/simplifier/architect/contrarian; `ooo unstuck`) · `evaluation/` (pipeline) · `evolution/` (wonder/reflect) · `persistence/` (SQLite append-only `events`, replay + checkpoints) · `observability/` (drift, retrospective) · `providers/` (LiteLLM, 100+ models) · `mcp/` (bidirectional hub: serve + consume) · `plugin/` (22 skills + 21 agents, lazy `/ouroboros:` dispatch) · `tui/` + `cli/` (Typer).

Key invariants: Seed = frozen Pydantic model; events = dot-notation past-tense, never mutated; cheap checks before expensive consensus; start frugal, escalate only on failure.

## 5. Install for this repo (OpenCode on macOS)

We use **OpenCode**, so follow the OpenCode runtime guide, not the Claude-default ch.4 screenshots.

```bash
# prerequisites
opencode --version            # install if missing: curl -fsSL https://opencode.ai/install | bash
opencode                      # run once → provider auth (no ANTHROPIC_API_KEY needed)

# ouroboros
pipx install 'ouroboros-ai[mcp]'          # or: uv tool install 'ouroboros-ai[mcp]'
ouroboros setup --runtime opencode        # picker: plugin (default) vs subprocess
ouroboros status health
```

| Mode | Installs | Use when |
|---|---|---|
| `plugin` (default) | bridge at `<opencode_config>/plugins/ouroboros-bridge/` + MCP in `opencode.jsonc` | Daily interactive work; `_subagent` fan-out renders as inline Task panes, fresh context per child, no picker pollution |
| `subprocess` | `orchestrator.runtime_backend: opencode` + `llm.backend: opencode` in `~/.ouroboros/config.yaml` | Headless CI / `ouroboros run workflow` scripts; launches `opencode run --format json` per task |

Mutually exclusive on one machine (both = duplicate dispatch). `ooo` skills available inside OpenCode after setup: `interview, seed, run, status, evaluate, evolve, ralph, cancel, unstuck, qa, pm, brownfield, publish, tutorial, setup, update, help`.

Per-run overrides: `ouroboros run seed.yaml --runtime opencode`, `ouroboros init start --llm-backend opencode "…"`.

### .NET caveat (important for us)

Mechanical auto-detect covers Python/Rust/Go/Zig/Node — **not C#/.NET**. Without override, stage 1 skips and you lean on Semantic. Add `.ouroboros/mechanical.toml` to wire `dotnet build/test`:

```toml
# ponytail: minimal override, extend only when `ooo evaluate` stage-1 stays silent
[commands]
build = "dotnet build src/AvaloniaUi/CollegeAdmission/CollegeAdmission.slnx"
test  = "dotnet test src/PlatformUno/CollegeAdmission/CollegeAdmission.Tests/CollegeAdmission.Tests.csproj"
```

## 6. Benefits for CollegeAdmissionManagementSystem

| Pain here | Ouroboros fix | Concrete use |
|---|---|---|
| Avalonia (12 courses) vs Uno (21) vs Java (28 PDFs) drift | One Seed as cross-head contract + ontology diff | Seed pins single `Course` shape; Consensus triggers on ontology change |
| Phase-5 backend undecided (REST/Firebase/Supabase, `$`-delimited legacy) | Interview exposes the decision before code; Ledger records why | `ooo interview "Phase-5 registration backend…"`, non-goal = "keep flat JSON" or not |
| No Avalonia tests; Uno 4/4 green but asserts legacy names | Acceptance Criteria + Mechanical gate enforce parity | Criteria: "catalog count = 21 on both heads", "FY-missing shows caution anim" |
| Offline/in-app PDFs regressed to online URLs | Criteria reproduced manually + Semantic check | "Airplane-mode open of FY syllabus passes" |
| SQLite drafts vs flat JSON | Evolve lineage iterates schema safely, stops at convergence | `ooo evolve "move drafts to SQLite, keep confirm/cancel overlays"` |
| Long multi-day plan, sessions drop | EventStore resume | `ooo resume-session`, `auto --resume`, `status executions` |
| Team handoff / issues | `publish` | Seed → GitHub Epic/Tasks |
| Stuck loops (e.g. Lottie/Android asset quirks) | `unstuck` 5 personas + stagnation detection | `ooo unstuck` instead of 3rd identical retry |

## 7. How it fits our workflow (SOP)

Standard feature flow (catalog unify example):

```bash
# 1. register repo context once
ooo brownfield
# 2. spec
ooo interview "Unify 21-course catalog: single Course shape in Avalonia+Uno Core, keep card UI, FY/SY/TY collapse rules"
ooo seed <session_id>   # verify goal/constraints/criteria/ontology, ambiguity ≤0.2
# 3. build
ooo run
ooo status <session_id>
# 4. verify (manual reproduce: 21 cards both heads, dialog collapse, syllabus links)
ooo evaluate <session_id>
# 5. iterate or background it
ooo evolve "fix <failing criterion>" | ooo ralph --lineage-id <lin_…>
# 6. ship to team
ooo publish ~/.ouroboros/seeds/<seed>.yaml
```

Product-level flow: `ooo pm` → PRD → `ooo interview "<PRD excerpt>"` → Seed → run. Keep Java (`src/AndroidJava/`) as reference spec until Phase-5 + offline-PDF replacements land — mark it non-goal in the Ledger.

Suggested first pilot (small, measurable): **Avalonia test coverage for catalog** — `ooo auto "Avalonia Core catalog tests mirroring Uno CatalogTests" --skip-run`, review Seed, then `ooo run`. Proves Mechanical override + Semantic gate before touching backend/PDF work.

## 8. Troubleshooting quick table

| Symptom | Check → command → expect |
|---|---|
| `ooo` unknown | Inside OpenCode session? → `ouroboros setup --runtime opencode` → `ooo help` lists commands |
| MCP fail | `which opencode`, provider auth (`opencode providers auth …`), `ouroboros mcp doctor/info`, `status health` → ok |
| Run stuck | `ooo status <session_id>` → phase; `ooo cancel` → re-run |
| Interrupted `auto` | Output keeps `auto_session_id` → `ooo auto --resume <id>` |
| Closed window | Events persist → `ooo resume-session` → re-attach |
| Same failure ×N | `ooo unstuck` → pick persona approach |
| `-32000` plugin start | `ooo update` → ≥0.51.1, reopen session |
| Uninstall | `ouroboros uninstall --dry-run` → `uninstall [--keep-data]`; plugin entry separate |

Full channels: [Getting Started](https://github.com/Q00/ouroboros/blob/main/docs/getting-started.md) · [CLI Reference](https://github.com/Q00/ouroboros/blob/main/docs/cli-reference.md) · [Issues](https://github.com/Q00/ouroboros/issues)

---

# Part B — anti-slop (dmmulroy/anti-slop)

> Links: Repo <https://github.com/dmmulroy/anti-slop> · Skills registry <https://skills.sh/dmmulroy/anti-slop>
> One-liner: opinionated **Oxlint JS-plugin ruleset** that rejects low-evidence / low-signal TS+JS patterns. MIT. **Vendored, not an npm dep** — copy `src/` into your repo, own it, adapt to team taste.

## B.1 What it is / is not

| Aspect | Fact |
|---|---|
| Engine | Oxlint JS plugins (`@oxlint/plugins` + `oxlint`, pinned exact, currently 1.78.0) + ESTree/scope APIs, **no type-checker** (same-file aliases only, no cross-file inference) |
| Scope | 19 generic rules + 5 opt-in Effect rules; native `oxc/no-accumulating-spread` enabled alongside |
| Distribution | No official npm package; agent skill `install-anti-slop` copies + merges + validates; forks welcome |
| Philosophy | Author's taste, not universal standard — read every rule, keep/change per team |

## B.2 All nodes / sections map

Root nodes: Install-via-skill · Manual install · Generic rules (19) · Effect rules (5) · Analysis boundaries · Violation examples · Development (`pnpm check`, `sync:skill-assets`) · `AGENTS.md` (contrib rules).

| # | Node | Key content |
|---|---|---|
| 1 | Install via skill | `npx skills add dmmulroy/anti-slop --skill install-anti-slop` → ask agent to install/configure; matches existing Oxlint version; enables all generic rules (+ Effect group only if repo uses Effect); `--list` to inspect; update = 3-way merge preserving local customizations + provenance |
| 2 | Manual install | Copy `src/` → e.g. `tools/oxlint/anti-slop/`; install exact `@oxlint/plugins` + `oxlint`; register in `oxlint.config.ts` (`jsPlugins` + `rules` + `ignorePatterns` for `.agent/.agents/.claude/.codex/.cursor/.gemini/.opencode/… + vendored dir); Vite+ users merge ignores into `fmt.ignorePatterns` |
| 3 | Generic rules | Table B.3 below |
| 4 | Effect rules | Separate entry point `effect/index.ts`, only for Effect repos (table B.4) |
| 5 | Boundaries | Local-AST only; call/type checks documented as intentionally local; no autofix where semantics unclear (omission ≠ `undefined`, accumulator ownership unprovable, callback order/sparseness must be reviewed) |

## B.3 Generic rules table

| Rule | Rejects | Prefer instead |
|---|---|---|
| `no-array-filter-map` | Eager `filter().map()` / `map().filter()` (intermediate arrays) | Lazy iterator pipeline (`.values().filter().map().toArray()`), single `flatMap`, or push-reducer |
| `no-reduce-accumulator-copy` | `Object.assign({},acc)`, `concat/slice/toSpliced/toSorted/toReversed/with/Array.from(acc)` in reducers | Mutate fresh local `acc` and return it (`acc.push`, `Object.assign(acc,…)`) |
| `no-chained-type-assertions` | `x as A as B` fabricating evidence | Parse at boundary; `as const` chains still ok |
| `no-conditional-empty-object-spread` | `...(c ? {f} : {})` | Explicit branching (no autofix — omission ≠ `undefined`) |
| `no-known-value-widening` | Known value → `unknown/object/bare-{}/open-dict`, incl. calling local `unknown` predicate on already-known arg | Keep inference / `satisfies`; call predicates at unparsed boundary |
| `no-module-mocking` | `vi.mock/doMock`, Jest `mock`, `unstable_mockModule` | Real dependency seams |
| `no-object-parameters` | `object` (or unions/aliases → it) on fn inputs | Narrow interface/record |
| `no-reflect-apply` / `no-reflect-get` | `Reflect.apply/get` | Typed calls / property access / boundary parse |
| `no-runtime-typeof` | Ad-hoc `typeof` narrowing (`typeof x === "string"`) | Boundary schema parse; `typeof y === "undefined"` existence probes always ok; opt `allowInTypeGuards: true` |
| `no-shape-in-symbol-names` | `shape` substring in owned names (`UserShape`) | Neutral name; `schema.shape` member reads ok |
| `no-unknown-parameters` | `unknown` on fn inputs | Narrow type; only `cause` convention + narrowed subject of a type predicate exempt |
| `no-unknown-returns` | `unknown / Promise<unknown>` contracts (incl. aliases) | Explicit return type |
| `no-unknown-type-aliases` | `type X = unknown` (incl. transparent generics) | Concrete alias |
| `no-unsafe-dictionary-type` | `Record<string, unknown/any/object/{}>` values | Narrow value type; `T extends Record<string, unknown>` constraint ok |
| `no-widen-then-assert` | Known → wide → `as Narrow` round-trip | Don't widen; keep evidence |
| `require-readable-spacing` | Cramped top-level/control-flow/return/block spacing | Autofix via `oxlint --fix` → formatter → lint again (whitespace only) |
| `require-safety-comment-for-type-assertion` | Bare `as T` | `// SAFETY: <invariant>` above (markers configurable, default `SAFETY`, colon + non-empty required) |

## B.4 Effect rules table (opt-in, Effect repos only)

| Rule | Rejects | Prefer |
|---|---|---|
| `no-manual-effect-error-tag` | `_tag` compare/switch inside broad `catch/catchAll/catchIf` | `Effect.catchTag / catchReason / catchReasons` |
| `no-manual-tag-comparison` | Direct `_tag` compare/switch | `Match`, `Predicate.isTagged`, tagged-enum match |
| `no-manual-tagged-construction` | `{_tag: "Ready", …}` literals | Schema / tagged class/error / `Data.taggedEnum` (e.g. `Ready.make`) |
| `no-service-constructor-imports` | `import {makeX} from "./…"` outside `*.test.*`/`*.spec.*` | Import Layer, yield service; pkg/alias/default/static ctors exempt |
| `prefer-effect-match` | Chained ternaries over same value | `Match.value().pipe(Match.when…, Match.orElse…)` |

## B.5 Steps — install / use / update

```bash
# Via skill (recommended)
npx skills add dmmulroy/anti-slop --skill install-anti-slop
# then: "install anti-slop in this repo" / "update anti-slop, preserve local customizations"

# Manual
# 1. copy src/ → tools/oxlint/anti-slop/
# 2. pin deps: pnpm add -D -E oxlint@1.78.0 @oxlint/plugins@1.78.0  (match existing oxlint if present)
# 3. register entry point + rules + ignorePatterns (see README snippet)
# 4. run: oxlint --fix && <formatter> && oxlint
# 5. dev loop (upstream repo): pnpm install && pnpm check  (lint + RuleTester + tsc + skill-asset drift)
```

## B.6 Benefits, usage, fit in our workflow

- **Benefit:** kills AI-slop TS patterns at lint time (fake `as` chains, `unknown` leakage, mock-heavy tests, quadratic reducers) — complements Ouroboros Semantic evaluation with a $0 mechanical gate for JS/TS surfaces.
- **Usage here:** our shipping code is .NET/Java, so value is **scoped**: apply to JS/TS glue only — `hooks/`, `tools/` web assets, docs-site snippets, OpenCode bridge/plugin TS, any Vite/Node sidecar. Do **not** force onto C#/Java.
- **Fit (SOP):** (1) vendor under `tools/oxlint/anti-slop/`; (2) enable generic rules as errors in CI; (3) reference the Seed's acceptance criteria in `SAFETY:` comments where assertions are unavoidable; (4) on Ouroboros `evaluate` stage-1, run `oxlint` for the TS subset alongside `dotnet build/test` for .NET.
- **When not to use:** pure .NET/Java tasks with zero TS — procedure bigger than work (same YAGNI rule as Ouroboros ch.2.4).

---

# Part C — ui-skills (ibelick/ui-skills)

> Links: Repo <https://github.com/ibelick/ui-skills> · Site <https://www.ui-skills.com/> · Skills <https://www.ui-skills.com/skills> · Playbook <https://www.ui-skills.com/playbook> · MCP `https://www.ui-skills.com/mcp` (tools: `list_skills`, `get_skill`) · Registry `https://www.ui-skills.com/skills/registry.txt`
> One-liner: **303-skill registry + CLI + MCP + Playbook for Design Engineers**. MIT. Skills are discrete agent playbooks (accessibility, motion, systems, visual, interaction, performance, craft, taste), not a component library.

## C.1 Components / entry points

| Entry | What | Use |
|---|---|---|
| CLI | `npx ui-skills start / categories / list --category motion / get baseline-ui` | Browse + fetch from terminal |
| MCP | URL `https://www.ui-skills.com/mcp`, tools `list_skills` + `get_skill` | Agent auto-discovery inside OpenCode/Claude/Cursor/Copilot |
| Playbook | ~45 distilled lessons (aspect-ratio, text-balance, tabular-nums, 44px targets, concentric radius, press-scale 0.96, ease-out entrances, single accent, visible focus, sentence-case, …) | Fast review checklist, no install |
| Collections | Mobile App Design, Website Layout, Visual Hierarchy, Landing/Form/Button Design, … | Curated bundles per task |
| Agents pages | Claude Code, Cursor, OpenClaw, Codex, Copilot, … | Per-agent wiring |
| `DESIGN.md` | Repo's own design language (parchment neutrals, JetBrains Mono, narrow column, subtle elevation, restrained radius) + Do/Don't (focus states, reduced motion, one-action empty states, no glow/gradients) | Example output quality bar |

## C.2 Key skills table (most relevant subset; full list = 303)

| Skill (id) | Role |
|---|---|
| `ibelick/baseline-ui` | Fast deslop pass: spacing/hierarchy/typography/layout |
| `ibelick/improve-ui` | Read-only audit → self-contained implementation plans (no source edits) |
| `ibelick/create-design-md` | Generate `DESIGN.md` tokens/guidance from repo or site, evidence-based |
| `ibelick/fixing-accessibility` | ARIA/keyboard/focus/contrast/form-error fixes (WCAG) |
| `ibelick/fixing-metadata` | Titles/meta/OG/Twitter/canonical/JSON-LD fixes |
| `ibelick/fixing-motion-performance` | Layout-thrash/compositor/scroll/blur triage |
| `ibelick/ui-skills-root` | Router: pick smallest useful skill set by topic/stack/intent |
| `emilkowalski/*` (`animate`, `improve-animations`, `review-animations`, `emil-design-eng`, `prototype`, …) | Motion decisions in correct order; audit vs single-diff review; multi-variant picker |
| `jakubkrehel/*` (`better-ui`, `better-accessibility`, `better-typography`, `better-colors` OKLCH, `variant`, `break`, `interface-review`, …) | Polish passes, type/color systems, variant picker, state-coverage report |
| `anthropics/frontend-design` | Distinctive production UI without generic AI look |
| `figma/*` (`design-to-code`, `generate-library`, `implement-motion`, `swiftui`, …) | Figma↔code, tokens, motion handoff (mandatory prerequisites before matching MCP tools) |
| `accesslint/*`, `addyosmani/*` | WCAG scans/diffs, Core Web Vitals, perf/SEO audits |

## C.3 Steps — use

```bash
# CLI: browse + fetch
npx ui-skills start
npx ui-skills categories
npx ui-skills list --category motion
npx ui-skills get baseline-ui        # fetch one skill pack

# MCP (OpenCode): register https://www.ui-skills.com/mcp, then agent calls list_skills/get_skill
# Playbook: open https://www.ui-skills.com/playbook as review checklist, no install
```

Typical agent flow: `ui-skills-root` routes → fetch 1–2 skills (`baseline-ui` for cleanup, `improve-ui` for plan, `fixing-accessibility` for a11y) → apply → verify against Playbook items.

## C.4 Benefits, usage, fit in our workflow

- **Benefits:** (1) instant design-QA vocabulary (Playbook) without onboarding a design system; (2) `improve-ui` produces agent-ready plans another agent can execute — pairs with Ouroboros Seed/Evaluate; (3) `create-design-md` can freeze our visual tokens (cards 170×190, 2-col grid, dialog collapse rules, Lottie headers) into a `DESIGN.md` both .NET heads share.
- **Usage here:** our UI is Avalonia/Uno XAML, not web — apply **principles, not snippets**. Directly transferable: 44px touch targets, visible focus, single accent, sentence-case labels, field-adjacent errors, one-action empty states, concentric radius, press feedback, reduced-motion, contrast. Web-only tokens (Tailwind/CSS) stay as reference.
- **Fit (SOP):** (1) run `create-design-md` once → commit `docs/DESIGN.md` for the two heads; (2) before any Courses/Registration UI change, put Playbook checklist + relevant Seed acceptance criteria in the brief; (3) after `ooo run`, run `improve-ui` audit → feed findings into `ooo evolve`; (4) cite skill IDs in Seeds (`evaluation_principles`) so Evaluate checks them.
- **When not to use:** backend-only work (SQLite, registration protocol, CI/signing) — no UI surface, skip.

---

# Cross-tool fit (all three together)

| Layer | Tool | Job in our loop |
|---|---|---|
| Spec + verify | Ouroboros | Interview → Seed → run → 3-stage Evaluate → Evolve/Ralph; brownfield/pm/publish for team |
| TS correctness | anti-slop | $0 mechanical lint for JS/TS glue; referenced from Seed criteria + Evaluate stage-1 |
| UI quality | ui-skills | Design tokens (`DESIGN.md`), Playbook checklist in briefs, `improve-ui` audit feeding `evolve` |

Order per feature: Ouroboros `interview/seed` (cite Playbook + lint rules in criteria) → `run` → `oxlint` + `improve-ui` audit → `ooo evaluate` → `ooo evolve` on findings → `publish`.
