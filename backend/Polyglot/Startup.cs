using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Polyglot.Authentication.Extensions;
using Polyglot.BusinessLogic.Implementations;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Repositories;
using mapper = Polyglot.Common.Mapping.AutoMapper;
using Polyglot.DataAccess.Interfaces;

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

            services.AddFirebaseAuthentication(Configuration.GetValue<string>("Firebase:ProjectId"));
            // automapper
            services.AddScoped<IMapper>(sp => mapper.GetDefaultMapper());
            //  uow
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

#warning костыль
            //repo
            services.AddScoped(typeof(IRepository<File>), typeof(Repository<File>));
            services.AddScoped(typeof(IRepository<Project>), typeof(Repository<Project>));
            services.AddScoped(typeof(IRepository<Glossary>), typeof(Repository<Glossary>));
            services.AddScoped(typeof(IRepository<Language>), typeof(Repository<Language>));
            services.AddScoped(typeof(IRepository<Manager>), typeof(Repository<Manager>));
            services.AddScoped(typeof(IRepository<ProjectGlossary>), typeof(Repository<ProjectGlossary>));
            services.AddScoped(typeof(IRepository<ProjectHistory>), typeof(Repository<ProjectHistory>));
            services.AddScoped(typeof(IRepository<ProjectLanguage>), typeof(Repository<ProjectLanguage>));
            services.AddScoped(typeof(IRepository<ProjectTag>), typeof(Repository<ProjectTag>));
            services.AddScoped(typeof(IRepository<Rating>), typeof(Repository<Rating>));
            services.AddScoped(typeof(IRepository<Right>), typeof(Repository<Right>));
            services.AddScoped(typeof(IRepository<Tag>), typeof(Repository<Tag>));
            services.AddScoped(typeof(IRepository<Team>), typeof(Repository<Team>));
            services.AddScoped(typeof(IRepository<TeamTranslator>), typeof(Repository<TeamTranslator>));
            services.AddScoped(typeof(IRepository<Translation>), typeof(Repository<Translation>));
            services.AddScoped(typeof(IRepository<Translator>), typeof(Repository<Translator>));
            services.AddScoped(typeof(IRepository<TranslatorLanguage>), typeof(Repository<TranslatorLanguage>));
            services.AddScoped(typeof(IRepository<TranslatorRight>), typeof(Repository<TranslatorRight>));
            services.AddScoped(typeof(IRepository<UserProfile>), typeof(Repository<UserProfile>));
            //  services
            services.AddScoped(typeof(ICRUDService<File, int>), typeof(CRUDService<File>));
            services.AddScoped(typeof(ICRUDService<Project, int>), typeof(CRUDService<Project>));
            services.AddScoped(typeof(ICRUDService<Glossary, int>), typeof(CRUDService<Glossary>));
            services.AddScoped(typeof(ICRUDService<Language, int>), typeof(CRUDService<Language>));
            services.AddScoped(typeof(ICRUDService<Manager, int>), typeof(CRUDService<Manager>));
            services.AddScoped(typeof(ICRUDService<ProjectGlossary, int>), typeof(CRUDService<ProjectGlossary>));
            services.AddScoped(typeof(ICRUDService<ProjectLanguage, int>), typeof(CRUDService<ProjectLanguage>));
            services.AddScoped(typeof(ICRUDService<ProjectHistory, int>), typeof(CRUDService<ProjectHistory>));
            services.AddScoped(typeof(ICRUDService<ProjectTag, int>), typeof(CRUDService<ProjectTag>));
            services.AddScoped(typeof(ICRUDService<Rating, int>), typeof(CRUDService<Rating>));
            services.AddScoped(typeof(ICRUDService<Right, int>), typeof(CRUDService<Right>));
            services.AddScoped(typeof(ICRUDService<Tag, int>), typeof(CRUDService<Tag>));
            services.AddScoped(typeof(ICRUDService<Team, int>), typeof(CRUDService<Team>));
            services.AddScoped(typeof(ICRUDService<TeamTranslator, int>), typeof(CRUDService<TeamTranslator>));
            services.AddScoped(typeof(ICRUDService<Translation, int>), typeof(CRUDService<Translation>));
            services.AddScoped(typeof(ICRUDService<Translator, int>), typeof(CRUDService<Translator>));
            services.AddScoped(typeof(ICRUDService<TranslatorLanguage, int>), typeof(CRUDService<TranslatorLanguage>));
            services.AddScoped(typeof(ICRUDService<TranslatorRight, int>), typeof(CRUDService<TranslatorRight>));
            services.AddScoped(typeof(ICRUDService<UserProfile, int>), typeof(CRUDService<UserProfile>));
            // ======================================================================================================


            services.AddScoped<IFileStorageProvider, FileStorageProvider>(provider =>
                new FileStorageProvider(Configuration.GetConnectionString("PolyglotStorage")));
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
