namespace SemanticKernelPluginDemov4.Services
{

    public interface IOpenAIChatcompletionService
    {
        IAsyncEnumerable<string?> RunQuery(string question);
    }

}
