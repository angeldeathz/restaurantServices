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
    [Authorize, RoutePrefix("api/documentoPagos")]
    public class DocumentoPagosController : ApiController
    {
        private readonly DocumentoPagoBl _documentoPagoBl;

        public DocumentoPagosController()
        {
            _documentoPagoBl = new DocumentoPagoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<DocumentoPago>))]
        public async Task<IHttpActionResult> Get()
        {
            var documentoPagos = await _documentoPagoBl.ObtenerTodosAsync();

            if (documentoPagos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(documentoPagos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(DocumentoPago))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var documentoPago = await _documentoPagoBl.ObtenerPorIdAsync(id);

            if (documentoPago == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(documentoPago);
        }

        [HttpPost, Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post([FromBody] DocumentoPago documentoPago)
        {
            var id = await _documentoPagoBl.GuardarAsync(documentoPago);

            if (id == 0) throw new Exception("No se pudo crear el documentoPago");
            return Ok(id);
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Put([FromBody] DocumentoPago documentoPago, int id)
        {
            if (id == 0) throw new Exception("El id del documentoPago debe ser mayor a cero");
            documentoPago.Id = id;
            var esActualizado = await _documentoPagoBl.ModificarAsync(documentoPago);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el documentoPago");
            return Ok(true);
        }
    }
}
