using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [RoutePrefix("api/Proveedores")]
    public class ProveedoresController : ApiController
    {
        private readonly ProveedorBl _proveedorBl;

        public ProveedoresController()
        {
            _proveedorBl = new ProveedorBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var proveedores = await _proveedorBl.ObtenerTodosAsync();

            if (proveedores.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(proveedores);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var proveedor = await _proveedorBl.ObtenerPorIdAsync(id);

            if (proveedor == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(proveedor);
        }
    }
}
