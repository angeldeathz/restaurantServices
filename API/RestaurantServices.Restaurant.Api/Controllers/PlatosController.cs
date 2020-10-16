using System;
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
    [Authorize, RoutePrefix("api/platos")]
    public class PlatosController : ApiController
    {
        private readonly PlatoBl _platoBl;

        public PlatosController()
        {
            _platoBl = new PlatoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<Plato>))]
        public async Task<IHttpActionResult> Get()
        {
            var platos = await _platoBl.ObtenerTodosAsync();

            if (platos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(platos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(Plato))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var plato = await _platoBl.ObtenerPorIdAsync(id);

            if (plato == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(plato);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Plato plato)
        {
            var idPlato = await _platoBl.GuardarAsync(plato);

            if (idPlato == 0) throw new Exception("No se pudo crear el plato");
            return Ok(idPlato);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] Plato plato, int id)
        {
            if (id == 0) throw new Exception("El id del plato debe ser mayor a cero");
            plato.Id = id;
            var esActualizado = await _platoBl.ModificarAsync(plato);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el plato");
            return Ok(esActualizado);
        }
    }
}
