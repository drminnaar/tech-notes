# 🗺️ MSSQL Guide

This quickstart guide provides resources and examples for running MSSQL locally using Docker Compose. It includes:

- **MSSQL Database Service**: Runs a MSSQL instance with configurable root credentials and persistent storage.

- **MSSQL Init Service**: Initialises the database with sql script automatically on startup.

- **MSSQL Client (sqlcmd)**: A dedicated container for accessing the MSSQL database using the `sqlcmd` shell, useful for administration and troubleshooting.

---

## 🗃️ Directory Structure

```

.
├── README.md          # This documentation file
├── compose.yaml       # Docker Compose configuration for MSSQL
├── .env               # Environment file required to configure local environent variables
├── .env.example       # Environment sample file that specifies the required local environent variables
├── compose.sh         # Shell script to execute docker compose commands
├── sqlcmd.sh          # Shell script to execute sqlcmd CLI commands
└── init-scripts       # Directory containing files to build custom Docker file used to seed mssql db
    ├── init-db.sh     # Initialization bash script that connects to mssql and runs init-db.sql sql script
    ├── init-db.sql    # Initialization sql script that creates a database called 'sample_db'
    └── wait-for-it.sh # Bash helper that waits until a TCP host:port becomes available then runs a command

```

---

## 🛠️ Usage

### Prerequisites

- Docker and Docker Compose
- Shell environment (bash)
- Execute permissions on `compose.sh` (`chmod +x compose.sh`)
- Execute permissions on `mssqlsh.sh` (`chmod +x mssqlsh.sh`)
- Create `.env` file based on sample `.env.example` file and configure environment variables

### Configure ".env" File

Create `.env` file based on sample `.env.example` file and configure environment variables.

```yaml
MSSQL_SA_PASSWORD=changeme
MSSQL_USERNAME=sa
```

After creating and configuring `.env` file, run the following command to load environment variables from `.env` file:

```bash
source ./.env
```

### Manage MSSQL Database

Run the following command to manage MSSQL database using docker compose:

```bash
# Run from ./mssql

# start mssql db stack
docker compose up --detach --build

# watch service logs
docker compose logs -f mssql
docker compose logs -f mssql-init
docker compose logs -f mssql-client

# show running containers
docker compose ps

# destroy mssql db stack
docker compose down --volumes --remove-orphans

```

Alternatively, use the provided utility script `compose.sh` to run common commands like `up`, `down`, `logs`, `ps` and `exec`.

```bash
# start mssql db
./compose.sh up

# destroy mssql db
./compose.sh down

# view mssql logs
./compose.sh logs -f mssql

# list containers for this stack
./compose.sh ps
```

### Connect To MSSQL Database

The `sqlcmd.sh` script supports connecting to the MSSQL database:

```bash
./sqlcmd.sh
```

The `./sqlcmd.sh` script opens a `sqlcmd` session that allows one to run sqlcmd commands as follows:

```bash
# show version
SELECT @@version
GO

# list databases
SELECT name FROM sys.databases
GO

# select sample_db that was created as part of initialization scripts
USE sample_db
GO

```

---

## ⚙️ Configuration

The Docker Compose file (`compose.yaml`) includes:

1. **MSSQL Server Service**:
   - Uses `mcr.microsoft.com/mssql/server:2022-latest` image
   - Port: 1433
   - Persistent volume: mssql_data
   - Environment variables for connection and behavior settings

2. **MSSQL Init Service**:
   - Uses custom Docker file using `mcr.microsoft.com/mssql-tools` base image
   - Configured to initialize database with custom sql script
   - Environment variables for connection and behavior settings

3. **MSSQL Client - sqlcmd**:
   - Uses `mcr.microsoft.com/mssql-tools` image
   - Configured to connect to MSSQL server using `sqlcmd`
   - Environment variables for connection and behavior settings

For environment Settings:

1. **".env" File**
   - Uses `.env` file to store environment variables
   - See `.env.example` for sample `.env` file

---

## 📝 Notes

- The setup uses Docker networks for service isolation
- MSSQL data is persisted using Docker volumes
- ".env" file is used to configure environment variables

---
