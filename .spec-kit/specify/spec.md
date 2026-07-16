# Functional Specification: IT Helpdesk Log Analyzer Chatbot

## Project Overview
The objective is to build a Proof of Concept (PoC) application consisting of a React frontend and a C# backend. The system allows users to upload system logs (CSV/Structured data) and interact with an AI Chatbot to analyze, filter, and extract insights from those logs.

## Target User Scenario
An IT Support Engineer needs to quickly review thousands of system log rows to find critical errors, security warnings, or system drift. Instead of writing complex SQL queries, the engineer asks natural language questions to the Chatbot.

## Core Capabilities
1. **Log Data Storage**: A pre-seeded SQL Server table containing fields: `Timestamp`, `SystemName`, `Component`, `LogLevel`, `CorrelationId`, `UserId`, and `Message`.
2. **Chat Interface**: A clean, single-page React UI with a chat input and a scrollable message history.
3. **AI Query Execution**: The C# backend receives the chat question, routes it to the LLM along with necessary data context (or translates it to filtered queries), and returns a precise answer.

## Functional Requirements

### FR-01: Log Management
- The backend must provide a mechanism to read and seed the sample log data into the SQL database.
- The data fields must match the template provided in the AI initiative guide.

### FR-02: Chat API Endpoint
- **Endpoint**: `POST /api/chat/ask`
- **Payload**: `{ "message": "string" }`
- **Response**: `{ "answer": "string" }`

### FR-03: Chat Frontend UI
- Display a header titled "IT Helpdesk Log Analyzer".
- Provide a message box showing the conversation history.
- Provide a bottom input field with a "Send" button.
- Disable input while the AI response is loading.
