# Flyway - Cassandra Quickstart

This quickstart demonstrates how to use Flyway with Apache Cassandra for database schema migrations. It provides a complete setup with Docker Compose, migration scripts, and a convenient runner script.

---

## Directory Structure

```
.
├── README.md              # This documentation file
├── compose.yaml           # Docker Compose configuration for Cassandra and Flyway services
├── compose.sh             # Shell script to execute docker compose commands
├── flyway.sh              # Shell script to execute Flyway commands
├── cqlsh.sh               # Shell script to execute Cassandra CLI commands (cqlsh)
└── migrations/            # Directory containing all migration scripts
    ├── V1_*.cql           # Versioned migrations
    ├── R__*.cql           # Repeatable migrations (seed data)
    └── afterClean__*.cql  # Post-clean scripts

```

---

## Migration Scripts

The `migrations` directory contains the following types of scripts:

1. **Versioned Migrations** (`V1_*`):
   - Create and modify keyspaces and tables
   - Follow a sequential version numbering (V1_0_0, V1_1_0, etc.)
   - Example: `V1_0_0__Create_customers_keyspace.cql`

2. **Repeatable Migrations** (`R__*`):
   - Used for seed data and views
   - Run after versioned migrations whenever their content changes
   - Example: `R__Seed_customers-by-id_table.cql`

3. **After Clean** (`afterClean__*`):
   - Scripts executed after `flyway clean`
   - Used for cleanup operations
   - Example: `afterClean__Drop_keyspaces.cql`

---

## Database Schema

This quickstart implements a customer database with the following structure:

- Keyspace: `customers`
- Tables:
  - `customers_by_id`: Primary customer table
  - `customers_by_email`: Secondary index by email
  - `customers_by_region`: Secondary index by region with contact information

---

## Usage

### Prerequisites

- Docker and Docker Compose
- Shell environment (bash)
- Execute permissions on `flyway.sh` (`chmod +x flyway.sh`)
- Execute permissions on `compose.sh` (`chmod +x compose.sh`)
- Execute permissions on `cqlsh.sh` (`chmod +x cqlsh.sh`)

### Start Cassandra Database

Run the following command to start Cassandra database:

```bash
# Run from ./quickstarts/cassandra root

# start cassandra db
docker compose up --detach

# watch cassandra logs
docker compose logs -f cassandra
```

Alternatively, use the provided utility script `compose.sh` to run common commands like `up`, `down`, `logs`, `ps` and `exec`.

```bash
# start postgres db
./compose.sh up

# destroy postgres db
./compose.sh down

# view postgres logs
./compose.sh logs -f cassandra

# list containers for this stack
./compose.sh ps
```

### Available Commands

The `flyway.sh` script supports the following Flyway commands:

```bash
./flyway.sh <command>
```

Where `<command>` can be:
- `info`: Display current migration status
- `migrate`: Apply pending migrations
- `clean`: Wipe the database clean (use with caution!)
- `validate`: Check migration integrity
- `repair`: Fix flyway_schema_history issues

#### Examples

1. View migration status:
   
   ```bash
   ➜ /tech-notes/flyway/quickstarts/cassandra$ ./flyway.sh info

   Flyway OSS Edition 11.15.0 by Redgate

   See release notes here: https://rd.gt/416ObMi
   Database: jdbc:cassandra://cassandra:9042 (Cassandra 5.0)
   Schema history table "migrations"."flyway_schema_history" does not exist yet
   Schema version: << Empty Schema >>
   
   +------------+---------+---------------------------------------------------------+------+--------------+---------+----------+
   | Category   | Version | Description                                             | Type | Installed On | State   | Undoable |
   +------------+---------+---------------------------------------------------------+------+--------------+---------+----------+
   | Versioned  | 1.0.0   | Create customers keyspace                               | SQL  |              | Pending | No       |
   | Versioned  | 1.1.0   | Create customers-by-id table                            | SQL  |              | Pending | No       |
   | Versioned  | 1.2.0   | Create customers-by-email table                         | SQL  |              | Pending | No       |
   | Versioned  | 1.3.0   | Create customers-by-region table                        | SQL  |              | Pending | No       |
   | Versioned  | 1.4.0   | Add primary-contact-number to customers-by-region table | SQL  |              | Pending | No       |
   | Repeatable |         | Seed customers-by-email table                           | SQL  |              | Pending |          |
   | Repeatable |         | Seed customers-by-id table                              | SQL  |              | Pending |          |
   | Repeatable |         | Seed customers-by-region table                          | SQL  |              | Pending |          |
   +------------+---------+---------------------------------------------------------+------+--------------+---------+----------+

   ```

