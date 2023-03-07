using OpenAI.Net;
using OpenAI.Net.TextCompletions;

namespace OpenAI.Tests
{
    public class TextCompletionApiUnitTest
    {
        private readonly OpenAIClient _openAiClient;

        public TextCompletionApiUnitTest(OpenAIClient openAiClient)
        {
            _openAiClient = openAiClient;
        }

        [Fact]
        public async Task Completion_Request_Should_Be_Success()
        {
            var completion = await _openAiClient.CreateCompletionAsync(new CompletionRequest()
            {
                Prompt = new[]{"Say this is a test"},
                Model = "text-davinci-003",
                MaxTokens = 50,
                Temperature = 0,
                TopP = 1,
                N = 1
            });

            Assert.True(completion.Choices.Any());
            Assert.True(completion.Usage.CompletionTokens > 0);
        }
    }
}
