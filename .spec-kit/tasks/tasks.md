# Actionable Implementation Tasks

## Phase 1: Backend Core & Infrastructure (C#)
- [ ] **Task B1**: Create the `.NET 8` Solution named `LogAnalyzer.sln`.
- [ ] **Task B2**: Create `LogAnalyzer.Core` class library and add `Entities/LogEntry.cs` with fields matching the Jira log sample.
- [ ] **Task B3**: Define `Interfaces/ILogRepository.cs` and `Interfaces/IAIService.cs` in the Core project.
- [ ] **Task B4**: Create `LogAnalyzer.Infrastructure` and implement `LogDbContext.cs` using EF Core.
- [ ] **Task B5**: Implement `Data/LogRepository.cs` with embedded seed data logic (populating the database with sample logs on startup).
- [ ] **Task B6**: Implement `Services/AzureAIService.cs` to handle HTTP communication with the designated LLM endpoint.

## Phase 2: Backend WebAPI
- [ ] **Task B7**: Create `LogAnalyzer.WebAPI` project and reference Core and Infrastructure.
- [ ] **Task B8**: Configure Dependency Injection, Connection Strings, and CORS policies in `Program.cs`.
- [ ] **Task B9**: Create `Controllers/ChatController.cs` exposing the `POST /api/chat/ask` endpoint.

## Phase 3: Frontend Setup & Components (React TS)
- [ ] **Task F1**: Initialize a Vite + React TypeScript project in the `frontend/` folder.
- [ ] **Task F2**: Implement `src/services/api.ts` to connect to the C# backend API.
- [ ] **Task F3**: Create `src/components/ChatWindow.tsx` for displaying message history.
- [ ] **Task F4**: Create `src/components/ChatInput.tsx` for user message input and loading state management.
- [ ] **Task F5**: Assemble everything in `src/App.tsx` and apply basic layouts in `src/index.css`.
