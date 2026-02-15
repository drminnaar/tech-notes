# Flink guide

This quickstart guide provides resources and examples for running Kafka and Flink locally using Docker Compose. 

It includes:

- **Zookeeper:** Coordination service for Kafka

- **Kafka Broker:** Single broker configuration suitable for development

- **Kafka UI:** Web interface at http://localhost:8080 for easy management

- **Flink Job Manager:** Master process in an Apache Flink cluster that coordinates the execution of a Flink application, handling tasks like scheduling, resource management, and fault tolerance (checkpointing and recovery).

- **Flink Task Manager:** The worker process in an Apache Flink cluster that executes the parallelized tasks of a dataflow, manages its own memory and network buffers, and reports its status to the JobManager.

- **Flink SQL Client:** Serves as an access point to flink cluster using Flink sql-client.

---

## 🗃️ Directory Structure

```

.
├── README.md                           # This documentation file
└── stack                               # This folder contains everything required to run the Flink stack
    ├── compose.yaml                    # Docker Compose configuration and a small wrapper to start the Flink services used in this guide (JobManager, TaskManager, and optionally connectors).
    ├── compose.sh                      # Shell script to execute docker compose commands. Use `./compose.sh up` and `./compose.sh down` to manage the stack.
    └── lib                             # Directory for user-provided connector jars and extra libraries. Place connector jars (for example Kafka connectors) here so the compose setup mounts them into the Flink container.
        └── flink-connector-kafka.jar   # Connect Flink to Kafka for running Flink SQL queries
└── examples                            # Directory containing files to build custom Docker file used to seed kafka
    └── example-1                       # Example showing how to use sample product reviews
        ├── README.md                   # README that provides steps to run example
        └── example-1.json              # Sample product review data

```

---

## 🛠️ Usage

### Prerequisites

- Docker and Docker Compose
- Shell environment (bash)
- Execute permissions on `compose.sh` and `sql-client` (`chmod +x compose.sh sql-client.sh`)

### Manage Stack

Run the following command to manage the stack using docker compose:

```bash
# Run from ./flink

# start stack
docker compose up --detach

# follow all service logs
docker compose logs -f

# list running containers
docker compose ps

# destroy stack
docker compose down --volumes --remove-orphans

```

Alternatively, use the provided utility script `compose.sh` to run common commands like `up`, `down`, `logs`, `ps` and `exec`.

```bash
./compose.sh up    # start kafka
./compose.sh down  # destroy kafka
./compose.sh ps    # list containers for this stack
```

> [!IMPORTANT]
> &nbsp;  
> Access Kafka UI at: http://localhost:8080  
> &nbsp;  
> Access Flink UI at: http://localhost:8081  
> &nbsp;  
> 

### Connect To Kafka

Access Kafka UI at: http://localhost:8080.

Alternatively run the following CLI command:

```bash
docker exec -it kafka kafka-broker-api-versions --bootstrap-server localhost:9092
```

### Connect To Flink

Access Flink UI at: http://localhost:8081.

To access the Flink CLI, run the following command:

```bash
docker compose exec -it sql-client bash -c '/opt/flink/bin/sql-client.sh embedded'
```

Alternatively use the helper script:

```bash
./sql-client.sh
```

### Connector JARS

Add connector jars to `lib/` if you plan to use external connectors like Kafka.

Currently, a default connector to Kafka is available called `flink-connector-kafka.jar`. Find the latest version at the official maven repository [https://repo.maven.apache.org/maven2/org/apache/flink/flink-sql-connector-kafka](https://repo.maven.apache.org/maven2/org/apache/flink/flink-sql-connector-kafka). Also see [https://mvnrepository.com/artifact/org.apache.flink/flink-connector-kafka](https://mvnrepository.com/artifact/org.apache.flink/flink-connector-kafka)

---

## 📝 Notes & tips

- The `lib/` directory is intended to hold jars you don't want baked into images. When using Docker Compose the directory is typically mounted into the Flink container classpath so Flink can discover connector jars at startup.

- If you add or change jars in `lib/` after the containers are running, restart the JobManager/TaskManager so the new jars are picked up.

- The included `sql-client.sh` is a thin wrapper; you can also use the Flink CLI inside the JobManager container for advanced tasks.

- This folder is intended for local development and demos.

---

## 💡 Examples

Examples are located within the [./examples](./examples) folder.

- [Example 1](./examples/example-1) - Publish product reviews to a kafka topic and perform SQL queries over stream.

---
