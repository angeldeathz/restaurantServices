using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Shared.Itextsharp;
using RestaurantServices.Restaurant.Shared.Mail;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class MedioPagoDocumentoBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MedioPagoBl _medioPagoBl;
        private readonly DocumentoPagoBl _documentoPagoBl;
        private readonly EmailClient _emailClient;
        private readonly ItextSharpClient _itextSharpClient;

        public MedioPagoDocumentoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _medioPagoBl = new MedioPagoBl();
            _documentoPagoBl = new DocumentoPagoBl();
            _emailClient = new EmailClient();
            _itextSharpClient = new ItextSharpClient();
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

            var url = _itextSharpClient.CreatePdf(GetHtmlBoleta(), "Boleta_consumo.pdf");

            _emailClient.Send(new Email
            {
                UrlAdjunto = new List<string>
                {
                    url
                },
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
                           Tipo Documento.....: {documentoPago.TipoDocumentoPago.Nombre}        <br/>
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

        private string GetHtmlBoleta()
        {
            return @"<div id=""container"" style=""width: 1000px;height: 1200px;margin-top: 100px;margin-left: 40px;"">
	<div id=""emisor"" style=""font-weight: 500;position: absolute;line-height: 10%;margin-top: 10px;font-size: 18px;"">
		<p class=""azul b2"" style=""color: #2d89ad;font-weight: bold;font-size: 21px;"">Siglo XX2 Restaurante SPA</p>
		<p>Restaurantes, cafes y otros establecimientos que expenden comidas y bebidas</p>
		<p>Casa Matriz: Vicuña Mackenna 5602, La Florida, Santiago.</p>
		<p>Fonos: 2 2269901, 9 5508912</p>
	</div>
	<div id=""datosFactura"" style=""position: absolute;margin-left: 700px;"">
		<div id=""factura"" class=""rojo"" style=""color: #d64431;border-width: 3px;border-style: solid;border-color: #d64431;padding: 0px 8px 0px 8px;text-align: center;font-weight: bold;line-height: 90%;width: 235px;"">
			<p>R.U.T. : 76161082-1</p>
			<p>BOLETA ELECTRÓNICA</p>
			<p>N° ""+Id+""</p>
		</div>
		<div id=""sii"" class=""rojo b"" style=""color: #d64431;font-weight: bold;text-align: center;"">S.I.I. -  La Florida</div>
	</div>
	<div id=""receptor"" style=""position: absolute;width: 950px;height: 70px;margin-top: 230px;padding-top: 15px;padding-left: 5px;padding-bottom: 15px;border: 1px solid #929292;border-radius: 6px;"">
		<div class=""tabla1"" style=""position: absolute;width: 350px;margin-left: 20px;"">
			<table id=""tablaReceptor1"" style=""border-collapse: separate;border-spacing: 0px 3px 5px 0;"">
				<tr>
				<td style=""padding-bottom: 3px;padding-right: 4px;font-weight: bold;"">Fecha Emisión</td>
				<td style=""padding-bottom: 3px;padding-right: 4px;"">: ""+FechHora+""</td>
			</tr>
			<tr>
				<td style=""padding-bottom: 3px;padding-right: 4px;font-weight: bold;"">Medio de Pago</td>
				<td style=""padding-bottom: 3px;padding-right: 4px;"">: ""+TipoDocumentoPago.Nombre+""</td>
			</tr>
			</table>
		</div>
		<div class=""tabla2"" style=""position: absolute;margin-left: 500px;width: 400px;"">
		</div>
	</div>
	<div id=""detalle"" style=""position: absolute;margin-top: 375px;height: 300px;"">
		<table id=""tablaDetalle"" style=""border-collapse: collapse;width: 950px;text-align: center;"">
			<tr>
				<th style=""padding-left: 10px;padding-right: 10px;background-color: #53a9be;color: white;font-weight: bold;border: 1px solid #53a9be;height: 12px;"">Descripción</th>
				<th style=""padding-left: 10px;padding-right: 10px;background-color: #53a9be;color: white;font-weight: bold;border: 1px solid #53a9be;height: 12px;"">Precio</th>
				<th style=""padding-left: 10px;padding-right: 10px;background-color: #53a9be;color: white;font-weight: bold;border: 1px solid #53a9be;height: 12px;"">Cantidad</th>				
				<th style=""padding-left: 10px;padding-right: 10px;background-color: #53a9be;color: white;font-weight: bold;border: 1px solid #53a9be;height: 12px;"">Sub Total</th>
			</tr>
			""+detalles+""
		</table>
	<table id=""valores"" style=""table-layout: fixed;width: 190px;margin-left: 760px;margin-top: 5px;font-weight: bold;border-collapse: collapse;border: 1px solid grey;"">
		<tr>
			<td style=""width: 60%;padding: 5px 10px 2px 10px;"">Total $</td><td style=""border: 1px solid #929292;background-color: #ececec;text-align: right;width: 40%;"">""+Total+""</td>
		</tr>
	</table>
	</div>
	<div id=""recibo"" style=""position: absolute;margin-top: 1150px;margin-left: 370px;width: 350px;float: right;padding: 5px;text-align: justify;"">
		<table id=""datosRecibo"">
        <tr>
            <td style=""font-weight: bold;"">Nombre</td><td> ....................................................................................</td>
        </tr>
        <tr>
            <td style=""font-weight: bold;"">R.U.T.</td><td> ....................................................................................</td>
        </tr>
        <tr>
            <td style=""font-weight: bold;"">Fecha</td><td> ....................................................................................</td>
        </tr>
    	</table>
    	<table id=""datosRecibo"">
        <tr>    
            <td>Recinto</td><td> .............................................</td>
            <td class=""b"" style=""font-weight: bold;"">Firma</td><td> .............................</td>
        </tr>
        </table>
        
        <div id=""acuse"" style=""border: 1px solid black;border-radius: 5px;padding: 9px 9px 9px 9px;width: 570px;font-size: 13px;"">
        	EL ACUSE DE RECIBO QUE SE DECLARA EN ESTE ACTO, DE ACUERDO A LO DISPUESTO EN LA LETRA B) DEL ART. 4°, Y LA LETRA C) DEL ART. 5°DE LA LEY 19.983, ACREDITA QUE LA ENTREGA DE MERCADERÍAS O SERVICIO (S) PRESTADO (S) HA (N) SIDO RECIBIDO (S).
    	</div>
    </div>

    <div id=""timbre"" style=""position: absolute;margin-left: 10px;margin-top: 1150px;text-align: center;"">                           
			<p style=""line-height: 10%;font-size: 14px;font-weight: bold;color: #d64431;"">Timbre Electr&oacute;nico SII</p>
            <p style=""line-height: 10%;font-size: 14px;font-weight: bold;color: #d64431;"">Verifique documento: www.sii.cl</p>
    </div>     
</div>";

//            return
//                @"<table cellpadding='4' cellspacing='4' border='1' width='100%' style='width:100%'>
//    <tr style='background-color:#000000'>
//        <td colspan='2' align='center' valign='middle'>
//            <font face='Calibri' size='6' color='#FFFFFF'>XXXX XXXXX XXXXX</font>
//        </td>
//    </tr>
//    <tr>
//        <td colspan='2'>&nbsp;</td>
//    </tr>
//    <tr>
//        <td width='90%' style='width:90%'>
//            <table cellpadding='0' cellspacing='0' border='1' width='100%'>
//                <tr>
//                    <td width='42%'>
//                        <font face='Calibri' size='4'>
//                            <b>Deal Number</b>
//                        </font>
//                    </td>
//                    <td width='1%'>&nbsp;</td>
//                    <td width='57%'>
//                        <font face='Calibri' size='4'>
//                            <b>XXXXXXXXXX</b>
//                        </font>
//                    </td>
//                </tr>
//                <tr>
//                    <td colspan='3' width='100%'>&nbsp;</td>
//                </tr>
//                <tr>
//                    <td width='42%'>
//                        <font face='Calibri' size='2'>
//                            <b>Trade Date</b>
//                        </font>
//                    </td>
//                    <td width='1%'>&nbsp;</td>
//                    <td width='57%'>
//                        <font face='Calibri' size='2'>February 09, 2015</font>
//                    </td>
//                </tr>
//                <tr>
//                    <td width='42%'>
//                        <font face='Calibri' size='2'>
//                            <b>Price Date</b>
//                        </font>
//                    </td>
//                    <td width='1%'>&nbsp;</td>
//                    <td width='57%'>
//                        <font face='Calibri' size='2'>February 09, 2015</font>
//                    </td>
//                </tr>
//                <tr>
//                    <td width='42%'>
//                        <font face='Calibri' size='2'>
//                            <b>Authorize Date</b>
//                        </font>
//                    </td>
//                    <td width='1%'>&nbsp;</td>
//                    <td width='57%'>
//                        <font face='Calibri' size='2'>February 09, 2015</font>
//                    </td>
//                </tr>
//                <tr>
//                    <td colspan='3' width='100%'>&nbsp;</td>
//                </tr>
//            </table>
//        </td>
//        <td width='10%' style='width:10%' valign='top'>
//            <table cellpadding='0' cellspacing='0' border='1' width='100%'>
//                <tr>
//                    <td colspan='2' align='center' width='100%'>
//                        <font face='Calibri' size='2'>
//                            <b>Xxxxxxx (XXXXXXX)</b>
//                        </font>
//                    </td>
//                </tr>
//            </table>
//        </td>
//    </tr>
//</table>";
        }
    }
}
