using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.BusinessLogic.Implementations;
using Polyglot.DataAccess.Entities;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using Polyglot.DataAccess.Repositories;
using Polyglot.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Polyglot.Authentication.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        static ICRUDService<UserProfile, int> service;
        public static IApplicationBuilder UseCustomizedIdentity(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseWhen(
                context => context.Request.Path.StartsWithSegments("/"),
                a => a.Use(async (context, next) =>
                {
                    //DataContext dataContext = new DataContext(new DbContextOptions<DataContext>());
                    //CRUDService<UserProfile> service = new CRUDService<UserProfile>(
                    //    new Repository<UserProfile>(dataContext),
                    //    new UnitOfWork(dataContext));

                    //UserProfile userProfile = new UserProfile()
                    //{
                    //    FirebaseId = context.User.GetUid(),
                    //    FullName = context.User.GetName(),
                    //    AvatarUrl = context.User.GetProfilePicture()
                    //};

                    //List<UserProfile> users = await service.GetListAsync() as List<UserProfile>;
                    //UserProfile userSearched = users.FirstOrDefault(user => user.FirebaseId == userProfile.FirebaseId);

                    //if (userSearched == null)
                    //{
                    //    userSearched = await service.PostAsync(userProfile);
                    //}
                    //UserNataliHelper.FullName = context.User.GetName();
                    //UserNataliHelper.AvatarUrl = context.User.GetName();
                }));

            return app;
        }

        public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection services, string projectId)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/" + projectId;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/" + projectId,
                        ValidateAudience = true,
                        ValidAudience = projectId,
                        ValidateLifetime = true
                    };
                });
            return services;
        }      
    }
}


//CRUDService<UserProfile> service = new CRUDService<UserProfile>(
//    new Repository<UserProfile>(
//        new DataContext(
//            new DbContextOptions<DataContext>())),
//    new UnitOfWork(
//        new DataContext(
//            new DbContextOptions<DataContext>())));

//Repository<UserProfile> service = new Repository<UserProfile>(new DataContext(new DbContextOptions<DataContext>()));
