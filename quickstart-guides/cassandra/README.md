# 🗺️ Cassandra Guide

This quickstart guide demonstrates how to use Apache Cassandra. It provides a complete setup with Docker Compose, and a convenient runner scripts.

---

## 🗃️ Directory Structure

```
.
├── README.md     # This documentation file
├── compose.yaml  # Docker Compose configuration for Cassandra
├── compose.sh    # Shell script to execute docker compose commands
├── cqlsh.sh      # Shell script to execute Cassandra CLI commands (cqlsh)
├── init.cql      # Initialization CQL to create keyspace and table
└── init.sh       # Initialization script to seed Cassandra db

```

---

## 🛢️ Database Schema

This quickstart implements a customer database with the following structure:

- Keyspace: `product_catalog`
- Tables:
  - `products`: Primary product table

---

## 🛠️ Usage

### Prerequisites

- Docker and Docker Compose
- Shell environment (bash)
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

# show tables in product_catalog keyspace
USE product_catalog;
DESCRIBE tables;

# query data
SELECT * FROM product_catalog.products;
```

---

## ⚙️ Configuration

The Docker Compose file (`compose.yaml`) includes:

1. **Cassandra Service**:
   - Version: Uses `cassandra:5` image
   - Port: 9042
   - Persistent volume: cassandra_data
   - Configured for single-node cluster
  
2. **Cassandra Client (cqlsh) Service**:
   - Version: Uses `cassandra:5` image


---

## 📝 Notes

- The setup uses Docker networks for service isolation
- Cassandra data is persisted using Docker volumes
- Connection retries are enabled for reliability
- Transactions are disabled (not supported by Cassandra)
- Uses a jump host `cassandra-client` to run CQL commands

---
