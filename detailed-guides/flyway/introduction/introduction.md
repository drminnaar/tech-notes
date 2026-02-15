# Redgate Flyway - Getting Started

## 🧐 Overview

**Redgate Flyway** is an open-source database migration tool designed to automate and version-control changes to database schemas, enabling teams to evolve databases reliably across multiple environments like development, testing, and production. It supports DevOps practices by integrating database deployments into CI/CD pipelines, reducing risks associated with manual changes and ensuring consistency.

<br />

> [!NOTE]
> &nbsp;  
> 👨‍💼 The [official Redgate Flyway documentation](https://documentation.red-gate.com/fd) states the following:
> 
> "Redgate Flyway extends DevOps to your databases to accelerate software delivery and ensure quality code.  From version control to continuous delivery, Flyway builds on application delivery processes to automate database deployments."

<br />

It supports a large number of different database systems, both on-premises and in the cloud, and integrates with popular CI/CD and release tools like GitHub, Azure DevOps, GitLab, Jenkins, and more. Below is a brief summary of supported databases:

| Type | Name |
| --- | --- |
| SQL Server Variants | Azure SQL Database, Azure SQL Database Managed Instance, SQL Server |
| PostgreSQL Variants | PostgreSQL, Aurora PostgreSQL, CockroachDB, Azure PostgreSQL |
| Apache Cassandra | Apache Cassandra |
| MongoDB | MongoDB, AtlasCloud |
| SQLite | SQLite |

<br />

> [!NOTE]
> &nbsp;  
> See ["Supported databases and versions"](https://documentation.red-gate.com/fd/supported-databases-and-versions-143754067.html) for a full list of supported databases.

<br />


A key concept of the above definition is the notion of a _"database migration"_. Within the context of discussing _Flyway_, a _"database migration"_ is a group of one or more database changes. In order to keep track of migrations, _Flyway_ creates a table (per schema) that maintains a history of all migrations for a specific schema.

Flyway supports 2 forms of migration namely **_versioned_** and **_repeatable_** migrations.

* _Versioned_
  
  * Has a version number, checksum, and description. The version number ensures that migrations are unique. The checksum helps prevent accidental changes from being migrated. The description provides metadata about the migration.
  * Versioned migrations are applied in order and only once
  * An _"undo migration"_ having the same version number can be provided

* _Repeatable_

  * Has a description and checksum but a version number is not required.
  * Repeatable migrations can be re-applied everytime their checksum changes.
  * Always applied after _versioned_ migrations.

Historically, migrations could only be written in Java and/or SQL. However, as of the time of this writing (circa October 2025), Flyway also supports NoSQL database migrations where migrations can be written for example in:

- "CQL" for Cassandra
- Javascript for MongoDB

---

The history of **Redgate Flyway** is the story of a successful open-source project being acquired and further developed by a leading Database DevOps company.

---

## 🏛️ Flyway History

Flyway began as a highly popular open-source database migration tool. Its history with Redgate began in 2019, marking a significant evolution from a community-driven CLI tool to an enterprise-grade Database DevOps solution with both free and commercial editions, leveraging Redgate's established database tooling expertise.

### Origins as Flyway (Open Source)

- **Initial Development:** Flyway was originally developed as an open-source database migration tool, primarily by **Axel Fontaine**.

- **Purpose:** It was created to simplify and automate database migrations across multiple platforms using a "migrations-based" approach, where changes are applied through a sequence of versioned scripts (primarily SQL or Java).

- **Adoption:** Flyway gained significant traction within the development community. By 2018, it had over 11.5 million downloads, and it was noted on the [Thoughtworks Technology Radar](https://www.thoughtworks.com/radar) as a technology to "Adopt." It supported a wide range of databases, including Oracle, MySQL, and PostgreSQL.

### Acquisition by Redgate

- **The Acquisition:** In **July 2019**, Flyway was acquired by **Redgate Software**, a company already specializing in Database DevOps tools, particularly for SQL Server (like SQL Compare and SQL Source Control, which use a "state-based" approach). Redgate committed to investing in Flyway's continued development.

- **Strategic Move:** This acquisition was a major step for Redgate, allowing them to rapidly expand their offerings to support cross-platform database DevOps and a migrations-based approach, complementing their existing state-based tools.

- **Continued Open Source:** Redgate committed to maintaining and supporting the free, open-source **Flyway Community** version under the Apache v2 license.

### Development as Redgate Flyway

Since the acquisition, Redgate has significantly invested in and expanded the Flyway product line:

- **2019-2020:** Redgate quickly integrated its technology, bringing its market-leading database comparison engine to Flyway. This enabled advanced features like:
    * **Object-level Versioning:** Capturing the history of database changes at the individual object level.
    * **Auto-generation of Migration Scripts:** Automatically creating migration scripts by comparing the database state.

- **Post-Acquisition Product Expansion:** The product evolved into a tiered offering to support different team sizes and enterprise needs:
  - **Flyway Community:** The free, open-source version.
  - **Flyway Teams (and later, Flyway Enterprise):** Paid versions that build upon the open-source core, adding advanced features necessary for team collaboration, automation, compliance, and support for a greater number of database systems.
- **Flyway Desktop:** A new **Graphical User Interface (GUI)** was introduced for Windows, Mac, and Linux to help developers more easily author, track, and understand database changes, integrating Flyway with Redgate's comparison technology.

- **Expanded Support:** Redgate continued to broaden Flyway's compatibility, increasing its multi-DBMS support from over 20 to an ever-growing **50+ database systems** today, including Snowflake and cloud platforms.

---

## 🔑 Key Features

- **Migration Scripts**: Uses SQL or Java-based scripts to apply incremental changes, with automatic tracking of applied migrations via a metadata table.

- **Cross-Platform Support**: Works on Windows, macOS, Linux, Docker, and Java environments, and supports databases like PostgreSQL, MySQL, Oracle, SQL Server, and more.

- **Enterprise Edition**: Offers advanced capabilities such as object-level version control, data masking, and enhanced reporting for complex deployments.

- **Community and Simplicity**: The core tool is lightweight and focused, with a command-line interface, Maven/Gradle plugins, and API integration for easy adoption.

- **Version-Controlled Migrations**: Store changes as numbered SQL scripts (e.g., `V1__Create_users_table.sql`) in your VCS. Flyway applies them in order, tracking applied versions in a metadata table (`flyway_schema_history`) to prevent re-execution.

- **Script Generation and Undo Support**: Automatically generates forward migration and rollback (undo) scripts for schema objects like tables, views, and stored procedures, reducing manual errors.

- **Dependency Management**: Handles complex inter-object dependencies, ensuring safe ordering during deployments.

- **Drift Detection and Validation**: Compares database states across environments to identify schema drift; includes pre-deployment checks and code analysis for quality gates.

- **CI/CD Automation**: Integrates seamlessly with pipelines for automated testing, including unit/integration tests and left-shifted development (early error detection).

- **Audit and Compliance Tools**: Provides full traceability with deployment histories, supporting regulations like GDPR, SOX, or HIPAA.

- **Cross-Platform Execution**: Runs via Command-Line Interface (CLI), Desktop GUI, API (e.g., Java-based), or Docker containers on Windows, macOS, and Linux.

- **Advanced Analytics**: In enterprise editions, AI-driven insights help predict deployment risks and optimize change management.

---

## 🛠️ How Flyway Works

Flyway follows a convention-over-configuration model:

1. **Develop Changes**: Write migration scripts in your IDE, committing them to VCS alongside application code.

2. **Baseline Existing Databases**: For legacy systems, create a baseline script to mark the current state as versioned.

3. **Validate and Test**: Run `flyway validate` or `check` in CI to ensure scripts are correct and dependencies are met.

4. **Migrate**: Execute `flyway migrate` to apply pending scripts. Flyway locks the database during application to avoid conflicts.

5. **Monitor and Rollback**: Use `flyway info` for status, `undo` for reversions, or `repair` to fix metadata issues.

6. **Deploy Across Environments**: Integrate with pipelines to promote changes from dev → staging → prod, with environment-specific configs (e.g., via `flyway.conf`).

This process ensures databases evolve predictably, with each deployment being idempotent and auditable. For example, in a microservices setup, Flyway can run migrations on application startup via its API.

---

## 🤲 Editions

<br />

> [!NOTE]
> &nbsp;  
> See the available [Pricing and editions here](https://www.red-gate.com/products/flyway/editions).

<br />

Flyway offers tiered editions to suit different needs:

| Edition          | Target Users                  | Key Differences                                                                 | Cost Model                  |
|------------------|-------------------------------|---------------------------------------------------------------------------------|-----------------------------|
| **Community**   | Individuals, small teams, open-source projects | Core migration features, CLI/API support, basic SQL scripting. No advanced automation or GUI. | Free (open-source license) |
| **Enterprise**  | Large teams, enterprises     | All Community features + script generation, undo migrations, drift detection, AI insights, Flyway Pipelines (centralized deployment tracking), and premium support. Includes Desktop GUI for visual management. | Subscription-based (starts at ~$500/user/year; contact Redgate for quotes). Enterprise adds scalability for 100+ devs. |

The Community Edition remains fully open-source under the Apache 2.0 license, while Enterprise unlocks paid enhancements for production-scale use.

---

## 🛢️ Supported Databases

Flyway supports over 50 databases, covering relational, NoSQL, and cloud-native systems. Key ones include:

- **Relational**: Oracle, SQL Server, PostgreSQL (including CockroachDB, Yugabyte), MySQL (MariaDB, TiDB), IBM DB2, Snowflake, SAP HANA.
- **NoSQL**: MongoDB, Cassandra.
- **Cloud**: Amazon RDS/Aurora, Google Cloud SQL, Azure SQL Database, BigQuery.

It uses native SQL dialects for optimal performance, with foundational support for most and advanced features (e.g., undo scripts) for top DBMS like SQL Server and Oracle.

---

## 📢 Recent Updates in 2025

- **June 2025 Release**: Major upgrades for schema comparison, automated change deployment, and versioning to reduce deployment stress. New AI capabilities generate precise SQL scripts from schema diffs, accelerating DevOps for AI/ML workloads.

- **January 2025 Preview**: Upcoming features in the next major version include enhanced CI script generation and multi-environment management tutorials.

- **Awards and Adoption**: Earned G2's High Performer badge in Spring 2025 for enterprise database tools.

The 2025 State of Database DevOps report highlights Flyway's role in 70% of surveyed teams adopting automated migrations.

---

## ✨ Benefits and Use Cases

Flyway addresses common pain points like manual scripting, deployment failures, and siloed database changes. Benefits include:

- **Faster Releases**: Automates what used to take hours (e.g., one bank saved 2 hours/day on reviews).

- **Reduced Risk**: 99% success rate in deployments via validation and undos.

- **Scalability**: Handles monoliths to microservices, on-prem to multi-cloud.

- **Use Cases**: E-commerce schema updates, fintech compliance audits, AI data pipeline evolutions, or migrating legacy Oracle systems to PostgreSQL.

---

## 📚 Further Reading & Resources

<br />

> [!NOTE]
> &nbsp;  
> See the [red-gate.com/products/flyway](https://www.red-gate.com/products/flyway) product page.  
> 

<br />

**Documentation:**

- [Official RedGate Flyway Documentation](https://documentation.red-gate.com/fd/redgate-flyway-documentation-138346877.html)
- [Getting Started](https://www.red-gate.com/products/flyway/get-started/)

**Courses:**

- [RedGate Hub - RedGate Flyway Univesity](https://www.red-gate.com/hub/university/courses/flyway)

**Videos:**

- [Getting Started with Flyway - Redgate](https://youtube.com/playlist?list=PLhFdCK734P8DPVc-PjxDKHnt91ZmCAh2y&si=ykdEX4L2KaCoIw3j)

**Other:**

- [RedGate Flyway GitHub](https://github.com/flyway/flyway)
- [RedGate Flyway Docker Hub](https://hub.docker.com/r/redgate/flyway)
- [RedGate Flyway Installers](https://documentation.red-gate.com/fd/installers-172490864.html)

---
