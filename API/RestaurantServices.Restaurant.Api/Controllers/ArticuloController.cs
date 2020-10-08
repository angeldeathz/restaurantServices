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
    [Authorize, RoutePrefix("api/articulos")]
    public class ArticuloController : ApiController
    {
        private readonly ArticuloBl _articuloBl;

        public ArticuloController()
        {
            _articuloBl = new ArticuloBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<Articulo>))]
        public async Task<IHttpActionResult> Get()
        {
            var articulos = await _articuloBl.ObtenerTodosAsync();
            if (articulos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(articulos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(Articulo))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var articulo = await _articuloBl.ObtenerPorIdAsync(id);
            if (articulo == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(articulo);
        }

        [HttpPost, Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post([FromBody] Articulo articulo)
        {
            var idArticulo = await _articuloBl.GuardarAsync(articulo);
            if (idArticulo == 0) throw new Exception("No se pudo crear el articulo");
            return Ok(idArticulo);
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Put([FromBody] Articulo articulo, int id)
        {
            if (id == 0) throw new Exception("El id del articulo debe ser mayor a cero");
            articulo.Id = id;
            var esActualizado = await _articuloBl.ModificarAsync(articulo);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el articulo");
            return Ok(true);
        }
    }
}
