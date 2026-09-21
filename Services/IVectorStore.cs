using RagExample.Models;

namespace RagExample.Services;

public interface IVectorStore
{
    Task EnsureIndexAsync(CancellationToken ct = default);
    Task<long> CountAsync(CancellationToken ct = default);
    Task IndexAsync(IEnumerable<RagDocument> documents, CancellationToken ct = default);
    Task<IReadOnlyList<SearchResult>> SearchAsync(float[] queryVector, int topK, CancellationToken ct = default);
}

/// <summary>Eén zoekresultaat: het document plus de relevantiescore.</summary>
public sealed record SearchResult(RagDocument Document, double Score);
