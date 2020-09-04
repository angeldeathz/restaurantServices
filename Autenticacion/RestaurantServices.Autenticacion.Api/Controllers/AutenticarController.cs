using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantServices.Autenticacion.Api.Controllers
{
    [Route("api/autenticar")]
    public class AutenticarController : ApiController
    {
        [HttpGet, Authorize]
        public IHttpActionResult Algo()
        {
            var a = new
            {
                algo = "Prueba",
                nombre = "david"
            };

            return Ok(a);
        }
    }
}
