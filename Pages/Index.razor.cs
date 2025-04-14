namespace SemanticKernelPluginDemov4.Pages
{

    public partial class Index
    {

        public string Question { get; set; } = string.Empty;

        public string Answer { get; set; } = string.Empty;

        public async Task RunQuery()
        {
            Answer = string.Empty;

            await foreach (var chatUpdate in OpenAIChatcompletionService.RunQuery(Question))
            {

                Answer += chatUpdate;
                StateHasChanged();
            }

        }

    }

}
