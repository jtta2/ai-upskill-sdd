namespace LogAnalyzer.Core.Entities;

/// <summary>
/// Domain entity representing a single IT Helpdesk log record.
/// </summary>
public class LogEntry
{
    public Guid Id { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string SystemName { get; set; } = string.Empty;

    public string Component { get; set; } = string.Empty;

    public string LogLevel { get; set; } = string.Empty;

    public string CorrelationId { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}
