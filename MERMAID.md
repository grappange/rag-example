# RAG-pijplijn — diagram

Visuele weergave van [RagService.cs](Services/RagService.cs): de twee
publieke methodes `IngestAsync` en `AskAsync`, en hoe ze de drie services
(`IEmbeddingService`, `IVectorStore`, `ILlmService`) gebruiken.

## Sequence diagram — IngestAsync + AskAsync

```mermaid
sequenceDiagram
    participant P as Program.cs
    participant R as RagService
    participant E as IEmbeddingService (Ollama embeddings)
    participant V as IVectorStore (Elasticsearch)
    participant L as ILlmService (Ollama chat)

    note over P,V: Ingest - alleen als index leeg is
    P->>R: IngestAsync(documents)
    loop elk document
        R->>E: EmbedAsync(Title + Content)
        E-->>R: embedding vector
    end
    R->>V: IndexAsync(documents)
    V-->>R: OK - bulk + refresh

    note over P,L: Ask - per gebruikersvraag
    P->>R: AskAsync(question)
    R->>E: EmbedAsync(question)
    E-->>R: query vector
    R->>V: SearchAsync(queryVector, TopK)
    V-->>R: search results - document plus score
    alt geen hits
        R-->>P: "Geen relevante documenten gevonden"
    else hits gevonden
        R->>R: bouw context uit hits
        R->>L: CompleteAsync(systemPrompt, context plus question)
        L-->>R: answer text
        R-->>P: RagAnswer met answer en sources
    end
```

## Componentdiagram — architectuur

```mermaid
flowchart TB
    subgraph Console["Program.cs (console host)"]
        direction TB
        Start["Startup:<br/>EnsureIndexAsync + CountAsync"]
        Loop["Interactieve loop /<br/>eenmalige args-modus"]
    end

    RagService["RagService<br/>(Services/RagService.cs)"]

    subgraph Deps["Geïnjecteerde services"]
        Embed["IEmbeddingService<br/>OllamaEmbeddingService"]
        Store["IVectorStore<br/>ElasticsearchVectorStore"]
        Llm["ILlmService<br/>OllamaLlmService"]
    end

    Ollama[("Ollama<br/>localhost:11434")]
    ES[("Elasticsearch<br/>localhost:9200<br/>index: rag-documents")]
    SampleData["Data/SampleData.cs<br/>105 documenten"]

    Start --> RagService
    Loop --> RagService
    SampleData -.-> RagService

    RagService --> Embed
    RagService --> Store
    RagService --> Llm

    Embed -->|"POST /api/embeddings<br/>model: nomic-embed-text"| Ollama
    Llm -->|"POST /api/chat<br/>model: mistral:7b"| Ollama
    Store -->|"kNN search / bulk index<br/>dense_vector, cosine"| ES
```

## Wat er gebeurt bij "Wat is RAG?"

```mermaid
flowchart LR
    Q["Vraag:<br/>'Wat is RAG?'"] --> E1["Embed via Ollama<br/>-> vector[768]"]
    E1 --> K["kNN-search in Elasticsearch<br/>top-4 op cosine similarity"]
    K --> D1["rag-basis<br/>(hoogste score)"]
    K --> D2["embeddings"]
    K --> D3["..."]
    D1 --> C["Context samengesteld<br/>[1] rag-basis<br/>[2] embeddings<br/>..."]
    D2 --> C
    D3 --> C
    C --> Sys["System-prompt:<br/>'antwoord UITSLUITEND<br/>op basis van context'"]
    Sys --> L["Ollama chat<br/>model: mistral:7b"]
    L --> A["Antwoord + bronvermeldingen [1][2]"]
```
