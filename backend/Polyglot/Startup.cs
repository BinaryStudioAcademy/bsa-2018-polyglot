using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polyglot.Authentication.Extensions;
using Polyglot.BusinessLogic;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.BusinessLogic.Services;
using Polyglot.BusinessLogic.TranslationServices;
using Polyglot.DataAccess.FileRepository;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.MongoRepository;
using Polyglot.DataAccess.Seeds;
using Polyglot.DataAccess.SqlRepository;
using Polyglot.GlobalExceptionHandler;
using Polyglot.Hubs;
using mapper = Polyglot.Common.Mapping.AutoMapper;

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

            string connectionStr = Configuration.GetConnectionString("PolyglotDatabase");
            services.AddDbContext<DataContext>(options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlServer(connectionStr);
                });
            services.AddScoped(typeof(DbContext), typeof(DataContext));

            services.AddScoped<IFileStorageProvider, FileStorageProvider>(provider =>
                new FileStorageProvider(Configuration.GetConnectionString("PolyglotStorage")));

            services.AddScoped<ITranslatorProvider, TranslatorProvider>(provider =>
                new TranslatorProvider("https://translation.googleapis.com/language/translate/v2",
                    Configuration.GetValue<string>("google_translation_key")));

            services.AddFirebaseAuthentication(Configuration.GetValue<string>("Firebase:ProjectId"));
            services.AddScoped<IMapper>(sp => mapper.GetDefaultMapper());
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(ICRUDService<,>), typeof(CRUDService<,>));


            services.Configure<Settings>(options =>{
                        options.ConnectionString = Configuration.GetSection("MongoConnection:MongoConnectionString").Value;
                        options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            
            services.AddScoped<IMongoDataContext, MongoDataContext>();

            services.AddSignalR();

            BusinessLogicModule.ConfigureServices(services);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate();
                serviceScope.ServiceProvider.GetService<DataContext>().EnsureSeeded();
            }

            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<IMongoDataContext>();
            //    MongoDbSeedsInitializer.MongoSeedAsync(context);
            //}

            // if (env.IsDevelopment())
            // {
            app.UseCors("AllowAll");
            //}

            app.UseAuthentication();

            //app.UseCustomizedIdentity();

            app.ConfigureCustomExceptionMiddleware();

            app.UseMvc();

            app.UseSignalR(options =>
            {
                options.MapHub<ChatHub>("/hub");
                options.MapHub<WorkspaceHub>("/workspaceHub");
            });
        }
    }
}


/*

app.UseCors(builder => builder
    .WithOrigins("http://localhost:4200")
    .AllowAnyOrigin()
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod());
*/
