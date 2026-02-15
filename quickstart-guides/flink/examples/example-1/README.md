# Apache Flink - Example 1

Publish product reviews to a kafka topic and perform SQL queries over stream.

## Pre-requisites

- running kakfa cluster
- running flink cluster

```bash

# Start Flink Stack (includes Kafka)
docker compose up --detach

# View logs
docker compose logs -f

```

---

## Steps

### Step 1 - Create Product Reviews Topic

```bash

docker exec -it kafka kafka-topics --create \
  --bootstrap-server localhost:9092 \
  --topic customer.product-reviews.v1 \
  --partitions 3 \
  --replication-factor 1

```

### Step 2 - Send Data to Product Review Topic

```bash

cat example-1.json | \
  docker exec -i kafka kafka-console-producer \
  --bootstrap-server localhost:9092 \
  --topic customer.product-reviews.v1 \
  --property "parse.key=true" \
  --property "key.separator=:"

```

### Step 3 - Open SQL Client

For `Step 4 - Create SQL Table` and `Step 5 - Run Query` we will use the SQL Client to run SQL queries.

```bash

docker compose exec -it sql-client bash -c '/opt/flink/bin/sql-client.sh embedded'

# alternatively, use the helper script (requires execute permissions - chmod +x ./sql-client.sh)
./sql-client.sh

```

### Step 4 - Create SQL Table

Paste and run the following text into SQL Client:

```sql

CREATE TABLE product_reviews (
  product_id STRING,
  user_id STRING,
  rating INT
) WITH (
  'connector' = 'kafka',
  'topic' = 'customer.product-reviews.v1',
  'properties.bootstrap.servers' = 'kafka:29092',
  'format' = 'json',
  'scan.startup.mode' = 'earliest-offset',
  'json.ignore-parse-errors' = 'true'
);

```

Verify that the `product_reviews` table was created successfully:

```sql

SHOW TABLES;

```

### Step 5 - Run Query

Paste and run the following text into SQL Client:

```sql

SELECT * FROM product_reviews;

```

---
