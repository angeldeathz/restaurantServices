using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/reportes")]
    public class ReportesController : ApiController
    {
        [HttpGet, Route("{id}")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var reporteBase64 = string.Empty;
            if (reporteBase64 == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(reporteBase64);
        }
    }
}
