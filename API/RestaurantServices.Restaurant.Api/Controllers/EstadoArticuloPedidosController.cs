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
    [Authorize, RoutePrefix("api/EstadoArticuloPedidos")]
    public class EstadoArticuloPedidosController : ApiController
    {
        private readonly EstadoArticuloPedidoBl _estadoArticuloPedidoBl;

        public EstadoArticuloPedidosController()
        {
            _estadoArticuloPedidoBl = new EstadoArticuloPedidoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<EstadoArticuloPedido>))]
        public async Task<IHttpActionResult> Get()
        {
            var estados = await _estadoArticuloPedidoBl.ObtenerTodosAsync();
            if (estados.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estados);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(EstadoArticuloPedido))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var estado = await _estadoArticuloPedidoBl.ObtenerPorIdAsync(id);
            if (estado == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estado);
        }
    }
}
