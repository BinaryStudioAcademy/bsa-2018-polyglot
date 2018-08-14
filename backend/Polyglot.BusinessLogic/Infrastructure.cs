using Microsoft.Extensions.DependencyInjection;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.BusinessLogic.Services;

namespace Polyglot.BusinessLogic
{
    public static class BusinessLogicModule
    {
        /// <summary>
        /// Register DI dependencies
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IComplexStringService, Services.ComplexStringService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IManagerService, ManagerService>();
            services.AddTransient<ITeamService, TeamsService>();
        }
    }
}
