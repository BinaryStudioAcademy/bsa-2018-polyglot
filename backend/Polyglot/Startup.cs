using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polyglot.Authentication.Extensions;
using Polyglot.BusinessLogic.Implementations;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.NoSQL_Models;
using Polyglot.DataAccess.Repositories;
using mapper = Polyglot.Common.Mapping.AutoMapper;
//using Polyglot.DataAccess.NoSQL_Repository;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            string connectionStr = Configuration.GetConnectionString("PolyglotDatabase");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionStr));
            services.AddScoped(typeof(DbContext), typeof(DataContext));

            services.AddScoped<IFileStorageProvider, FileStorageProvider>(provider =>
                new FileStorageProvider(Configuration.GetConnectionString("PolyglotStorage")));

            services.AddFirebaseAuthentication(Configuration.GetValue<string>("Firebase:ProjectId"));
            services.AddScoped<IMapper>(sp => mapper.GetDefaultMapper());
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ICRUDService), typeof(CRUDService));


            services.Configure<Polyglot.DataAccess.NoSQL_Repository.Settings>(options =>{
                        options.ConnectionString = Configuration.GetSection("MongoConnection:MongoConnectionString").Value;
                        options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });
            services.AddScoped<Polyglot.DataAccess.NoSQL_Repository.IComplexStringRepository, Polyglot.DataAccess.NoSQL_Repository.ComplexStringRepository>();
            services.AddScoped<IRepository<ComplexString>, DataAccess.NoSQL_Repository.ComplexStringRepository>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IMongoDataContext, MongoDataContext>();

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
                context.Database.EnsureCreated();
            }

            app.UseCors("AllowAll");
            /*

            app.UseCors(builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyOrigin()
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod());
            */
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
