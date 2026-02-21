$repo = git rev-parse --show-toplevel
$bash = "C:\Program Files\Git\bin\bash.exe"
$cmd_worker = "dotnet watch run --project ./detailed-guides/temporal/examples/example-2/Example2.Worker/Example2.Worker.csproj"
$cmd_api = "dotnet watch run --project ./detailed-guides/temporal/examples/example-2/Example2.Api/Example2.Api.csproj"
$cmd_sqlite = "sqlite3 ./detailed-guides/temporal/examples/example-2/Example2.Worker/customers.db"

wt `
  -d $repo `
  `; new-tab -p "Git Bash" -d $repo --title "Example 2 - SQLite" $bash -lc $cmd_sqlite `
  `; split-pane -H -p "Git Bash" -d $repo --title "Example 2 - API" $bash -lc $cmd_api `
  `; split-pane -V -p "Git Bash" -d $repo --title "Example 2 - Worker" $bash -lc $cmd_worker