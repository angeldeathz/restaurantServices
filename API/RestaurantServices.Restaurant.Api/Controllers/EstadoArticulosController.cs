using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [RoutePrefix("api/estadoArticulos")]
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
    }
}
