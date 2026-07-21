using LogAnalyzer.Core.Entities;

namespace LogAnalyzer.Core.Interfaces;

/// <summary>
/// Data access abstraction for retrieving and seeding log entries.
/// </summary>
public interface ILogRepository
{
    Task<IEnumerable<LogEntry>> GetLogsAsync();

    Task SeedLogsAsync(IEnumerable<LogEntry> logs);
}
