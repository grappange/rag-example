# RAG-voorbeeld: .NET + Elasticsearch + Ollama

Een compact, zelfstandig RAG-voorbeeld (Retrieval-Augmented Generation) in C# / .NET 10.
Elasticsearch dient als vector database (dense_vector + kNN); Ollama levert lokaal de
embeddings en de tekstgeneratie. Geen externe API-keys nodig.

## Architectuur

```
vraag -> [Ollama /api/embeddings] -> queryvector
                                        |
                                        v
                          [Elasticsearch kNN search] -> top-k documenten
                                        |
                                        v
        context + vraag -> [Ollama /api/chat] -> antwoord (+ bronvermelding)
```

Pijplijn in code: `RagService` (embed -> retrieve -> augment -> generate), met
`OllamaEmbeddingService`, `ElasticsearchVectorStore` en `OllamaLlmService` achter
interfaces en geregistreerd via dependency injection in `Program.cs`.

## Vereisten

- .NET SDK 10
- Docker Desktop (voor Elasticsearch)
- Ollama, met de modellen:
  - `nomic-embed-text` (embeddings, 768 dimensies)  ->  `ollama pull nomic-embed-text`
  - een chatmodel, standaard `mistral:7b`            ->  `ollama pull mistral:7b`

Elk chatmodel uit `ollama list` werkt; pas `Rag:Ollama:ChatModel` in `appsettings.json`
aan (bijv. `qwen2.5:14b`, `gpt-oss:20b`, `gemma4`).

## Starten

1. Elasticsearch starten:

   ```powershell
   docker compose up -d
   ```

2. De applicatie draaien (indexeert bij de eerste run automatisch de voorbeelddata):

   ```powershell
   dotnet run
   ```

   Of eenmalig een vraag stellen:

   ```powershell
   dotnet run -- "Waarom is Elasticsearch geschikt voor RAG?"
   ```

## Configuratie

Alles staat in `appsettings.json` (of via omgevingsvariabelen met prefix `RAG_`,
bijv. `RAG_Rag__Ollama__ChatModel=gpt-oss:20b`):

| Instelling                  | Standaard                 |
|-----------------------------|---------------------------|
| Elasticsearch.Url           | http://localhost:9200     |
| Elasticsearch.IndexName     | rag-documents             |
| Ollama.Url                  | http://localhost:11434    |
| Ollama.EmbeddingModel       | nomic-embed-text          |
| Ollama.EmbeddingDimensions  | 768                       |
| Ollama.ChatModel            | mistral:7b                |
| TopK                        | 4                         |

> Let op: wijzig je van embedding-model, dan verandert doorgaans het aantal dimensies.
> Verwijder in dat geval de index (of kies een andere IndexName), omdat het
> dense_vector-veld een vaste dimensie heeft.

## Opnieuw indexeren

De index wordt alleen gevuld als hij leeg is. Wil je opnieuw indexeren:

```powershell
curl -X DELETE http://localhost:9200/rag-documents
dotnet run
```

## Projectstructuur

```
RagExample.csproj
appsettings.json
docker-compose.yml
Program.cs                         # host, DI, ingest + vraag-loop
Configuration/RagOptions.cs        # sterk getypeerde configuratie
Models/RagDocument.cs              # document + embedding
Data/SampleData.cs                 # Nederlandstalige voorbeeldkennisbank
Services/
  IEmbeddingService.cs / OllamaEmbeddingService.cs
  ILlmService.cs       / OllamaLlmService.cs
  IVectorStore.cs      / ElasticsearchVectorStore.cs
  RagService.cs        # de RAG-pijplijn
```
