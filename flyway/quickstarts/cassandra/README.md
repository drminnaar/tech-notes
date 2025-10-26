# Flyway - Cassandra Quickstart

This quickstart demonstrates how to use Flyway with Apache Cassandra for database schema migrations. It provides a complete setup with Docker Compose, migration scripts, and a convenient runner script.

---

## Directory Structure

```
.
├── README.md              # This documentation file
├── compose.yaml           # Docker Compose configuration for Cassandra and Flyway services
├── run.sh                 # Shell script to execute Flyway commands
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
- Execute permissions on `run.sh` (`chmod +x run.sh`)

### Start Cassandra Database

Run the following command to start Cassandra database:

```bash
# Run from ./quickstarts/cassandra root

# start cassandra db
docker compose --detach

# watch cassandra logs
docker compose logs -f cassandra
```

### Available Commands

The `run.sh` script supports the following Flyway commands:

```bash
./run.sh <command>
```

Where `<command>` can be:
- `info`: Display current migration status
- `migrate`: Apply pending migrations
- `clean`: Wipe the database clean (use with caution!)
- `validate`: Check migration integrity
- `repair`: Fix flyway_schema_history issues

### Examples

1. View migration status:
   
   ```bash
   ./run.sh info
   ```

2. Apply migrations:
   
   ```bash
   ./run.sh migrate
   ```

3. Validate migrations:
   
   ```bash
   ./run.sh validate
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
