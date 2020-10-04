using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/estadoMesas")]
    public class EstadosMesasController : ApiController
    {
        private readonly EstadoMesaBl _estadoMesaBl;

        public EstadosMesasController()
        {
            _estadoMesaBl = new EstadoMesaBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var estadoMesas = await _estadoMesaBl.ObtenerTodosAsync();

            if (estadoMesas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoMesas);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var estadoMesa = await _estadoMesaBl.ObtenerPorIdAsync(id);

            if (estadoMesa == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoMesa);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] EstadoMesa estadoMesa)
        {
            var idEstadoMesa = await _estadoMesaBl.GuardarAsync(estadoMesa);

            if (idEstadoMesa == 0) throw new Exception("No se pudo crear el estado mesa");
            return Ok(idEstadoMesa);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] EstadoMesa estadoMesa, int id)
        {
            if (id == 0) throw new Exception("El id del estado mesa debe ser mayor a cero");
            estadoMesa.Id = id;
            var esActualizado = await _estadoMesaBl.ModificarAsync(estadoMesa);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el estado mesa");
            return Ok(true);
        }
    }
}
