# Requirements Clarification Log

## Q1: How should the LLM access the log data? (RAG vs. Text-to-SQL vs. Direct Context)
- **Question**: Should we use a Vector Database (RAG), dynamic Text-to-SQL generation, or pass the relevant logs directly in the prompt context?
- **Decision**: Since this is a minimal PoC with a small sample dataset, we will retrieve the log rows from SQL Server via EF Core, format them as text, and inject them directly into the LLM context window along with the user's question. This avoids the cost and complexity of setting up a vector database or risking destructive dynamic SQL queries.

## Q2: How will the system connect to the AI model?
- **Question**: What infrastructure are we using to connect to the LLM?
- **Decision**: The backend will use standard .NET `HttpClient` to call the provided infrastructure API endpoint using the designated API Key. We will use standard system prompts to instruct the LLM to behave as an IT data analyst.

## Q3: How will the database be populated with the sample data?
- **Question**: Do we need a file upload button on the UI, or should the data be seeded automatically?
- **Decision**: To keep the React frontend as simple as possible, the sample log data will be hardcoded as a seed dataset inside the C# `LogDbContext` (Entity Framework Core). When the application starts, it will automatically check and populate the local SQL database if it is empty. No frontend upload UI is required.

## Q4: What happens if the log volume exceeds the LLM context window?
- **Question**: How do we handle context limits if there are too many logs?
- **Decision**: For this iteration, the seed data will be limited to 50-100 rows, which easily fits within any standard LLM context window (e.g., GPT-4). The backend will also apply a default `Take(100)` limitation on the log query to prevent context overflow.
