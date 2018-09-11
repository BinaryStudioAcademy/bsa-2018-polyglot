using System;
using System.Linq;
using System.Security.Claims;

namespace Polyglot.Core.Authentication
{
    public static class IdentityExtensions
    {
        public static string GetEmail(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
        }

        public static string GetName(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, "name");
        }

        public static string GetUid(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, "user_id");
        }

        public static string GetProfilePicture(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, "picture");
        }

        public static DateTime GetExpirationDateTimeUtc(this ClaimsPrincipal current)
        {
            var timestamp = long.Parse(GetClaimValue(current, "exp"));
            var datetime = DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
            return datetime;
        }

        public static bool IsEmailVerified(this ClaimsPrincipal current)
        {
            return Convert.ToBoolean(GetClaimValue(current, "email_verified"));
        }

        public static string GetClaimValue(ClaimsPrincipal principal, string type)
        {
            return principal.Claims.FirstOrDefault(c => c.Type.Equals(type))?.Value;
        }
    }
}
