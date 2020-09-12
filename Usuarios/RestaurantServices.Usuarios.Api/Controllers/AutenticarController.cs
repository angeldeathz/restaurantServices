using System.Web.Http;

namespace RestaurantServices.Usuarios.Api.Controllers
{
    [Route("api/personas/autenticar")]
    public class AutenticarController : ApiController
    {
        [HttpGet, Authorize]
        public IHttpActionResult Algo()
        {
            var a = new
            {
                algo = "Prueba",
                nombre = "david"
            };

            return Ok(a);
        }
    }
}
