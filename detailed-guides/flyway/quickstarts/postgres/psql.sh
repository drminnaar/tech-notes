#!/bin/bash

docker compose exec postgres-client sh -c \
  'PGPASSWORD="$POSTGRES_PASSWORD" \
  psql \
  -h postgres \
  -U "$POSTGRES_USER" \
  -d "$POSTGRES_DB"'