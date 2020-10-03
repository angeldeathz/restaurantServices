using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.API.Controllers
{
    [Authorize, RoutePrefix("api/estadoReservas")]
    public class EstadoReservasController : ApiController
    {
        private readonly EstadoReservaBl _estaoReservaBl;

        public EstadoReservasController()
        {
            _estaoReservaBl = new EstadoReservaBl();
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var estadoReservas = await _estaoReservaBl.ObtenerTodosAsync();

            if (estadoReservas.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoReservas);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var estadoReserva = await _estaoReservaBl.ObtenerPorIdAsync(id);

            if (estadoReserva == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(estadoReserva);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] EstadoReserva estadoReserva)
        {
            var idEstadoReserva = await _estaoReservaBl.GuardarAsync(estadoReserva);

            if (idEstadoReserva == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(idEstadoReserva);
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Put([FromBody] EstadoReserva estadoReserva)
        {
            var esActualizado = await _estaoReservaBl.ModificarAsync(estadoReserva);

            if (!esActualizado) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(true);
        }
    }
}
