using LogAnalyzer.Core.Entities;

namespace LogAnalyzer.Core.Interfaces;

/// <summary>
/// AI processing abstraction for answering user questions using log context.
/// </summary>
public interface IAIService
{
    Task<string> AskQuestionAsync(string userQuestion, IEnumerable<LogEntry> logContext);
}
