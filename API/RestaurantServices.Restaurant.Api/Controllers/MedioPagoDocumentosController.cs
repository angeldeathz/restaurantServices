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
    [Authorize, RoutePrefix("api/medioPagoDocumentos")]
    public class MedioPagoDocumentosController : ApiController
    {
        private readonly MedioPagoDocumentoBl _medioPagoDocumentoBl;

        public MedioPagoDocumentosController()
        {
            _medioPagoDocumentoBl = new MedioPagoDocumentoBl();
        }

        [HttpGet, Route("")]
        [ResponseType(typeof(List<MedioPagoDocumento>))]
        public async Task<IHttpActionResult> Get()
        {
            var medioPagoDocumentos = await _medioPagoDocumentoBl.ObtenerTodosAsync();

            if (medioPagoDocumentos.Count == 0) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(medioPagoDocumentos);
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(MedioPagoDocumento))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var medioPagoDocumento = await _medioPagoDocumentoBl.ObtenerPorIdAsync(id);

            if (medioPagoDocumento == null) return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            return Ok(medioPagoDocumento);
        }

        [HttpPost, Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post([FromBody] MedioPagoDocumento medioPagoDocumento)
        {
            var id = await _medioPagoDocumentoBl.GuardarAsync(medioPagoDocumento);

            if (id == 0) throw new Exception("No se pudo crear el medioPagoDocumento");
            return Ok(id);
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Put([FromBody] MedioPagoDocumento medioPagoDocumento, int id)
        {
            if (id == 0) throw new Exception("El id del medioPagoDocumento debe ser mayor a cero");
            medioPagoDocumento.Id = id;
            var esActualizado = await _medioPagoDocumentoBl.ModificarAsync(medioPagoDocumento);

            if (esActualizado == 0) throw new Exception("No se pudo actualizar el medioPagoDocumento");
            return Ok(true);
        }
    }
}
