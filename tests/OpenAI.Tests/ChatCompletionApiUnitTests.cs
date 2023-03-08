using OpenAI.Net;
using OpenAI.Net.Completions.ChatCompletions;

namespace OpenAI.Tests;

public class ChatCompletionApiUnitTests
{
    private readonly OpenAIClient _openAiClient;

    public ChatCompletionApiUnitTests(OpenAIClient openAiClient)
    {
        _openAiClient = openAiClient;
    }
    
    [Fact]
    public async Task Chat_Completion_Request_Should_Be_Success()
    {
        var completion = await _openAiClient.CreateChatCompletionAsync(new ChatCompletionRequest
        {
            Messages = new List<Message>
            {
                new Message
                {
                    Role = "user",
                    Content = "Please tell me what are the 3 best selling games on steam recently"
                }
            },
            Model = "gpt-3.5-turbo"
        });

        Assert.True(completion.Choices.Any());
        Assert.True(completion.Usage.CompletionTokens > 0);
    }
}