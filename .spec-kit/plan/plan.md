# Technical Implementation Plan

## Solution Structure (C# .NET 8)
We will create a single .NET Solution named `LogAnalyzer.sln` containing three projects:

1. **LogAnalyzer.Core (Class Library)**
   - `Entities/LogEntry.cs` (Domain model matching the CSV schema)
   - `Interfaces/ILogRepository.cs` (Data access abstraction)
   - `Interfaces/IAIService.cs` (AI chat processing abstraction)

2. **LogAnalyzer.Infrastructure (Class Library)**
   - `Data/LogDbContext.cs` (EF Core context with In-Memory or Local SQL Server support)
   - `Data/LogRepository.cs` (Implements ILogRepository, includes data seeding logic)
   - `Services/AzureAIService.cs` (Implements IAIService using HttpClient to connect to the infrastructure LLM endpoint)

3. **LogAnalyzer.WebAPI (Web API)**
   - `Controllers/ChatController.cs` (Exposes `POST /api/chat/ask`)
   - `Program.cs` (Configures Dependency Injection, EF Core, and CORS for React)

## Frontend Structure (React TS)
A lightweight Vite + TypeScript boilerplate with standard CSS:
- `src/components/ChatWindow.tsx` (Handles message history display)
- `src/components/ChatInput.tsx` (Input field and Send button)
- `src/services/api.ts` (Fetch wrapper to communicate with the C# WebAPI)
- `src/App.tsx` (Main layout container)
- `src/index.css` (Basic styling for chat bubbles and layout)
