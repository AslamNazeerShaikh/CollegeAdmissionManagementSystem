# College Admissions CRM — Tauri 2 (React + Vite)

Vertical-first desktop UI. No horizontal scroll at any window size: content
wraps/stacks, and below 1100px the 3-pane shell collapses to a single
scrolling column with the detail rail stacked underneath.

## Window (macOS landscape, resizable)

`src-tauri/tauri.conf.json` → 1280×800 (16:10), `minWidth` 960,
`minHeight` 600, `resizable: true`. Bundle targets include `dmg` + `app`.

## Vertical layout rules

- Global `overflow-x: clip` + `min-width: 0` + `overflow-wrap: anywhere`
  (`src/index.css`) — clipping replaces scrolling everywhere.
- Dashboard KPIs / funnel / Courses use
  `grid-cols-[repeat(auto-fill,minmax(...))]` so cards reflow vertically.
- Applications register is one card per applicant (header + 3-cell meta
  grid), not a fixed 6-column row.
- Course fill bars stack label → bar (no fixed-width side columns).

## Dev / build / test

```bash
npm install --cache /tmp/npm-cache-college   # ~/.npm/_cacache perms broken on this box
npm run dev        # Vite dev server (port 1420)
npm run build      # tsc + vite build → dist/
npm test           # node --test src/crm/data.test.ts (5 tests, stdlib only)
npm run tauri dev  # needs Rust toolchain (see below)
npm run tauri build -- --bundles dmg,app
```

## Runtime footprint (measured 2026-09-19, macOS release build)

- Main process RSS: **~115 MB**, 0% CPU idle (`ps -o rss`).
- No separate WebKit child process attributed; WKWebView rendering lives
  inside that footprint plus shared system WebKit services.
- Native bundle: `src-tauri/target/release/bundle/macos/College Admissions CRM.app`
  (built with tauri-cli 2.11.4 + rustc 1.98.1, zero build errors).

## Required toolchain (macOS)

- Rust stable ≥ 1.77 — installed: rustc/cargo **1.98.1** at `~/.cargo/bin/`.
  Fresh install: `curl --proto '=https' --tlsv1.2 -sSf https://sh.rustup.rs | sh`.
- Xcode CLT (present at `/Applications/Xcode.app`): `xcode-select --install` only if prompted.
- Node 22+ (have 26.7.0), Tauri CLI 2.x ships via devDependencies (`@tauri-apps/cli`).

## Recommended IDE Setup

- [VS Code](https://code.visualstudio.com/) + [Tauri](https://marketplace.visualstudio.com/items?itemName=tauri-apps.tauri-vscode) + [rust-analyzer](https://marketplace.visualstudio.com/items?itemName=rust-lang.rust-analyzer)
