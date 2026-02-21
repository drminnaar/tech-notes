
#!/bin/bash

# Check if sqlite3 is installed
if ! command -v sqlite3 &> /dev/null; then
	echo "Error: sqlite3 is not installed. Please install sqlite3 and try again."
	exit 1
fi

# Set the database file name (change as needed)
DB_NAME="customers.db"

# Get the directory of the script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Full path to the database file
DB_PATH="$SCRIPT_DIR/$DB_NAME"

# Open sqlite3 with the specified database file and display results in a readable format with
# headers and columns. The -header option displays column names, and the -column option formats
# the output in columns.
echo "Opening SQLite database at: $DB_PATH"
sqlite3 -header -column "$DB_PATH"
