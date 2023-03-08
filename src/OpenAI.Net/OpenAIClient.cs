using OpenAI.Net.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenAI.Net.Completions;
using OpenAI.Net.Completions.ChatCompletions;
using OpenAI.Net.Completions.TextCompletions;

namespace OpenAI.Net
{
    public class OpenAIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string BaseUrl = "https://api.openai.com";
        private const string ModelApiEndPoint = "https://api.openai.com/v1/models";
        private readonly OpenAIOptions _openAiOptions;

        public OpenAIClient(IHttpClientFactory httpClientFactory, IOptions<OpenAIOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _openAiOptions = options.Value;
        }

        public async Task<List<Model>> GetModelsAsync()
        {
            var apiResult = await GetAsync<ModelResponse>(ModelApiEndPoint);
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
        public async Task<TextCompletionResponse> CreateTextCompletionAsync(TextCompletionRequest request)
        {
            var completionResponse =
                await PostAsync<TextCompletionRequest, TextCompletionResponse>(request);
            return completionResponse;
        }

        /// <summary>
        /// Creates a completion for the chat message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ChatCompletionResponse> CreateChatCompletionAsync(ChatCompletionRequest request)
        {
            var chatCompletionResponse = await PostAsync<ChatCompletionRequest, ChatCompletionResponse>(request);
            return chatCompletionResponse;
        }

        private HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient("OpenAI");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _openAiOptions.ApiSecretKey);

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
                        return await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
                    }

                    var error = await response.Content.ReadFromJsonAsync<ErrorResult>().ConfigureAwait(false);
                    throw new HttpRequestException(error.Error.Message);
                }
            }
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request)
            where TRequest : BaseRequest
        {
            using (var client = GetClient())
            {
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                };

                using (var response =
                       await client.PostAsJsonAsync($"{BaseUrl}{request.GetEndPoint()}", request, jsonSerializerOptions)
                           .ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<TResponse>().ConfigureAwait(false);
                    }

                    var error = await response.Content.ReadFromJsonAsync<ErrorResult>().ConfigureAwait(false);

                    throw new HttpRequestException(error.Error.Message);
                }
            }
        }
    }
}