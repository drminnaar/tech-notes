#!/bin/bash

docker compose exec mssql-client bash -c '/opt/mssql-tools/bin/sqlcmd -S mssql -U ${MSSQL_USERNAME} -P "$MSSQL_PASSWORD"'