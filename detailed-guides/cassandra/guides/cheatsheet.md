# CQLSH Cheatsheet

This cheatsheet covers essential commands for using **cqlsh** (Cassandra Query Language Shell) to interact with Apache Cassandra databases. It includes connection basics, shell-specific commands, DDL (Data Definition Language), DML (Data Manipulation Language), data types, and other utilities.

---

## Connecting to cqlsh

- Start cqlsh: `cqlsh [host] [port] -u username -p password`
  - Example: `cqlsh 127.0.0.1 9042 -u cassandra`
- With color output: `cqlsh [host] -C -u username -p password`
- Execute single command: `cqlsh -e 'DESCRIBE CLUSTER;'`
- Execute from file: `cqlsh -f script.cql`
- Remote connection: Set `CQLSH_HOST=<ip>` and `CQLSH_PORT=9042`, then `cqlsh`

---

## cqlsh Shell Commands

These are meta-commands for the shell environment (not CQL statements).

| Command | Syntax/Example | Description |
|---------|----------------|-------------|
| HELP | `HELP;` or `HELP <topic>;` | Shows synopsis of all commands or details on a specific one. |
| CONSISTENCY | `CONSISTENCY <level>;` (e.g., `CONSISTENCY QUORUM;`) | Sets default consistency level for queries. |
| SOURCE | `SOURCE 'file.cql';` | Executes commands from a file. |
| CAPTURE | `CAPTURE 'output.txt';` then `CAPTURE OFF;` | Redirects output to a file. |
| TRACING | `TRACING ON;` or `TRACING ONE;` | Enables query tracing (full or single query). |
| EXPAND | `EXPAND ON;` | Prints rows in vertical format for readability. |
| SHOW SESSION | `SHOW SESSION <session_id>;` | Displays details of a traced session. |
| EXIT | `EXIT;` | Quits the shell. |
| DESCRIBE | `DESCRIBE KEYSPACES;`<br>`DESCRIBE TABLES;`<br>`DESCRIBE CLUSTER;` | Lists keyspaces, tables, or cluster info. |

---

## DDL Commands

### Keyspaces

| Command | Syntax/Example |
|---------|----------------|
| CREATE KEYSPACE | `CREATE KEYSPACE IF NOT EXISTS ks_name WITH REPLICATION = {'class': 'SimpleStrategy', 'replication_factor': 3} AND DURABLE_WRITES = true;` |
| ALTER KEYSPACE | `ALTER KEYSPACE ks_name WITH REPLICATION = {'class': 'SimpleStrategy', 'replication_factor': 1};` |
| DROP KEYSPACE | `DROP KEYSPACE IF EXISTS ks_name;` |
| USE | `USE ks_name;` |

### Tables

| Command | Syntax/Example |
|---------|----------------|
| CREATE TABLE | `CREATE TABLE IF NOT EXISTS ks_name.table_name (id UUID PRIMARY KEY, name TEXT, age INT) WITH CLUSTERING ORDER BY (name ASC);` |
| ALTER TABLE (Add Column) | `ALTER TABLE ks_name.table_name ADD email TEXT;` |
| ALTER TABLE (Change Type) | `ALTER TABLE ks_name.table_name ALTER age TYPE BIGINT;` |
| ALTER TABLE (Properties) | `ALTER TABLE ks_name.table_name WITH compaction = {'class': 'LeveledCompactionStrategy'};` |
| DROP TABLE | `DROP TABLE IF EXISTS ks_name.table_name;` |
| TRUNCATE TABLE | `TRUNCATE ks_name.table_name;` |

### Indexes

| Command | Syntax/Example |
|---------|----------------|
| CREATE INDEX | `CREATE INDEX ix_name ON ks_name.table_name (column_name);` |
| DROP INDEX | `DROP INDEX IF EXISTS ix_name;` |

### User-Defined Types (UDTs)

| Command | Syntax/Example |
|---------|----------------|
| CREATE TYPE | `CREATE TYPE ks_name.address (street TEXT, city TEXT, zip INT);` |

### Roles/Users

| Command | Syntax/Example |
|---------|----------------|
| CREATE ROLE | `CREATE ROLE role_name WITH LOGIN = true AND SUPERUSER = true;` |
| CREATE USER | `CREATE USER user_name WITH PASSWORD 'pass' NOSUPERUSER;` |
| GRANT ROLE | `GRANT role_name TO user_name;` |
| REVOKE ROLE | `REVOKE role_name FROM user_name;` |
| LIST ROLES | `LIST ROLES;` |

---

## DML Commands

| Command | Syntax/Example |
|---------|----------------|
| INSERT | `INSERT INTO ks_name.table_name (id, name) VALUES (uuid(), 'Alice') USING TTL 86400;` (TTL in seconds) |
| SELECT | `SELECT * FROM ks_name.table_name WHERE id = uuid() ALLOW FILTERING;`<br>`SELECT JSON * FROM table_name LIMIT 10;` |
| UPDATE | `UPDATE ks_name.table_name SET name = 'Bob' WHERE id = uuid() IF EXISTS;` |
| DELETE | `DELETE name FROM ks_name.table_name WHERE id = uuid();` |

- **SELECT Clauses**:
  - WHERE: Must include partition key; clustering keys for ordering.
  - ORDER BY: Only on clustering columns (ASC/DESC).
  - LIMIT: `LIMIT 5;`
  - GROUP BY: On partition or clustering keys.
  - ALLOW FILTERING: For non-primary key queries (performance hit).

---

## Data Types

Cassandra supports these core types:

| Category | Types |
|----------|-------|
| Strings | ascii, text/varchar, inet (IP), blob (binary) |
| Numbers | tinyint (8-bit), smallint (16-bit), int (32-bit), bigint (64-bit), varint, decimal, float, double, counter |
| Booleans | boolean |
| Time/Date | timestamp, date, time, duration |
| UUIDs | uuid, timeuuid |
| Collections | list<T>, set<T>, map<K,V> |
| Tuples/UDTs | tuple<type1, type2>, user-defined types |

---

## Other Utilities

- **Consistency Levels**: ONE, QUORUM, ALL, LOCAL_ONE, etc. Set via `CONSISTENCY` or `USING CONSISTENCY <level>`.

- **Functions**: `uuid()`, `now()`, `token(partition_key)`, `WRITETIME(col)`, `TTL(col)`.

- **Aggregates**: COUNT(*), MIN(col), MAX(col), SUM(col), AVG(col).

- **Operators**: =, >, >=, <, <=, IN, CONTAINS (for collections), LIKE.

---
