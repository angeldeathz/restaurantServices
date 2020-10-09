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
    [Authorize, RoutePrefix("api/medioPagos")]
    public class MedioPagoController : ApiController
    {
        private readonly MedioPagoBl _medioPagoBl;

        public MedioPagoController()
        {
            _medioPagoBl = new MedioPagoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<MedioPago>))]
        public async Task<IHttpActionResult> Get()
        {
            var estadoArticulos = await _medioPagoBl.ObtenerTodosAsync();

            if (estadoArticulos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoArticulos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(MedioPago))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var medioPago = await _medioPagoBl.ObtenerPorIdAsync(id);

            if (medioPago == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(medioPago);
        }
    }
}
