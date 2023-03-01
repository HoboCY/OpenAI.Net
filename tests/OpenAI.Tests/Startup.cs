using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services,HostBuilderContext context)
        {
            services.AddOpenAI(context.Configuration.GetSection("OpenAIOptions"));
        }

        public void ConfigureHost(IHostBuilder hostBuilder) =>
        hostBuilder
            .ConfigureHostConfiguration(builder => 
            {
                builder.AddUserSecrets(typeof(Startup).Assembly);
                builder.AddJsonFile("appsettings.json", true); 
            });
    }
}
