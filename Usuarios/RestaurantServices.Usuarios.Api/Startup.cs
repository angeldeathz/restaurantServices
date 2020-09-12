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
            //ConfigureOAuth(app);
            app.UseExternalSignInCookie();
            var oAuthServerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(oAuthServerOptions);

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        //public void ConfigureOAuth(IAppBuilder app)
        //{
        //    var oAuthServerOptions = new OAuthAuthorizationServerOptions
        //    {
        //        AllowInsecureHttp = true,
        //        TokenEndpointPath = new PathString("/token"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
        //        Provider = new AuthConfig()
        //    };

        //    // Token Generation
        //    app.UseOAuthAuthorizationServer(oAuthServerOptions);
        //    app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        //}
    }
}