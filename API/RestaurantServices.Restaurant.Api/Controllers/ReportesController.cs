using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Dto;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/reportes")]
    public class ReportesController : ApiController
    {
        private readonly ReportesBl _reportesBl;

        public ReportesController()
        {
            _reportesBl = new ReportesBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> Get([FromUri] ReporteDto reporte)
        {
            var reporteBase64 = await _reportesBl.ObtenerReporteAsync(reporte);
            if (reporteBase64 == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(reporteBase64);
        }
    }
}
