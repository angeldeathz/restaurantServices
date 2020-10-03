using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/reservas")]
    public class ReservasController : ApiController
    {
        private readonly ReservaBl _reservaBl;

        public ReservasController()
        {
            _reservaBl = new ReservaBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var reservas = await _reservaBl.ObtenerTodosAsync();

            if (reservas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(reservas);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var reserva = await _reservaBl.ObtenerPorIdAsync(id);

            if (reserva == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(reserva);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Reserva reserva)
        {
            var idReserva = await _reservaBl.GuardarAsync(reserva);

            if (idReserva == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(idReserva);
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Put([FromBody] Reserva reserva)
        {
            var esActualizado = await _reservaBl.ModificarAsync(reserva);

            if (!esActualizado) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(true);
        }
    }
}
