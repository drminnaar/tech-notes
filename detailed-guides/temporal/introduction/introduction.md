# Temporal - Introduction

In this chapter, you will work toward the goal of being able to configure an environment for developing Temporal Applications.

Specifically, by the end of this chapter, you will:

- Find Temporal’s documentation and code samples
- Run a Temporal Service for development use
- Install a Temporal SDK
- Install the temporal CLI
- Understand the different options for running Temporal in production

## 🌟 Overview

> [!IMPORTANT]
> &nbsp;  
> ☝️🤓 Temporal is not just a "workflow engine", It's a Durable Execution Platform.
> Think of Temporal as a "fault-tolerant sidekick" for your code. It ensures that your application logic runs to completion, regardless of network reliability, server crashes, or even months-long wait times.
> &nbsp;  
> &nbsp;  

Temporal is an **open-source durable execution platform**. This means once your code starts running, the system guarantees it will eventually finish, even if processes crash, machines die, or deployments happen in between. It is designed to make building reliable, fault-tolerant applications easier. In typical distributed systems, if a server crashes or a network call fails, there would need to be sophisticated mechanisms/code in place to help ensure resiliency. For example, state machines, message queues, and database records. Temporal abstracts all that complexity away, thereby  allowing one to write code in a way that handles errors more reliable and seamless way. Code can be written in languages like Go, TypeScript/JavaScript, Python, Java, PHP, .NET, etc.

Below are some examples that Temporal can automatically handle in a reliable fashion:

- Crashes of servers / workers / pods  
- Network blips and timeouts  
- Service outages  
- Redeployments  
- Scaling up & down

---

## 🧱 Core Building Blocks

Below is a table summary of the Temporal building blocks:

| Concept              | What it is                                                                    | Analogy / Typical responsibility                       | Can fail / retry?       |
| -------------------- | ----------------------------------------------------------------------------- | ------------------------------------------------------ | ----------------------- |
| **Workflow**         | Your durable business logic function. Deterministic. State is auto-persisted. | The "saga" / process / state machine / orchestration   | No — it is replayed     |
| **Activity**         | Non-deterministic side-effect code (API calls, DB writes, sending emails…)    | The "unsafe" steps that talk to the outside world      | Yes — automatic retries |
| **Worker**           | Process that hosts your Workflow/Activity code and polls for tasks            | The runtime/engine that actually executes your code    | Yes — many workers      |
| **Task Queue**       | Routing mechanism — like a work queue with smart dispatching                  | Decides which worker gets which Workflow/Activity task | —                       |
| **Temporal Service** | The brain: event store, history, matching, visibility                         | PostgreSQL/Cassandra/… + coordination layer            | Highly available        |

The two primary components to take note of:

