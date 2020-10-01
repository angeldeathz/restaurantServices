using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/UnidadesDeMedida")]
    public class UnidadesDeMedidaController : ApiController
    {
        private readonly UnidadMedidaBl _unidadMedidaBl;

        public UnidadesDeMedidaController()
        {
            _unidadMedidaBl = new UnidadMedidaBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var unidadMedidas = await _unidadMedidaBl.ObtenerTodosAsync();

            if (unidadMedidas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(unidadMedidas);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var unidadMedida = await _unidadMedidaBl.ObtenerPorIdAsync(id);

            if (unidadMedida == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(unidadMedida);
        }
    }
}
