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
    [Authorize, RoutePrefix("api/tipopreparaciones")]
    public class TipoPreparacionesController : ApiController
    {
        private readonly TipoPreparacionBl _tipoPreparacionBl;

        public TipoPreparacionesController()
        {
            _tipoPreparacionBl = new TipoPreparacionBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<TipoPreparacion>))]
        public async Task<IHttpActionResult> Get()
        {
            var tipoPreparaciones = await _tipoPreparacionBl.ObtenerTodosAsync();

            if (tipoPreparaciones.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(tipoPreparaciones);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(TipoPreparacion))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var tipoPreparacion = await _tipoPreparacionBl.ObtenerPorIdAsync(id);

            if (tipoPreparacion == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(tipoPreparacion);
        }
    }
}
