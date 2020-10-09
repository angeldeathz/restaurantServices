using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace RestaurantServices.Restaurant.API.Config
{
    public class AuthFilter : IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "testUser"));
            claims.Add(new Claim(ClaimTypes.Role, "client"));
            claims.Add(new Claim("sub", "testUser"));
            claims.Add(new Claim("APP:USERID", "50123"));

            var identity = new ClaimsIdentity(claims, "Auth_Key");

            var principal = new ClaimsPrincipal(new[] { identity });
            context.Principal = principal;
            HttpContext.Current.User = context.Principal;
            Thread.CurrentPrincipal = context.Principal;
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
        }

        public bool AllowMultiple { get; }
    }
}