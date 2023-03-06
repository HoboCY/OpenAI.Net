using OpenAI.Net.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using OpenAI.Net.Completions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenAI.Net
{
    public class OpenAIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ModelApiEndPoint = "https://api.openai.com/v1/models";
        private const string CompletionEndPoint = "https://api.openai.com/v1/completions";
        private readonly OpenAIOptions _openAIOptions;

        public OpenAIClient(IHttpClientFactory httpClientFactory, IOptions<OpenAIOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _openAIOptions = options.Value;
        }

        public async Task<List<Model>> GetModelsAsync()
        {
            var apiResult = await GetAsync<ApiResult>(ModelApiEndPoint);
            return apiResult.Data;
        }

        /// <summary>
        /// Retrieves a model instance, providing basic information about the model such as the owner and permissioning.
        /// </summary>
        /// <param name="id">The ID of the model to use for this request</param>
        /// <returns></returns>
        public async Task<Model> GetModelAsync(string id)
        {
            var model = await GetAsync<Model>($"{ModelApiEndPoint}/{id}");
            return model;
        }

        /// <summary>
        /// Creates a completion for the provided prompt and parameters
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CompletionRespnse> CreateCompletionAsync(CompletionRequest request)
        {
            var completionResponse = await PostAsync<CompletionRequest, CompletionRespnse>(CompletionEndPoint, request);
            return completionResponse;
        }

        private HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _openAIOptions.ApiSecretKey);

            return client;
        }

        private async Task<T> GetAsync<T>(string url)
        {
            using (var client = GetClient())
            {
                using (var response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return (await response.Content.ReadFromJsonAsync<T>());
                    }

                    var error = await response.Content.ReadFromJsonAsync<ErrorResult>();
                    throw new HttpRequestException(error.Error.Message);
                }
            }
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request)
            where TRequest : class, new()
            where TResponse : class
        {
            using (var client = GetClient())
            {
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                };
                using (var response = await client.PostAsJsonAsync(url, request, jsonSerializerOptions))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return (await response.Content.ReadFromJsonAsync<TResponse>());
                    }

                    var error = await response.Content.ReadFromJsonAsync<ErrorResult>();

                    throw new HttpRequestException(error.Error.Message);
                }
            }
        }
    }
}