using System.Web.Http.ExceptionHandling;

namespace RestaurantServices.Shared.Transversal.WebApiConfig
{
    public class ErrorHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            base.Handle(context);
        }
    }
}
