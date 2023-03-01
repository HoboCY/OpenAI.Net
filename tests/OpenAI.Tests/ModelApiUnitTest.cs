using OpenAI.Net;


namespace OpenAI.Tests
{
    public class ModelApiUnitTest
    {
        private readonly OpenAIClient _openAIClient;

        public ModelApiUnitTest(OpenAIClient openAIClient)
        {
            _openAIClient = openAIClient;
        }

        [Fact]
        public async void Get_Model_List_Should_Be_Success()
        {
            var models = await _openAIClient.GetModelsAsync();
            Assert.True(models.Any());
        }

        [Theory]
        [InlineData("babbage")]
        [InlineData("davinci")]
        public async void Get_Model_Should_Be_Success(string id)
        {
            var model = await _openAIClient.GetModelAsync(id);

            Assert.NotNull(model);
            Assert.True(model.Id == id);
        }

        [Fact]
        public async Task Get_Model_List_With_InvalidKey_Should_Be_Throw_Exception()
        {
            await Assert.ThrowsAsync<HttpRequestException>(async () => await _openAIClient.GetModelsAsync());
        }
    }
}