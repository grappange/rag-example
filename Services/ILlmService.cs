namespace RagExample.Services;

public interface ILlmService
{
    Task<string> CompleteAsync(string systemPrompt, string userPrompt, CancellationToken ct = default);
}
