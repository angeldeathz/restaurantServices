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
            HtmlConverter.ConvertToPdf(GetHtmlDocumentoPago(), file);
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
    }
}
