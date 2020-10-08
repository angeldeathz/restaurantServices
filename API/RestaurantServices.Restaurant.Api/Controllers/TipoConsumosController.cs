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
    [Authorize, RoutePrefix("api/tipoConsumos")]
    public class TipoConsumosController : ApiController
    {
        private readonly TipoConsumoBl _tipoConsumoBl;

        public TipoConsumosController()
        {
            _tipoConsumoBl = new TipoConsumoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<TipoConsumo>))]
        public async Task<IHttpActionResult> Get()
        {
            var tipoConsumos = await _tipoConsumoBl.ObtenerTodosAsync();

            if (tipoConsumos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(tipoConsumos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(TipoConsumo))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var tipoConsumo = await _tipoConsumoBl.ObtenerPorIdAsync(id);

            if (tipoConsumo == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(tipoConsumo);
        }
    }
}
