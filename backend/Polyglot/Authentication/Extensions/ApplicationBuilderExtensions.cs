using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace Polyglot.Authentication.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        //[Authorize]
        //public static IApplicationBuilder UseCustomizedIdentity(this IApplicationBuilder app)
        //{
        //    //app.UseWhen(context => context.Request.Headers.ContainsKey("Authorization"), appBuilder =>
        //    //    //{
        //    //    //}

        //    app.UseWhen(
        //    context => context.Request.Path.StartsWithSegments(""),
        //    a => a.Use(async (context, next) =>
        //    {
        //        UserIdentityService identityService = new UserIdentityService();
        //        await identityService.SaveDate(context);
        //        await next();
        //    }));
        //    return app;
        //}

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
