# Git Commit Convention

This document outlines the Conventional Commits 1.0.0 specification for git commit messages, explaining the standard format `<type>[(optional scope)]: <description>` along with comprehensive descriptions of common and additional commit types used to categorize code changes.

- [1. Summary](#1-summary)
- [2. Common Types](#2-common-types)
- [3. Other Types](#3-other-types)

---

## 1. Summary

[Conventional Commits 1.0.0](https://www.conventionalcommits.org/en/v1.0.0/) is used as the convention for commit messages. This convention is primarily used to provide more informative git commit messages.

Conventional Commits provides a specification for adding human-readable and machine-readable meaning to commit messages. There are several common types used to categorize the purpose of a commit. These types help structure commit messages in a consistent way, typically following the following structure:

```text
<type>[(optional scope)]: <description>

[optional body]

[optional footer(s)]
```

The next section provides a list of the most widely recognized types.

---

## 2. Common Types

type | description | example
---- | ----------- | -------
feat | A new feature is introduced to codebase | feat(login): Add user authentication
fix | A bug fix is applied to the codebase | fix(api): Resolve null pointer exception in endpoint
docs | Changes to documentation, such as README updates or code comments | docs(readme): update installation instructions
style |Changes that do not affect the meaning of the code, like formatting or linting | style(ui): Adjust padding in button component
refactor | Code changes that neither fix a bug nor add a feature, but improve structure or readability | refactor(auth): Simplify token validation logic
test | Adding or modifying tests, without changing production code | test(api): Add unit tests for user endpoint
chore | Routine tasks or maintenance, like updating dependencies or build scripts | chore(deps): Update npm packages to latest versions
perf | Performance improvements to the codebase | perf(db): Optimize query execution time
ci | Changes to continuous integration or deployment configurations | ci(pipeline): Add linting step to GitHub Actions
build |Changes affecting the build system or external dependencies| build(webpack): Update to version 5
revert | Reverting a previous commit | revert: Undo feat(login) due to breaking changes

<small>[prev](#1-summary) | [contents]</small>

---

## 3. Other Types

While the _Conventional Commits_ specification doesn't enforce a strict list of types beyond the most commonly used ones (like feat, fix, etc.), it’s designed to be extensible. Beyond the widely recognized types already mentioned (feat, fix, docs, style, refactor, test, chore, perf, ci, build, and revert), additional types may emerge based on community practices, specific project needs, or tool integrations. Here are some less common but still notable types that have been adopted in various ecosystems:

type | description | example
---- | ----------- | -------
security | Used for commits addressing security vulnerabilities or improvements | security(auth): Patch XSS vulnerability in input handling
deps | Specifically for adding, removing, or modifying dependencies (sometimes falls under chore, but can be split out for clarity) | deps: Add lodash 4.17.21
config | Changes to configuration files (e.g., environment variables, settings) | config(server): Update port in .env
init | For initial commits or setting up a project | init: scaffold project structure
wip | Work-in-progress commits, often temporary, to mark incomplete changes | wip(ui): Partial implementation of modal component
hotfix | Quick fixes deployed to production, often in emergency situations (sometimes overlaps with fix) | hotfix(api): Resolve downtime caused by rate limiting
release | Preparing or tagging a release version | release: Bump version to 1.2.3
design | Changes related to UI/UX design updates, distinct from style which focuses on code formatting | design(header): Redesign navigation bar
i18n | Updates related to internationalization or localization | i18n: Add French translations
breaking | Indicates a breaking change (though often paired with another type like feat! or fix! using the ! notation) | breaking(api): Remove deprecated endpoint
experiment | Experimental changes or proofs of concept, not necessarily intended for production | experiment(cache): Test Redis implementation

These additional types aren’t part of a universal standard but are practical extensions seen in real-world projects, especially in larger teams or specialized workflows (e.g., monorepos, DevOps-heavy setups). The Conventional Commits spec encourages customization, so a team might define its own types—like db for database changes or ux for user experience tweaks—as long as they’re documented and consistently applied.

The ! notation (e.g., feat!: add new API) is also worth noting—it’s not a type itself but a modifier to signal breaking changes, and it can be combined with any type. Similarly, scopes (in parentheses, like feat(ui)) can refine meaning further but aren’t types.

<small>[prev](#2-common-types) | [contents]</small>

---

[contents]: #contents