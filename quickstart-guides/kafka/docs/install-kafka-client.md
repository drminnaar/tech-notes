# Installing Kafka Client Tools on Ubuntu 24.04 (later)

Here's a comprehensive guide to install Apache Kafka client tools on Ubuntu 24.04 (later):

---

## Prerequisites

```bash
# Update package list
sudo apt update

# Install Java (Kafka requires Java)
sudo apt install -y default-jdk

# Verify Java installation
java -version
```

---

## Method 1: Install from Apache Kafka Binary (Recommended)

Before downloading Kafka, find the latest available version by visiting `[https://downloads.apache.org/kafka](https://downloads.apache.org/kafka/)`.

### 1. Download Kafka

```bash
# Navigate to your preferred directory
cd /opt

# Download Kafka (replace with latest version if needed)
sudo wget https://downloads.apache.org/kafka/4.1.0/kafka_2.13-4.1.0.tgz

# Extract the archive
sudo tar -xzf kafka_2.13-4.1.0.tgz

# Create a symbolic link for easier access
sudo ln -s kafka_2.13-4.1.0 kafka

# Set ownership (optional)
sudo chown -R $USER:$USER kafka_2.13-4.1.0
```

### 2. Add Kafka to PATH

```bash
# Edit your .bashrc or .profile
echo 'export PATH=$PATH:/opt/kafka/bin' >> ~/.bashrc

# Reload the configuration
source ~/.bashrc
```

### 3. Verify Installation

```bash
# Check if kafka-console-producer is available
kafka-console-producer.sh --version

# Check if kafka-console-consumer is available
kafka-console-consumer.sh --version
```

---

## Method 2: Using Package Manager (Confluent Platform)

Visit `[https://packages.confluent.io/deb](https://packages.confluent.io/deb)` to find the latest packages.

### 1. Add Confluent Repository

```bash
# Install prerequisites
sudo apt install -y wget gnupg2 software-properties-common

# Add Confluent's public key
wget -qO - https://packages.confluent.io/deb/8.1/archive.key | sudo apt-key add -

# Add repository
sudo add-apt-repository "deb [arch=amd64] https://packages.confluent.io/deb/8.1 stable main"

# Update package list
sudo apt update
```

### 2. Install Kafka Client Tools

```bash
# Install only the client tools
sudo apt install -y confluent-kafka-clients

# Or install the full Confluent Community Platform
sudo apt install -y confluent-community-2.13
```

---

## Troubleshooting

**If commands are not found:**

- Ensure the bin directory is in your PATH
- Verify the installation directory
- Try using the full path: `/opt/kafka/bin/kafka-console-producer.sh`

**Connection issues:**

- Verify your Kafka broker is running
- Check firewall settings
- Ensure correct bootstrap server address

**Permission issues:**

```bash
sudo chown -R $USER:$USER /opt/kafka
```

## Notes

- The client tools are included in the full Kafka distribution
- You don't need to run a Kafka broker locally to use client tools (you can connect to remote brokers)
- Replace `localhost:9092` with your actual Kafka broker address
- For production use, consider using specific versions rather than "latest"

---
