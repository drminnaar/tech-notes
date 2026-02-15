#!/bin/bash
set -euo pipefail

# Get the directory where this script is located
DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
COMPOSE_FILE="$DIR/../compose.yaml"
ENV_FILE="$DIR/../.env"

# Function to check if Docker is installed
check_docker() {
    if ! command -v docker >/dev/null; then
        echo -e "\n⚠️  Docker CLI not found; please install Docker" >&2
        exit 1
    fi
}

# Function to check if .NET is installed
check_dotnet() {
    if ! command -v dotnet >/dev/null; then
        echo -e "\n⚠️  .NET CLI not found; please install .NET 10 SDK" >&2
        exit 1
    fi
}

# Function to check if .env file exists
check_env_file() {
    if [ ! -f "$ENV_FILE" ]; then
        echo -e "\n⚠️  .env file not found; please create one based on .env.example" >&2
        exit 1
    fi
}

# Bring up the stack
cmd_up() {
    check_docker
    check_dotnet
    check_env_file

    # load environment variables (see .env.example)
    source $ENV_FILE

    # create and start containers
    echo -e "\n"
    docker compose --file "$COMPOSE_FILE" up --build --detach

    # list containers
    echo -e "\n"
    docker compose --file "$COMPOSE_FILE" ps -a --format "table {{.Name}}\t{{.Status}}"

    echo -e "\n✅  Stack is up"
    echo -e "\n"
}

# Bring down the stack
cmd_down() {
    check_docker

    # stop and remove containers, networks, and volumes
    echo -e "\n"
    docker compose --file "$COMPOSE_FILE" down --volumes --remove-orphans

    # list all containers
    echo -e "\n"
    docker compose --file "$COMPOSE_FILE" ps -a --format "table {{.Name}}\t{{.Status}}"

    echo -e "\n✅  Stack is down"
    echo -e "\n"
}

# Show container status
cmd_ps() {
    check_docker

    echo -e "\n"
    docker compose --file "$COMPOSE_FILE" ps -a
    echo -e "\n"
}

# Show logs
cmd_logs() {
    check_docker

    # Strip any trailing carriage return from arguments (common on Windows with VS Code inputs)
    if [[ $# -eq 1 ]]; then
        SERVICE="${1%"$(printf '\r')"}"  # Remove trailing \r if present
    fi

    echo -e "\n"
    # If a service name is provided as the first argument, use it; otherwise, no service argument
    if [[ $# -eq 1 ]]; then
        SERVICE="$1"
        docker compose --file "$COMPOSE_FILE" logs "$SERVICE" --follow
    else
        docker compose --file "$COMPOSE_FILE" logs --follow
    fi
    echo -e "\n"
}

# Recreate the init container
cmd_reinit() {
    check_docker

    # create and start containers
    echo -e "\n"
    docker compose --file "$COMPOSE_FILE" up --build -d --force-recreate typesense-init

    echo -e "\n✅  Init container recreated"
    echo -e "\n"
}

# Show usage information
cmd_help() {
    cat <<EOF
Usage: $(basename "$0") <command> [options]

Commands:
    up          Bring up the Docker Compose stack
    down        Bring down the Docker Compose stack (removes volumes)
    ps          Show status of all containers
    logs        Show logs for all containers (or specify service name)
    reinit      Recreate the typesense-init container
    help        Show this help message

Examples:
    $(basename "$0") up
    $(basename "$0") down
    $(basename "$0") ps
    $(basename "$0") logs
    $(basename "$0") logs typesense-init
    $(basename "$0") reinit

EOF
}

# Main command dispatcher
case "${1:-}" in
    up)
        cmd_up
        ;;
    down)
        cmd_down
        ;;
    ps)
        cmd_ps
        ;;
    logs)
        shift
        cmd_logs "$@"
        ;;
    reinit)
        cmd_reinit
        ;;
    help|--help|-h)
        cmd_help
        ;;
    "")
        echo -e "\n⚠️  No command specified\n"
        cmd_help
        exit 1
        ;;
    *)
        echo -e "\n⚠️  Unknown command: $1\n"
        cmd_help
        exit 1
        ;;
esac
