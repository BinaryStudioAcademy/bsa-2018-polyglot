using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polyglot.Authentication;
using Polyglot.BusinessLogic;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.BusinessLogic.Services;
using Polyglot.BusinessLogic.TranslationServices;
using Polyglot.DataAccess.FileRepository;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.MongoRepository;
using Polyglot.DataAccess.Seeds;
using Polyglot.DataAccess.SqlRepository;
using Polyglot.Hubs;
using mapper = Polyglot.Common.Mapping.AutoMapper;
using Polyglot.Common;
using Polyglot.Core;
using Polyglot.DataAccess;
using Microsoft.EntityFrameworkCore;
namespace Polyglot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );


            services.AddFirebaseAuthentication(Configuration.GetValue<string>("Firebase:ProjectId"));

            services.AddSignalR();

            services.AddScoped<ISignalrWorkspaceService, SignalrWorkspaceService>();

            BusinessLogicModule.ConfigureServices(services, Configuration);
            CommonModule.ConfigureServices(services);
            CoreModule.ConfigureServices(services);
            DataAccessModule.ConfigureServices(services, Configuration);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            BusinessLogicModule.ConfigureMiddleware(app);
            CommonModule.ConfigureMiddleware(app);
            CoreModule.ConfigureMiddleware(app);
            DataAccessModule.ConfigureMiddleware(app);

            // if (env.IsDevelopment())
            //  {
            app.UseCors("AllowAll");
            // }

            app.UseAuthentication();

            app.UseMvc();

            app.UseSignalR(options =>
            {
                options.MapHub<WorkspaceHub>("/workspaceHub");
            });

        }
    }
}