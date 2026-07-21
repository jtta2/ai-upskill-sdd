# Actionable Implementation Tasks

## Phase 1: Backend Core & Infrastructure (C#)
- [ ] **Task B1**: Create the `.NET 8` Solution named `LogAnalyzer.sln`.
- [x] **Task B2**: Create `LogAnalyzer.Core` class library and add `Entities/LogEntry.cs` with fields matching the Jira log sample.
- [x] **Task B3**: Define `Interfaces/ILogRepository.cs` and `Interfaces/IAIService.cs` in the Core project.
- [x] **Task B4**: Create `LogAnalyzer.Infrastructure` and implement `LogDbContext.cs` using EF Core.
- [x] **Task B5**: Implement `Data/LogRepository.cs` with embedded seed data logic (populating the database with sample logs on startup).
- [x] **Task B6**: Implement `Services/AzureAIService.cs` to handle HTTP communication with the designated LLM endpoint.

## Phase 2: Backend WebAPI
- [x] **Task B7**: Create `LogAnalyzer.WebAPI` project and reference Core and Infrastructure.
- [x] **Task B8**: Configure Dependency Injection, Connection Strings, and CORS policies in `Program.cs`.
- [x] **Task B9**: Create `Controllers/ChatController.cs` exposing the `POST /api/chat/ask` endpoint.

## Phase 3: Frontend Setup & Components (React TS)
- [x] **Task F1**: Initialize a Vite + React TypeScript project in the `frontend/` folder.
- [x] **Task F2**: Implement `src/services/api.ts` to connect to the C# backend API.
- [x] **Task F3**: Create `src/components/ChatWindow.tsx` for displaying message history.
- [x] **Task F4**: Create `src/components/ChatInput.tsx` for user message input and loading state management.
- [x] **Task F5**: Assemble everything in `src/App.tsx` and apply basic layouts in `src/index.css`.
