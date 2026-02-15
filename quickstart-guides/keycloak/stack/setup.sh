#!/bin/bash

/opt/keycloak/bin/kcadm.sh config credentials \
  --server http://keycloak:8080 \
  --realm master \
  --user admin \
  --password admin

# Create realm
/opt/keycloak/bin/kcadm.sh create realms \
  -s realm=myrealm \
  -s enabled=true

# Create user
/opt/keycloak/bin/kcadm.sh create users \
  -r myrealm \
  -s username=testuser \
  -s enabled=true

# Set password
/opt/keycloak/bin/kcadm.sh set-password \
  -r myrealm \
  --username testuser \
  --new-password password123