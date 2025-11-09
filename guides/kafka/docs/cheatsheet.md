# Kafka Cheatsheet

## Common Kafka Client Commands

### Kafka Console Producer

```bash
kafka-console-producer.sh --bootstrap-server localhost:9092 --topic my-topic
```

### Kafka Console Consumer

```bash
kafka-console-consumer.sh --bootstrap-server localhost:9092 --topic my-topic --from-beginning
```

### Kafka Topics

```bash
# List topics
kafka-topics.sh --bootstrap-server localhost:9092 --list

# Create topic
kafka-topics.sh --bootstrap-server localhost:9092 --create --topic my-topic --partitions 3 --replication-factor 1

# Describe topic
kafka-topics.sh --bootstrap-server localhost:9092 --describe --topic my-topic

# Delete topic
kafka-topics.sh --bootstrap-server localhost:9092 --delete --topic my-topic
```

### Kafka Consumer Groups

```bash
# List consumer groups
kafka-consumer-groups.sh --bootstrap-server localhost:9092 --list

# Describe consumer group
kafka-consumer-groups.sh --bootstrap-server localhost:9092 --describe --group my-group
```

---
