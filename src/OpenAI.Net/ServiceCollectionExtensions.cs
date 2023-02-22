using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Net.Interfaces;
using OpenAI.Net.Services;

namespace OpenAI.Net
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAI(this IServiceCollection services, IConfiguration section)
        {
            services.AddHttpClient("OpenAI");

            services.Configure<OpenAIOptions>(section);
            services.AddTransient<IModelService, ModelService>();

            return services;
        }
    }
}
