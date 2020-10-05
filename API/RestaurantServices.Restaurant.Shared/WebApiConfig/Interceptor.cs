using System.Collections.Generic;
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
                    var errores = new
                    {
                        errores = new List<string>
                        {
                            "La solicitud no puede estar vacía"
                        },
                        codigoError = (int)HttpStatusCode.BadRequest
                    };

                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errores);
                    return;
                }
            }

            if (!actionContext.ModelState.IsValid)
            {
                var errores = new
                {
                    errores = actionContext.ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage)).ToList(),
                    codigoError = (int)HttpStatusCode.BadRequest
                };

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errores);
            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }
    }
}
