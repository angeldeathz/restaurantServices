﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;

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
        [ResponseType(typeof(List<Reserva>))]
        public async Task<IHttpActionResult> Get()
        {
            var reservas = await _reservaBl.ObtenerTodosAsync();

            if (reservas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(reservas);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(Reserva))]
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

            if (idReserva == 0) throw new Exception("No se pudo crear la reserva");
            return Ok(idReserva);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] Reserva reserva, int id)
        {
            if (id == 0) throw new Exception("El id de la reserva debe ser mayor a cero");
            reserva.Id = id;
            var esActualizado = await _reservaBl.ModificarAsync(reserva);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar la reserva");
            return Ok(true);
        }

        [HttpPost, Route("NuevoEstado")]
        public async Task<IHttpActionResult> PostNuevoEstado([FromBody] ReservaEstado estado)
        {
            await _reservaBl.AgregarEstadoAsync(estado);
            return Ok(true);
        }
    }
}
