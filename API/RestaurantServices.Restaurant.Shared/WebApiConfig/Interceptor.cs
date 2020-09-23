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
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }
    }
}
