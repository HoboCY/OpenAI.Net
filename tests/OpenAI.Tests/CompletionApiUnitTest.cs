using OpenAI.Net;
using OpenAI.Net.Completions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class CompletionApiUnitTest
    {
        private readonly OpenAIClient _openAIClient;

        public CompletionApiUnitTest(OpenAIClient openAIClient)
        {
            _openAIClient = openAIClient;
        }

        [Fact]
        public async Task Completion_Request_Should_Be_Success()
        {
            var completion = await _openAIClient.CreateCompletionAsync(new CompletionRequest()
            {
                Prompt = "Say this is a test",
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
