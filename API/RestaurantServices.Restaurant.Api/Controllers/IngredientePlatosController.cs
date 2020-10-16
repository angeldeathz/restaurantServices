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
    [Authorize, RoutePrefix("api/ingredientePlatos")]
    public class IngredientePlatosController : ApiController
    {
        private readonly IngredientePlatoBl _ingredientePlatoBl;

        public IngredientePlatosController()
        {
            _ingredientePlatoBl = new IngredientePlatoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<IngredientePlato>))]
        public async Task<IHttpActionResult> Get()
        {
            var ingredientePlatos = await _ingredientePlatoBl.ObtenerTodosAsync();

            if (ingredientePlatos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(ingredientePlatos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(IngredientePlato))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var ingredientePlato = await _ingredientePlatoBl.ObtenerPorIdAsync(id);

            if (ingredientePlato == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(ingredientePlato);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] IngredientePlato ingredientePlato)
        {
            var idIngredientePlato = await _ingredientePlatoBl.GuardarAsync(ingredientePlato);

            if (idIngredientePlato == 0) throw new Exception("No se pudo crear el ingredientePlato");
            return Ok(idIngredientePlato);
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> Put([FromBody] IngredientePlato ingredientePlato, int id)
        {
            if (id == 0) throw new Exception("El id del ingredientePlato debe ser mayor a cero");
            ingredientePlato.Id = id;
            var esActualizado = await _ingredientePlatoBl.ModificarAsync(ingredientePlato);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el ingredientePlato");
            return Ok(esActualizado);
        }
    }
}
