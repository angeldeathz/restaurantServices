using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/clientes")]
    public class ClientesController : ApiController
    {
        private readonly ClienteBl _clienteBl;

        public ClientesController()
        {
            _clienteBl = new ClienteBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var clientes = await _clienteBl.ObtenerTodosAsync();

            if (clientes.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(clientes);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var cliente = await _clienteBl.ObtenerPorIdAsync(id);

            if (cliente == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(cliente);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Cliente cliente)
        {
            var idCliente = await _clienteBl.GuardarAsync(cliente);

            if (idCliente == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(idCliente);
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Put([FromBody] Cliente cliente)
        {
            var esActualizado = await _clienteBl.ModificarAsync(cliente);

            if (!esActualizado) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(true);
        }
    }
}
