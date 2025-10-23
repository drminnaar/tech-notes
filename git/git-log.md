# Git Log

This document provides a guide to using the git log command, including various formatting options and instructions for creating custom aliases to better visualize commit histories, particularly for conventional commits.

---

## Summary

Command | Description
--- | ---
git log | display commit history
git log -10 | display last 10 commits
git log -p -1 | display differences in last commit
git log @{u}.. | display a list of pending commits (commits that have not been pushed)
git log --pretty=oneline/short/full/fuller | display commit history using specified format
git log --pretty=oneline -2 | display last 2 commits using one line per commit
git log --pretty=oneline --author='' | display commit history filtering by author
git log --pretty=oneline --since='5 minutes ago' | display commit history for last 5 minutes
git log --pretty=oneline --until='5 minutes ago' | display commit history until 5 minutes ago
git log --all --pretty=format:'%h %ad \| %s%d [%an]' --graph --date=short | custom log format
git log --oneline --decorate --graph | another custom log format

**Formats:**

* --pretty="..." defines the format of the output.
* %h is the abbreviated hash of the commit
* %d are any decorations on that commit (e.g. branch heads or tags)
* %ad is the author date
* %s is the comment
* %an is the author name
* --graph informs git to display the commit tree in an ASCII graph layout
* --date=short keeps the date format nice and short

---

## Log Alias Configuration

### Log Summary

Create alias to display log summary of commit history:

```bash
git config --global alias.log-summary "log --all --pretty=format:'%h %ad - %s%d [%an]' --graph --date=short"
```

### Log Graph

Create alias to display commit history as a graph:

```bash
git config --global alias.log-graph "log --oneline --decorate --graph"
```

### Log Conventional Commit Type

Create alias to display log summary of commit history where commit messages start with conventional commit type (feat, docs, fix, refactor, etc):

According to official documentation:

> The Conventional Commits specification is a lightweight convention on top of commit messages. It provides an easy set of rules for creating an explicit commit history; which makes it easier to write automated tools on top of. This convention dovetails with SemVer, by describing the features, fixes, and breaking changes made in commit messages. <br /><br />"Conventional Commits -  A specification for adding human and machine readable meaning to commit messages", [https://www.conventionalcommits.org/en/v1.0.0](https://www.conventionalcommits.org/en/v1.0.0).


For this repository, _Conventional Commits 1.0.0_ is used as the convention for commit messages. This convention is primarily used to provide more informative git commit messages.

To learn more about _Conventional Commits_ see the following resources:

- [Conventional Commits 1.0.0 Official Documentation](https://www.conventionalcommits.org/en/v1.0.0/): The official _Conventional Commits_ website and documentation.
- [Accompanying Guide on Conventional Commits](./git-commit-convention.md): This is a guide included as part of this repository's Git documentation that provides information of how Conventional Commits are used in this repository.

Below shows how to configure git logging to list git logs adhering to the _Convention Commit_ standard:

```bash

# Create shell function as git alias that accepts parameters

git config --global alias.log-type '!f() { git log --grep="^$1" --all --pretty=format:"%C(yellow)%h%Creset %C(green)%ad%Creset - %s%d %C(bold cyan)[%an]%Creset" --graph --date=short --color=always; }; f'

```

Using git alias from above enables one to use `git log-type` command as follows:

```bash
git log-type
git log-type feat
git log-type docs
git log-type "refactor(api"
```

---