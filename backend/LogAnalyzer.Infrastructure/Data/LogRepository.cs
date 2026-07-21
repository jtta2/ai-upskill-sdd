using LogAnalyzer.Core.Entities;
using LogAnalyzer.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogAnalyzer.Infrastructure.Data;

public class LogRepository(LogDbContext dbContext) : ILogRepository
{
    private static readonly IReadOnlyList<LogEntry> DefaultSeedLogs =
    [
        new LogEntry
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Timestamp = DateTimeOffset.Parse("2025-05-01T00:04:59+00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
            SystemName = "casb-proxy-01",
            Component = "CASB",
            LogLevel = "WARN",
            CorrelationId = "7de471bf...",
            UserId = "SUPERUSER",
            Message = "Inactive Box OAuth grant for eroberts not revoked — 5 days since last use"
        },
        new LogEntry
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Timestamp = DateTimeOffset.Parse("2025-05-01T00:11:57+00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
            SystemName = "vuln-scan-01",
            Component = "Vulnerability_Scanner",
            LogLevel = "INFO",
            CorrelationId = "ad6853c6...",
            UserId = "ADMIN",
            Message = "Asset WKSTN-117 (172.16.254.3) added to scan scope — first scan scheduled"
        },
        new LogEntry
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Timestamp = DateTimeOffset.Parse("2025-05-01T00:34:30+00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
            SystemName = "pam-vault-01",
            Component = "PAM",
            LogLevel = "INFO",
            CorrelationId = "caf3c094...",
            UserId = "STANDARDUSER",
            Message = "User glopez checked out credentials for SRV-APP-02 from VAULT-PROD-01 — session initiated"
        },
        new LogEntry
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Timestamp = DateTimeOffset.Parse("2025-05-01T00:49:26+00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
            SystemName = "casb-proxy-02",
            Component = "CASB",
            LogLevel = "ERROR",
            CorrelationId = "31286dc8...",
            UserId = "ADMIN",
            Message = "Threat score enrichment service unreachable — 15 CASB decisions made without TI context"
        }
    ];

    public async Task<IEnumerable<LogEntry>> GetLogsAsync()
    {
        await EnsureSeedDataAsync();

        return await dbContext.LogEntries
            .AsNoTracking()
            .OrderBy(logEntry => logEntry.Timestamp)
            .ToListAsync();
    }

    public async Task SeedLogsAsync(IEnumerable<LogEntry> logs)
    {
        ArgumentNullException.ThrowIfNull(logs);

        var logEntries = logs
            .Where(logEntry => logEntry is not null)
            .Select(NormalizeLogEntry)
            .ToList();

        if (logEntries.Count == 0)
        {
            return;
        }

        var existingIds = await dbContext.LogEntries
            .Select(logEntry => logEntry.Id)
            .ToListAsync();

        var existingIdSet = existingIds.ToHashSet();
        var newEntries = logEntries
            .Where(logEntry => !existingIdSet.Contains(logEntry.Id))
            .ToList();

        if (newEntries.Count == 0)
        {
            return;
        }

        await dbContext.LogEntries.AddRangeAsync(newEntries);
        await dbContext.SaveChangesAsync();
    }

    private async Task EnsureSeedDataAsync()
    {
        if (await dbContext.LogEntries.AnyAsync())
        {
            return;
        }

        await SeedLogsAsync(DefaultSeedLogs);
    }

    private static LogEntry NormalizeLogEntry(LogEntry logEntry)
    {
        if (logEntry.Id == Guid.Empty)
        {
            logEntry.Id = Guid.NewGuid();
        }

        return logEntry;
    }
}