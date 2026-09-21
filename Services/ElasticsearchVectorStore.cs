using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RagExample.Configuration;
using RagExample.Models;

namespace RagExample.Services;

/// <summary>
/// Vector store bovenop Elasticsearch. Gebruikt een dense_vector-veld met
/// cosine-similarity en kNN-search voor het ophalen van relevante documenten.
/// </summary>
public sealed class ElasticsearchVectorStore : IVectorStore
{
    private readonly ElasticsearchClient _client;
    private readonly RagOptions _options;
    private readonly ILogger<ElasticsearchVectorStore> _logger;

    public ElasticsearchVectorStore(
        ElasticsearchClient client,
        IOptions<RagOptions> options,
        ILogger<ElasticsearchVectorStore> logger)
    {
        _client = client;
        _options = options.Value;
        _logger = logger;
    }

    private string Index => _options.Elasticsearch.IndexName;

    public async Task EnsureIndexAsync(CancellationToken ct = default)
    {
        var exists = await _client.Indices.ExistsAsync(Index, ct);
        if (exists.Exists)
        {
            _logger.LogInformation("Index '{Index}' bestaat al.", Index);
            return;
        }

        _logger.LogInformation("Index '{Index}' wordt aangemaakt...", Index);

        var response = await _client.Indices.CreateAsync<RagDocument>(Index, c => c
            .Mappings(m => m
                .Properties(p => p
                    .Keyword(d => d.Title)
                    .Text(d => d.Content)
                    .Keyword(d => d.Source)
                    .DenseVector(d => d.Embedding!, dv => dv
                        .Dims(_options.Ollama.EmbeddingDimensions)
                        .Index(true)
                        .Similarity(DenseVectorSimilarity.Cosine)))), ct);

        if (!response.IsValidResponse)
            throw new InvalidOperationException($"Aanmaken index mislukt: {response.DebugInformation}");
    }

    public async Task<long> CountAsync(CancellationToken ct = default)
    {
        var response = await _client.CountAsync<RagDocument>(c => c.Indices(Index), ct);
        return response.IsValidResponse ? response.Count : 0;
    }

    public async Task IndexAsync(IEnumerable<RagDocument> documents, CancellationToken ct = default)
    {
        var response = await _client.BulkAsync(b => b
            .Index(Index)
            .IndexMany(documents, (op, doc) => op.Id(doc.Id)), ct);

        if (response.Errors)
        {
            var first = response.ItemsWithErrors.FirstOrDefault();
            throw new InvalidOperationException($"Indexeren mislukt: {first?.Error?.Reason ?? response.DebugInformation}");
        }

        // Direct doorzoekbaar maken (handig voor een demo; niet voor bulk-productie).
        await _client.Indices.RefreshAsync(Index, ct);
    }

    public async Task<IReadOnlyList<SearchResult>> SearchAsync(float[] queryVector, int topK, CancellationToken ct = default)
    {
        var response = await _client.SearchAsync<RagDocument>(s => s
            .Index(Index)
            .Knn(k => k
                .Field(d => d.Embedding!)
                .QueryVector(queryVector)
                .k(topK)
                .NumCandidates(Math.Max(topK * 10, 50)))
            .Source(new Elastic.Clients.Elasticsearch.Core.Search.SourceConfig(
                new Elastic.Clients.Elasticsearch.Core.Search.SourceFilter { Excludes = "embedding" })), ct);

        if (!response.IsValidResponse)
            throw new InvalidOperationException($"Zoeken mislukt: {response.DebugInformation}");

        return response.Hits
            .Where(h => h.Source is not null)
            .Select(h => new SearchResult(h.Source!, h.Score ?? 0d))
            .ToList();
    }
}
