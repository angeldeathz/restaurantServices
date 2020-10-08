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
    [Authorize, RoutePrefix("api/tipoUsuarios")]
    public class TipoUsuariosController : ApiController
    {
        private readonly TipoUsuarioBl _tipoUsuarioBl;

        public TipoUsuariosController()
        {
            _tipoUsuarioBl = new TipoUsuarioBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<TipoUsuario>))]
        public async Task<IHttpActionResult> Get()
        {
            var tipoUsuarios = await _tipoUsuarioBl.ObtenerTodosAsync();

            if (tipoUsuarios.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(tipoUsuarios);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(TipoUsuario))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var tipoUsuario = await _tipoUsuarioBl.ObtenerPorIdAsync(id);

            if (tipoUsuario == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(tipoUsuario);
        }
    }
}
