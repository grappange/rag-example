using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using RagExample.Configuration;

namespace RagExample.Services;

/// <summary>
/// Embeddings via de lokale Ollama-server (POST /api/embeddings).
/// </summary>
public sealed class OllamaEmbeddingService : IEmbeddingService
{
    private readonly HttpClient _http;
    private readonly RagOptions _options;

    public OllamaEmbeddingService(HttpClient http, IOptions<RagOptions> options)
    {
        _http = http;
        _options = options.Value;
    }

    public async Task<float[]> EmbedAsync(string text, CancellationToken ct = default)
    {
        var payload = new { model = _options.Ollama.EmbeddingModel, prompt = text };

        using var response = await _http.PostAsJsonAsync("/api/embeddings", payload, ct);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<EmbeddingResponse>(cancellationToken: ct)
                   ?? throw new InvalidOperationException("Leeg embedding-antwoord van Ollama.");

        if (body.Embedding is null || body.Embedding.Length == 0)
            throw new InvalidOperationException($"Ollama gaf geen embedding terug voor model '{_options.Ollama.EmbeddingModel}'.");

        return body.Embedding;
    }

    private sealed record EmbeddingResponse(
        [property: JsonPropertyName("embedding")] float[]? Embedding);
}
