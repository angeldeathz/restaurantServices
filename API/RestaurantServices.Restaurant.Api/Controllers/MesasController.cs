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
    [Authorize, RoutePrefix("api/mesas")]
    public class MesasController : ApiController
    {
        private readonly MesaBl _mesaBl;

        public MesasController()
        {
            _mesaBl = new MesaBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<Mesa>))]
        public async Task<IHttpActionResult> Get()
        {
            var mesas = await _mesaBl.ObtenerTodosAsync();

            if (mesas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(mesas);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(Mesa))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var mesa = await _mesaBl.ObtenerPorIdAsync(id);

            if (mesa == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(mesa);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Mesa mesa)
        {
            var idMesa = await _mesaBl.GuardarAsync(mesa);

            if (idMesa == 0) throw new Exception("No se pudo crear la mesa");
            return Ok(idMesa);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] Mesa mesa, int id)
        {
            if (id == 0) throw new Exception("El id de la mesa debe ser mayor a cero");
            mesa.Id = id;
            var esActualizado = await _mesaBl.ModificarAsync(mesa);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar la mesa");
            return Ok(esActualizado);
        }
    }
}
