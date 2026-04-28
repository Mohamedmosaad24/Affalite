using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace AffaliteBLL.Services
{
    public class ClaudeAiClient
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;
        private readonly string _model;

        private const string ApiUrl = "https://api.anthropic.com/v1/messages";

        public ClaudeAiClient(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["Claude:ApiKey"]
                ?? throw new InvalidOperationException("Claude:ApiKey missing in appsettings.");
            _model = config["Claude:Model"] ?? "claude-3-5-sonnet-latest";
        }

        public Task<string> SendMessageAsync(string prompt)
        {
            return SendMessageAsync(prompt, null);
        }

        public async Task<string> SendMessageAsync(string prompt, string? systemPrompt)
        {
            var request = new ClaudeRequest
            {
                Model = _model,
                MaxTokens = 1500,
                System = systemPrompt,
                Messages = new List<ClaudeMessage>
                {
                    new() { Role = "user", Content = prompt }
                }
            };

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, ApiUrl);
            httpRequest.Headers.Add("x-api-key", _apiKey);
            httpRequest.Headers.Add("anthropic-version", "2023-06-01");
            httpRequest.Content = JsonContent.Create(request, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var response = await _http.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Claude API error {(int)response.StatusCode}: {errorBody}",
                    null,
                    response.StatusCode);
            }

            var result = await response.Content.ReadFromJsonAsync<ClaudeResponse>();
            var firstTextChunk = result?.Content?.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c.Text))?.Text;
            return firstTextChunk
                ?? throw new HttpRequestException(
                    "Claude API returned an empty response body.",
                    null,
                    HttpStatusCode.BadGateway);
        }

        // ── Private models ──────────────────────────────────────────────

        private class ClaudeRequest
        {
            [JsonPropertyName("model")] public string Model { get; set; } = string.Empty;
            [JsonPropertyName("max_tokens")] public int MaxTokens { get; set; }
            [JsonPropertyName("system")] public string? System { get; set; }
            [JsonPropertyName("messages")] public List<ClaudeMessage> Messages { get; set; } = new();
        }

        private class ClaudeMessage
        {
            [JsonPropertyName("role")] public string Role { get; set; } = string.Empty;
            [JsonPropertyName("content")] public string Content { get; set; } = string.Empty;
        }

        private class ClaudeResponse
        {
            [JsonPropertyName("content")] public List<ClaudeContent>? Content { get; set; }
        }

        private class ClaudeContent
        {
            [JsonPropertyName("text")] public string? Text { get; set; }
        }
    }
}