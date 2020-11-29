using System.IO;
using iText.Html2pdf;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "C:\\Storage";
            var exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            path = path + "\\documento.pdf";

            var file = new FileStream(path, FileMode.Create);
            HtmlConverter.ConvertToPdf(GetHtmlReporte(), file);
            file.Close();
        }

        public static string GetHtmlDocumentoPago()
        {
            var Id = "0000001";
            var FechaHora = "01/12/2020 13:45:00";
            var Total = "$19.990";
            var TipoDocumentoPago = "Boleta";

            //Recorrer los ArticuloPedido
            var detalles = "<tr>";
            detalles+= "<td></td>";
            detalles += "<td></td>";
            detalles += "<td></td>";
            detalles += "<td></td>";
            detalles += "</tr>";

            return
                @"<!DOCTYPE html><html><head><meta charset='utf-8'><title></title><style>body{font-family: 'Arial', 'Verdana', 'Helvetica', Sans-serif;font-size: 12px;}h2{color: #22776b;font-weight: bold;}#container{width: 700px;height: 1000px;margin-left: 30px;}#emisor{font-weight: 500;position: absolute;line-height: 10%;margin-top: 10px;}#datosFactura{position: absolute;margin-left: 460px;}#factura{border-width: 3px;border-style: solid;border-color: #d64431;padding: 0px 8px 0px 8px;text-align: center;font-weight: bold;line-height: 90%;width: 160px;}#sii{text-align: center;}#receptor{position: absolute;width: 650px;height: 70px;margin-top: 160px;padding-top: 15px;padding-left: 5px;padding-bottom: 15px;border: 2px solid #22776b;border-radius: 6px;}#receptor table tr td:first-child{font-weight: bold;}#receptor table td{padding-bottom: 3px;padding-right: 4px;}.tabla1{position: absolute;width: 350px;margin-left: 20px;}#tablaReceptor1{border-collapse: separate; border-spacing: 0px 3px 5px 0;}.tabla2{position: absolute;margin-left: 500px;width: 400px;}#tablaReceptor2{border-collapse: separate; border-spacing: 0px 3px;}#fecha{position: absolute;margin-top: 125px;margin-left: 580px;}#detalle{position: absolute;margin-top: 300px;height: 300px;}#tablaDetalle{border-collapse: collapse; width: 650px; text-align: center;}#tablaDetalle td, #tablaDetalle th{padding-left: 10px; padding-right: 10px;}#tablaDetalle th{background-color: #22776b;color: white;font-weight: bold;border: 1px solid #22776b;height: 12px;}#tablaDetalle th:first-child{width: 45%;}#tablaDetalle th:nth-child(3){width: 15%;}#tablaDetalle td{border-left: 1px solid #929292; border-right: 1px solid #929292; height: 15px;}#tablaDetalle tr:last-child td{border-bottom: 1px solid #929292;}#valores{table-layout: fixed;width: 190px; margin-left: 460px;margin-top: 5px; font-weight: bold; border-collapse: collapse; border: 1px solid grey;}#valores td:first-child{width: 40%;padding: 5px 10px 2px 10px;}#valores td:nth-child(2){border: 1px solid #929292;background-color: #ececec;text-align: right;width: 60%;}#recibo{position: absolute;margin-top: 650px;margin-left: 270px;width: 450px;padding: 5px;text-align: justify;}#datosRecibo td:nth-child(1){font-weight: bold;}#datosRecibo2 td:nth-child(1){font-weight: bold;}#acuse{border: 1px solid black;border-radius: 5px;padding: 9px 9px 9px 9px;width: 380px;font-size: 9px;}#timbre{position: absolute;margin-left: 10px;margin-top: 750px;text-align: center;}#timbre p{line-height: 10%;font-size: 14px;font-weight: bold;color: #d64431;;}.rojo{color:#d64431;}.b{font-weight: bold;}.b2{font-weight: bold;}</style></head><body><div id='container'><div id='emisor'><h2 class='b2'>Restaurante Siglo XXI SPA</h2><h4>Restaurantes, cafes y otros establecimientos que expenden comidas y bebidas</h4><p>Casa Matriz: Vicuña Mackenna 5602, La Florida, Santiago.</p><p>Fonos: 2 2269901, 9 5508912</p></div><div id='datosFactura'><div id='factura' class='rojo'><p>R.U.T. : 76161082-1</p><p>BOLETA ELECTRÓNICA</p><p>N° " + Id + "</p></div><div id='sii' class='rojo b'>S.I.I. - La Florida</div></div><div id='receptor'><div class='tabla1'><table id='tablaReceptor1'><tr><td>Fecha Emisión</td><td>: " + FechaHora + "</td></tr><tr><td>Medio de Pago</td><td>: " + TipoDocumentoPago + "</td></tr></table></div><div class='tabla2'></div></div><div id='detalle'><table id='tablaDetalle'><tr><th>Descripción</th><th>Precio</th><th>Cantidad</th><th>Sub Total</th></tr>" + detalles + "</table><table id='valores'><tr><td>Total</td><td>" + Total + "</td></tr></table></div><div id=recibo><table id=datosRecibo> <tr> <td>Nombre</td><td> ....................................................................................</td></tr><tr> <td>R.U.T.</td><td> ....................................................................................</td></tr><tr> <td>Fecha</td><td> ....................................................................................</td></tr></table> <table id=datosRecibo> <tr> <td>Recinto</td><td> .............................................</td><td class=b>Firma</td><td> .......................................</td></tr></table> <br><div id=acuse> EL ACUSE DE RECIBO QUE SE DECLARA EN ESTE ACTO, DE ACUERDO A LO DISPUESTO EN LA LETRA B) DEL ART. 4°, Y LA LETRA C) DEL ART. 5°DE LA LEY 19.983, ACREDITA QUE LA ENTREGA DE MERCADERÍAS O SERVICIO (S) PRESTADO (S) HA (N) SIDO RECIBIDO (S). </div></div><div id='timbre'> <p>Timbre Electr&oacute;nico SII</p><p>Verifique documento: www.sii.cl</p></div></div></body></html>";
        }

        public static string GetHtmlReporte()
        {
            var fecha = "Jueves 19/11/2020";
            var solicitante = "Alberto Farías";
            var cantOrdenes = "2";
            var montoEgresos = "$85.000";
            var cantPedidos = "19";
            var montoIngresos = "$321.000";
            var montoBalance = "$245.000";
            var rutaImagen = "C:\\Storage\\logo_sxxi.png";
            //Recorrer las boletas/facturas del dia y las ordenes de compra a proveedores INGRESADAS 
            var detalleIngresos = "";
            var detalleEgresos = "";
            for (var i = 0; i < 5; i++)
            {
                detalleEgresos += "<tr>";
                detalleEgresos += "<td>6</td>";
                detalleEgresos += "<td>09:21:12</td>";
                detalleEgresos += "<td>Aceptada</td>";
                detalleEgresos += "<td>$45.000</td>";
                detalleEgresos += "</tr>";

                detalleIngresos += "<tr>";
                detalleIngresos += "<td>12</td>";
                detalleIngresos += "<td>09:21:12</td>";
                detalleIngresos += "<td>Débito</td>";
                detalleIngresos += "<td>$23.980</td>";
                detalleIngresos += "</tr>";
            }
            return
                @"<!DOCTYPE html><html><head><meta charset='utf-8'><title></title><style>body{font-family: 'Arial', 'Verdana', 'Helvetica', Sans-serif;font-size: 12px;}#container{width: 700px;height: 1000px;}h1{color: #22776b;}h3{color: #383838;}.center{margin: 0 auto;}.w-100{width: 100%;}.w-50{width: 49%;}.logo{width: 100px;}.text-center{text-align: center;}.text-left{text-align: left;}.d-inline-block{display: inline-block;vertical-align: top;}.tabla-estilizada{width: 100%; margin: 25px 0; font-size: 0.9em;}.tabla-estilizada thead tr{background-color: #009879; color: #ffffff; text-align: left;}.tabla-estilizada.egresos thead tr{background-color: #d49292;}.tabla-estilizada.ingresos thead tr{background-color: #71a2a5;}.tabla-estilizada thead tr th.transparente{background-color: #ffffff !important;}.tabla-estilizada th,.tabla-estilizada td{padding: 12px 15px;}.tabla-estilizada tbody tr{border-bottom: 1px solid #dddddd;}.tabla-estilizada tbody tr:nth-of-type(even){background-color: #f3f3f3;}.tabla-estilizada tbody tr:last:child{border-bottom: 2px solid #009879;}.tabla-estilizada.tabla-estilizada-resumen th:nth-child(even){background-color: #efefef;color: #2d2d2d;}.tabla-estilizada.tabla-estilizada-resumen{font-size: 1.1em;}</style></head><body><div id='container'><div class='w-100'><img class='logo' src='" + rutaImagen + "'/></div><div class='w-100 text-center'><h1>Reporte de utilidad diaria</h1></div><div class='w-100'><div class='w-50 d-inline-block' style='margin-bottom: 68px;'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>Fecha:</th><th>" + fecha + "</th><th class='transparente'></th><th class='transparente'></th></tr><tr><th>Solicitante:</th><th>" + solicitante + "</th><th class='transparente'></th><th class='transparente'></th></tr></thead></table></div><div class='w-50 d-inline-block'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>N° órdenes</th><th>" + cantOrdenes + "</th><th>Egresos</th><th>" + montoEgresos + "</th></tr><tr><th>N° pedidos</th><th>" + cantPedidos + "</th><th>Ingresos</th><th>" + montoIngresos + "</th></tr><tr><th colspan='4' class='transparente'></th><tr><th class='transparente'></th><th class='transparente'></th><th>Utilidades</th><th>" + montoBalance + "</th></tr></thead></table></div></div><div class='w-100'><div class='w-50 d-inline-block'><h3>Detalle de Egresos</h3><table class='tabla-estilizada egresos text-left'><thead><tr><th>Id Orden</th><th>Hora</th><th>Estado</th><th>Total</th></tr></thead><tbody>" + detalleEgresos + "</tbody></table></div><div class='w-50 d-inline-block'><h3>Detalle de Ingresos</h3><table class='tabla-estilizada ingresos text-left'><thead><tr><th>Id Pedido</th><th>Hora</th><th>Medio de pago</th><th>Total</th></tr></thead><tbody>" + detalleIngresos + "</tbody></table></div></div></div>";
        }
    }
}
