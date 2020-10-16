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
    [Authorize, RoutePrefix("api/ArticulosConsumoDirecto")]
    public class ArticulosConsumoDirectoController : ApiController
    {
        private readonly ArticuloConsumoDirectoBl _articuloConsumoDirectoBl;

        public ArticulosConsumoDirectoController()
        {
            _articuloConsumoDirectoBl = new ArticuloConsumoDirectoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<ArticuloConsumoDirecto>))]
        public async Task<IHttpActionResult> Get()
        {
            var articuloConsumoDirectos = await _articuloConsumoDirectoBl.ObtenerTodosAsync();

            if (articuloConsumoDirectos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(articuloConsumoDirectos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(ArticuloConsumoDirecto))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var articuloConsumoDirecto = await _articuloConsumoDirectoBl.ObtenerPorIdAsync(id);

            if (articuloConsumoDirecto == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(articuloConsumoDirecto);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] ArticuloConsumoDirecto articuloConsumoDirecto)
        {
            var idIngredientePlato = await _articuloConsumoDirectoBl.GuardarAsync(articuloConsumoDirecto);

            if (idIngredientePlato == 0) throw new Exception("No se pudo crear el articuloConsumoDirecto");
            return Ok(idIngredientePlato);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] ArticuloConsumoDirecto articuloConsumoDirecto, int id)
        {
            if (id == 0) throw new Exception("El id del articuloConsumoDirecto debe ser mayor a cero");
            articuloConsumoDirecto.Id = id;
            var esActualizado = await _articuloConsumoDirectoBl.ModificarAsync(articuloConsumoDirecto);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el articuloConsumoDirecto");
            return Ok(esActualizado);
        }
    }
}
