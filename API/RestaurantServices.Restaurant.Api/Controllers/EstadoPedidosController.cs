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
    [Authorize, RoutePrefix("api/estadoPedidos")]
    public class EstadoPedidosController : ApiController
    {
        private readonly EstadoPedidoBl _estadoPedidoBl;

        public EstadoPedidosController()
        {
            _estadoPedidoBl = new EstadoPedidoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<EstadoPedido>))]
        public async Task<IHttpActionResult> Get()
        {
            var estadoArticulos = await _estadoPedidoBl.ObtenerTodosAsync();

            if (estadoArticulos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoArticulos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(EstadoPedido))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var estadoArticulo = await _estadoPedidoBl.ObtenerPorIdAsync(id);

            if (estadoArticulo == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoArticulo);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] EstadoPedido estadoPedido)
        {
            var idEstadoPedido = await _estadoPedidoBl.GuardarAsync(estadoPedido);

            if (idEstadoPedido == 0) throw new Exception("No se pudo crear el estado pedido");
            return Ok(idEstadoPedido);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] EstadoPedido estadoPedido, int id)
        {
            if (id == 0) throw new Exception("El id del estado pedido debe ser mayor a cero");
            estadoPedido.Id = id;
            var esActualizado = await _estadoPedidoBl.ModificarAsync(estadoPedido);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el estado pedido");
            return Ok(true);
        }
    }
}
