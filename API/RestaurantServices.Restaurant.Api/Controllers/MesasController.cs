using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/mesas")]
    public class MesasController : ApiController
    {
        private readonly MesaBl _mesaBl;

        public MesasController()
        {
            _mesaBl = new MesaBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var mesas = await _mesaBl.ObtenerTodosAsync();

            if (mesas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(mesas);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var mesa = await _mesaBl.ObtenerPorIdAsync(id);

            if (mesa == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(mesa);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Mesa mesa)
        {
            var idMesa = await _mesaBl.GuardarAsync(mesa);

            if (idMesa == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(idMesa);
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Put([FromBody] Mesa mesa)
        {
            var esActualizado = await _mesaBl.ModificarAsync(mesa);

            if (!esActualizado) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(esActualizado);
        }
    }
}
