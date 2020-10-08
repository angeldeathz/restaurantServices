using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/personas")]
    public class PersonasController : ApiController
    {
        private readonly PersonaBl _personaBl;

        public PersonasController()
        {
            _personaBl = new PersonaBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<Persona>))]
        public async Task<IHttpActionResult> Get1()
        {
            var personas = await _personaBl.ObtenerTodosAsync();

            if (personas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(personas);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(Persona))]
        public async Task<IHttpActionResult> Get2(int id)
        {
            var persona = await _personaBl.ObtenerPorIdAsync(id);

            if (persona == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(persona);
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(Persona))]
        public async Task<IHttpActionResult> Get3([FromUri] string rut)
        {
            var persona = await _personaBl.ObtenerPorRutAsync(rut);

            if (persona == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(persona);
        }
    }
}