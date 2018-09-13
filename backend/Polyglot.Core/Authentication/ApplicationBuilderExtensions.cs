using Microsoft.AspNetCore.Builder;

namespace Polyglot.Core.Authentication
{
    public static class ApplicationBuilderExtensions
    {
      //  public static IApplicationBuilder UseCustomizedIdentity(this IApplicationBuilder app)
      //  {
      //      app.UseWhen(
      //          context => context.Request.Path.StartsWithSegments(""),
      //          a => a.Use(async (context, next) =>
      //              {
						//if (!context.Request.Path.StartsWithSegments("/workspaceHub") &&
						//	!context.Request.Path.StartsWithSegments("/chatHub"))
						//{
						//	CurrentUser.CurrentContext = context;
						//}
      //                  await next();
      //              }
      //          )
      //      );
      //      return app;
      //  }

        //public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection services, string projectId)
        //{
        //    services
        //        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //        .AddJwtBearer(options =>
        //        {
        //            options.Authority = "https://securetoken.google.com/" + projectId;
        //            options.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuer = true,
        //                ValidIssuer = "https://securetoken.google.com/" + projectId,
        //                ValidateAudience = true,
        //                ValidAudience = projectId,
        //                ValidateLifetime = true
        //            };
        //        });
        //    return services;
        //}
    }
}
