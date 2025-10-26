#!/bin/bash

# makes script exit on errors (-e) and on references to unset variables (-u)
set -eu

# Check if a parameter is provided
if [ $# -eq 0 ]; then
    echo "Error: No parameter provided"
    echo "Usage: $0 <command>"
    echo "Available commands: clean, migrate, info, validate, repair"
    exit 1
fi

# Get the command parameter
command="$1"

# Define valid commands
valid_commands=("clean" "migrate" "info" "validate" "repair")

# Check if the command is valid
is_valid=0
for valid_command in "${valid_commands[@]}"; do
    if [ "$command" = "$valid_command" ]; then
        is_valid=1
        break
    fi
done

# If command is not valid, show error and usage
if [ $is_valid -eq 0 ]; then
    echo "Error: Invalid command '$command'"
    echo "Usage: $0 <command>"
    echo "Available commands: clean, migrate, info, validate, repair"
    exit 1
fi

# Execute the appropriate docker compose command
docker compose run --rm "migration-${command}"
