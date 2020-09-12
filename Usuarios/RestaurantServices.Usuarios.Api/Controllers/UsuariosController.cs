using System.Web.Http;

namespace RestaurantServices.Usuarios.Api.Controllers
{
    [Route("")]
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
