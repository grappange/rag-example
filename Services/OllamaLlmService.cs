using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using RagExample.Configuration;

namespace RagExample.Services;

/// <summary>
/// Tekstgeneratie via de lokale Ollama-server (POST /api/chat, non-streaming).
/// </summary>
public sealed class OllamaLlmService : ILlmService
{
    private readonly HttpClient _http;
    private readonly RagOptions _options;

    public OllamaLlmService(HttpClient http, IOptions<RagOptions> options)
    {
        _http = http;
        _options = options.Value;
    }

    public async Task<string> CompleteAsync(string systemPrompt, string userPrompt, CancellationToken ct = default)
    {
        var payload = new
        {
            model = _options.Ollama.ChatModel,
            stream = false,
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userPrompt }
            }
        };

        using var response = await _http.PostAsJsonAsync("/api/chat", payload, ct);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<ChatResponse>(cancellationToken: ct)
                   ?? throw new InvalidOperationException("Leeg chat-antwoord van Ollama.");

        return body.Message?.Content?.Trim() ?? string.Empty;
    }

    private sealed record ChatResponse(
        [property: JsonPropertyName("message")] ChatMessage? Message);

    private sealed record ChatMessage(
        [property: JsonPropertyName("content")] string? Content);
}
