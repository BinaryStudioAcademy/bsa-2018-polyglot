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
using Polyglot.DataAccess;
using Microsoft.EntityFrameworkCore;
using Polyglot.BusinessLogic.Implementations;
using Polyglot.Common.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Polyglot.Authentication.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        [Authorize]
        public static IApplicationBuilder UseCustomizedIdentity(this IApplicationBuilder app)
        {
            //app.UseWhen(context => context.Request.Headers.ContainsKey("Authorization"), appBuilder =>
            //    //{
            //    //}
            app.UseWhen(
            context => context.Request.Path.StartsWithSegments(""),
            a => a.Use(async (context, next) =>
            {
                UserIdentityService identityService = new UserIdentityService();
                await identityService.SaveDate(context);
                await next();
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
