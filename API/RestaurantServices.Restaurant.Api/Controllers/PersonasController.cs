using System.Web.Http;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, Route("personas")]
    public class PersonasController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody] Persona persona)
        {
            return Ok("Éxito");
        }
    }
}