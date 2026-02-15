# Git Configuration

This document provides a comprehensive guide to configuring Git at three levels (local, global, and system), including commands for viewing, editing, and setting various Git configuration options like user details, editor preferences, and line ending behaviors.

Git configuration can be managed at 3 levels of specificity:

- Local - applies only to current git repository
- Global - applies to all git repositories for the current user
- System - applies to all git repositories for all users

---

## Local Configuration

Run the following commands via the terminal from the root of your git project (where the .git hidden folder resides) to view or edit a list of local settings.

### View and Edit Local Config

Command | Description
--- | ---
cat ./.git/config | View local config
vim ./.git/config | Edit local config
git config --edit | Use default configured editor to edit local config

### Get Local Config Settings

Command | Description
--- | ---
git config --list | List all git config settings
git config user.name | Get username config setting
git config user.email | Get email config setting
git config core.autocrlf | Get autocrlf (see note below) config setting
git config core.editor | Get editor config setting
git config merge.tool | Get merge tool config setting

### Set Local Config Settings

Command | Description
--- | ---
git config user.name "example" | Set username config setting
git config user.email "example@example.com" | Set email config setting
git config core.autocrlf input | Set autocrlf (see note below) config setting
git config core.editor vim | Set editor config setting
git config merge.tool meld | Set merge tool config setting

---

## Global Configuration

### View and Edit Global Config

Command | Description
--- | ---
cat ~/.gitconfig | View global config
vim ~/.gitconfig | Edit global config
git config --global --edit | Use default configured editor to edit global config

### Get Global Config Settings

Command | Description
--- | ---
git config --global --list | List all git config settings
git config --global user.name | Get username config setting
git config --global user.email | Get email config setting
git config --global core.autocrlf | Get autocrlf (see note below) config setting
git config --global core.editor | Get editor config setting
git config --global merge.tool | Get merge tool config setting
git config --global diff.tool meld | Gets the tool for viewing diffs
git config --global init.defaultBranch | Gets default branch name
git config --global color.ui | Check if coloured output is enabled
git config --global core.pager | Controls the pager for long outputs (e.g., git log)
git config --global log.date | Log date format (e.g., ISO 8601: "2025-04-03 14:30:00")
git config --global alias.st | Gets alias

### Set Global Config Settings

Command | Description
--- | ---
git config --global user.name "example" | Set username config setting
git config --global user.email "example@example.com" | Set email config setting
git config --global core.autocrlf input | Set autocrlf (see note below) config setting
git config --global core.editor vim | Set editor config setting
git config --global merge.tool meld | Set merge tool config setting
git config --global diff.tool meld | Sets the tool for viewing diffs
git config --global init.defaultBranch main | Sets default branch name when initializing a new repo
git config --global color.ui auto | Enables colored output in the terminal (e.g., for git status, git diff)
git config --global core.pager "less -FX" | Controls the pager for long outputs (e.g., git log). Recommendation: `less -FX` exits immediately for short output, improving UX.
git config --global log.date iso | Formats dates in git log (e.g., ISO 8601: "2025-04-03 14:30:00"). Use `iso` for clarity and standardization.
git config --global alias.st "status -sb" | short status with branch info

---

## System Configuration

Run the following commands via the terminal from any location

First check that system wide config exists.

```bash
ls /etc/gitconfig
```

### View and Edit System Config

Command | Description
--- | ---
cat /etc/gitconfig | View global config
vim /etc/gitconfig | Edit global config
git config --system --edit | Use default configured editor to edit system config

### Get System Config Settings

Command | Description
--- | ---
git config --system --list | List all git config settings
git config --system user.name | Get username config setting
git config --system user.email | Get email config setting
git config --system core.autocrlf | Get autocrlf (see note below) config setting
git config --system core.editor | Get editor config setting
git config --system merge.tool | Get merge tool config setting

### Set System Config Settings

Command | Description
--- | ---
git config --system user.name "bob" | Set username config setting
git config --system user.email "bob@bob.com" | Set email config setting
git config --system core.autocrlf input | Set autocrlf (see note below) config settinsystem
git config --system core.editor vim | Set editor config setting
git config --system merge.tool meld | Set merge tool config setting

---

## A note on autocrlf

One can instruct Git to convert CRLF to LF on commit but not the other way around by setting core.autocrlf to input. This setup means that one can use CRLF endings in Windows checkouts, but with LF endings on Mac and Linux systems. In order to do this, run any one of the following commands depending on required specificity.

Command | Specificity
--- | ---
git config core.autocrlf input | local
git config --global core.autocrlf input | global
git config --system core.autocrlf input | system

---
