using Microsoft.Extensions.Options;
using RagExample.Configuration;
using RagExample.Models;

namespace RagExample.Services;

/// <summary>
/// De RAG-pijplijn: embed -> retrieve (kNN in Elasticsearch) -> augment -> generate (Ollama).
/// </summary>
public sealed class RagService
{
    private readonly IEmbeddingService _embeddings;
    private readonly IVectorStore _store;
    private readonly ILlmService _llm;
    private readonly RagOptions _options;

    public RagService(
        IEmbeddingService embeddings,
        IVectorStore store,
        ILlmService llm,
        IOptions<RagOptions> options)
    {
        _embeddings = embeddings;
        _store = store;
        _llm = llm;
        _options = options.Value;
    }

    /// <summary>Embed elk document en sla het op in de vector store.</summary>
    public async Task IngestAsync(IReadOnlyList<RagDocument> documents, CancellationToken ct = default)
    {
        foreach (var doc in documents)
            doc.Embedding = await _embeddings.EmbedAsync($"{doc.Title}\n{doc.Content}", ct);

        await _store.IndexAsync(documents, ct);
    }

    /// <summary>Beantwoord een vraag op basis van de opgehaalde context.</summary>
    public async Task<RagAnswer> AskAsync(string question, CancellationToken ct = default)
    {
        var queryVector = await _embeddings.EmbedAsync(question, ct);
        var hits = await _store.SearchAsync(queryVector, _options.TopK, ct);

        if (hits.Count == 0)
            return new RagAnswer("Ik kon geen relevante documenten in de kennisbank vinden.", hits);

        var context = string.Join(
            "\n\n",
            hits.Select((h, i) => $"[{i + 1}] Titel: {h.Document.Title}\nBron: {h.Document.Source}\n{h.Document.Content}"));

        const string system =
            "Je bent een behulpzame assistent. Beantwoord de vraag UITSLUITEND op basis van de gegeven context. " +
            "Staat het antwoord niet in de context, zeg dat dan eerlijk en verzin niets. " +
            "Antwoord in het Nederlands en verwijs naar de gebruikte bronnen met [nummer].";

        var user = $"Context:\n{context}\n\nVraag: {question}";

        var answer = await _llm.CompleteAsync(system, user, ct);
        return new RagAnswer(answer, hits);
    }
}

public sealed record RagAnswer(string Answer, IReadOnlyList<SearchResult> Sources);
