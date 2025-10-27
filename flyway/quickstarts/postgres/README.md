# Flyway - Postgres Quickstart

This quickstart demonstrates how to use Flyway with Postgres for database schema migrations. It provides a complete setup with Docker Compose, migration scripts, and a convenient runner script.

---

## Directory Structure

```
.
├── README.md              # This documentation file
├── compose.yaml           # Docker Compose configuration for Postgres and Flyway services
├── .env                   # Environment file required to configure local environent variables
├── .env.example           # Environment sample file that specifies the required local environent variables
├── flyway.sh              # Shell script to execute Flyway commands
├── compose.sh             # Shell script to execute docker compose commands
├── psql.sh                # Shell script to execute Postgresql CLI commands (psql)
└── migrations/            # Directory containing all migration scripts
    ├── V1_*.sql           # Versioned migrations
    ├── R__*.sql           # Repeatable migrations (seed data)
    └── afterClean__*.sql  # Post-clean scripts

```

---

## Migration Scripts

The `migrations` directory contains the following types of scripts:

1. **Versioned Migrations** (`V1_*`):
   - Create and modify schemas and tables
   - Follow a sequential version numbering (V1_0, V1_0, etc.)
   - Example: `V1_0__Create_operations_schema.sql`

2. **Repeatable Migrations** (`R__*`):
   - Used for seed data and views
   - Run after versioned migrations whenever their content changes
   - Example: `R__Insert_track_data.sql`

3. **After Clean** (`afterClean__*`):
   - Scripts executed after `flyway clean`
   - Used for cleanup operations
   - Example: `afterClean__Drop_all_schemas.sql`

---

## Database Schema

This quickstart implements an online music store database with the following structure:

- Schemas: `operations`, `music_catalog`, `sales`
- Tables:
  
  | table_schema  |  table_name  
  | --------------| ---------------
  | music_catalog | album
  | music_catalog | artist
  | music_catalog | composition
  | music_catalog | genre
  | music_catalog | media_type
  | music_catalog | playlist
  | music_catalog | review
  | music_catalog | track
  | operations    | employee
  | sales         | customer
  | sales         | invoice
  | sales         | invoice_line

---

## Usage

### Prerequisites

- Docker and Docker Compose
- Shell environment (bash)
- Execute permissions on `flyway.sh` (`chmod +x flyway.sh`)
- Execute permissions on `psql.sh` (`chmod +x psql.sh`)
- Create `.env` file based on sample `.env.example` file and configure environment variables

### Configure ".env" File

Create `.env` file based on sample `.env.example` file and configure environment variables.

```yaml
LAB_POSTGRES_USER=
LAB_POSTGRES_PASSWORD=
LAB_POSTGRES_DB=
```

After creating and configuring `.env` file, run the following command to load environment variables from `.env` file:

```bash
source ./.env
```

### Manage Postgres Database

Run the following command to manage Postgres database using docker compose:

```bash
# Run from ./quickstarts/Postgres root

# start Postgres db
docker compose up --detach

# watch Postgres logs
docker compose logs -f postgres

# destroy Postgres db
docker compose down --volumes --remove-orphans
```

Alternatively, use the provided utility script `compose.sh` to run common commands like `up`, `down`, `logs`, `ps` and `exec`.

```bash
# start postgres db
./compose.sh up

# destroy postgres db
./compose.sh down

# view postgres logs
./compose.sh logs -f postgres

# list containers for this stack
./compose.sh ps
```

### Available Commands

The `flyway.sh` script supports the following Flyway commands:

```bash
./flyway.sh <command>
```

Where `<command>` can be:

| command         | description                                                          |
| --------------- | -------------------------------------------------------------------- |
| `help`          | Print this usage info and exit |
| `migrate`       | Migrates the database |
| `clean`         | Drops all objects in the configured schemas |
| `info`          | Prints the information about applied, current and pending migrations |
| `validate`      | Validates the applied migrations against the ones on the classpath |
| `repair`        | Repairs the schema history table |
| `version`       | Print the Flyway version and edition |

#### Examples

1. View migration status:
   
   ```bash
   ./flyway.sh info
   ```

2. Apply migrations:
   
   ```bash
   ./flyway.sh migrate
   ```

3. Validate migrations:
   
   ```bash
   ./flyway.sh validate
   ```

### Connect To Postgres Database

The `psql.sh` script supports connecting to the Postgres database:

```bash
./psql.sh
```

The `./psql.sh` script opens a `psql` session that allows one to run psql commands as follows:

```bash
# list databases
\l

# list schemas
\dn

# list tables
\dt operations.*
\dt music_catalog.*
\dt sales.*

# list tracks
SELECT a.title, t.name FROM music_catalog.track t JOIN music_catalog.album a ON t.album_id = a.album_id LIMIT 10;
```

---

## Configuration

The Docker Compose file (`compose.yaml`) includes:

1. **Postgres Service**:
   - Uses `postgres:18-alpine` image
   - Port: 5432
   - Persistent volume: postgres_data
   - Environment variables for connection and behavior settings

2. **Flyway Services**:
   - Uses `flyway:11-alpine` image
   - Configured for Postgres compatibility
   - Environment variables for connection and behavior settings

For environment Settings:

1. **".env" File**
   - Uses `.env` file to store environment variables
   - See `.env.example` for sample `.env` file

---

## Notes

- The setup uses Docker networks for service isolation
- Postgres data is persisted using Docker volumes
- Migration scripts use the `.sql` extension
- Flyway is configured to handle Postgres-specific requirements
- Connection retries are enabled for reliability
- ".env" file is used to configure environment variables

---
