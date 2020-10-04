using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RestaurantServices.Restaurant.Shared.WebApiConfig
{
    public class Interceptor : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Method.Equals(HttpMethod.Post) ||
                actionContext.Request.Method.Equals(HttpMethod.Put))
            {
                if (actionContext.ActionArguments.Values.FirstOrDefault() == null)
                {
                    actionContext.Response = actionContext.Request
                        .CreateErrorResponse(HttpStatusCode.BadRequest, "La solitud no puede estar vacia");
                }
            }

            if (!actionContext.ModelState.IsValid)
            {
                var error = new
                {
                    error = actionContext.ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage)).ToList(),
                    codigoError = (int)HttpStatusCode.BadRequest
                };

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }
    }
}
