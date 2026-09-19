# Conventional Commits (v1.0.0) — adopted

Full spec: https://www.conventionalcommits.org/en/v1.0.0/#specification

## Format

```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

- `fix:` → PATCH, `feat:` → MINOR, `!` or `BREAKING CHANGE:` footer → MAJOR.
- Description is imperative, lowercase, no trailing period: `fix: array parsing issue`, not `Fixed.`
- Body (blank line after description) explains *why*; footers use git-trailer style (`Refs: #123`, `Acked-by:`).

## Types we use

`feat` `fix` `docs` `chore` `refactor` `perf` `test` `build` `ci` `style` `revert`
(per `@commitlint/config-conventional`; extra types carry no SemVer meaning).

## Scopes (this repo)

Scope = codebase or area, e.g. `tauri`, `flutter`, `avalonia`, `uno`, `graphify`, `docs`, `deps`.

## Examples

```
feat(tauri): collapse shell to single column below 1100px
fix(flutter): prevent racing of requests
docs: adopt Conventional Commits v1.0.0
chore: drop tracked graphify cache files (now gitignored)
feat(api)!: send an email to the customer when a product is shipped

BREAKING CHANGE: `extends` key is now used for extending other config files
```

## Notes

- One type per commit — split mixed changes instead.
- Wrong type before merge: `git rebase -i`; after release, leave it (tooling just skips it).
- Squash merges: maintainer writes the conventional message at squash time.
