using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using mapper = Polyglot.Common.Mapping.AutoMapper;

namespace Polyglot.Common
{
    public static class CommonModule
    {
        /// <summary>
        /// Register DI dependencies
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMapper>(sp => mapper.GetDefaultMapper());
        }

        public static void ConfigureMiddleware(IApplicationBuilder app)
        {
        }
    }
}
