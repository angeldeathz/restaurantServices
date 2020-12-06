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

        /// <summary>
        /// Obtiene un reporte en formato base64
        /// </summary>
        /// <remarks>
        /// Opciones Id Reporte:
        ///
        ///     1 = Reporte diario
        ///     2 = Reporte filtrado por fecha
        ///     3 = Reporte clientes
        ///     4 = Reporte platos
        ///     5 = Reporte tiempos
        /// </remarks>
        /// <param name="reporte"></param>
        /// <returns></returns>
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
