using System;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using FluentValidation.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using RestaurantServices.Restaurant.API;
using RestaurantServices.Restaurant.API.Config;
using RestaurantServices.Restaurant.Shared.WebApiConfig;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(Startup))]
namespace RestaurantServices.Restaurant.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Filters.Add(new Interceptor());
            config.Services.Replace(typeof(IExceptionHandler), new ErrorHandler());

            app.UseExternalSignInCookie();

            // se documenta para no pedir token en swagger
            //var oAuthServerOptions = new OAuthBearerAuthenticationOptions();
            //app.UseOAuthBearerAuthentication(oAuthServerOptions);

            // comentar esto para usar correctamente los token
            config.Filters.Add(new AuthFilter());

            Register(config);
            app.UseCors(CorsOptions.AllowAll);

            config
                .EnableSwagger(c =>
                    {
                        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\bin\";
                        var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                        var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                        c.SingleApiVersion("v1", "RestaurantServices.Restaurant.API")
                            .Description("API Rest de Restaurant Siglo XXI")
                            .Contact(cc => cc
                                .Name("Administrador")
                                .Url("https://www.restaurantesigloxxi.cl")
                                .Email("admin@restaurantesigloxxi.cl"));

                        c.PrettyPrint();
                        c.IncludeXmlComments(commentsFile);
                        c.RootUrl(req => "http://localhost/restaurant/");
                        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    }
                )
                .EnableSwaggerUi(c => c.DisableValidator());

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

            // elimina el formateo en XML de las respuestas 
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Configure FluentValidation
            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}