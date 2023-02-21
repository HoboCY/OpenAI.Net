using OpenAI.Net.Models;
using OpenAI.Net.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace OpenAI.Net.Services
{
    public class ModelService : IModelService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _baseUrl = "https://api.openai.com/v1/models";
        private readonly OpenAIOptions _openAIOptions;

        public ModelService(IHttpClientFactory httpClientFactory,IOptions<OpenAIOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _openAIOptions = options.Value;
        }

        public async Task<List<Model>> GetModelsAsync()
        {
            var apiResult = await GetAsync<ApiResult>(_baseUrl);

            return apiResult!.Data;
        }

        public async Task<Model> GetModelAsync(string id)
        {
            var client = GetClient();
            var model = await client.GetFromJsonAsync<Model>($"{_baseUrl}/{id}");

            return model!;
        }

        private HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _openAIOptions.ApiSecretKey);

            return client;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            using var client = GetClient();
            using var response = await client.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<T>())!;
            }

            var error = await response.Content.ReadFromJsonAsync<ErrorResult>()!;

            throw new HttpRequestException(error!.Error.Message);
        }
    }
}
