using System.Web.Http;
using RestaurantServices.Shared.Modelo.Clases;

namespace RestaurantServices.Personas.Api.Controllers
{
    [Authorize, Route("")]
    public class PersonasController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody] Persona persona)
        {
            return Ok("Éxito");
        }
    }
}