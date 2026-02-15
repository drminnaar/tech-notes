# Windows Subsystem for Linux (WSL) - Getting Started

The [Windows Subsystem for Linux (WSL)](https://learn.microsoft.com/en-us/windows/wsl/about) is a compatibility layer in Windows that lets you run a Linux distribution (like Ubuntu, Debian, or Kali) alongside your Windows system without needing a virtual machine or dual-boot setup. It provides a Linux kernel interface, allowing you to use Linux command-line tools, utilities, and scripts directly on Windows. You can access Linux files from Windows and vice versa, share environment variables, and even run graphical Linux apps with WSL2, which uses a lightweight virtual machine for better performance and full kernel support. It’s built into Windows 10 and 11, installable via the Microsoft Store or command line, and is mainly aimed at developers, sysadmins, and those needing Linux tools in a Windows environment.

---

## Contents

- [Contents](#contents)
- [Summary](#summary)
- [Prerequisites](#prerequisites)
- [Setup](#setup)
  - [1. Enable WSL](#1-enable-wsl)
  - [2. Set WSL2 as Default](#2-set-wsl2-as-default)
  - [3. Install Ubuntu 24.04](#3-install-ubuntu-2404)
  - [4. Configure Ubuntu 24.04](#4-configure-ubuntu-2404)
  - [5. Verify WSL Installation](#5-verify-wsl-installation)

---

## Summary

This is a guide on how to install Windows Subsystem for Linux (WSL). It also demonstrates how to install Ubuntu 24.04.

For more information on WSL and setup:

- [What is WSL?](https://learn.microsoft.com/en-us/windows/wsl/about)
- [Install WSL](https://learn.microsoft.com/en-us/windows/wsl/install)
- [Grok Guide - WSL Installation and Setup](https://x.com/i/grok/share/RdUXrIdtoHaZWlryZb5SgW7iy)

---

## Prerequisites

- Windows Version: Windows 10 (version 2004 or higher) or Windows 11.
- Admin Access: You need administrative privileges on your Windows machine.
- Internet Connection: Required for downloading WSL and Ubuntu.

---

## Setup

### 1. Enable WSL

- Open PowerShell as Administrator:

  Press Win + S, type PowerShell, right-click, and select "Run as administrator."

- Enable WSL:

  Run the following command to enable WSL:

  ```powershell
  # powershell

  wsl --install

  ```

- This command installs WSL, the default Linux distribution (usually Ubuntu), and the WSL2 backend. If you only want to enable WSL without installing a distro, use:
powershell

  ```powershell
  # powershell

  dism.exe /online /enable-feature /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart

  ```

- Enable Virtual Machine Platform (required for WSL2):

  Run:

  ```powershell
  # powershell

  dism.exe /online /enable-feature /featurename:VirtualMachinePlatform /all /norestart

  ```

- Restart your computer to apply changes.

### 2. Set WSL2 as Default

This step is optional, but recommended.

- To ensure new distributions use WSL2:

  ```powershell
  # powershell

  wsl --set-default-version 2

  ```

### 3. Install Ubuntu 24.04

- Open Microsoft Store:

  Search for "Ubuntu 24.04" in the Microsoft Store app or visit the Microsoft Store website.

- Install Ubuntu 24.04 LTS:

  Click "Get" or "Install" to download and install Ubuntu 24.04.

  Alternatively, install it via PowerShell:

  ```powershell
  # powershell

  wsl --install -d Ubuntu-24.04

  ```

- Launch Ubuntu:

  After installation, open Ubuntu from the Start menu or by typing `ubuntu2404` in PowerShell or Command Prompt.

  The first launch will prompt you to configure the distro.

### 4. Configure Ubuntu 24.04

- Set up a User Account:

  When Ubuntu starts, it will ask you to create a username and password for the default user. Enter your desired credentials.

- Update Ubuntu:

  Run the following commands to update the package lists and installed packages:

  ```bash
  # bash

  sudo apt update  
  sudo apt upgrade -y

  ```

### 5. Verify WSL Installation

- Check the installed distributions and their WSL versions:

  ```powershell
  # powershell

  wsl --list --all

  ```

- Ensure Ubuntu 24.04 is set as the default distro (if desired):

  ```powershell
  # powershell

  wsl --set-default Ubuntu-24.04

  ```

---
