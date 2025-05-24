using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MentalHealthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ChatbotController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiKey = configuration["Gemini:ApiKey"];
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskChatbot([FromBody] ChatRequest request)
        {
            string apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}";

            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = request.Content }
                        }
                    }
                }
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(apiUrl, httpContent);
                var resultJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, new { error = "Gemini API error", details = resultJson });
                }

                var responseObj = JsonSerializer.Deserialize<GeminiResponse>(resultJson);

                var reply = responseObj?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text ?? "No response received.";
                return Ok(new { reply });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal error", details = ex.Message });
            }
        }
    }

    public class ChatRequest
    {
        public string Content { get; set; }
    }

    // DTOs to match Gemini's response structure
    public class GeminiResponse
    {
        public Candidate[] Candidates { get; set; }
    }

    public class Candidate
    {
        public Content Content { get; set; }
    }

    public class Content
    {
        public Part[] Parts { get; set; }
    }

    public class Part
    {
        public string Text { get; set; }
    }
}
