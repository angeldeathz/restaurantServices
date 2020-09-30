using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [RoutePrefix("api/Insumos")]
    public class InsumosController : ApiController
    {
        private readonly InsumoBl _insumoBl;

        public InsumosController()
        {
            _insumoBl = new InsumoBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var insumos = await _insumoBl.ObtenerTodosAsync();

            if (insumos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(insumos);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var insumo = await _insumoBl.ObtenerPorIdAsync(id);

            if (insumo == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(insumo);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Insumo insumo)
        {
            var idArticulo = await _insumoBl.GuardarAsync(insumo);

            if (idArticulo == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(idArticulo);
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Put([FromBody] Insumo insumo)
        {
            var esActualizado = await _insumoBl.ModificarAsync(insumo);

            if (!esActualizado) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(esActualizado);
        }
    }
}
