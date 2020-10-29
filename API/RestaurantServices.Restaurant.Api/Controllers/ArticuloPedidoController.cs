using System;
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
    [Authorize, RoutePrefix("api/articuloPedidos")]
    public class ArticuloPedidoController : ApiController
    {
        private readonly ArticuloPedidoBl _articuloPedidoBl;

        public ArticuloPedidoController()
        {
            _articuloPedidoBl = new ArticuloPedidoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<ArticuloPedido>))]
        public async Task<IHttpActionResult> Get()
        {
            var articuloPedidos = await _articuloPedidoBl.ObtenerTodosAsync();
            if (articuloPedidos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(articuloPedidos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(ArticuloPedido))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var articuloPedido = await _articuloPedidoBl.ObtenerPorIdAsync(id);
            if (articuloPedido == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(articuloPedido);
        }

        [HttpPost, Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post([FromBody] ArticuloPedido articulo)
        {
            var idArticuloPedido = await _articuloPedidoBl.GuardarAsync(articulo);
            if (idArticuloPedido == 0) throw new Exception("No se pudo crear el articulo pedido");
            return Ok(idArticuloPedido);
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Put([FromBody] ArticuloPedido articulo, int id)
        {
            if (id == 0) throw new Exception("El id del articulo pedido debe ser mayor a cero");
            articulo.Id = id;
            var esActualizado = await _articuloPedidoBl.ModificarAsync(articulo);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el articulo pedido");
            return Ok(true);
        }

        [HttpPost, Route("NuevoEstado")]
        public async Task<IHttpActionResult> PostNuevoEstado([FromBody] ArticuloPedidoEstado estado)
        {
            await _articuloPedidoBl.AgregarEstadoAsync(estado);
            return Ok(true);
        }
    }
}
