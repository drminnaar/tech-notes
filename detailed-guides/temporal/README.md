![Cover Image](temporal.png)

# Temporal Guide

See [🚀 /quickstart-guides/temporal/](/quickstart-guides/temporal/) for a guide to run Temporal Server in your local development environment.

## Topics

- 👋 [Introduction](./introduction/introduction.md)

  A high-level overview of Temporal, its core concepts, and how it enables reliable, scalable workflow orchestration for distributed systems.

- 🏛️ [Architecture](./architecture/architecture.md)
  
  A detailed explanation of Temporal's architecture, including its components, data flow, and how it achieves durability, scalability, and fault tolerance.

## Examples

All examples can be found at [./examples/](./examples/).

- ✨ [Example 1 - Hello World](./examples/example-1/)
  
  A simple workflow demonstrating Temporal's basic functionality. The "Hello World" example shows how to define a workflow, and execute it using Temporal's SDK. It serves as an introduction to Temporal's programming model and workflow execution.

- ✨ [Example 2 - Suspend/Reinstate Customer](./examples/example-2/)
  
  A .NET 10 / C# 14 solution demonstrating a **manager-approval workflow** for customer suspension and reinstatement. Instead of a backoffice operator's request going straight to the database, it enters a durable workflow where a manager must approve or reject it within 48 hours. The workflow survives worker restarts, handles timeouts gracefully, and provides full auditability via Temporal's event history.

---
