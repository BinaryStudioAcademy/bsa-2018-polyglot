using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Polyglot.Core.Authentication;
using Polyglot.Core.GlobalExceptionHandler;
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


        public static void ConfigureMiddleware(this IApplicationBuilder app)
        {
            app.ConfigureCustomExceptionMiddleware();
        }
    }
}
