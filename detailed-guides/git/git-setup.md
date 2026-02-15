# Git Setup Guide for Linux

This document provides a comprehensive guide for setting up and using Git on Linux systems.

It includes the following sections:

- [Installing Git](#step-1-install-git): Instructions for installing Git on various Linux distributions, including Debian/Ubuntu and Fedora/CentOS/RHEL, with commands for testing the installation.

- [Configuring Git](#step2-configure-git): Details on configuring Git at system, global, and local levels, with recommended global settings such as user identity, default editor, branch naming, credential storage, and aliases for common commands.

- [Recommended Practices](#step-3-recommended-practices): Best practices for writing commit messages

The guide emphasizes the importance of clarity, consistency, and professionalism in Git usage, providing examples and configurations to streamline workflows.

- [Step 1: Install Git](#step-1-install-git)
- [Step 2: Configure Git](#step-2-configure-git)
- [Step 3: Recommended Practices](#step-3-recommended-practices)
  - [3.1 Use a Clear and Descriptive Summary Line](#31-use-a-clear-and-descriptive-summary-line)
  - [3.2 Follow a Consistent Structure](#32-follow-a-consistent-structure)
  - [3.3 Explain the Context and Reasoning](#33-explain-the-context-and-reasoning)
  - [3.4 Reference Issues or Tickets](#34-reference-issues-or-tickets)
  - [3.5 Use Conventional Commits for Consistent Format](#35-use-conventional-commits-for-consistent-format)
  - [3.6 Avoid Generic Messages](#36-avoid-generic-messages)
  - [3.7 Break Commits into Logical Units](#37-break-commits-into-logical-units)
  - [3.8 Use Proper Grammar and Spelling](#38-use-proper-grammar-and-spelling)
  - [3.9 Leverage Git Tools](#39-leverage-git-tools)
  - [3.10 Examples of a good commit message](#310-examples-of-a-good-commit-message)

---

## Step 1: Install Git

For information about how to install and/or download Git for your OS, please see the following resources:

- [General Install](https://git-scm.com/downloads)
- [Linux Install](https://git-scm.com/downloads/linux)

For Debian/Ubuntu-based distributions (apt):

```bash
# install
sudo apt update
sudo apt install git

# test
git --version
```

For Fedora/CentOS/RHEL-based distributions (dnf or yum):

If you're on a modern Fedora system or CentOS/RHEL 8+, use dnf:

```bash
# install
yum install git # (up to Fedora 21)
dnf install git # (Fedora 22 and later)

# test
git --version
```

<small>[Contents] | [Step 1: Install Git] | [Step 2: Configure Git] | [Step 3: Recommended Practices]</small>

---

## Step 2: Configure Git

This section details the recommended Git configuration for new/existing git repositories. Also see the accompanying Git guides that can be found here:

- [Git Configuration](./git-configuration.md)
- [Git Commands](./git-commands.md)
- [Git Log](./git-log.md)

Git configuration is stored in three levels:

- System (/etc/gitconfig): Applies to all users.

  ```bash  
  cat ~/etc/gitconfig

  # alternatively
  git config --system --edit
  ```

- Global (~/.gitconfig): Applies to the current user.

  ```bash
  cat ~/.gitconfig

  # alternatively
  git config --global --edit
  ```

- Local (.git/config in a repository): Applies to a specific repository.

  ```bash
  cat ./.git/config

  # alternatively
  git config --edit
  ```

For most users, the global configuration is the primary focus. We will therefore use the global configuration (`~/.gitconfig`) to configure most git settings.

Below is a list of recommended Global configuration settings. Run the following commands to configure Git global configuration:

```bash
# Set your name and email:
# - These identify you in commit messages and are required for version control.

    git config --global user.name "Your Full Name"

    git config --global user.email "your.email@example.com"

# -----------------------------------------------------------------------------

# Set the default text editor:
# - Git uses a text editor for commit messages. Set it to your preferred editor (e.g., vim, or vscode).

    # for vim users
    git config --global core.editor "vim"

    # for Visual Studio Code users
    git config --global core.editor "code --wait"

# -----------------------------------------------------------------------------

# Enable color output:
# - colored output makes Git commands easier to read.

    git config --global color.ui auto

# -----------------------------------------------------------------------------

# Set the default branch name:
# - Modern Git recommends using main

    git config --global init.defaultBranch main

# -----------------------------------------------------------------------------

# Configure line endings:
# - On Linux, Git should use Unix-style line endings (LF) by default.

    git config --global core.autocrlf false
    git config --global core.eol lf

# -----------------------------------------------------------------------------

# Enable credential storage:
#   - To avoid entering credentials repeatedly for remote repositories, use a
#   credential helper.
#   - Note: The store option saves credentials in plain text in
#     ~/.git-credentials, which may not be secure on shared systems.

    git config --global credential.helper store

# -----------------------------------------------------------------------------

# Set up a default push behavior:
#   - Configure Git to push only the current branch by default to avoid
#     accidental pushes:

    git config --global push.default simple

# -----------------------------------------------------------------------------

# Configure paging
#   - Controls the pager for long outputs (e.g., git log).
#     Recommendation: `less -FX` exits immediately for short output, improving UX.

    git config --global core.pager "less -FX"

# -----------------------------------------------------------------------------

# Configure date:
#   - Formats dates in git log (e.g., ISO 8601: "2025-04-03 14:30:00").
#     Use `iso` for clarity and standardization.

    git config --global log.date iso

# -----------------------------------------------------------------------------

# Configure aliases for common commands:
#   - Aliases simplify repetitive Git commands. Add these to your global
#     configuration

    git config --global alias.st status
    git config --global alias.co checkout
    git config --global alias.br branch
    git config --global alias.cm "commit -m"
    git config --global alias.lg "log --oneline --graph --all"
```

<small>[Previous](#step-1-install-git) | [Contents] | [Step 1: Install Git] | [Step 2: Configure Git] | [Step 3: Recommended Practices]</small>

---

## Step 3: Recommended Practices

### 3.1 Use a Clear and Descriptive Summary Line

- Keep it concise: Limit the first line to 50-72 characters. It should summarize the change clearly.  
- Be specific: Avoid vague terms like "fix" or "update." Instead, describe what was changed and why.
  - Bad: "Fixed stuff"
  - Good: "Fix null pointer exception in user authentication module"

### 3.2 Follow a Consistent Structure

- Use the imperative mood in the summary line, as if giving a command (e.g., "Add", "Fix", "Refactor").

  The imperative mood is a grammatical mood used to express commands, instructions, or requests. It’s typically directed at someone, implying that they should take action. In the context of Git commit messages, using the imperative mood means writing the summary line as if you’re giving a command to the codebase.

  Characteristics of the Imperative Mood:
  
  - Direct and action-oriented: It describes what the commit does or should do.
  - Present tense: It uses the present tense, not past or future.
  - No subject pronoun: The subject (e.g., "you") is implied, not explicitly stated.

  Examples in Git Commit Messages:
  
  - Imperative: "Add user authentication module"
  - Not imperative: "Added user authentication module" (past tense) or "Adding user authentication module" (gerund).

  Why Use It for Git Commits?
  
  - Consistency: Matches the style of Git’s own commands (e.g., git commit, git push).
  - Clarity: Reads like an instruction, making it clear what the commit accomplishes.
  - Convention: Aligns with widely adopted standards like Conventional Commits and open-source project guidelines.

  <br />

  ```text
  NOTE: Another way of thinking about using the imperative mood is as follows:
   
  "Applying this git commit will .... ":
      - "Add customer profile service"
      - "Fix product page performance bug"
      - "Refactor user authentication module"
  ```

- Structure the message with:
  - Summary line: A brief description of the change (50-72 characters).
  - Blank line: Separate the summary from the body.
  - Body (optional): Provide more context, explaining why the change was made, what it affects, and any relevant details. Can use bullet list of changes made.
  - Example:
    
    ```bash
    Add user login validation to prevent SQL injection

    - Implemented input sanitization in login form to address potential SQL injection vulnerabilities.
    - Updated tests to cover new validation logic.
    
    Resolves issue #123
    ```

### 3.3 Explain the Context and Reasoning

- Answer why the change was made. This helps future developers (or yourself) understand the intent.
- Mention any trade-offs, side effects, or dependencies.
- Example: 
  
  ```
  Refactor database queries to improve performance by 20%, but may increase memory usage slightly.
  ```

### 3.4 Reference Issues or Tickets

- Include references to issue trackers (e.g., `Fixes #123`) to link commits to specific tasks or bugs.
- Example:

  ```bash
  Add pagination to user list page
  
  Resolves #456
  ```

### 3.5 Use Conventional Commits for Consistent Format

Adopt a team-wide convention using _[Conventional Commits]_ messages. [Conventional Commits 1.0.0] is used as the convention for commit messages. This convention is primarily used to provide more informative git commit messages. For example, prefix git commit messages with type (e.g., feat:, fix:, docs:, refactor:, test:) to categorize changes

_Conventional Commits_ provides a specification for adding human-readable and machine-readable meaning to commit messages. There are several common types used to categorize the purpose of a commit. These types help structure commit messages in a consistent way, typically following the format: `<type>(<scope>): <description>`. For a detailed description of _Conventional Commits_, and a list of useful resources, see the following:

- [Conventional Commits]: Official documentation
- [Conventional Commits Guide](./git-commit-convention.md): Accompanying guide on Conventional Commits for this repository
- [Conventional Commits Common Types](./git-commit-convention.md#2-common-types): A list of common types
- [Conventional Commits Other Types](./git-commit-convention.md#3-other-types): A list of other types
- [Git Log - Conventional Commits](./git-log.md#log-conventional-commit-type): Accompanying guide for configuring git log alias to display commit logs based on Conventional Commit type

**Examples:**

```bash
# Changes to documentation, such as README updates or code comments.
git commit -m "docs(docs/git): Add section on conventional commits"

# A new feature is introduced to the codebase.
git commit -m "feat(api/catalog): Add product catalog endpoint"

# A bug fix is applied to the codebase
git commit -m "fix(api/profile): Resolve issue relating to non-existent profile on login."
```

**Benefits:**

- Enables automated changelog generation.
- Supports semantic versioning (e.g., feat → minor, fix → patch, BREAKING CHANGE → major).
- Improves clarity and consistency in commit history.
- Facilitates collaboration and project maintenance.

**Reason:**

The primary reasons for using _Conventional Commits_ are as follows:

- Improves clarity and consistency in commit history.
- Facilitates collaboration and project maintenance.

**Configure:**

Configure Git aliases to view logs based on conventional commit type:

```bash

# Create shell function as git alias that accepts parameters
git config --global alias.log-type '!f() { git log --grep="^$1" --all --pretty=format:"%C(yellow)%h%Creset %C(green)%ad%Creset - %s%d %C(bold cyan)[%an]%Creset" --graph --date=short --color=always; }; f'

git config --global alias.lt "log-type"
```

After configuring git to use _Conventional Commits_, you can view git commits as follows:

```bash
# show features
git lt feat

# show features having api context
git lt "feat(api)"
```

### 3.6 Avoid Generic Messages

- Avoid messages like "WIP," "misc changes," or "done." They provide no useful information. If a commit is temporary, consider using `git commit --amend` later to improve the message.

### 3.7 Break Commits into Logical Units

- Each commit should represent a single logical change. If you’re fixing a bug and adding a feature, split them into separate commits with distinct messages.
- Example:

  ```bash
  # Commit 1:
  git commit -m "fix: Resolve crash in payment processing"
  ```

  ```bash
  # Commit 2:
  git commit -m "feat: Add support for Apple Pay"
  ```

### 3.8 Use Proper Grammar and Spelling

- Write in clear, professional language. Avoid abbreviations or slang unless they’re widely understood in your team and also documented in a data dictionary.
- Capitalize the first letter of the summary line and avoid ending with a period.

### 3.9 Leverage Git Tools

- Use `git commit -m` for simple messages or a text editor (e.g., `git commit`) for detailed messages with a body. Typing `git commit [Enter]` will open configured Git editor.

  ```bash
  # show current editor
  git config --global core.editor

  # configure editor
  git config --global core.editor "vim"
  ```

- If you make a mistake, use git commit --amend to edit the message or git rebase -i to fix older messages.

### 3.10 Examples of a good commit message

- Single-line Commit

  ```bash
  git commit -m "docs(azure): Add initial azure naming standards. Resolves #1234"
  ```

- Multi-line Commit

  ```text
  git commit # press enter to open editor

  # provide the following details for commit message
  feat(app): Implement user authentication using Azure AD B2C

  - implement signup form component that integrates with Azure AD B2C
  - add signup component test
  - see #4546 for information about B2C App Registration that is configured for application
  - see #4547 for information about B2C user flow configuration. A signup_signin userflow is used

  Resolves #4567659

  ```

- Multi-line Commit

  ```bash
  git commit # press enter to open editor

  # provide the following details for commit message
  feat(api): Add 'products' endpoint to fetch list of products from SQL database

  - implement GET 'products' endpoint
  - implement offset-based pagination (offset/limit)

  Resolves #82374
  ```

<small>[Previous](#step-2-configure-git) | [Contents] | [Step 1: Install Git] | [Step 2: Configure Git] | [Step 3: Recommended Practices]</small>

---

[Conventional Commits]: https://www.conventionalcommits.org/en/v1.0.0/
[Conventional Commits 1.0.0]: https://www.conventionalcommits.org/en/v1.0.0/
[contents]: #contents
[Step 1: Install Git]: #step-1-install-git
[Step 2: Configure Git]: #step-2-configure-git
[Step 3: Recommended Practices]: #step-3-recommended-practices