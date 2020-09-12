using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace RestaurantServices.Usuarios.Api.Config
{
    public class AuthConfig : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => { context.Validated(); });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});

                //using (AuthRepository _repo = new AuthRepository())
                //{
                //    IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                //    if (user == null)
                //    {
                //        context.SetError("invalid_grant", "The user name or password is incorrect.");
                //        return;
                //    }
                //}

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                context.Validated(identity);
            });
        }
    }
}