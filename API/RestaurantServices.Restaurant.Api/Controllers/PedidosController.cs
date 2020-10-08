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
    [Authorize, RoutePrefix("api/pedidos")]
    public class PedidosController : ApiController
    {
        private readonly PedidoBl _pedidoBl;

        public PedidosController()
        {
            _pedidoBl = new PedidoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<Pedido>))]
        public async Task<IHttpActionResult> Get()
        {
            var pedidos = await _pedidoBl.ObtenerTodosAsync();

            if (pedidos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(pedidos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(Pedido))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var pedido = await _pedidoBl.ObtenerPorIdAsync(id);

            if (pedido == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(pedido);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Pedido pedido)
        {
            var idPedido = await _pedidoBl.GuardarAsync(pedido);

            if (idPedido == 0) throw new Exception("No se pudo crear el pedido");
            return Ok(idPedido);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] Pedido pedido, int id)
        {
            if (id == 0) throw new Exception("El id del pedido debe ser mayor a cero");
            pedido.Id = id;
            var esActualizado = await _pedidoBl.ModificarAsync(pedido);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el pedido");
            return Ok(true);
        }
    }
}
