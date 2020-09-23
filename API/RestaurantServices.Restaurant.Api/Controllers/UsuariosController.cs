using System.Web.Http;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Route("usuarios")]
    public class UsuariosController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ValidarSesion([FromUri] string rut, [FromUri] string contrasena)
        {
            if (rut == "a" && contrasena == "b")
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
