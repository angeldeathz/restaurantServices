using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [RoutePrefix("api/personas")]
    public class PersonasController : ApiController
    {
        private readonly PersonaBl _personaBl;

        public PersonasController()
        {
            _personaBl = new PersonaBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var personas = await _personaBl.ObtenerTodosAsync();

            if (personas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(personas);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var persona = await _personaBl.ObtenerPorIdAsync(id);

            if (persona == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(persona);
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get([FromUri] string rut)
        {
            var persona = await _personaBl.ObtenerPorRutAsync(rut);

            if (persona == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(persona);
        }
    }
}