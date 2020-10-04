using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json;

namespace RestaurantServices.Restaurant.Shared.WebApiConfig
{
    public class ErrorHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = JsonConvert.SerializeObject(new
                {
                    error = new List<string>
                    {
                        context.Exception.Message
                    },
                    codigoError = HttpStatusCode.InternalServerError
                })
            };
        }

        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(Content, Encoding.UTF8, "application/json"), RequestMessage = Request
                };
                return Task.FromResult(response);
            }
        }
    }
}