using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RagExample.Configuration;
using RagExample.Data;
using RagExample.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .AddEnvironmentVariables("RAG_");

builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.Configure<RagOptions>(builder.Configuration.GetSection("Rag"));

// Elasticsearch-client (singleton).
builder.Services.AddSingleton(sp =>
{
    var opts = sp.GetRequiredService<IOptions<RagOptions>>().Value;
    var settings = new ElasticsearchClientSettings(new Uri(opts.Elasticsearch.Url))
        .DefaultIndex(opts.Elasticsearch.IndexName);
    return new ElasticsearchClient(settings);
});

// Ollama-services via typed HttpClients.
builder.Services.AddHttpClient<IEmbeddingService, OllamaEmbeddingService>((sp, http) =>
{
    var opts = sp.GetRequiredService<IOptions<RagOptions>>().Value;
    http.BaseAddress = new Uri(opts.Ollama.Url);
    http.Timeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddHttpClient<ILlmService, OllamaLlmService>((sp, http) =>
{
    var opts = sp.GetRequiredService<IOptions<RagOptions>>().Value;
    http.BaseAddress = new Uri(opts.Ollama.Url);
    http.Timeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddSingleton<IVectorStore, ElasticsearchVectorStore>();
builder.Services.AddSingleton<RagService>();

using var host = builder.Build();

var store = host.Services.GetRequiredService<IVectorStore>();
var rag = host.Services.GetRequiredService<RagService>();

Console.WriteLine("=== RAG-voorbeeld (Elasticsearch + Ollama) ===");

try
{
    await store.EnsureIndexAsync();

    var count = await store.CountAsync();
    if (count == 0)
    {
        Console.WriteLine("Index is leeg. Voorbeelddocumenten worden geembed en geindexeerd...");
        await rag.IngestAsync(SampleData.Documents);
        Console.WriteLine($"{SampleData.Documents.Count} documenten geindexeerd.");
    }
    else
    {
        Console.WriteLine($"{count} documenten aanwezig in de index '{store.GetType().Name}'.");
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Initialisatie mislukt: {ex.Message}");
    Console.Error.WriteLine("Draaien Elasticsearch (http://localhost:9200) en Ollama (http://localhost:11434)?");
    return 1;
}

// Eenmalige modus: dotnet run -- "je vraag hier"
if (args.Length > 0)
{
    await AskAndPrint(rag, string.Join(' ', args));
    return 0;
}

// Interactieve modus.
Console.WriteLine();
Console.WriteLine("Stel je vraag over de kennisbank. Lege regel = stoppen.");
while (true)
{
    Console.Write("\nVraag> ");
    var question = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(question))
        break;

    await AskAndPrint(rag, question);
}

return 0;

static async Task AskAndPrint(RagService rag, string question)
{
    try
    {
        var result = await rag.AskAsync(question);
        Console.WriteLine();
        Console.WriteLine(result.Answer);
        if (result.Sources.Count > 0)
        {
            Console.WriteLine("\nBronnen:");
            for (var i = 0; i < result.Sources.Count; i++)
            {
                var s = result.Sources[i];
                Console.WriteLine($"  [{i + 1}] {s.Document.Title} ({s.Document.Source}) - score {s.Score:F3}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Fout bij beantwoorden: {ex.Message}");
    }
}
