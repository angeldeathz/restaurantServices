using System;
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

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Proveedor proveedor)
        {
            var idProveedor = await _proveedorBl.GuardarAsync(proveedor);
            if (idProveedor == 0) throw new Exception("No se pudo crear el proveedor");
            return Ok(idProveedor);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] Proveedor proveedor, int id)
        {
            if (id == 0) throw new Exception("El id del proveedor debe ser mayor a cero");
            proveedor.Id = id;
            var esActualizado = await _proveedorBl.ModificarAsync(proveedor);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el proveedor");
            return Ok(true);
        }
    }
}
