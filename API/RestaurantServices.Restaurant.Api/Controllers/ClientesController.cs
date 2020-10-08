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
    [Authorize, RoutePrefix("api/clientes")]
    public class ClientesController : ApiController
    {
        private readonly ClienteBl _clienteBl;

        public ClientesController()
        {
            _clienteBl = new ClienteBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<Cliente>))]
        public async Task<IHttpActionResult> Get()
        {
            var clientes = await _clienteBl.ObtenerTodosAsync();

            if (clientes.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(clientes);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var cliente = await _clienteBl.ObtenerPorIdAsync(id);

            if (cliente == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(cliente);
        }

        [HttpPost, Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post([FromBody] Cliente cliente)
        {
            var idCliente = await _clienteBl.GuardarAsync(cliente);

            if (idCliente == 0) throw new Exception("No se pudo crear el cliente");
            return Ok(idCliente);
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Put([FromBody] Cliente cliente, int id)
        {
            if (id == 0) throw new Exception("El id del cliente debe ser mayor a cero");
            cliente.Id = id;
            var esActualizado = await _clienteBl.ModificarAsync(cliente);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el cliente");
            return Ok(true);
        }
    }
}
