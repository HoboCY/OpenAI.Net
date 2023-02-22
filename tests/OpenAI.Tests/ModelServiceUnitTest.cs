using Microsoft.Extensions.Configuration;
using OpenAI.Net.Interfaces;


namespace OpenAI.Tests
{
    public class ModelServiceUnitTest
    {
        private readonly IModelService _modelService;

        public ModelServiceUnitTest(IModelService modelService)
        {
            _modelService = modelService;
        }

        [Fact]
        public async void Get_Model_List_Should_Be_Success()
        {
            var models = await _modelService.GetModelsAsync();
            Assert.True(models.Any());
        }

        [Theory]
        [InlineData("babbage")]
        [InlineData("davinci")]
        public async void Get_Model_Should_Be_Success(string id)
        {
            var model = await _modelService.GetModelAsync(id);

            Assert.NotNull(model);
            Assert.True(model.Id == id);
        }

        [Fact]
        public async Task Get_Model_List_With_InvalidKey_Should_Be_Throw_Exception()
        {
            await Assert.ThrowsAsync<HttpRequestException>(async () => await _modelService.GetModelsAsync());
        }
    }
}