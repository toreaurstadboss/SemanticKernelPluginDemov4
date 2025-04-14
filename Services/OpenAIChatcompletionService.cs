using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace SemanticKernelPluginDemov4.Services
{

    public class OpenAIChatcompletionService : IOpenAIChatcompletionService
    {
        private readonly Kernel _kernel;

        private IChatCompletionService _chatCompletionService;

        public OpenAIChatcompletionService(Kernel kernel)
        {
            _kernel = kernel;
            _chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
        }

        public async IAsyncEnumerable<string?> RunQuery(string question)
        {
            var chatHistory = new ChatHistory();

            chatHistory.AddSystemMessage("You are a helpful assistant, answering only on questions about Northwind database. In case you got other questions, inform that you only can provide questions about the Northwind database. It is important that only the provided Northwind database functions added to the language model through plugin is used when answering the questions. If no answer is available, inform this.");

            chatHistory.AddUserMessage(question);

            await foreach (var chatUpdate in _chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory, CreateOpenAIExecutionSettings(), _kernel))
            {
                yield return chatUpdate.Content;
            }
        }

        private OpenAIPromptExecutionSettings? CreateOpenAIExecutionSettings()
        {
            return new OpenAIPromptExecutionSettings
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };
        }

    }
}
