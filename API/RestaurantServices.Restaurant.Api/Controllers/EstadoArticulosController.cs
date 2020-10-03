﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/estadoArticulos")]
    public class EstadoArticulosController : ApiController
    {
        private readonly EstadoArticuloBl _estadoArticuloBl;

        public EstadoArticulosController()
        {
            _estadoArticuloBl = new EstadoArticuloBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var estadoArticulos = await _estadoArticuloBl.ObtenerTodosAsync();

            if (estadoArticulos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoArticulos);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var estadoArticulo = await _estadoArticuloBl.ObtenerPorIdAsync(id);

            if (estadoArticulo == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoArticulo);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] EstadoArticulo estadoArticulo)
        {
            var idEstadoArticulo = await _estadoArticuloBl.GuardarAsync(estadoArticulo);

            if (idEstadoArticulo == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(idEstadoArticulo);
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Put([FromBody] EstadoArticulo estadoArticulo)
        {
            var esActualizado = await _estadoArticuloBl.ModificarAsync(estadoArticulo);

            if (!esActualizado) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(true);
        }
    }
}
