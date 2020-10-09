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
    [Authorize, RoutePrefix("api/tipoDocumentosPago")]
    public class TipoDocumentoPagosController : ApiController
    {
        private readonly TipoDocumentoPagoBl _tipoDocumentoPagoBl;

        public TipoDocumentoPagosController()
        {
            _tipoDocumentoPagoBl = new TipoDocumentoPagoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<TipoDocumentoPago>))]
        public async Task<IHttpActionResult> Get()
        {
            var tipoDocumentoPagos = await _tipoDocumentoPagoBl.ObtenerTodosAsync();

            if (tipoDocumentoPagos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(tipoDocumentoPagos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(TipoDocumentoPago))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var tipoDocumentoPago = await _tipoDocumentoPagoBl.ObtenerPorIdAsync(id);

            if (tipoDocumentoPago == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(tipoDocumentoPago);
        }
    }
}
