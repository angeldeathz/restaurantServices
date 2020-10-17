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
    [Authorize, RoutePrefix("api/horarioReservas")]
    public class HorarioReservasController : ApiController
    {
        private readonly HorarioReservaBl _horarioReservaBl;

        public HorarioReservasController()
        {
            _horarioReservaBl = new HorarioReservaBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<HorarioReserva>))]
        public async Task<IHttpActionResult> Get()
        {
            var horarioReservas = await _horarioReservaBl.ObtenerTodosAsync();

            if (horarioReservas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(horarioReservas);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(HorarioReserva))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var horarioReserva = await _horarioReservaBl.ObtenerPorIdAsync(id);

            if (horarioReserva == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(horarioReserva);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] HorarioReserva horarioReserva)
        {
            var idMesa = await _horarioReservaBl.GuardarAsync(horarioReserva);

            if (idMesa == 0) throw new Exception("No se pudo crear el horarioReserva");
            return Ok(idMesa);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] HorarioReserva horarioReserva, int id)
        {
            if (id == 0) throw new Exception("El id del horarioReserva debe ser mayor a cero");
            horarioReserva.Id = id;
            var esActualizado = await _horarioReservaBl.ModificarAsync(horarioReserva);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el horarioReserva");
            return Ok(esActualizado);
        }
    }
}
