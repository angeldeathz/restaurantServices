using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Shared.Mail;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class MedioPagoDocumentoBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MedioPagoBl _medioPagoBl;
        private readonly DocumentoPagoBl _documentoPagoBl;
        private readonly EmailClient _emailClient;

        public MedioPagoDocumentoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _medioPagoBl = new MedioPagoBl();
            _documentoPagoBl = new DocumentoPagoBl();
            _emailClient = new EmailClient();
        }

        public async Task<List<MedioPagoDocumento>> ObtenerTodosAsync()
        {
            var medioPagoDocumentos = await _unitOfWork.MedioPagoDocumentoDal.GetAsync();

            foreach (var x in medioPagoDocumentos)
            {
                x.MedioPago = await _medioPagoBl.ObtenerPorIdAsync(x.IdMedioPago);
                x.DocumentoPago = await _documentoPagoBl.ObtenerPorIdAsync(x.IdDocumentoPago);
            }

            return (List<MedioPagoDocumento>)medioPagoDocumentos;
        }

        public async Task<MedioPagoDocumento> ObtenerPorIdAsync(int id)
        {
            var medioPagoDocumento = await _unitOfWork.MedioPagoDocumentoDal.GetAsync(id);
            if (medioPagoDocumento == null) return null;

            medioPagoDocumento.MedioPago = await _medioPagoBl.ObtenerPorIdAsync(medioPagoDocumento.IdMedioPago);
            medioPagoDocumento.DocumentoPago = await _documentoPagoBl.ObtenerPorIdAsync(medioPagoDocumento.IdDocumentoPago);

            return medioPagoDocumento;
        }

        public async Task<int> GuardarAsync(MedioPagoDocumento medioPagoDocumento)
        {
            var documentoPago = await _documentoPagoBl.ObtenerPorIdAsync(medioPagoDocumento.IdDocumentoPago);
            if (documentoPago == null) throw new Exception($"No se ha encontrado el documento de pago {medioPagoDocumento.IdDocumentoPago}");

            var id = await _unitOfWork.MedioPagoDocumentoDal.InsertAsync(medioPagoDocumento);
            var medioPago = await ObtenerPorIdAsync(id);

            _emailClient.Send(new Email
            {
                ReceptorEmail = documentoPago.Pedido.Reserva.Cliente.Persona.Email,
                ReceptorNombre = documentoPago.Pedido.Reserva.Cliente.Persona.Nombre,
                Asunto = "Comprobante de su compra en Restaurante Siglo XXI",
                Contenido =
                    $@"Estimad@ {documentoPago.Pedido.Reserva.Cliente.Persona.Nombre} {documentoPago.Pedido.Reserva.Cliente.Persona.Apellido} :<br/><br/>
                           Se ha emitido el Pago #{medioPago.Id} en Restaurante Siglo XXI de forma correcta. <br/>
                           Se adjuntan los datos de la transacción: <br/><br/>
                           -------------------------------------------------------------------- <br/>
                           N° Documento.......: {documentoPago.Id}                              <br/>
                           -------------------------------------------------------------------- <br/>
                           Tip Documento......: {documentoPago.TipoDocumentoPago.Nombre}        <br/>
                           -------------------------------------------------------------------- <br/>
                           Método de Pago.....: {medioPago.MedioPago.Nombre}                    <br/>
                           -------------------------------------------------------------------- <br/>
                           Fecha de emisión...: {documentoPago.FechaHora:dd-MM-yyyy hh:mm}      <br/>
                           -------------------------------------------------------------------- <br/>
                           Monto Total........: $ {medioPago.Monto:N}                           <br/>
                           -------------------------------------------------------------------- <br/><br/>

                           Atte, <br/>
                           Restaurante Siglo XXI. <br/>"
            });

            return id;
        }

        public Task<int> ModificarAsync(MedioPagoDocumento medioPagoDocumento)
        {
            return _unitOfWork.MedioPagoDocumentoDal.UpdateAsync(medioPagoDocumento);
        }
    }
}
