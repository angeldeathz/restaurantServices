using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;
using Swashbuckle.Swagger.Annotations;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/estadoArticulos")]
    public class EstadoArticulosController : ApiController
    {
        private readonly EstadoArticuloBl _estadoArticuloBl;

        public EstadoArticulosController()
        {
            _estadoArticuloBl = new EstadoArticuloBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<EstadoArticulo>))]
        public async Task<IHttpActionResult> Get()
        {
            var estadoArticulos = await _estadoArticuloBl.ObtenerTodosAsync();

            if (estadoArticulos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoArticulos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(EstadoArticulo))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var estadoArticulo = await _estadoArticuloBl.ObtenerPorIdAsync(id);

            if (estadoArticulo == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoArticulo);
        }

        [HttpPost, Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post([FromBody] EstadoArticulo estadoArticulo)
        {
            var idEstadoArticulo = await _estadoArticuloBl.GuardarAsync(estadoArticulo);
            if (idEstadoArticulo == 0) throw new Exception("No se pudo crear el estado articulo");
            return Ok(idEstadoArticulo);
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(bool))]
        [SwaggerResponse(HttpStatusCode.OK, null, typeof(bool))]
        public async Task<IHttpActionResult> Put([FromBody] EstadoArticulo estadoArticulo, int id)
        {
            if (id == 0) throw new Exception("El id del estado articulo debe ser mayor a cero");
            estadoArticulo.Id = id;
            var esActualizado = await _estadoArticuloBl.ModificarAsync(estadoArticulo);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el estado articulo");
            return Ok(true);
        }
    }
}