2. Apply migrations:

   ```bash
   ➜ /tech-notes/flyway/quickstarts/cassandra$ ./flyway.sh migrate

   Flyway OSS Edition 11.15.0 by Redgate
   
   See release notes here: https://rd.gt/416ObMi
   Database: jdbc:cassandra://cassandra:9042 (Cassandra 5.0)
   Creating schema "migrations" ...
   Creating schema "customers" ...
   Creating Schema History table "migrations"."flyway_schema_history" ...
   Current version of schema "migrations": null
   Migrating schema "migrations" to version "1.0.0 - Create customers keyspace" [non-transactional]
   Migrating schema "migrations" to version "1.1.0 - Create customers-by-id table" [non-transactional]
   Migrating schema "migrations" to version "1.2.0 - Create customers-by-email table" [non-transactional]
   Migrating schema "migrations" to version "1.3.0 - Create customers-by-region table" [non-transactional]
   Migrating schema "migrations" to version "1.4.0 - Add primary-contact-number to customers-by-region table" [non-transactional]
   Migrating schema "migrations" with repeatable migration "Seed customers-by-email table" [non-transactional]
   Migrating schema "migrations" with repeatable migration "Seed customers-by-id table" [non-transactional]
   Migrating schema "migrations" with repeatable migration "Seed customers-by-region table" [non-transactional]
   Successfully applied 8 migrations to schema "migrations", now at version v1.4.0 (execution time 00:37.430s)


   ➜ /tech-notes/flyway/quickstarts/cassandra$ ./flyway.sh info

   Flyway OSS Edition 11.15.0 by Redgate

   See release notes here: https://rd.gt/416ObMi
   Database: jdbc:cassandra://cassandra:9042 (Cassandra 5.0)
   Schema version: 1.4.0
   
   +------------+---------+---------------------------------------------------------+--------+---------------------+---------+----------+
   | Category   | Version | Description                                             | Type   | Installed On        | State   | Undoable |
   +------------+---------+---------------------------------------------------------+--------+---------------------+---------+----------+
   |            |         | << Flyway Schema Creation >>                            | SCHEMA | 2025-10-27 03:06:44 | Success |          |
   | Versioned  | 1.0.0   | Create customers keyspace                               | SQL    | 2025-10-27 03:06:44 | Success | No       |
   | Versioned  | 1.1.0   | Create customers-by-id table                            | SQL    | 2025-10-27 03:06:50 | Success | No       |
   | Versioned  | 1.2.0   | Create customers-by-email table                         | SQL    | 2025-10-27 03:07:01 | Success | No       |
   | Versioned  | 1.3.0   | Create customers-by-region table                        | SQL    | 2025-10-27 03:07:12 | Success | No       |
   | Versioned  | 1.4.0   | Add primary-contact-number to customers-by-region table | SQL    | 2025-10-27 03:07:22 | Success | No       |
   | Repeatable |         | Seed customers-by-email table                           | SQL    | 2025-10-27 03:07:22 | Success |          |
   | Repeatable |         | Seed customers-by-id table                              | SQL    | 2025-10-27 03:07:22 | Success |          |
   | Repeatable |         | Seed customers-by-region table                          | SQL    | 2025-10-27 03:07:22 | Success |          |
   +------------+---------+---------------------------------------------------------+--------+---------------------+---------+----------+
   ```

3. Validate migrations:
   
   ```bash
   ➜ /tech-notes/flyway/quickstarts/cassandra$ ./flyway.sh validate

   Flyway OSS Edition 11.15.0 by Redgate

   See release notes here: https://rd.gt/416ObMi
   Database: jdbc:cassandra://cassandra:9042 (Cassandra 5.0)
   Successfully validated 9 migrations (execution time 00:00.095s)
   ```

### Connect To Cassandra Database

The `cqlsh.sh` script supports connecting to the Cassandra database:

```bash
./cqlsh.sh
```

The `./cqlsh.sh` script opens a `cqlsh` session that allows one to run cqlsh commands as follows:

```bash
# show cluster info
DESCRIBE cluster;

# list keyspaces
DESCRIBE keyspaces;

# show tables in customers keyspace
USE customers;
DESCRIBE tables;

# query data
SELECT * FROM customers.customers_by_id;
```

---

## Configuration

The Docker Compose file (`compose.yaml`) includes:

1. **Cassandra Service**:
   - Version: 5.0
   - Port: 9042
   - Persistent volume: cassandra_data
   - Configured for single-node cluster

2. **Flyway Services**:
   - Uses `flyway:11-alpine` image
   - Configured for Cassandra compatibility
   - Separate profiles for each Flyway command
   - Environment variables for connection and behavior settings

---

## Notes

- The setup uses Docker networks for service isolation
- Cassandra data is persisted using Docker volumes
- Migration scripts use the `.cql` extension
- Flyway is configured to handle Cassandra-specific requirements
- Connection retries are enabled for reliability
- Transactions are disabled (not supported by Cassandra)

---
