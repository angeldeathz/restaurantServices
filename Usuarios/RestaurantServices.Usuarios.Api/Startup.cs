using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using FluentValidation.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using RestaurantServices.Shared.Transversal.WebApiConfig;
using RestaurantServices.Usuarios.Api;

[assembly: OwinStartup(typeof(Startup))]
namespace RestaurantServices.Usuarios.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Filters.Add(new Interceptor());
            app.UseExternalSignInCookie();
            var oAuthServerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(oAuthServerOptions);

            Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private static void Register(HttpConfiguration config)
        {
            // Configure routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Configure Json formatter
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Configure FluentValidation
            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}