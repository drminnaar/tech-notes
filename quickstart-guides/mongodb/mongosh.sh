#!/bin/bash

docker compose exec mongosh bash -c 'mongosh --host mongo --port 27017 -u "$MONGO_INITDB_ROOT_USERNAME" -p "$MONGO_INITDB_ROOT_PASSWORD"'