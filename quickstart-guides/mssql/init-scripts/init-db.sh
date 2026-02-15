#!/bin/bash

echo "Running database initialization..."

/opt/mssql-tools/bin/sqlcmd -S mssql -U ${MSSQL_USERNAME} -P "$MSSQL_PASSWORD" -C -i /scripts/init-db.sql

if [ $? -eq 0 ]; then
    echo "Database initialization completed successfully."
    exit 0
else
    echo "Database initialization failed."
    exit 1
fi