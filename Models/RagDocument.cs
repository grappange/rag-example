namespace RagExample.Models;

/// <summary>
/// Eén document in de kennisbank. De <see cref="Embedding"/> wordt door de
/// embedding-service gevuld en als dense_vector in Elasticsearch opgeslagen.
/// </summary>
public sealed class RagDocument
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public float[]? Embedding { get; set; }
}
