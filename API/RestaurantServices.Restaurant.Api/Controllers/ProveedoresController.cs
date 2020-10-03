using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/Proveedores")]
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

        [Authorize, HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Proveedor proveedor)
        {
            var idProveedor = await _proveedorBl.GuardarAsync(proveedor);

            if (idProveedor == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(idProveedor);
        }

        [Authorize, HttpPut, Route("")]
        public async Task<IHttpActionResult> Put([FromBody] Proveedor proveedor)
        {
            var esActualizado = await _proveedorBl.ModificarAsync(proveedor);

            if (!esActualizado) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(true);
        }
    }
}
