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
using Polyglot.Common.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Polyglot.Authentication.Extensions
{

    //public class Test : AuthorizeAttribute
    //{

    //}

    public static class ApplicationBuilderExtensions
    {
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

        //// [Authorize]
        //public static IApplicationBuilder UseCustomizedIdentity(this IApplicationBuilder app)
        //{
        //    //app.UseWhen(context => context.Request.Headers.ContainsKey("Authorization"), appBuilder =>
        //    //{
        //    //}
        //    app.UseAuthentication();

        //    app.UseWhen(
        //        context => context.Request.Path.StartsWithSegments(""),
        //        a => a.Use(async (context, next) =>
        //        {
        //            //context.User.Claims.ToArray().Count() == 0;
        //            //UserNataliHelper.FullName = context.User.Claims.ToArray()[0].Value; ;

        //            //context.User.GetName() don`t work here, but work in controllers
        //            if (context.User.GetName() != null)
        //            {
        //                UserNataliHelper.FullName = context.User.GetName();
        //            }
        //            else
        //            {
        //                UserNataliHelper.FullName = "unknown";
        //            }
        //            await context.Response.WriteAsync("Hi");
        //            await next();
        //        }));
        //    return app;
        //}

    }
}
