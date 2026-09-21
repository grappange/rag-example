namespace RagExample.Configuration;

public sealed class RagOptions
{
    public ElasticsearchOptions Elasticsearch { get; set; } = new();
    public OllamaOptions Ollama { get; set; } = new();

    /// <summary>Aantal documenten dat als context wordt opgehaald (kNN top-k).</summary>
    public int TopK { get; set; } = 4;
}

public sealed class ElasticsearchOptions
{
    public string Url { get; set; } = "http://localhost:9200";
    public string IndexName { get; set; } = "rag-documents";
}

public sealed class OllamaOptions
{
    public string Url { get; set; } = "http://localhost:11434";
    public string EmbeddingModel { get; set; } = "nomic-embed-text";
    public int EmbeddingDimensions { get; set; } = 768;
    public string ChatModel { get; set; } = "qwen2.5:14b";
}