- **Workflows (The Orchestrator):**
  
  Conceptually, a workflow is a sequence of steps. In Temporal, this is where your business logic lives, and your code defines those steps. It must be **deterministic**. You write it in standard code (Java, Go, Python, TypeScript, C# etc.). Because Temporal uses "Event Sourcing," it can "replay" your workflow code to reconstruct its state after a failure.

- **Activities (The Workers):**
  
  These are where you perform non-deterministic actions: calling an API, querying a database, or sending an email. If an Activity fails, Temporal automatically retries it based on a policy you define (e.g., exponential backoff).

> [!IMPORTANT]
> &nbsp;  
> ☝️🤓 When building applications using Temporal, follow this one simple rule:  
> &nbsp;  
> - Workflows are **deterministic (produce same results upon replay)**
> - Activities are non-deterministic and should be idempotent (safe to execute multiple times producing the same result where system is affected only once)
> &nbsp;  
> &nbsp;

---

## 🎯 The Core Paradigm: Durable Execution

The "magic" of Temporal is **Durable Execution**. When you run a function in Temporal (called a **Workflow**), the platform guarantees it will run to completion, regardless of whether the process crashes, the server is rebooted, or a downstream API is down for three days.

Temporal achieves this by tracking the state of your code. If the worker running your code dies, another worker picks up exactly where it left off, with all local variables and call stacks intact.

Below is a simplified summary of how the "magic" happens:

1. Start a **Workflow** → Temporal records "Workflow started" event  
2. Workflow code calls an **Activity** → Temporal records "Activity scheduled"  
3. Worker picks up the Activity → executes your real code (e.g. HTTP call)  
4. Activity succeeds/fails → result is recorded as event  
5. Workflow code continues (or retries, sleeps, handles compensation…)  
6. Server crashes mid-way → new worker resumes → Temporal **replays** the history from events → code runs again deterministically → picks up exactly where it left off

Temporal doesn't store your actual memory heap in a database. Instead, it stores an **Event History**.

1. When a Workflow executes an Activity, Temporal records the *result* in its database.
2. If the process crashes and needs to recover, Temporal restarts the code from the beginning.
3. When the code reaches a point where it previously called an Activity, Temporal says: *"Wait, don't actually call that API again. I have the result right here in my history. Just use this value."*
4. This "Replay" mechanism allows the code to jump back to its exact state before the crash.

---

## 📐 Architecture Overview

A Temporal deployment consists of two distinct parts:

- **The Temporal Cluster:** A set of backend services (Frontend, History, Matching, and Worker) backed by a database (PostgreSQL, MySQL, or Cassandra). It manages the state, queues, and timers.

- **The Workers:** These are hosted by **you**. They poll the Cluster for tasks, execute your Workflow/Activity code, and send the results back. The Cluster never actually sees your code; it only sees the events and results.

---

## 🙌 Key Benefits in Practice

- **No more fragile cron jobs + reconciliation loops**  
- **No more "payment succeeded but fulfillment failed" ghost orders**  
- **No more complex state machines in databases**  
- **Built-in retries, timeouts, rate limiting, sagas / compensation**  
- **Signals** → you can update/pause/cancel running workflows (great for feature flags, customer support actions)  
- **Queries** → read current state without stopping anything  
- **Very good developer experience** — unit-testable, debugger-friendly  
- Scales to millions of active workflows  

---

## ⚖️ Why Developers Choose Temporal

- **Eliminate Boilerplate:** No more manual state machines, "zombie" cron jobs, or complex "Saga pattern" logic.
- **Long-Running Processes:** Workflows can sleep for minutes, months, or even years. Temporal handles the timers efficiently without keeping a thread open.
- **Observability:** The Temporal Web UI allows you to see the exact state of every execution, its history, and where it is currently blocked.
- **Reliability:** It was built by the creators of Uber’s Cadence and engineers from AWS (SQS/SWF) and Azure, specifically to handle massive scale and mission-critical reliability.

---

## 💼 Common Use Cases

- **Order Fulfillment:** Handling the complex state of payments, shipping, and cancellations.
- **Infrastructure Provisioning:** Orchestrating Terraform/Pulumi calls that might take hours or fail halfway through.
- **AI Pipelines:** Managing long-running model training or batch processing jobs.
- **Financial Transactions:** Ensuring money is never "lost" in a distributed system during a transfer.

---

## ☝️🤓 Notable Use Cases (circa 2025–2026)

- **NVIDIA** — GPU fleet orchestration & AI training workflows  
- **Salesforce** — migrating monoliths to reliable microservices  
- **Twilio** — communication orchestration  
- **Descript** — AI audio/video processing pipelines  
- Many fintechs (durable ledgers, payment flows), AI companies (agent loops, long-running inference chains), and startups replacing home-grown queue + state solutions

---

## 🚀 Getting Started (2026 recommendation)

The fastest path right now:

1. Go to https://learn.temporal.io/getting_started  
2. Pick your language (TypeScript/Python/Go are the most beginner-friendly)  
3. Run the hello-world style app locally (uses Temporal CLI + Docker)  
4. Then try Temporal Cloud free tier ($1000 credits) — zero ops  

Once comfortable, read:

- https://docs.temporal.io/workflows  
- https://docs.temporal.io/workflow-execution  
- The "What is Durable Execution?" article on the blog