using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polyglot.DataAccess.FileRepository;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.MongoRepository;
using Polyglot.DataAccess.Seeds;
using Polyglot.DataAccess.SqlRepository;

namespace Polyglot.DataAccess
{
    public static class DataAccessModule
    {
        /// <summary>
        /// Register DI dependencies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            string connectionStr = configuration.GetConnectionString("PolyglotDatabase");
            services.AddDbContext<DataContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(connectionStr);
            });
            services.AddScoped(typeof(DbContext), typeof(DataContext));

            services.AddScoped<IFileStorageProvider, FileStorageProvider>(provider =>
                new FileStorageProvider(configuration.GetConnectionString("PolyglotStorage")));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));


            services.Configure<Settings>(options => {
                options.ConnectionString = configuration.GetSection("MongoConnection:MongoConnectionString").Value;
                options.Database = configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddScoped<IMongoDataContext, MongoDataContext>();
        }


        public static void ConfigureMiddleware(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate();
                serviceScope.ServiceProvider.GetService<DataContext>().EnsureSeeded();
            }

            // using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            // {
            //     var context = serviceScope.ServiceProvider.GetRequiredService<IMongoDataContext>();
            //     MongoDbSeedsInitializer.MongoSeedAsync(context);
            // }
        }
    }
}
