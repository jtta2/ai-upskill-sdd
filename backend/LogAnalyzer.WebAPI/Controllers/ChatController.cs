using LogAnalyzer.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogAnalyzer.WebAPI.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController(ILogRepository logRepository, IAIService aiService) : ControllerBase
{
    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] ChatRequest request)
    {
        var logs = await logRepository.GetLogsAsync();
        var aiResponse = await aiService.AskQuestionAsync(request.Message, logs);

        return Ok(new { answer = aiResponse });
    }
}

public class ChatRequest
{
    public string Message { get; set; } = string.Empty;
}
