# 🗺️ MongoDB Guide

This quickstart guide provides resources and examples for running MongoDB locally using Docker Compose. It includes:

- **MongoDB Database Service**: Runs a MongoDB instance with configurable root credentials and persistent storage.

- **MongoDB Seed Service**: Seeds the database with sample data (mflix dataset) automatically on startup.

- **Mongo Express GUI**: Web-based interface for managing and viewing MongoDB data at [http://localhost:8081](http://localhost:8081).

- **Mongosh Jump Host**: A dedicated container for accessing the MongoDB database using the `mongosh` shell, useful for administration and troubleshooting.

---

## 🗃️ Directory Structure

```
.
├── README.md        # This documentation file
├── compose.yaml     # Docker Compose configuration for Postgres and Flyway services
├── .env             # Environment file required to configure local environent variables
├── .env.example     # Environment sample file that specifies the required local environent variables
├── compose.sh       # Shell script to execute docker compose commands
├── mongosh.sh       # Shell script to execute mongo db CLI commands (mongosh)
└── mongo-seed       # Directory containing files to build custom Docker file used to seed mongo db
    ├── Dockerfile   # Used to seed the mongo db. It runs commands to copy mflix.gz and restore it to mongo db
    └── mflix.gz     # The sample mflix database

```

---

## 🛢️ Database Structure

This quickstart implements a movie database with the following structure:

- Database: mflix

- Collections: `comments`, `movies`, `sessions`, `theaters`, `users`

---

## 🛠️ Usage

### Prerequisites

- Docker and Docker Compose
- Shell environment (bash)
- Execute permissions on `compose.sh` (`chmod +x compose.sh`)
- Execute permissions on `mongosh.sh` (`chmod +x mongosh.sh`)
- Create `.env` file based on sample `.env.example` file and configure environment variables

### Configure ".env" File

Create `.env` file based on sample `.env.example` file and configure environment variables.

```yaml
LAB_MONGO_INITDB_ROOT_USERNAME=admin
LAB_MONGO_INITDB_ROOT_PASSWORD=changeme

LAB_ME_CONFIG_MONGODB_ADMINUSERNAME=admin
LAB_ME_CONFIG_MONGODB_ADMINPASSWORD=changeme
LAB_ME_CONFIG_MONGODB_SERVER=mongo
LAB_ME_CONFIG_BASICAUTH_USERNAME=meuser
LAB_ME_CONFIG_BASICAUTH_PASSWORD=changeme
```

After creating and configuring `.env` file, run the following command to load environment variables from `.env` file:

```bash
source ./.env
```

### Manage Mongo Database

Run the following command to manage Mongo database using docker compose:

```bash
# Run from ./mongodb

# start mongo db stack
docker compose up --detach --build

# watch mongo/mongo-express logs
docker compose logs -f mongo
docker compose logs -f mongo-express

# show running containers
docker compose ps

# destroy mongo db stack
docker compose down --volumes --remove-orphans

```

Alternatively, use the provided utility script `compose.sh` to run common commands like `up`, `down`, `logs`, `ps` and `exec`.

```bash
# start mongo db
./compose.sh up

# destroy mongo db
./compose.sh down

# view mongo logs
./compose.sh logs -f mongo

# list containers for this stack
./compose.sh ps
```

1. Set environment variables for credentials in a `.env` file or your shell.
2. Start the stack:
	```bash
	docker compose up -d
	```
3. Access MongoDB via `mongosh`:
	```bash
	docker compose exec mongosh mongosh --host mongo --port 27017 -u "$MONGO_INITDB_ROOT_USERNAME" -p "$MONGO_INITDB_ROOT_PASSWORD"
	```
4. Use Mongo Express at [http://localhost:8081](http://localhost:8081).

### Connect To Mongo Database

The `mongosh.sh` script supports connecting to the Postgres database:

```bash
./mongosh.sh
```

The `./mongosh.sh` script opens a `mongosh` session that allows one to run mongosh commands as follows:

```bash
# list databases
show dbs

# select db
use mflix

# list collections
show collections

# list users
db.users.find().pretty()

# find users having name 'stark'
db.users.find({ "name": { $regex: /stark/i } }).pretty()

# find movies released in 1900 and prior
db.movies.find({ year: { $lte: 1900 } }).pretty();

# count movies
db.movies.count()

```

---

## ⚙️ Configuration

The Docker Compose file (`compose.yaml`) includes:

1. **Mongo Db Service**:
   - Uses `mongo:8-noble` image
   - Port: 27017
   - Persistent volume: mongo_data
   - Environment variables for connection and behavior settings

2. **Mongo Db Seed Service**:
   - Uses custom Docker filed using `mongo:8-noble` base image
   - Configured to seed Mongo Db
   - Environment variables for connection and behavior settings

3. **Mongo Db Client - mongosh**:
   - Uses `mongo:8-noble` image
   - Environment variables for connection and behavior settings

4. **Mongo Express Service**:
   - Uses `mongo-express` image
   - Port: 8081
   - Environment variables for connection and behavior settings

For environment Settings:

1. **".env" File**
   - Uses `.env` file to store environment variables
   - See `.env.example` for sample `.env` file

---

## 📝 Notes

- The setup uses Docker networks for service isolation
- Mongo data is persisted using Docker volumes
- ".env" file is used to configure environment variables

---
