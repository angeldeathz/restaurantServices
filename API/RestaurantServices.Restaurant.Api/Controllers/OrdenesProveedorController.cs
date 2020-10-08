using System;
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
    [Authorize, RoutePrefix("api/ordenesProveedor")]
    public class OrdenesProveedorController : ApiController
    {
        private readonly OrdenProveedorBl _ordenProveedorBl;

        public OrdenesProveedorController()
        {
            _ordenProveedorBl = new OrdenProveedorBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<OrdenProveedor>))]
        public async Task<IHttpActionResult> Get()
        {
            var ordenesProveedores = await _ordenProveedorBl.ObtenerTodosAsync();

            if (ordenesProveedores.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(ordenesProveedores);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(OrdenProveedor))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var ordenProveedor = await _ordenProveedorBl.ObtenerPorIdAsync(id);

            if (ordenProveedor == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(ordenProveedor);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] OrdenProveedor ordenProveedor)
        {
            var idOrdenProveedor = await _ordenProveedorBl.GuardarAsync(ordenProveedor);
            if (idOrdenProveedor == 0) throw new Exception("No se pudo crear la orden proveedor");
            return Ok(idOrdenProveedor);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] OrdenProveedor ordenProveedor, int id)
        {
            if (id == 0) throw new Exception("El id de la orden proveedor debe ser mayor a cero");
            ordenProveedor.Id = id;
            var esActualizado = await _ordenProveedorBl.ModificarAsync(ordenProveedor);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar la orden proveedor");
            return Ok(true);
        }
    }
}
