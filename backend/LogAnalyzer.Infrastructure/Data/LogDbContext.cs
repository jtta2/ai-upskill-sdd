using LogAnalyzer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogAnalyzer.Infrastructure.Data;

public class LogDbContext(DbContextOptions<LogDbContext> options) : DbContext(options)
{
    public DbSet<LogEntry> LogEntries => Set<LogEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LogEntry>(entity =>
        {
            entity.HasKey(logEntry => logEntry.Id);

            entity.Property(logEntry => logEntry.Timestamp)
                .IsRequired();

            entity.Property(logEntry => logEntry.SystemName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(logEntry => logEntry.Component)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(logEntry => logEntry.LogLevel)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(logEntry => logEntry.CorrelationId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(logEntry => logEntry.UserId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(logEntry => logEntry.Message)
                .IsRequired()
                .HasMaxLength(2000);
        });
    }
}