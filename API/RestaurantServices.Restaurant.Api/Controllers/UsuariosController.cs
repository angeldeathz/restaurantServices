using System.Web.Http;
using RestaurantServices.Restaurant.Modelo.Dto;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        [HttpPost, Route("login")]
        public IHttpActionResult ValidarSesion([FromBody] UsuarioLogin usuarioLogin)
        {
            if (usuarioLogin.Rut == "a" && usuarioLogin.Contrasena == "b")
            {
                return Ok("Ha iniciado sesion");
            }
            else
            {
                return BadRequest("Credenciales Incorrectas");
            }
        }
    }
}
