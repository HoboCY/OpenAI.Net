using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OpenAI.Net
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAI(this IServiceCollection services, IConfiguration section)
        {
            services.AddHttpClient("OpenAI");

            services.Configure<OpenAIOptions>(section);
            services.AddTransient<OpenAIClient>();

            return services;
        }
    }
}
