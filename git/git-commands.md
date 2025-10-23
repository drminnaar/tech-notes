# Git Commands

This document is a comprehensive reference guide for Git commands, covering repository creation, local changes management, undoing changes, remote operations, merging and rebasing, branch management, commit handling, tag management, and Git aliases.

- [Create Repositories](#create-repositories)
- [Manage Local Changes](#manage-local-changes)
  - [git status](#git-status)
  - [git add](#git-add)
  - [git commit](#git-commit)
  - [git stash](#git-stash)
- [Undo Changes](#undo-changes)
  - [git clean](#git-clean)
  - [git restore](#git-restore)
  - [git revert](#git-revert)
  - [git reset](#git-reset)
- [Remote Operations](#remote-operations)
  - [git remote](#git-remote)
  - [git push](#git-push)
  - [git fetch](#git-fetch)
- [Merging \& Rebasing](#merging--rebasing)
  - [git merge](#git-merge)
  - [git rebase](#git-rebase)
- [Manage Branching](#manage-branching)
- [Manage Commits](#manage-commits)
- [Manage Tags](#manage-tags)
- [Aliases](#aliases)

---

## Create Repositories

Command | Description
--- | ---
git init | created new local repository in current directory
git init [project-name] | create new local repository with specified name
git clone [url] | downloads a project and its entire version history

## Manage Local Changes

### git status

Command | Description
--- | ---
git status | Lists all new or modified files to be committed
git status -u | Like *git status*, but also lists all untracked files even files inside directories)
git status --ignored | List all files and folders that are currently ignored (based on .gitignore file)

### git add

Command | Description
--- | ---
git add . | add all new and modified files to next commit
git add . -n | do dry run for add all new and modified files to next commit
git add [file-name] [file-name] | adds specified files to next commit

### git commit

Command | Description
--- | ---
git commit -m "descriptive message" | records file snapshots permanently n version history
git commit -m "descriptive message" --amend | add current changes to previous commit
git commit --amend --no-edit | add current changes to previous commit using message from previous commit

### git stash

Command | Description
--- | ---
git stash push -m "stash message" | Stash changes
git stash list | List stashes
git stash apply or git stash apply stash@{n} | Apply stash
git stash drop stash@{n} | Drop stash
git stash pop | Pop stash (apply and drop)

---

## Undo Changes

### git clean

Command | Description
--- | ---
git clean -f | discard untracked files

### git restore

Command | Description
--- | ---
git restore --staged <file> | discard staged changes
git restore <file> | discard working changes

### git revert

Using *'git revert'* is a safe way of undoing commits. Instead of removing a commit, a new commit is created with all the changes undone. Use *'git revert'* when you want to remove a specific commit from the history but you still wish to preserve any changes from that commit.

Command | Description
--- | ---
git revert [commit] | undo specified commit

### git reset

Using *'git reset'* is essentially a permanent undo meaning that there is no way to access original changes.

Command | Description
--- | ---
git reset HEAD -- [file] | Unstage all changes to previous commit but eave working directory unchanged. The '--' is used to separate paths from revisions
git reset [file] | Equivalent to above as HEAD (previous commit) is optional and is used by default
git reset --hard | Unstage all changes to match working directory of last ommit. All uncommited changes are lost
git reset [commit] | Unstage all changes to specified commit but preserve uncommited changes
git reset --hard [commit] | Unstage all changes to match working directory of specified commit. All uncommited changes are lost

---

## Remote Operations

### git remote

Command | Description
--- | ---
git remote -v | show list of remotes
git remote show [remote] | show information about remote
git remote add [short-name] [url] | add new remote repository named *[short-name]*

### git push

Command | Description
--- | ---
git push -u origin --all | push local repository to remote
git push | push local changes to remote
git pull | download changes and merge into HEAD

### git fetch

Command | Description
--- | ---
git fetch | download object and refs
git fetch origin | get data only from origin
git fetch --all | get data from all remotes
git fetch --all --prune | get data for all remotes and remove all deleted data

---

## Merging & Rebasing

### git merge

Command | Description
--- | ---
git merge [branch-name] | combines the specified branch’s history into the current branch
git merge --abort | abort merge

### git rebase

Command | Description
--- | ---
git rebase <branch_name> | rebase onto branch

---

## Manage Branching

Command | Description
--- | ---
git branch | show list of local branches
git branch -r | show list of remote branches
git branch -a | show list of both local and remote branches
git branch -v | show list of branches along with details of last commit on each branch
git branch -vv | like *git branch -v* but also shows which branches are tracked
git show-branch | shows the commit ancestry graph
git checkout [branch-name] | switch from current branch to *[branch-name]*. Switches HEAD branch
git checkout -b [branch-name] | create new branch called *[branch-name]* and switch to it
git branch --merged | see which branches are already merged into the branch you’re on
git branch --no-merged | see all the branches that contain work you haven’t yet merged in
git branch -d [branch-name] | safe delete *[branch-name]*. will not delete branch if there are any uncommited changes
git branch -D [branch-name] | force delete *[branch-name]* ignoring any uncommitted changes
git push --set-upstream origin [branch-name] | share and transfer *[branch-name]* to remote server
git merge [branch-name] | combines the specified branch’s history into the current branch
git push origin --delete [branch-name] | deletes remote branch called [branch-name]
git fetch && git checkout [branch-name] | checkout remote branch

---

## Manage Commits

Command | Description
--- | ---
git log @{u}.. | display a list of pending commits (commits that have not been pushed)
git cherry-pick abc123 | add specific commit (abc123) to current branch

---

## Manage Tags

Command | Description
--- | ---
git tag | show a list of existing tags
git tag -a V1.0 -m "Created V1.0 release." | create an annotated tag (the preferred approach)
git show tag V1.0 | show information relating to specified tag
git tag -d V1.0 | remove tag from repository
git checkout -b [branch-name] [tag-name] | You can’t really check out a tag in Git, since they can’t be moved around. If you want to put a version of your repository in your working directory that looks like a specific tag, you can create a new branch at a specific tag with '`git checkout -b [branch-name] [tag-name]`'
git push --set-upstream origin [tag-name] | share and transfer tag *[tag-name]* to remote server
git push --set-upstream origin --tags | share and transfer all tags to remote server

---

## Aliases

Command | Description | Example
--- | --- | ---
git config --global alias.co "checkout" | Set alias | `git co <branch>` instead of `git checkout <branch>`
git config --global alias.lg "log --oneline --graph" | Log alias | `git lg`
git config --global alias.alias "config --get-regexp ^alias\." | An alias to list aliases | `git alias` instead of `git config --global --list | grep "^alias"

---
