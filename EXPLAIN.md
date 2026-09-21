# RAG Example — hoe dit project werkt

Een minimale .NET-console-applicatie die Retrieval-Augmented Generation (RAG)
demonstreert met **Elasticsearch** (vector store) en **Ollama** (lokale
embeddings + LLM). Geen externe cloud-API's nodig; alles draait lokaal.

## Wat doet het

1. Bij het opstarten wordt gecontroleerd of de Elasticsearch-index bestaat en
   gevuld is.
2. Is de index leeg, dan worden de voorbeelddocumenten uit
   [Data/SampleData.cs](Data/SampleData.cs) omgezet in embeddings en
   geïndexeerd.
3. Daarna kun je in de console vragen stellen. Elke vraag doorloopt de
   RAG-pijplijn: **embed → retrieve (kNN) → augment → generate**, en het
   antwoord wordt getoond samen met de gebruikte bronnen.

## Architectuur

```
Program.cs
  ├─ RagService              (Services/RagService.cs)       de pijplijn
  │    ├─ IEmbeddingService  (OllamaEmbeddingService)        tekst -> vector
  │    ├─ IVectorStore       (ElasticsearchVectorStore)      opslag + kNN-search
  │    └─ ILlmService        (OllamaLlmService)              vector/context -> antwoord
  ├─ Configuration/RagOptions.cs                             instellingen (appsettings.json)
  ├─ Models/RagDocument.cs                                   Id, Title, Content, Source, Embedding
  └─ Data/SampleData.cs                                      105 Nederlandstalige demo-documenten
```

Alles is opgezet met de generieke .NET `Host` (dependency injection,
configuratie, logging), zonder ASP.NET — het is puur een console-app.

### De pijplijn in detail (`RagService`)

- **IngestAsync** — voor elk document wordt `Title + Content` naar Ollama
  gestuurd (`POST /api/embeddings`, model `nomic-embed-text`, 768 dimensies)
  en de resulterende vector opgeslagen op `doc.Embedding`. Vervolgens worden
  alle documenten in één bulk-call naar Elasticsearch geïndexeerd.
- **AskAsync** — de vraag wordt ook geëmbed, en met een kNN-zoekopdracht
  (`ElasticsearchVectorStore.SearchAsync`) worden de `TopK` (standaard 4)
  meest gelijkende documenten opgehaald (cosine similarity op het
  `dense_vector`-veld). Die documenten worden als genummerde context in een
  prompt gestopt en naar Ollama's chat-endpoint gestuurd (model `mistral:7b`
  volgens `appsettings.json`, non-streaming). De system-prompt dwingt af dat
  het model **alleen** op basis van de context antwoordt, in het Nederlands,
  met bronverwijzingen `[nummer]`.

### Elasticsearch-index

`ElasticsearchVectorStore.EnsureIndexAsync` maakt (indien nog niet aanwezig)
een index `rag-documents` met:
- `Title`, `Source` als `keyword`
- `Content` als volledig doorzoekbare `text`
- `Embedding` als `dense_vector` (768 dims, cosine similarity, geïndexeerd
  voor kNN)

## Configuratie

Alles staat in [appsettings.json](appsettings.json) onder de `Rag`-sectie,
en kan overschreven worden met environment variables met prefix `RAG_`
(bijv. `RAG__Ollama__ChatModel`):

| Instelling | Standaard | Betekenis |
|---|---|---|
| `Elasticsearch:Url` | `http://localhost:9200` | Elasticsearch-endpoint |
| `Elasticsearch:IndexName` | `rag-documents` | Indexnaam |
| `Ollama:Url` | `http://localhost:11434` | Ollama-endpoint |
| `Ollama:EmbeddingModel` | `nomic-embed-text` | Embeddingmodel |
| `Ollama:EmbeddingDimensions` | `768` | Moet overeenkomen met het embeddingmodel |
| `Ollama:ChatModel` | `mistral:7b` | Chat/generatiemodel |
| `TopK` | `4` | Aantal documenten opgehaald per vraag |

## Vereisten

- .NET 10 SDK
- Een draaiende **Elasticsearch** op `http://localhost:9200`
- Een draaiende **Ollama** op `http://localhost:11434` met de modellen
  gepulled:
  ```bash
  ollama pull nomic-embed-text
  ollama pull mistral:7b
  ```

## Gebruik

**Bouwen:**
```bash
dotnet build
```

**Interactieve modus** (blijft vragen stellen tot een lege regel):
```bash
dotnet run
```
```
=== RAG-voorbeeld (Elasticsearch + Ollama) ===
Index is leeg. Voorbeelddocumenten worden geembed en geindexeerd...
105 documenten geindexeerd.

Stel je vraag over de kennisbank. Lege regel = stoppen.

Vraag> Wat is RAG?
```

**Eenmalige modus** (één vraag als command-line argument, dan stoppen):
```bash
dotnet run -- "Wat is het verschil tussen HNSW en exact kNN?"
```

Bij de eerste run wordt de index automatisch aangemaakt en gevuld met de 105
voorbeelddocumenten uit `Data/SampleData.cs` (dit duurt even, omdat elk
document een losse embedding-call naar Ollama vergt). Bij volgende runs
wordt de bestaande index hergebruikt — verwijder de index in Elasticsearch
handmatig (`DELETE /rag-documents`) als je opnieuw wilt indexeren, bijv. na
het toevoegen van nieuwe documenten aan `SampleData.cs`.

## Foutafhandeling

Als Elasticsearch of Ollama niet bereikbaar zijn, stopt het programma direct
met een duidelijke foutmelding en exit code 1. Fouten tijdens het
beantwoorden van een individuele vraag (bijv. een tijdelijke Ollama-timeout)
worden per vraag afgevangen zodat de interactieve sessie kan doorgaan.
