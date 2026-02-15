#!/bin/bash

# makes script exit on errors (-e) and on references to unset variables (-u)
set -eu

# wait for cassandra to be ready
echo "Waiting for Cassandra to accept CQL connections..."
until cqlsh cassandra 9042 -e "DESCRIBE KEYSPACES" >/dev/null 2>&1; do
  echo "Cassandra not ready yet; retrying in 5s..."
  sleep 5
done

# apply CQL with retries to ensure table exists before INSERTs run
MAX_ATTEMPTS=10
attempt=1
delay=2
while [ "$attempt" -le "$MAX_ATTEMPTS" ]; do
  echo "Attempt $attempt: applying /init.cql"
  if cqlsh cassandra 9042 -f /init.cql; then
    echo "CQL applied successfully"
    exit 0
  else
    echo "CQL apply failed. Will retry after ${delay}s..."
    sleep "$delay"
    attempt=$((attempt + 1))
    delay=$((delay * 2))
  fi
done

echo "Failed to apply CQL after $MAX_ATTEMPTS attempts" >&2
exit 1