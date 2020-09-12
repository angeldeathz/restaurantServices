using System.Web.Http;

namespace RestaurantServices.Usuarios.Api.Controllers
{
    [Route("")]
    public class AutenticarController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ValidarSesion([FromUri] string rut, string contrasena)
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
