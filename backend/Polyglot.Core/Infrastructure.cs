using Microsoft.Extensions.DependencyInjection;
using Polyglot.Core.HttpServices;

namespace Polyglot.Core
{
    public static class CoreModule
    {
        /// <summary>
        /// Register DI dependencies
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IHttpService<,>), typeof(HttpService<,>));
        }
    }
}
