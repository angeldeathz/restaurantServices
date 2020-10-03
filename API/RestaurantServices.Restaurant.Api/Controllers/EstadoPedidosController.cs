using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
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
        public async Task<IHttpActionResult> Get()
        {
            var estadoArticulos = await _estadoPedidoBl.ObtenerTodosAsync();

            if (estadoArticulos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoArticulos);
        }

        [HttpGet, Route("{id}")]
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

            if (idEstadoPedido == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(idEstadoPedido);
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Put([FromBody] EstadoPedido estadoPedido)
        {
            var esActualizado = await _estadoPedidoBl.ModificarAsync(estadoPedido);

            if (!esActualizado) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(true);
        }
    }
}
