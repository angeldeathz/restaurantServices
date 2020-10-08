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
    [Authorize, RoutePrefix("api/DetalleOrdenProveedor")]
    public class DetalleOrdenesProveedorController : ApiController
    {
        private readonly DetalleOrdenProveedorBl _detalleOrdenProveedorBl;

        public DetalleOrdenesProveedorController()
        {
            _detalleOrdenProveedorBl = new DetalleOrdenProveedorBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<DetalleOrdenProveedor>))]
        public async Task<IHttpActionResult> Get()
        {
            var detalleOrdenProveedors = await _detalleOrdenProveedorBl.ObtenerTodosAsync();

            if (detalleOrdenProveedors.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(detalleOrdenProveedors);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(DetalleOrdenProveedor))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var detalleOrdenProveedor = await _detalleOrdenProveedorBl.ObtenerPorIdAsync(id);

            if (detalleOrdenProveedor == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(detalleOrdenProveedor);
        }

        [HttpPost, Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post([FromBody] DetalleOrdenProveedor detalleOrdenProveedor)
        {
            var idOrdenProveedor = await _detalleOrdenProveedorBl.GuardarAsync(detalleOrdenProveedor);
            if (idOrdenProveedor == 0) throw new Exception("No se pudo crear el detalle orden proveedor");
            return Ok(idOrdenProveedor);
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Put([FromBody] DetalleOrdenProveedor detalleOrdenProveedor, int id)
        {
            if (id == 0) throw new Exception("El id de detalle orden proveedor debe ser mayor a cero");
            detalleOrdenProveedor.Id = id;
            var esActualizado = await _detalleOrdenProveedorBl.ModificarAsync(detalleOrdenProveedor);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el detalle orden proveedor");
            return Ok(true);
        }
    }
}
