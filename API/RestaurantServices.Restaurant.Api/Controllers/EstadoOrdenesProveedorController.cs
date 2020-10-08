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
    [Authorize, RoutePrefix("api/estadoOrdenesProveedor")]
    public class EstadoOrdenesProveedorController : ApiController
    {
        private readonly EstadoOrdenProveedorBl _estadoOrdenProveedorBl;

        public EstadoOrdenesProveedorController()
        {
            _estadoOrdenProveedorBl = new EstadoOrdenProveedorBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<EstadoOrdenProveedor>))]
        public async Task<IHttpActionResult> Get()
        {
            var estadoOrdenProveedores = await _estadoOrdenProveedorBl.ObtenerTodosAsync();

            if (estadoOrdenProveedores.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoOrdenProveedores);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(EstadoOrdenProveedor))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var estadoOrdenProveedor = await _estadoOrdenProveedorBl.ObtenerPorIdAsync(id);

            if (estadoOrdenProveedor == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoOrdenProveedor);
        }

        [HttpPost, Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post([FromBody] EstadoOrdenProveedor estadoOrdenProveedor)
        {
            var idEstadoPedido = await _estadoOrdenProveedorBl.GuardarAsync(estadoOrdenProveedor);

            if (idEstadoPedido == 0) throw new Exception("No se pudo crear el estado orden proveedor");
            return Ok(idEstadoPedido);
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Put([FromBody] EstadoOrdenProveedor estadoOrdenProveedor, int id)
        {
            if (id == 0) throw new Exception("El id del estado orden proveedor debe ser mayor a cero");
            estadoOrdenProveedor.Id = id;
            var esActualizado = await _estadoOrdenProveedorBl.ModificarAsync(estadoOrdenProveedor);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el estado orden proveedor");
            return Ok(true);
        }
    }
}
