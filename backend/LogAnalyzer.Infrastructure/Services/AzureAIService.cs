using System.Text;
using System.Text.Json;
using LogAnalyzer.Core.Entities;
using LogAnalyzer.Core.Interfaces;

namespace LogAnalyzer.Infrastructure.Services;

public class AzureAIService(HttpClient httpClient, string apiKey, string endpointUrl) : IAIService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public async Task<string> AskQuestionAsync(string userQuestion, IEnumerable<LogEntry> logContext)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userQuestion);
        ArgumentNullException.ThrowIfNull(logContext);

        var contextSnapshot = logContext.ToArray();
        var systemPrompt =
            "You are an expert IT Data Analyst specializing in log investigation, security analysis, incident triage, and operational diagnostics. " +
            "Use only the provided log context when answering unless additional general reasoning is needed. " +
            "Be precise, concise, and evidence-based. If the logs do not support a confident answer, state that clearly. " +
            "When relevant, cite key rows, patterns, timestamps, systems, components, and log levels.";

        var userPrompt = BuildUserPrompt(userQuestion, contextSnapshot);
        var requestPayload = BuildRequestPayload(systemPrompt, userPrompt);
        using var request = new HttpRequestMessage(HttpMethod.Post, endpointUrl)
        {
            Content = new StringContent(JsonSerializer.Serialize(requestPayload, JsonOptions), Encoding.UTF8, "application/json")
        };

        //request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {apiKey}");
        request.Headers.TryAddWithoutValidation("Api-Key", apiKey);


        using var response = await httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"The AI endpoint returned {(int)response.StatusCode} ({response.ReasonPhrase}). {responseContent}");
        }

        return ExtractAnswer(responseContent);
    }

    private static object BuildRequestPayload(string systemPrompt, string userPrompt) => new
    {
        messages = new[]
        {
            new { role = "system", content = systemPrompt },
            new { role = "user", content = userPrompt }
        }
    };

    private static string BuildUserPrompt(string userQuestion, IReadOnlyCollection<LogEntry> logContext)
    {
        var serializedContext = JsonSerializer.Serialize(logContext, JsonOptions);

        return
            $"User question:\n{userQuestion}\n\n" +
            $"Log context (JSON):\n{serializedContext}";
    }

    private static string ExtractAnswer(string responseContent)
    {
        if (string.IsNullOrWhiteSpace(responseContent))
        {
            return string.Empty;
        }

        using var document = JsonDocument.Parse(responseContent);
        var root = document.RootElement;

        if (TryReadString(root, "answer", out var answer) ||
            TryReadString(root, "content", out answer) ||
            TryReadString(root, "message", out answer) ||
            TryReadNestedMessageContent(root, out answer))
        {
            return answer;
        }

        return root.ValueKind == JsonValueKind.String ? root.GetString() ?? string.Empty : responseContent;
    }

    private static bool TryReadString(JsonElement element, string propertyName, out string value)
    {
        if (element.TryGetProperty(propertyName, out var property) && property.ValueKind == JsonValueKind.String)
        {
            value = property.GetString() ?? string.Empty;
            return true;
        }

        value = string.Empty;
        return false;
    }

    private static bool TryReadNestedMessageContent(JsonElement element, out string value)
    {
        if (element.TryGetProperty("choices", out var choices) &&
            choices.ValueKind == JsonValueKind.Array &&
            choices.GetArrayLength() > 0 &&
            choices[0].TryGetProperty("message", out var message) &&
            message.TryGetProperty("content", out var content) &&
            content.ValueKind == JsonValueKind.String)
        {
            value = content.GetString() ?? string.Empty;
            return true;
        }

        value = string.Empty;
        return false;
    }
}