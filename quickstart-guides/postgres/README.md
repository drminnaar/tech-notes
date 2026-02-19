# 🗺️ Postgres Guide

This quickstart guide provides resources and examples for running Postgresql locally using Docker Compose.

---

## 🗃️ Directory Structure

```
.
├── README.md              # This documentation file
├── compose.yaml           # Docker Compose configuration for Postgres
├── .env                   # Environment file required to configure local environent variables
├── .env.example           # Environment sample file that specifies the required local environent variables
├── compose.sh             # Shell script to execute docker compose commands
└── psql.sh                # Shell script to execute Postgresql CLI commands (psql)

```

---

## 🛠️ Usage

### Prerequisites

- Docker and Docker Compose
- Shell environment (bash)
- Execute permissions on `compose.sh` (`chmod +x compose.sh`)
- Execute permissions on `psql.sh` (`chmod +x psql.sh`)
- Create `.env` file based on sample `.env.example` file and configure environment variables

### Configure ".env" File

Create `.env` file based on sample `.env.example` file and configure environment variables.

```yaml
LAB_POSTGRES_USER=postgres
LAB_POSTGRES_PASSWORD=changeme
LAB_POSTGRES_DB=postgres

LAB_PGADMIN_DEFAULT_EMAIL=admin@example.com
LAB_PGADMIN_DEFAULT_PASSWORD=changeme
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
docker compose logs -f pgadmin

# list containers
docker compose ps

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
./compose.sh logs -f pgadmin

# list containers for this stack
./compose.sh ps
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
\dt

```

---

## ⚙️ Configuration

The Docker Compose file (`compose.yaml`) includes:

1. **Postgres Service**:
   - Uses `postgres:18-alpine` image
   - Port: 5432
   - Persistent volume: postgres_data
   - Environment variables for connection and behavior settings

2. **Postgres Client Service**:
   - Uses `postgres:18-alpine` image
   - Environment variables for connection and behavior settings

3. **Postgres Web Client (pgadmin) Service**:
   - Uses `dpage/pgadmin4` image
   - Port: 8080
   - Environment variables for connection and behavior settings

For environment Settings:

1. **".env" File**
   - Uses `.env` file to store environment variables
   - See `.env.example` for sample `.env` file

---

## 📝 Notes

- The setup uses Docker networks for service isolation
- Postgres data is persisted using Docker volumes
- ".env" file is used to configure environment variables
- Both a web client and a cli can be used to interact with postgres database
  - web client: http://localhost:8080
  - psql cli: ./psql.sh

---
