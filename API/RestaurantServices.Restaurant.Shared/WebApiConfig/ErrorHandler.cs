using System.Web.Http.ExceptionHandling;

namespace RestaurantServices.Restaurant.Shared.WebApiConfig
{
    public class ErrorHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            base.Handle(context);
        }
    }
}
