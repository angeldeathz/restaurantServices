using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/articulos")]
    public class ArticuloController : ApiController
    {
        private readonly ArticuloBl _articuloBl;

        public ArticuloController()
        {
            _articuloBl = new ArticuloBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var articulos = await _articuloBl.ObtenerTodosAsync();

            if (articulos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(articulos);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var articulo = await _articuloBl.ObtenerPorIdAsync(id);

            if (articulo == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(articulo);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Articulo articulo)
        {
            var idArticulo = await _articuloBl.GuardarAsync(articulo);

            if (idArticulo == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(idArticulo);
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Put([FromBody] Articulo articulo)
        {
            var esActualizado = await _articuloBl.ModificarAsync(articulo);

            if (!esActualizado) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(true);
        }
    }
}
