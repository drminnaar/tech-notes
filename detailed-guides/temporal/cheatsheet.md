# 💀 Temporal CLI Cheatsheet

The [Temporal CLI](https://docs.temporal.io/cli) is a powerful command-line tool for interacting with the Temporal Service.

> [!IMPORTANT]
> Conventions
> - `[]` used to indicate optional argument  
> &nbsp;

```bash
# get help
temporal help

# get version
temporal --version

# list workflows
temporal workflow list

# Show a Workflow Execution's Event History.
temporal workflow show --workflow-id 123 [--output json]

# Display information about a specific Workflow Execution
temporal workflow describe --workflow-id 123 [--output json]

# start workflow - note input must be json
temporal workflow start \
    --type YourWorkflow \
    --task-queue your-workflow-queue \
    --workflow-id your-workflow \
    --input '{"some-key": "some-value"}'
```