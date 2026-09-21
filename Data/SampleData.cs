using RagExample.Models;

namespace RagExample.Data;

/// <summary>Kleine Nederlandstalige kennisbank als voorbeelddata voor de demo.</summary>
public static class SampleData
{
    public static IReadOnlyList<RagDocument> Documents { get; } = new List<RagDocument>
    {
        new()
        {
            Id = "rag-basis",
            Title = "Wat is Retrieval-Augmented Generation (RAG)?",
            Source = "kb/rag.md",
            Content = "Retrieval-Augmented Generation combineert een taalmodel met een externe kennisbron. " +
                      "Bij een vraag worden eerst relevante documenten opgehaald (retrieval) en vervolgens meegegeven " +
                      "als context aan het taalmodel (generation). Zo kan het model antwoorden op basis van actuele, " +
                      "bedrijfsspecifieke informatie zonder dat het model opnieuw getraind hoeft te worden."
        },
        new()
        {
            Id = "embeddings",
            Title = "Embeddings en vector search",
            Source = "kb/embeddings.md",
            Content = "Een embedding is een numerieke vectorrepresentatie van tekst waarin semantisch verwante teksten " +
                      "dicht bij elkaar liggen. Door de vraag en de documenten naar embeddings om te zetten, kun je met " +
                      "een nearest-neighbour-zoekopdracht (kNN) de meest relevante passages vinden op betekenis in plaats " +
                      "van op exacte trefwoorden."
        },
        new()
        {
            Id = "elasticsearch-vectors",
            Title = "Elasticsearch als vector database",
            Source = "kb/elasticsearch.md",
            Content = "Elasticsearch ondersteunt sinds versie 8 een dense_vector-veldtype met ingebouwde kNN-search. " +
                      "Je configureert het aantal dimensies en de similarity-maat (bijvoorbeeld cosine). Elasticsearch " +
                      "combineert daarmee klassieke full-text search (BM25) en vector search in één engine, wat het geschikt " +
                      "maakt als opslag voor een RAG-toepassing."
        },
        new()
        {
            Id = "ollama",
            Title = "Ollama voor lokale modellen",
            Source = "kb/ollama.md",
            Content = "Ollama draait open-source taalmodellen lokaal op je eigen machine en biedt een eenvoudige HTTP-API. " +
                      "Het endpoint /api/embeddings genereert embeddings (bijvoorbeeld met nomic-embed-text, 768 dimensies) en " +
                      "/api/chat genereert antwoorden (bijvoorbeeld met qwen2.5). Zo blijft alle data on-premise zonder externe API-calls."
        },
        new()
        {
            Id = "dotnet-client",
            Title = "De .NET-client voor Elasticsearch",
            Source = "kb/dotnet.md",
            Content = "De officiële NuGet-package Elastic.Clients.Elasticsearch is de moderne, strongly-typed .NET-client. " +
                      "Je maakt een ElasticsearchClient met ElasticsearchClientSettings, definieert een index-mapping met een " +
                      "dense_vector-veld, indexeert documenten via de Bulk-API en zoekt met een Knn-query in de Search-API."
        },
        new()
        {
            Id = "chunking-strategie",
            Title = "Chunking-strategieën voor RAG",
            Source = "kb/chunking.md",
            Content = "Grote documenten moeten worden opgesplitst in kleinere stukken (chunks) voordat ze geëmbed worden. " +
                      "Een vaste chunk-grootte met overlap (bijvoorbeeld 500 tokens met 50 tokens overlap) is een eenvoudige " +
                      "aanpak. Geavanceerdere technieken houden rekening met semantische grenzen zoals alinea's of headers, " +
                      "zodat context niet halverwege een zin wordt afgekapt."
        },
        new()
        {
            Id = "reranking",
            Title = "Reranking van zoekresultaten",
            Source = "kb/reranking.md",
            Content = "Na de eerste retrieval-stap kan een reranker de top-N resultaten opnieuw ordenen op relevantie. " +
                      "Cross-encoder modellen zijn nauwkeuriger dan de initiële bi-encoder retrieval omdat ze vraag en document " +
                      "samen verwerken, maar zijn duurder om op grote schaal toe te passen. Daarom worden ze meestal alleen op " +
                      "een beperkte kandidatenlijst losgelaten."
        },
        new()
        {
            Id = "hybrid-search",
            Title = "Hybrid search: BM25 en vectoren combineren",
            Source = "kb/hybrid-search.md",
            Content = "Hybrid search combineert traditionele keyword search (BM25) met vector search om zowel exacte " +
                      "trefwoorden als semantische gelijkenis mee te wegen. Reciprocal Rank Fusion (RRF) is een populaire " +
                      "methode om de resultaten van beide zoekstrategieën samen te voegen tot één gerangschikte lijst."
        },
        new()
        {
            Id = "prompt-engineering-rag",
            Title = "Prompt engineering voor RAG-toepassingen",
            Source = "kb/prompt-engineering.md",
            Content = "Bij RAG is het belangrijk om de opgehaalde context duidelijk te scheiden van de instructie in de " +
                      "prompt, bijvoorbeeld met XML-tags of markdown-headers. Instrueer het model expliciet om alleen op basis " +
                      "van de gegeven context te antwoorden en aan te geven wanneer het antwoord niet in de context staat, om " +
                      "hallucinaties te beperken."
        },
        new()
        {
            Id = "hallucinaties",
            Title = "Hallucinaties bij taalmodellen",
            Source = "kb/hallucinaties.md",
            Content = "Een hallucinatie is wanneer een taalmodel plausibel klinkende maar feitelijk onjuiste informatie " +
                      "genereert. RAG vermindert hallucinaties door het model te voorzien van relevante, verifieerbare bronnen, " +
                      "maar garandeert geen correctheid. Grounding-technieken en citaties naar brondocumenten helpen gebruikers " +
                      "antwoorden te verifiëren."
        },
        new()
        {
            Id = "cosine-similarity",
            Title = "Cosine similarity uitgelegd",
            Source = "kb/cosine-similarity.md",
            Content = "Cosine similarity meet de hoek tussen twee vectoren in plaats van hun absolute afstand, en is daarom " +
                      "ongevoelig voor de lengte van de vectoren. De waarde ligt tussen -1 en 1, waarbij 1 betekent dat de " +
                      "vectoren exact dezelfde richting hebben. Voor tekstembeddings is cosine similarity de meest gebruikte " +
                      "similarity-maat."
        },
        new()
        {
            Id = "dimensionaliteit-embeddings",
            Title = "Dimensionaliteit van embeddingmodellen",
            Source = "kb/dimensionaliteit.md",
            Content = "Embeddingmodellen produceren vectoren met een vast aantal dimensies, bijvoorbeeld 384, 768 of 1536. " +
                      "Meer dimensies kunnen meer nuance vastleggen maar kosten meer opslag en rekenkracht bij het zoeken. " +
                      "Sommige modellen ondersteunen Matryoshka-representatie waarbij een vector ingekort kan worden zonder " +
                      "veel kwaliteitsverlies."
        },
        new()
        {
            Id = "knn-vs-ann",
            Title = "Exact kNN versus approximate nearest neighbour (ANN)",
            Source = "kb/knn-ann.md",
            Content = "Exact k-nearest-neighbour zoekt de werkelijk dichtstbijzijnde vectoren maar schaalt slecht bij grote " +
                      "datasets. Approximate nearest neighbour-algoritmes zoals HNSW (Hierarchical Navigable Small World) " +
                      "leveren bijna-optimale resultaten met veel betere zoeksnelheid door een graafstructuur te doorzoeken " +
                      "in plaats van alle vectoren te vergelijken."
        },
        new()
        {
            Id = "hnsw-index",
            Title = "HNSW-index in Elasticsearch",
            Source = "kb/hnsw.md",
            Content = "Elasticsearch gebruikt standaard een HNSW-index voor dense_vector-velden om snelle approximate kNN-" +
                      "search mogelijk te maken. Parameters zoals m (aantal verbindingen per node) en ef_construction " +
                      "beïnvloeden de balans tussen indexeersnelheid, geheugengebruik en zoeknauwkeurigheid."
        },
        new()
        {
            Id = "tokenization",
            Title = "Tokenization bij taalmodellen",
            Source = "kb/tokenization.md",
            Content = "Taalmodellen verwerken tekst niet als losse woorden maar als tokens, subword-eenheden die door een " +
                      "tokenizer (zoals Byte Pair Encoding) worden bepaald. Het aantal tokens bepaalt de kosten en de context-" +
                      "lengte van een aanroep, en één woord kan uit meerdere tokens bestaan, vooral bij samengestelde of " +
                      "minder frequente woorden."
        },
        new()
        {
            Id = "context-window",
            Title = "Context window van een taalmodel",
            Source = "kb/context-window.md",
            Content = "Het context window is het maximum aantal tokens dat een taalmodel in één keer kan verwerken, inclusief " +
                      "zowel de invoer als de gegenereerde uitvoer. Bij RAG-toepassingen is het belangrijk om de hoeveelheid " +
                      "opgehaalde context binnen dit limiet te houden, en de meest relevante passages prioriteit te geven " +
                      "wanneer er meer kandidaten zijn dan er passen."
        },
        new()
        {
            Id = "system-prompt",
            Title = "Het gebruik van system prompts",
            Source = "kb/system-prompt.md",
            Content = "Een system prompt geeft een taalmodel instructies over rol, toon en gedrag voordat de gebruiker een " +
                      "vraag stelt. In een RAG-toepassing bevat de system prompt vaak richtlijnen zoals: antwoord alleen op " +
                      "basis van de gegeven context, citeer bronnen, en geef aan wanneer informatie ontbreekt."
        },
        new()
        {
            Id = "temperature-parameter",
            Title = "De temperature-parameter",
            Source = "kb/temperature.md",
            Content = "Temperature bepaalt hoe willekeurig een taalmodel tokens kiest tijdens generatie. Een lage waarde " +
                      "(bijvoorbeeld 0) maakt de uitvoer deterministisch en feitelijk, terwijl een hogere waarde meer variatie " +
                      "en creativiteit oplevert. Voor factuele RAG-antwoorden wordt meestal een lage temperature aangeraden."
        },
        new()
        {
            Id = "vector-database-vergelijking",
            Title = "Vector databases vergelijken",
            Source = "kb/vector-db-vergelijking.md",
            Content = "Naast Elasticsearch zijn er gespecialiseerde vector databases zoals Pinecone, Weaviate, Qdrant en " +
                      "Milvus, evenals extensies zoals pgvector voor PostgreSQL. De keuze hangt af van factoren als schaal, " +
                      "bestaande infrastructuur, hybride zoekbehoefte en of losse operationele complexiteit gewenst is."
        },
        new()
        {
            Id = "pgvector",
            Title = "pgvector: vectorsearch in PostgreSQL",
            Source = "kb/pgvector.md",
            Content = "pgvector is een PostgreSQL-extensie die een vector-datatype en index-methoden zoals IVFFlat en HNSW " +
                      "toevoegt. Dit maakt het mogelijk om vector search toe te voegen aan een bestaande relationele database " +
                      "zonder een aparte vector database te introduceren, wat handig is voor kleinere of bestaande projecten."
        },
        new()
        {
            Id = "semantic-caching",
            Title = "Semantic caching voor LLM-aanroepen",
            Source = "kb/semantic-caching.md",
            Content = "Semantic caching slaat eerdere vraag-antwoordparen op als embeddings en hergebruikt een antwoord " +
                      "wanneer een nieuwe vraag semantisch voldoende lijkt op een eerdere vraag. Dit vermindert latency en " +
                      "kosten bij veelgestelde of vergelijkbare vragen, maar vereist een zorgvuldig gekozen similarity-drempel " +
                      "om foutieve hergebruik te voorkomen."
        },
        new()
        {
            Id = "multi-tenant-rag",
            Title = "Multi-tenant RAG-architectuur",
            Source = "kb/multi-tenant.md",
            Content = "In een multi-tenant RAG-systeem moeten documenten van verschillende klanten strikt gescheiden blijven. " +
                      "Dit kan via aparte indices per tenant, of via een gedeelde index met een verplicht tenant_id-filter op " +
                      "elke zoekopdracht. Het is cruciaal om deze filtering op query-niveau af te dwingen, niet alleen in de " +
                      "applicatielogica."
        },
        new()
        {
            Id = "metadata-filtering",
            Title = "Metadata filtering combineren met vector search",
            Source = "kb/metadata-filtering.md",
            Content = "Naast semantische gelijkenis wil je vaak filteren op metadata zoals datum, categorie of auteur. " +
                      "Elasticsearch ondersteunt gefilterde kNN-search, waarbij het filter wordt toegepast vóór of tijdens de " +
                      "vector-zoekopdracht, zodat alleen relevante subsets van documenten worden doorzocht."
        },
        new()
        {
            Id = "evaluatie-rag",
            Title = "RAG-systemen evalueren",
            Source = "kb/evaluatie.md",
            Content = "Het evalueren van een RAG-systeem omvat zowel de retrieval-kwaliteit (precision, recall, MRR) als de " +
                      "generatiekwaliteit (relevantie, feitelijke juistheid, grounding). Frameworks zoals RAGAS meten metrics " +
                      "zoals context precision, faithfulness en answer relevancy om de pipeline objectief te beoordelen."
        },
        new()
        {
            Id = "streaming-responses",
            Title = "Streaming van LLM-antwoorden",
            Source = "kb/streaming.md",
            Content = "Bij streaming stuurt een taalmodel tokens direct door zodra ze gegenereerd zijn, in plaats van te " +
                      "wachten tot het volledige antwoord klaar is. Dit verbetert de waargenomen snelheid in gebruikersinterfaces " +
                      "aanzienlijk. In .NET kan dit via IAsyncEnumerable of Server-Sent Events worden geïmplementeerd."
        },
        new()
        {
            Id = "function-calling",
            Title = "Function calling / tool use bij LLM's",
            Source = "kb/function-calling.md",
            Content = "Function calling stelt een taalmodel in staat om te bepalen welke externe functie aangeroepen moet " +
                      "worden en met welke parameters, op basis van een beschreven schema. Dit maakt het mogelijk om een model " +
                      "te koppelen aan echte acties zoals database-opzoekingen, API-calls of berekeningen, in plaats van alleen " +
                      "tekst te genereren."
        },
        new()
        {
            Id = "agentic-workflows",
            Title = "Agentic workflows met taalmodellen",
            Source = "kb/agentic.md",
            Content = "Een agentic workflow laat een taalmodel autonoom meerdere stappen uitvoeren: plannen, tools aanroepen, " +
                      "resultaten evalueren en vervolgstappen bepalen, totdat een doel is bereikt. Dit verschilt van een " +
                      "eenvoudige RAG-pipeline doordat de agent zelf beslist welke informatie nodig is en welke acties " +
                      "ondernomen moeten worden."
        },
        new()
        {
            Id = "embedding-modellen-vergelijking",
            Title = "Populaire embeddingmodellen vergeleken",
            Source = "kb/embedding-modellen.md",
            Content = "Bekende embeddingmodellen zijn OpenAI text-embedding-3, Cohere embed-v3, en open-source modellen zoals " +
                      "nomic-embed-text en BGE. Ze verschillen in dimensionaliteit, ondersteunde talen, maximale invoerlengte " +
                      "en of ze lokaal (via Ollama) of via een API gedraaid worden."
        },
        new()
        {
            Id = "data-preprocessing-rag",
            Title = "Data preprocessing voor een kennisbank",
            Source = "kb/preprocessing.md",
            Content = "Voordat documenten geïndexeerd worden, is opschoning belangrijk: HTML-tags verwijderen, whitespace " +
                      "normaliseren, headers en structuur behouden, en dubbele content deduplicaten. Slecht voorbewerkte data " +
                      "leidt tot ruizige embeddings en dus tot minder relevante zoekresultaten."
        },
        new()
        {
            Id = "index-mapping-elasticsearch",
            Title = "Index mappings ontwerpen in Elasticsearch",
            Source = "kb/index-mapping.md",
            Content = "Een index mapping definieert de velden en hun types, zoals text voor doorzoekbare content, keyword " +
                      "voor exacte filtering en dense_vector voor embeddings. Een goed ontworpen mapping voorkomt dynamic " +
                      "mapping-verrassingen en zorgt dat velden efficiënt geïndexeerd en doorzocht kunnen worden."
        },
        new()
        {
            Id = "bulk-indexing",
            Title = "Bulk-indexing in Elasticsearch",
            Source = "kb/bulk-indexing.md",
            Content = "De Bulk-API van Elasticsearch stelt je in staat om meerdere index-, update- en delete-operaties in " +
                      "één HTTP-request te versturen, wat veel efficiënter is dan individuele requests. Bij het indexeren van " +
                      "duizenden RAG-documenten is batching via de Bulk-API essentieel voor acceptabele doorvoersnelheid."
        },
        new()
        {
            Id = "refresh-interval",
            Title = "Refresh interval en near-realtime search",
            Source = "kb/refresh-interval.md",
            Content = "Elasticsearch is near-realtime: na het indexeren van een document duurt het standaard tot een refresh " +
                      "(elke seconde) voordat het doorzoekbaar is. Voor bulk-imports kun je het refresh interval tijdelijk " +
                      "uitzetten om indexeersnelheid te verhogen en het pas na afloop weer inschakelen."
        },
        new()
        {
            Id = "sharding-elasticsearch",
            Title = "Sharding en replicatie in Elasticsearch",
            Source = "kb/sharding.md",
            Content = "Een Elasticsearch-index wordt opgedeeld in shards die over meerdere nodes verdeeld kunnen worden voor " +
                      "horizontale schaalbaarheid. Replica shards zorgen voor hoge beschikbaarheid en leesbelasting-verdeling. " +
                      "Het aantal primary shards kan na het aanmaken van een index niet meer gewijzigd worden."
        },
        new()
        {
            Id = "asp-net-minimal-api",
            Title = "ASP.NET Core minimal APIs",
            Source = "kb/minimal-api.md",
            Content = "Minimal APIs in ASP.NET Core bieden een lichtgewicht manier om HTTP-endpoints te definiëren zonder de " +
                      "overhead van controllers, ideaal voor kleine services zoals een RAG-backend. Endpoints worden " +
                      "gedefinieerd met app.MapGet/MapPost en ondersteunen dependency injection net als reguliere controllers."
        },
        new()
        {
            Id = "dependency-injection-dotnet",
            Title = "Dependency injection in .NET",
            Source = "kb/dependency-injection.md",
            Content = ".NET heeft ingebouwde dependency injection via IServiceCollection. Services worden geregistreerd met " +
                      "een levensduur (Singleton, Scoped of Transient) en automatisch geïnjecteerd in constructors. Voor een " +
                      "RAG-applicatie worden bijvoorbeeld de Elasticsearch-client en embedding-service als singleton " +
                      "geregistreerd."
        },
        new()
        {
            Id = "httpclientfactory",
            Title = "IHttpClientFactory voor externe API-calls",
            Source = "kb/httpclientfactory.md",
            Content = "IHttpClientFactory beheert de levenscyclus van HttpClient-instanties en voorkomt problemen zoals " +
                      "socket exhaustion die kunnen ontstaan bij handmatig aanmaken van HttpClient. Het is de aanbevolen manier " +
                      "om in .NET te communiceren met externe services zoals Ollama of een embedding-API."
        },
        new()
        {
            Id = "configuration-dotnet",
            Title = "Configuratie en appsettings in .NET",
            Source = "kb/configuration.md",
            Content = ".NET-applicaties lezen configuratie uit meerdere bronnen: appsettings.json, environment-specifieke " +
                      "bestanden zoals appsettings.Development.json, environment variables en command-line argumenten, in een " +
                      "vaste prioriteitsvolgorde. Het IOptions-patroon maakt strongly-typed toegang tot configuratie mogelijk."
        },
        new()
        {
            Id = "logging-dotnet",
            Title = "Structured logging in .NET",
            Source = "kb/logging.md",
            Content = "ASP.NET Core biedt een ingebouwd logging-framework met providers voor console, debug en externe " +
                      "systemen zoals Application Insights of Serilog. Structured logging houdt logvelden als aparte " +
                      "parameters in plaats van ze in een string te verweven, wat query's en filtering in logaggregators " +
                      "vergemakkelijkt."
        },
        new()
        {
            Id = "async-await-dotnet",
            Title = "Async/await best practices in .NET",
            Source = "kb/async-await.md",
            Content = "Async/await maakt niet-blokkerende I/O mogelijk in .NET. Belangrijke richtlijnen zijn: gebruik nooit " +
                      ".Result of .Wait() op een Task omdat dit tot deadlocks kan leiden, geef CancellationToken door aan " +
                      "onderliggende async-calls, en gebruik ConfigureAwait(false) in bibliotheekcode waar toepasselijk."
        },
        new()
        {
            Id = "cancellationtoken",
            Title = "CancellationToken gebruiken bij lang lopende taken",
            Source = "kb/cancellationtoken.md",
            Content = "Een CancellationToken stelt een aanroeper in staat om een lopende asynchrone operatie netjes te " +
                      "annuleren, bijvoorbeeld wanneer een gebruiker een HTTP-request afbreekt. Bij een RAG-pipeline is dit " +
                      "nuttig om een lang lopende embedding- of generatie-call te kunnen stoppen zonder resources te verspillen."
        },
        new()
        {
            Id = "rate-limiting",
            Title = "Rate limiting voor LLM-API's",
            Source = "kb/rate-limiting.md",
            Content = "Externe LLM- en embedding-API's hanteren vaak rate limits op aantal requests of tokens per minuut. " +
                      "Een applicatie moet hiermee rekening houden via exponential backoff bij 429-responses, en eventueel " +
                      "een lokale queue of token-bucket-algoritme om binnen de limieten te blijven."
        },
        new()
        {
            Id = "retry-policies",
            Title = "Retry policies met Polly",
            Source = "kb/retry-policies.md",
            Content = "De Polly-bibliotheek biedt resilience-patronen zoals retry, circuit breaker en timeout voor .NET-" +
                      "applicaties. Bij tijdelijke fouten in een externe embedding- of LLM-service kan een retry-policy met " +
                      "exponential backoff de betrouwbaarheid van een RAG-pipeline aanzienlijk verbeteren."
        },
        new()
        {
            Id = "circuit-breaker",
            Title = "Het circuit breaker-patroon",
            Source = "kb/circuit-breaker.md",
            Content = "Een circuit breaker voorkomt dat een applicatie blijft proberen een falende externe service aan te " +
                      "roepen, door na een aantal mislukte pogingen tijdelijk alle calls te blokkeren. Dit beschermt zowel de " +
                      "eigen applicatie als de externe service tegen overbelasting tijdens een storing."
        },
        new()
        {
            Id = "docker-compose-rag",
            Title = "RAG-stack draaien met Docker Compose",
            Source = "kb/docker-compose.md",
            Content = "Docker Compose maakt het mogelijk om een volledige RAG-stack (Elasticsearch, Ollama en de .NET-API) " +
                      "lokaal te draaien met één commando. Elke service krijgt een eigen container met gedefinieerde poorten, " +
                      "volumes voor persistente data en environment variables voor configuratie."
        },
        new()
        {
            Id = "elasticsearch-security",
            Title = "Beveiliging van een Elasticsearch-cluster",
            Source = "kb/elasticsearch-security.md",
            Content = "Elasticsearch ondersteunt authenticatie via API keys of gebruikersaccounts, TLS-encryptie voor " +
                      "transport, en role-based access control om te bepalen welke indices en operaties toegestaan zijn. Voor " +
                      "productieomgevingen is het aan te raden security altijd expliciet in te schakelen."
        },
        new()
        {
            Id = "api-key-management",
            Title = "API keys veilig beheren",
            Source = "kb/api-keys.md",
            Content = "API keys voor externe diensten zoals een LLM-provider horen nooit in broncode of appsettings.json " +
                      "terecht te komen. Gebruik in plaats daarvan environment variables, user secrets tijdens ontwikkeling, " +
                      "of een secretsbeheerdienst zoals Azure Key Vault in productie."
        },
        new()
        {
            Id = "openai-api",
            Title = "De OpenAI API in het kort",
            Source = "kb/openai-api.md",
            Content = "De OpenAI API biedt endpoints voor chat completions, embeddings en meer, toegankelijk via HTTP of " +
                      "een officiële SDK. Voor RAG-toepassingen zijn vooral het embeddings-endpoint (voor het vectoriseren " +
                      "van documenten en vragen) en het chat completions-endpoint (voor het genereren van antwoorden) relevant."
        },
        new()
        {
            Id = "azure-openai",
            Title = "Azure OpenAI Service",
            Source = "kb/azure-openai.md",
            Content = "Azure OpenAI Service biedt toegang tot OpenAI-modellen binnen de Azure-infrastructuur, met " +
                      "enterprise-features zoals private networking, regionale data-residentie en integratie met Azure AD " +
                      "voor authenticatie. Dit is vaak de voorkeur voor organisaties met strikte compliance-eisen."
        },
        new()
        {
            Id = "on-premise-vs-cloud-llm",
            Title = "On-premise versus cloud-gebaseerde taalmodellen",
            Source = "kb/on-premise-vs-cloud.md",
            Content = "On-premise modellen via Ollama bieden volledige controle over data en geen externe afhankelijkheid, " +
                      "maar vereisen voldoende lokale rekenkracht (GPU) en presteren vaak minder goed dan de grootste cloud-" +
                      "modellen. Cloud-API's zijn krachtiger en schaalbaarder, maar brengen data buiten de eigen infrastructuur."
        },
        new()
        {
            Id = "gpu-inference",
            Title = "GPU-versnelling voor lokale inferentie",
            Source = "kb/gpu-inference.md",
            Content = "Het draaien van taalmodellen op een GPU versnelt inferentie aanzienlijk ten opzichte van CPU-only " +
                      "uitvoering, vooral bij grotere modellen. Ollama detecteert automatisch beschikbare NVIDIA- of AMD-GPU's " +
                      "en gebruikt VRAM om modelgewichten te laden, met CPU-offloading wanneer het model niet volledig past."
        },
        new()
        {
            Id = "quantization",
            Title = "Model-quantization uitgelegd",
            Source = "kb/quantization.md",
            Content = "Quantization verlaagt de precisie van modelgewichten (bijvoorbeeld van 16-bit naar 4-bit) om " +
                      "geheugengebruik en rekentijd te verminderen, met een beperkt verlies aan kwaliteit. GGUF is een populair " +
                      "bestandsformaat voor gequantiseerde modellen zoals gebruikt door Ollama en llama.cpp."
        },
        new()
        {
            Id = "fine-tuning-vs-rag",
            Title = "Fine-tuning versus RAG",
            Source = "kb/fine-tuning-vs-rag.md",
            Content = "Fine-tuning past de gewichten van een model aan op specifieke data, terwijl RAG externe kennis " +
                      "tijdens inferentie toevoegt zonder het model te wijzigen. RAG is doorgaans goedkoper, sneller aan te " +
                      "passen bij nieuwe data en transparanter, terwijl fine-tuning beter geschikt is om stijl of gedrag " +
                      "structureel te veranderen."
        },
        new()
        {
            Id = "few-shot-prompting",
            Title = "Few-shot prompting",
            Source = "kb/few-shot.md",
            Content = "Bij few-shot prompting worden enkele voorbeelden van vraag-antwoordparen in de prompt meegegeven " +
                      "om het model te laten zien welk formaat of welke stijl van antwoord gewenst is. Dit verbetert vaak de " +
                      "consistentie van de uitvoer zonder dat het model getraind hoeft te worden."
        },
        new()
        {
            Id = "chain-of-thought",
            Title = "Chain-of-thought reasoning",
            Source = "kb/chain-of-thought.md",
            Content = "Chain-of-thought prompting moedigt een taalmodel aan om een probleem stap voor stap te redeneren " +
                      "voordat het een eindantwoord geeft. Dit verbetert de nauwkeurigheid bij complexe reden­eertaken, al " +
                      "kost het meer tokens en dus meer tijd en kosten per antwoord."
        },
        new()
        {
            Id = "grounding",
            Title = "Grounding van taalmodel-antwoorden",
            Source = "kb/grounding.md",
            Content = "Grounding betekent dat een antwoord expliciet gebaseerd is op verifieerbare brondocumenten in plaats " +
                      "van op de interne, mogelijk verouderde of onjuiste kennis van het model. Een goed gegrond RAG-systeem " +
                      "toont bronverwijzingen zodat gebruikers antwoorden kunnen controleren."
        },
        new()
        {
            Id = "citaties-in-antwoorden",
            Title = "Bronvermelding toevoegen aan RAG-antwoorden",
            Source = "kb/citaties.md",
            Content = "Door elk opgehaald document een identifier mee te geven in de prompt, kan een taalmodel gevraagd " +
                      "worden om bij elke bewering het bijbehorende bronnummer te citeren. Dit verhoogt het vertrouwen van " +
                      "gebruikers en maakt het mogelijk om beweringen terug te traceren naar de oorspronkelijke tekst."
        },
        new()
        {
            Id = "query-rewriting",
            Title = "Query rewriting en expansie",
            Source = "kb/query-rewriting.md",
            Content = "Gebruikersvragen zijn vaak kort of dubbelzinnig. Query rewriting gebruikt een taalmodel om de " +
                      "oorspronkelijke vraag te herformuleren of uit te breiden met synoniemen en context, wat de kwaliteit " +
                      "van de daaropvolgende retrieval-stap verbetert, vooral bij vervolgvragen in een gesprek."
        },
        new()
        {
            Id = "hyde-techniek",
            Title = "HyDE: Hypothetical Document Embeddings",
            Source = "kb/hyde.md",
            Content = "HyDE laat een taalmodel eerst een hypothetisch antwoord op de vraag genereren, en gebruikt vervolgens " +
                      "de embedding van dát antwoord om te zoeken in de kennisbank, in plaats van de embedding van de vraag " +
                      "zelf. Dit kan de retrieval-kwaliteit verbeteren omdat antwoorden vaak meer lijken op documenten dan " +
                      "vragen dat doen."
        },
        new()
        {
            Id = "multi-hop-retrieval",
            Title = "Multi-hop retrieval",
            Source = "kb/multi-hop.md",
            Content = "Sommige vragen vereisen informatie uit meerdere, onderling verbonden documenten om beantwoord te " +
                      "worden. Multi-hop retrieval voert meerdere retrieval-rondes uit, waarbij tussentijdse resultaten worden " +
                      "gebruikt om vervolgvragen te genereren, totdat voldoende context verzameld is voor een volledig antwoord."
        },
        new()
        {
            Id = "conversation-memory",
            Title = "Gespreksgeheugen in een chatbot",
            Source = "kb/conversation-memory.md",
            Content = "Een chatbot met RAG moet vaak rekening houden met eerdere berichten in het gesprek om vervolgvragen " +
                      "correct te interpreteren. Dit kan door de volledige geschiedenis mee te sturen, door een samenvatting " +
                      "bij te houden, of door alleen de meest recente en relevante berichten te selecteren."
        },
        new()
        {
            Id = "token-kosten-optimalisatie",
            Title = "Token-kosten optimaliseren",
            Source = "kb/token-kosten.md",
            Content = "Kosten van LLM-API's worden meestal berekend per token. Optimalisaties zoals het beperken van het " +
                      "aantal opgehaalde documenten, het inkorten van system prompts, en het cachen van veelgebruikte context " +
                      "kunnen de kosten van een RAG-applicatie op schaal significant verlagen."
        },
        new()
        {
            Id = "prompt-injection",
            Title = "Prompt injection als beveiligingsrisico",
            Source = "kb/prompt-injection.md",
            Content = "Prompt injection is een aanval waarbij kwaadaardige instructies verborgen worden in invoerdata (zoals " +
                      "een opgehaald document) om het gedrag van een taalmodel te manipuleren. Bij RAG-systemen die externe of " +
                      "gebruikersgegenereerde content indexeren, is het belangrijk om opgehaalde content als data te behandelen " +
                      "en niet als vertrouwde instructies."
        },
        new()
        {
            Id = "data-privacy-rag",
            Title = "Privacy-overwegingen bij RAG",
            Source = "kb/data-privacy.md",
            Content = "Wanneer een kennisbank persoonsgegevens bevat, moet een RAG-systeem voldoen aan privacywetgeving " +
                      "zoals de AVG/GDPR. Dit betekent onder andere: toegangscontrole op documentniveau, het kunnen verwijderen " +
                      "van specifieke documenten inclusief hun embeddings, en het vermijden van het lekken van gevoelige data " +
                      "via gegenereerde antwoorden."
        },
        new()
        {
            Id = "document-versionering",
            Title = "Versionering van kennisbankdocumenten",
            Source = "kb/versionering.md",
            Content = "Wanneer brondocumenten wijzigen, moet de bijbehorende index bijgewerkt worden om verouderde " +
                      "informatie te voorkomen. Een strategie is elk document een versienummer of last-modified-timestamp te " +
                      "geven, zodat een incrementele sync-job alleen gewijzigde documenten opnieuw hoeft te embedden en te " +
                      "indexeren."
        },
        new()
        {
            Id = "incremental-indexing",
            Title = "Incrementeel indexeren versus volledige herindexering",
            Source = "kb/incremental-indexing.md",
            Content = "Een volledige herindexering van alle documenten is kostbaar bij grote kennisbanken. Incrementeel " +
                      "indexeren verwerkt alleen nieuwe of gewijzigde documenten, vaak via een change-data-capture-mechanisme " +
                      "of een periodieke sync die timestamps vergelijkt met de laatst geïndexeerde staat."
        },
        new()
        {
            Id = "index-aliasing",
            Title = "Index aliases voor zero-downtime herindexering",
            Source = "kb/index-aliasing.md",
            Content = "Elasticsearch aliases laten een logische naam verwijzen naar een fysieke index. Door een nieuwe index " +
                      "op te bouwen, deze te vullen, en pas daarna de alias te verplaatsen, kan een volledige herindexering " +
                      "plaatsvinden zonder downtime voor de applicatie die op de alias zoekt."
        },
        new()
        {
            Id = "monitoring-rag-pipeline",
            Title = "Monitoring van een RAG-pipeline",
            Source = "kb/monitoring.md",
            Content = "Belangrijke metrics om te monitoren in een RAG-systeem zijn: retrieval-latency, generatie-latency, " +
                      "aantal opgehaalde documenten, tokengebruik, en gebruikersfeedback (zoals thumbs up/down). Application " +
                      "Insights of vergelijkbare APM-tools kunnen deze metrics vastleggen voor analyse en alerting."
        },
        new()
        {
            Id = "ab-testing-prompts",
            Title = "A/B-testen van prompts en modellen",
            Source = "kb/ab-testing.md",
            Content = "Door verschillende prompt-varianten of modellen willekeurig aan gebruikers toe te wijzen en de " +
                      "resultaten te meten (bijvoorbeeld via feedback-scores of taakvoltooiing), kan objectief bepaald worden " +
                      "welke aanpak beter presteert voordat deze breed wordt uitgerold."
        },
        new()
        {
            Id = "unit-testing-llm-code",
            Title = "Unit testen van LLM-integraties",
            Source = "kb/unit-testing-llm.md",
            Content = "Directe calls naar een LLM zijn niet-deterministisch en traag, wat ze ongeschikt maakt voor unit " +
                      "tests. Door de LLM-client achter een interface te abstraheren, kan deze in tests worden gemockt zodat " +
                      "de omliggende logica (prompt-opbouw, response-parsing) betrouwbaar getest kan worden."
        },
        new()
        {
            Id = "integration-testing-elasticsearch",
            Title = "Integratietests met Elasticsearch",
            Source = "kb/integration-testing.md",
            Content = "Voor integratietests tegen Elasticsearch wordt vaak een tijdelijke container gebruikt, bijvoorbeeld " +
                      "via Testcontainers. Dit start een schone Elasticsearch-instantie per testrun, zodat tests niet " +
                      "afhankelijk zijn van gedeelde infrastructuur en geen bijwerkingen achterlaten voor andere tests."
        },
        new()
        {
            Id = "xunit-basics",
            Title = "Testen met xUnit in .NET",
            Source = "kb/xunit.md",
            Content = "xUnit is een populair testframework voor .NET waarbij tests worden gemarkeerd met [Fact] voor " +
                      "eenvoudige tests of [Theory] met [InlineData] voor geparametriseerde tests. Het ondersteunt dependency " +
                      "injection van fixtures via constructor-injectie voor gedeelde testcontext."
        },
        new()
        {
            Id = "swagger-openapi",
            Title = "API-documentatie met Swagger/OpenAPI",
            Source = "kb/swagger.md",
            Content = "Swagger (OpenAPI) genereert automatisch interactieve API-documentatie op basis van de endpoints en " +
                      "modellen in een ASP.NET Core-applicatie. Dit maakt het voor andere ontwikkelaars eenvoudig om de RAG-API " +
                      "te verkennen en te testen zonder aparte documentatie te hoeven schrijven."
        },
        new()
        {
            Id = "cors-configuratie",
            Title = "CORS configureren in ASP.NET Core",
            Source = "kb/cors.md",
            Content = "Cross-Origin Resource Sharing (CORS) bepaalt welke externe domeinen een browser toestaat om requests " +
                      "te sturen naar een API. Voor een RAG-frontend die op een ander domein draait dan de backend, moet CORS " +
                      "expliciet geconfigureerd worden om de juiste origins, headers en methodes toe te staan."
        },
        new()
        {
            Id = "health-checks-dotnet",
            Title = "Health checks in ASP.NET Core",
            Source = "kb/health-checks.md",
            Content = "ASP.NET Core biedt ingebouwde ondersteuning voor health check-endpoints die de status van een " +
                      "applicatie en zijn afhankelijkheden (zoals Elasticsearch of een LLM-service) rapporteren. Dit is " +
                      "essentieel voor orchestratieplatforms zoals Kubernetes om te bepalen of een instantie gezond is."
        },
        new()
        {
            Id = "containerization-dotnet",
            Title = ".NET-applicaties containeriseren",
            Source = "kb/containerization.md",
            Content = "Een .NET-applicatie kan met een Dockerfile op basis van het officiële mcr.microsoft.com/dotnet/" +
                      "aspnet-image gecontaineriseerd worden. Multi-stage builds scheiden de build-omgeving (SDK-image) van de " +
                      "uiteindelijke runtime-image, wat resulteert in kleinere en veiligere productie-images."
        },
        new()
        {
            Id = "kubernetes-deployment-rag",
            Title = "Een RAG-applicatie deployen op Kubernetes",
            Source = "kb/kubernetes.md",
            Content = "Bij het deployen van een RAG-stack op Kubernetes draaien Elasticsearch en de API doorgaans als " +
                      "aparte Deployments met eigen Services. Persistent Volumes zorgen voor duurzame opslag van de " +
                      "Elasticsearch-data, en ConfigMaps/Secrets beheren configuratie en gevoelige waarden zoals API-keys."
        },
        new()
        {
            Id = "caching-strategie",
            Title = "Caching-strategieën voor embeddings",
            Source = "kb/caching-embeddings.md",
            Content = "Het opnieuw berekenen van embeddings voor onveranderde content is verspilling. Door de embedding van " +
                      "een document te cachen op basis van een hash van de content, wordt herberekening alleen uitgevoerd " +
                      "wanneer de content daadwerkelijk gewijzigd is, wat tijd en API-kosten bespaart."
        },
        new()
        {
            Id = "redis-caching",
            Title = "Redis als cache-laag",
            Source = "kb/redis.md",
            Content = "Redis is een in-memory key-value store die vaak gebruikt wordt als cache-laag voor snelle toegang tot " +
                      "veelgebruikte data, zoals eerder berekende embeddings of LLM-antwoorden. Redis ondersteunt ook TTL " +
                      "(time-to-live) om gecachte waarden automatisch te laten verlopen."
        },
        new()
        {
            Id = "vector-quantization-storage",
            Title = "Vector quantization voor opslagoptimalisatie",
            Source = "kb/vector-quantization.md",
            Content = "Elasticsearch ondersteunt scalar en binary quantization van dense vectors, waarbij float32-waarden " +
                      "worden omgezet naar compactere representaties zoals int8 of zelfs 1-bit. Dit vermindert geheugengebruik " +
                      "aanzienlijk met een beperkte impact op zoeknauwkeurigheid."
        },
        new()
        {
            Id = "elser-sparse-vectors",
            Title = "ELSER en sparse vector search",
            Source = "kb/elser.md",
            Content = "ELSER (Elastic Learned Sparse EncodeR) is een door Elastic ontwikkeld model dat sparse vectors " +
                      "genereert in plaats van dense embeddings. Sparse vectors combineren voordelen van keyword-matching met " +
                      "semantisch begrip en zijn direct bruikbaar binnen Elasticsearch zonder externe embedding-service."
        },
        new()
        {
            Id = "knowledge-graph-vs-rag",
            Title = "Knowledge graphs versus vector-based RAG",
            Source = "kb/knowledge-graph.md",
            Content = "Een knowledge graph legt entiteiten en hun relaties expliciet vast, wat goed is voor precieze, " +
                      "gestructureerde vragen. Vector-based RAG is flexibeler voor ongestructureerde tekst maar mist expliciete " +
                      "relaties. GraphRAG combineert beide door een graaf te gebruiken naast embeddings voor rijkere context."
        },
        new()
        {
            Id = "pdf-extractie",
            Title = "Tekst extraheren uit PDF-documenten",
            Source = "kb/pdf-extractie.md",
            Content = "Voordat PDF-documenten in een RAG-kennisbank kunnen worden opgenomen, moet de tekst geëxtraheerd " +
                      "worden, inclusief het omgaan met kolommen, tabellen en gescande afbeeldingen (via OCR). Bibliotheken " +
                      "zoals PdfPig of iText worden hiervoor vaak gebruikt in .NET-projecten."
        },
        new()
        {
            Id = "markdown-parsing",
            Title = "Markdown-documenten verwerken voor RAG",
            Source = "kb/markdown-parsing.md",
            Content = "Markdown-bestanden lenen zich goed voor chunking op basis van headers, omdat de structuur al " +
                      "expliciet aanwezig is. Een parser kan de documentstructuur (H1, H2, secties) gebruiken om logische, " +
                      "samenhangende chunks te maken in plaats van willekeurige tekstblokken."
        },
        new()
        {
            Id = "web-scraping-kennisbank",
            Title = "Webcontent scrapen voor een kennisbank",
            Source = "kb/web-scraping.md",
            Content = "Bij het opbouwen van een kennisbank uit websites moet rekening gehouden worden met robots.txt, " +
                      "rate limiting richting de bron, en het verwijderen van navigatie- en advertentie-elementen zodat alleen " +
                      "de relevante hoofdcontent overblijft voor indexering."
        },
        new()
        {
            Id = "sql-database-als-bron",
            Title = "Structured data uit SQL-databases in RAG",
            Source = "kb/sql-als-bron.md",
            Content = "Data uit relationele databases kan omgezet worden naar tekstuele representaties (bijvoorbeeld " +
                      "'Klant X heeft op datum Y bestelling Z geplaatst') om doorzoekbaar te maken via RAG. Alternatief kan " +
                      "een agent met function calling direct SQL-queries genereren en uitvoeren op basis van een vraag."
        },
        new()
        {
            Id = "text-to-sql",
            Title = "Text-to-SQL met taalmodellen",
            Source = "kb/text-to-sql.md",
            Content = "Text-to-SQL laat een taalmodel een natuurlijke-taalvraag omzetten naar een SQL-query, op basis van " +
                      "het databaseschema dat als context wordt meegegeven. Dit is een alternatief voor RAG wanneer de data al " +
                      "gestructureerd in een database staat en actuele, exacte resultaten vereist zijn."
        },
        new()
        {
            Id = "feedback-loop-rag",
            Title = "Feedback loops verbeteren RAG-kwaliteit",
            Source = "kb/feedback-loop.md",
            Content = "Door gebruikers de mogelijkheid te geven antwoorden te beoordelen (bijvoorbeeld thumbs up/down), " +
                      "ontstaat een dataset om retrieval- en generatieproblemen te identificeren. Deze feedback kan gebruikt " +
                      "worden om prompts te verbeteren, slechte documenten te herzien, of retrieval-parameters bij te stellen."
        },
        new()
        {
            Id = "cold-start-probleem",
            Title = "Het cold-start-probleem bij een nieuwe kennisbank",
            Source = "kb/cold-start.md",
            Content = "Bij een nieuwe RAG-toepassing is er vaak nog weinig data over welke vragen gesteld worden en welke " +
                      "documenten relevant zijn. Synthetische testvragen genereren op basis van bestaande documenten kan " +
                      "helpen om de pipeline te valideren voordat er echte gebruikersdata beschikbaar is."
        },
        new()
        {
            Id = "latency-optimalisatie",
            Title = "Latency optimaliseren in een RAG-pipeline",
            Source = "kb/latency.md",
            Content = "De totale latency van een RAG-antwoord bestaat uit embedding-tijd, retrieval-tijd en generatietijd. " +
                      "Parallellisatie van onafhankelijke stappen, kleinere/snellere embeddingmodellen, en streaming van de " +
                      "generatie-output kunnen de waargenomen wachttijd voor gebruikers aanzienlijk verkorten."
        },
        new()
        {
            Id = "batch-processing-embeddings",
            Title = "Batch processing van embeddings",
            Source = "kb/batch-embeddings.md",
            Content = "In plaats van elk document los naar een embedding-API te sturen, kunnen meerdere teksten in één " +
                      "batch-aanroep verwerkt worden. Dit vermindert netwerkoverhead en maakt beter gebruik van GPU-parallellisme, " +
                      "wat vooral bij het initieel vullen van een grote kennisbank tijd bespaart."
        },
        new()
        {
            Id = "elasticsearch-aggregaties",
            Title = "Aggregaties in Elasticsearch",
            Source = "kb/aggregaties.md",
            Content = "Naast zoeken ondersteunt Elasticsearch krachtige aggregaties om statistieken over data te berekenen, " +
                      "zoals aantallen per categorie, gemiddelden of histogrammen. Dit kan gebruikt worden om bijvoorbeeld een " +
                      "dashboard te bouwen met inzicht in welke documenttypes het vaakst worden teruggevonden."
        },
        new()
        {
            Id = "highlighting-search-results",
            Title = "Highlighting van zoekresultaten",
            Source = "kb/highlighting.md",
            Content = "Elasticsearch kan automatisch de matchende tekstfragmenten in een zoekresultaat markeren via de " +
                      "highlight-functionaliteit. Dit is vooral nuttig bij keyword- of hybride zoekopdrachten, om gebruikers " +
                      "snel te laten zien waarom een document als relevant werd beschouwd."
        },
        new()
        {
            Id = "synonym-filters",
            Title = "Synoniemen configureren in Elasticsearch",
            Source = "kb/synonym-filters.md",
            Content = "Een synonym filter in de Elasticsearch-analyzer zorgt ervoor dat zoekopdrachten ook matchen op " +
                      "synoniemen van de ingevoerde term, bijvoorbeeld 'auto' en 'wagen'. Dit verbetert de recall van keyword-" +
                      "search en vult goed aan op de semantische mogelijkheden van vector search."
        },
        new()
        {
            Id = "analyzer-tokenizer",
            Title = "Analyzers en tokenizers in Elasticsearch",
            Source = "kb/analyzer-tokenizer.md",
            Content = "Een analyzer in Elasticsearch bestaat uit een tokenizer die tekst opsplitst in termen, en filters die " +
                      "deze termen bewerken (bijvoorbeeld lowercase, stemming, stopwoorden verwijderen). De juiste analyzer-" +
                      "configuratie is bepalend voor de kwaliteit van keyword-gebaseerde zoekresultaten."
        },
        new()
        {
            Id = "nederlandse-taalverwerking",
            Title = "Nederlandstalige tekstverwerking",
            Source = "kb/nederlandse-taal.md",
            Content = "Voor Nederlandstalige content is het gebruik van een Nederlandse stemmer en stopwoordenlijst " +
                      "belangrijk voor goede keyword search, aangezien Nederlandse woorden anders vervoegen dan Engelse. " +
                      "Embeddingmodellen moeten daarnaast expliciet meertalige of Nederlandstalige ondersteuning bieden voor " +
                      "goede semantische resultaten."
        },
        new()
        {
            Id = "multilinguale-rag",
            Title = "Meertalige RAG-systemen",
            Source = "kb/multilinguale-rag.md",
            Content = "Een meertalig RAG-systeem moet vragen in de ene taal kunnen matchen met documenten in een andere " +
                      "taal. Meertalige embeddingmodellen plaatsen semantisch gelijke tekst in verschillende talen dicht bij " +
                      "elkaar in de vectorruimte, wat cross-linguale retrieval mogelijk maakt zonder expliciete vertaling."
        },
        new()
        {
            Id = "user-permissions-search",
            Title = "Toegangsrechten toepassen op zoekresultaten",
            Source = "kb/user-permissions.md",
            Content = "In een enterprise RAG-toepassing mogen gebruikers alleen documenten zien waarvoor ze geautoriseerd " +
                      "zijn. Dit wordt meestal afgedwongen door een verplicht permissions-filter toe te voegen aan elke " +
                      "zoekopdracht, gebaseerd op de rollen of groepen van de ingelogde gebruiker."
        },
        new()
        {
            Id = "audit-logging-rag",
            Title = "Audit logging voor RAG-toepassingen",
            Source = "kb/audit-logging.md",
            Content = "Voor compliance en traceerbaarheid is het belangrijk om vast te leggen welke gebruiker welke vraag " +
                      "stelde, welke documenten werden opgehaald, en welk antwoord werd gegenereerd. Deze audit trail helpt " +
                      "bij het onderzoeken van incidenten en het aantonen van naleving van interne beleidsregels."
        },
        new()
        {
            Id = "cost-monitoring-llm",
            Title = "Kosten monitoren van LLM-gebruik",
            Source = "kb/cost-monitoring.md",
            Content = "Door tokengebruik per request te loggen, gekoppeld aan gebruiker of feature, kan een organisatie " +
                      "inzicht krijgen in waar LLM-kosten vandaan komen. Dit maakt het mogelijk om budgetten in te stellen en " +
                      "kostbare patronen, zoals overbodig lange prompts, te identificeren en te optimaliseren."
        },
        new()
        {
            Id = "versioning-embeddings-model",
            Title = "Omgaan met embeddingmodel-versies",
            Source = "kb/embedding-versioning.md",
            Content = "Wanneer je overstapt naar een nieuw embeddingmodel, moeten alle bestaande documenten opnieuw " +
                      "geëmbed worden, omdat vectoren van verschillende modellen niet vergelijkbaar zijn. Het is aan te raden " +
                      "om het gebruikte modelversienummer bij elk document op te slaan om dit soort migraties te vereenvoudigen."
        },
        new()
        {
            Id = "blue-green-deployment",
            Title = "Blue-green deployment voor een RAG-index",
            Source = "kb/blue-green.md",
            Content = "Bij het herindexeren met een nieuw embeddingmodel kan blue-green deployment gebruikt worden: een " +
                      "nieuwe index (green) wordt volledig opgebouwd naast de bestaande (blue), en pas na validatie wordt het " +
                      "verkeer overgeschakeld, zodat gebruikers geen verstoring merken."
        },
        new()
        {
            Id = "chatgpt-vs-lokale-modellen",
            Title = "Cloud-modellen versus lokale modellen: een afweging",
            Source = "kb/cloud-vs-lokaal.md",
            Content = "De keuze tussen een cloud-LLM zoals GPT of Claude en een lokaal model via Ollama hangt af van " +
                      "vereisten rond datagevoeligheid, kosten op schaal, gewenste modelkwaliteit en beschikbare hardware. " +
                      "Sommige organisaties kiezen voor een hybride aanpak: lokaal voor gevoelige data, cloud voor complexere " +
                      "taken."
        },
        new()
        {
            Id = "embedding-normalisatie",
            Title = "Normalisatie van embeddingvectoren",
            Source = "kb/embedding-normalisatie.md",
            Content = "Veel embeddingmodellen leveren al genormaliseerde vectoren (lengte 1), maar als dat niet het geval " +
                      "is, kan normalisatie vóór opslag nodig zijn om cosine similarity correct te laten werken als dot " +
                      "product, wat rekenkundig efficiënter is bij grote datasets."
        },
        new()
        {
            Id = "similarity-threshold",
            Title = "Similarity-drempels instellen",
            Source = "kb/similarity-threshold.md",
            Content = "Een minimale similarity-score kan gebruikt worden om irrelevante resultaten uit te sluiten, zodat " +
                      "een RAG-systeem liever aangeeft geen relevante informatie te hebben gevonden dan een zwak passend " +
                      "document als context te gebruiken. De juiste drempelwaarde is vaak afhankelijk van het gebruikte " +
                      "embeddingmodel en moet empirisch bepaald worden."
        },
        new()
        {
            Id = "top-k-selectie",
            Title = "Het kiezen van de juiste top-k waarde",
            Source = "kb/top-k.md",
            Content = "De top-k parameter bepaalt hoeveel documenten worden opgehaald voor een vraag. Een te lage waarde " +
                      "kan relevante context missen, terwijl een te hoge waarde het context window vult met ruis en de kosten " +
                      "verhoogt. Vaak wordt een grotere k opgehaald en vervolgens door een reranker teruggebracht naar een " +
                      "kleinere, meer relevante set."
        },
    };
}
