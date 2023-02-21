using Microsoft.Extensions.DependencyInjection;
using OpenAI.Net.Interfaces;
using OpenAI.Net.Services;

namespace OpenAI.Net
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAI(this IServiceCollection services)
        {
            services.AddHttpClient("OpenAI");
            services.AddTransient<IModelService, ModelService>();

            return services;
        }
    }
}
