using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Website.ViewModels.Visitor;

namespace Website.Services
{
    public class GeminiService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public GeminiService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<AIInsightViewModel> GenerateInsight(string text)
        {
            var payload = new
            {
                contents = new[]
                {
                new { parts = new[] { new { text } } }
            }
            };

            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _config["Gemini:ApiKey"]);

            var response = await _http.PostAsync(
                "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AIInsightViewModel>(json);
        }
    }
}
