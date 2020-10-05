using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/estadoReservas")]
    public class EstadoReservasController : ApiController
    {
        private readonly EstadoReservaBl _estadoReservaBl;

        public EstadoReservasController()
        {
            _estadoReservaBl = new EstadoReservaBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var estadoReservas = await _estadoReservaBl.ObtenerTodosAsync();

            if (estadoReservas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoReservas);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var estadoReserva = await _estadoReservaBl.ObtenerPorIdAsync(id);

            if (estadoReserva == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoReserva);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] EstadoReserva estadoReserva)
        {
            var idEstadoReserva = await _estadoReservaBl.GuardarAsync(estadoReserva);

            if (idEstadoReserva == 0) throw new Exception("No se pudo crear el estado reserva");
            return Ok(idEstadoReserva);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] EstadoReserva estadoReserva, int id)
        {
            if (id == 0) throw new Exception("El id del estado reserva debe ser mayor a cero");
            estadoReserva.Id = id;
            var esActualizado = await _estadoReservaBl.ModificarAsync(estadoReserva);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el estado reserva");
            return Ok(true);
        }
    }
}
