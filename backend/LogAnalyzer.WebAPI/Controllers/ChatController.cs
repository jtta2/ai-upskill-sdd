using Microsoft.AspNetCore.Mvc;

namespace LogAnalyzer.WebAPI.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        [HttpPost("ask")]
        public IActionResult Ask([FromBody] ChatRequest request)
        {
            return Ok(new { answer = $"Skeleton Response to: {request.Message}" });
        }
    }

    public class ChatRequest { public string Message { get; set; } = string.Empty; }
}
