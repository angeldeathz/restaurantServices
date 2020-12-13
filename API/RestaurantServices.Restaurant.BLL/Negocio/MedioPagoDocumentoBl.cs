using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;
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
        private readonly ArticuloPedidoBl _articuloPedidoBl;
        private readonly MesaBl _mesaBl;

        public MedioPagoDocumentoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _medioPagoBl = new MedioPagoBl();
            _documentoPagoBl = new DocumentoPagoBl();
            _emailClient = new EmailClient();
            _itextSharpClient = new ItextSharpClient();
            _articuloPedidoBl = new ArticuloPedidoBl();
            _mesaBl = new MesaBl();
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

            var articulos = await _articuloPedidoBl.ObtenerPorIdPedidoAsync(documentoPago.IdPedido);
            if (articulos == null || articulos.Count <= 0)
                throw new Exception("No se puede generar un pago que no tenga articulos");

            var id = await _unitOfWork.MedioPagoDocumentoDal.InsertAsync(medioPagoDocumento);
            var medioPago = await ObtenerPorIdAsync(id);

            var url = _itextSharpClient.CreatePdf(GetHtmlBoleta(documentoPago, medioPago, articulos), $"Boleta_pedido_{documentoPago.IdPedido}_{DateTime.Now:dd-MM-yyyy}.pdf");

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


            var articuloPedidos = await _articuloPedidoBl.ObtenerPorIdPedidoAsync(documentoPago.IdPedido);

            foreach (var x in articuloPedidos)
            {
                await _articuloPedidoBl.AgregarEstadoAsync(new ArticuloPedidoEstado
                {
                    IdArticuloPedido = x.Id,
                    IdEstadoArticuloPedido = 4
                });
            }

            documentoPago.Pedido.Reserva.Mesa.IdEstadoMesa = 1;
            await _mesaBl.ModificarAsync(documentoPago.Pedido.Reserva.Mesa);

            return id;
        }

        public Task<int> ModificarAsync(MedioPagoDocumento medioPagoDocumento)
        {
            return _unitOfWork.MedioPagoDocumentoDal.UpdateAsync(medioPagoDocumento);
        }

        private string GetHtmlBoleta(DocumentoPago documentoPago, MedioPagoDocumento medioPago, List<ArticuloPedido> articulos)
        {
            var id = documentoPago.Id;
            var fechaHora = $"{documentoPago.FechaHora:dd-MM-yyyy hh:mm}";
            var total = $"$ {medioPago.Monto:N}";
            var tipoDocumentoPago = medioPago.MedioPago.Nombre;

            var detalles = string.Empty;

            foreach (var x in articulos)
            {
                detalles += "<tr>";
                detalles += $"<td>{x.Articulo.Nombre}</td>";
                detalles += $"<td>$ {x.Articulo.Precio}</td>";
                detalles += $"<td>{x.Cantidad}</td>";
                detalles += $"<td>$ {x.Cantidad * x.Articulo.Precio}</td>";
                detalles += "</tr>";
            }

            return @"<!DOCTYPE html><html><head><meta charset='utf-8'><title></title><style>body{font-family: 'Arial', 'Verdana', 'Helvetica', Sans-serif;font-size: 12px;}h2{color: #22776b;font-weight: bold;}#container{width: 700px;height: 1000px;margin-left: 30px;}#emisor{font-weight: 500;position: absolute;line-height: 10%;margin-top: 10px;}#datosFactura{position: absolute;margin-left: 460px;}#factura{border-width: 3px;border-style: solid;border-color: #d64431;padding: 0px 8px 0px 8px;text-align: center;font-weight: bold;line-height: 90%;width: 160px;}#sii{text-align: center;}#receptor{position: absolute;width: 650px;height: 70px;margin-top: 160px;padding-top: 15px;padding-left: 5px;padding-bottom: 15px;border: 2px solid #22776b;border-radius: 6px;}#receptor table tr td:first-child{font-weight: bold;}#receptor table td{padding-bottom: 3px;padding-right: 4px;}.tabla1{position: absolute;width: 350px;margin-left: 20px;}#tablaReceptor1{border-collapse: separate; border-spacing: 0px 3px 5px 0;}.tabla2{position: absolute;margin-left: 500px;width: 400px;}#tablaReceptor2{border-collapse: separate; border-spacing: 0px 3px;}#fecha{position: absolute;margin-top: 125px;margin-left: 580px;}#detalle{position: absolute;margin-top: 300px;height: 300px;}#tablaDetalle{border-collapse: collapse; width: 650px; text-align: center;}#tablaDetalle td, #tablaDetalle th{padding-left: 10px; padding-right: 10px;}#tablaDetalle th{background-color: #22776b;color: white;font-weight: bold;border: 1px solid #22776b;height: 12px;}#tablaDetalle th:first-child{width: 45%;}#tablaDetalle th:nth-child(3){width: 15%;}#tablaDetalle td{border-left: 1px solid #929292; border-right: 1px solid #929292; height: 15px;}#tablaDetalle tr:last-child td{border-bottom: 1px solid #929292;}#valores{table-layout: fixed;width: 190px; margin-left: 460px;margin-top: 5px; font-weight: bold; border-collapse: collapse; border: 1px solid grey;}#valores td:first-child{width: 40%;padding: 5px 10px 2px 10px;}#valores td:nth-child(2){border: 1px solid #929292;background-color: #ececec;text-align: right;width: 60%;}#recibo{position: absolute;margin-top: 650px;margin-left: 270px;width: 450px;padding: 5px;text-align: justify;}#datosRecibo td:nth-child(1){font-weight: bold;}#datosRecibo2 td:nth-child(1){font-weight: bold;}#acuse{border: 1px solid black;border-radius: 5px;padding: 9px 9px 9px 9px;width: 380px;font-size: 9px;}#timbre{position: absolute;margin-left: 10px;margin-top: 750px;text-align: center;}#timbre p{line-height: 10%;font-size: 14px;font-weight: bold;color: #d64431;;}.rojo{color:#d64431;}.b{font-weight: bold;}.b2{font-weight: bold;}</style></head><body><div id='container'><div id='emisor'><h2 class='b2'>Restaurante Siglo XXI SPA</h2><h4>Restaurantes, cafes y otros establecimientos que expenden comidas y bebidas</h4><p>Casa Matriz: Vicuña Mackenna 5602, La Florida, Santiago.</p><p>Fonos: 2 2269901, 9 5508912</p></div><div id='datosFactura'><div id='factura' class='rojo'><p>R.U.T. : 76161082-1</p><p>BOLETA ELECTRÓNICA</p><p>N° " + id + "</p></div><div id='sii' class='rojo b'>S.I.I. - La Florida</div></div><div id='receptor'><div class='tabla1'><table id='tablaReceptor1'><tr><td>Fecha Emisión</td><td>: " + fechaHora + "</td></tr><tr><td>Medio de Pago</td><td>: " + tipoDocumentoPago + "</td></tr></table></div><div class='tabla2'></div></div><div id='detalle'><table id='tablaDetalle'><tr><th>Descripción</th><th>Precio</th><th>Cantidad</th><th>Sub Total</th></tr>" + detalles + "</table><table id='valores'><tr><td>Total</td><td>" + total + "</td></tr></table></div><div id=recibo><table id=datosRecibo> <tr> <td>Nombre</td><td> ....................................................................................</td></tr><tr> <td>R.U.T.</td><td> ....................................................................................</td></tr><tr> <td>Fecha</td><td> ....................................................................................</td></tr></table> <table id=datosRecibo> <tr> <td>Recinto</td><td> .............................................</td><td class=b>Firma</td><td> .......................................</td></tr></table> <br><div id=acuse> EL ACUSE DE RECIBO QUE SE DECLARA EN ESTE ACTO, DE ACUERDO A LO DISPUESTO EN LA LETRA B) DEL ART. 4°, Y LA LETRA C) DEL ART. 5°DE LA LEY 19.983, ACREDITA QUE LA ENTREGA DE MERCADERÍAS O SERVICIO (S) PRESTADO (S) HA (N) SIDO RECIBIDO (S). </div></div><div id='timbre'> <p>Timbre Electr&oacute;nico SII</p><p>Verifique documento: www.sii.cl</p></div></div></body></html>";
        }
    }
}
