using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using RestaurantServices.Usuarios.Api;

[assembly: OwinStartup(typeof(Startup))]
namespace RestaurantServices.Usuarios.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            app.UseExternalSignInCookie();
            var oAuthServerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(oAuthServerOptions);

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}