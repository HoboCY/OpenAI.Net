using Microsoft.Extensions.DependencyInjection;
using OpenAI.Net.Interfaces;
using OpenAI.Net.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<IModelService, ModelService>();
        }
    }
}
