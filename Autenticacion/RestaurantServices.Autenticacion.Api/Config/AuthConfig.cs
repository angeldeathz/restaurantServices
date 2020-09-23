using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using RestaurantServices.Restaurant.Shared.Http;

namespace RestaurantServices.Autenticacion.Api.Config
{
    public class AuthConfig : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => { context.Validated(); });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});
            var esInicioSesionValido = await ValidarCredencialesUsuarioAsync(context.UserName, context.Password);

            if (esInicioSesionValido)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("error", "Credenciales inválidas o el usuario está desactivado.");
            }
        }

        private async Task<bool> ValidarCredencialesUsuarioAsync(string rut, string contrasena)
        {
            var restClient = new RestClient();
            var respuesta = await restClient.GetAsync($"http://localhost/restaurant/usuarios?rut={rut}&contrasena={contrasena}");
            return respuesta.StatusName == HttpStatusCode.OK;
        }
    }
}