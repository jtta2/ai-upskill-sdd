# Project Constitution

## Global Technical Stack
- **Backend**: C# .NET 8 Web API using Clean Architecture.
- **Frontend**: React with TypeScript (Vite-based setup).
- **Styling**: Standard CSS only. No Tailwind CSS or external styling frameworks allowed.
- **Database**: Local Microsoft SQL Server / Express.
- **AI Integration**: Azure AI Foundry or hosted LLM endpoints using standard HttpClient or Semantic Kernel.

## Backend Architecture Requirements
The C# solution must strictly follow Clean Architecture principles, separated into three distinct projects:
1. **LogAnalyzer.WebAPI**: Entry point, controllers, dependency injection configuration, and HTTP middleware.
2. **LogAnalyzer.Core**: Core business logic, interfaces, domain entities, and data contracts. No dependencies on database or external APIs.
3. **LogAnalyzer.Infrastructure**: Data access (Entity Framework Core), repository implementations, database context, and external AI service wrappers.

## Code Quality and Rules
- All code, variables, and documentation must be written in English.
- Avoid monolithic functions; enforce single-responsibility principles.
- Use native async/await for all I/O and network operations.
- The AI should favor generating clear, self-explanatory code over complex clever hacks.
