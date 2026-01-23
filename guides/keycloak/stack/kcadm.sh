#!/usr/bin/env bash
set -euo pipefail

docker compose exec -it keycloak bash -ic '
  echo -e "\n"
  cd /opt/keycloak/bin || exit 1
  ./kcadm.sh config credentials \
    --server http://localhost:8080 \
    --realm master \
    --user admin \
    --password admin || true
  echo -e "\e[32mEntering container shell (type exit to return)...\e[0m\n"
  exec bash
'
