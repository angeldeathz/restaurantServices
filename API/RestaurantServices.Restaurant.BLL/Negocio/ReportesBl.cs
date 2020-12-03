using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;
using RestaurantServices.Restaurant.Shared.Itextsharp;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class ReportesBl
    {
        private readonly UsuarioBl _usuarioBl;
        private readonly OrdenProveedorBl _proveedorBl;
        private readonly DocumentoPagoBl _documentoPagoBl;
        private readonly ItextSharpClient _itextSharpClient;

        public ReportesBl()
        {
            _usuarioBl = new UsuarioBl();
            _proveedorBl = new OrdenProveedorBl();
            _itextSharpClient = new ItextSharpClient();
            _documentoPagoBl = new DocumentoPagoBl();
        }

        public async Task<string> ObtenerReporteAsync(ReporteDto reporte)
        {
            var usuario = await _usuarioBl.ObtenerPorIdAsync(reporte.IdUsuario);
            if (usuario == null) throw new Exception($"No existe el usuario con ID {reporte.IdUsuario}");

            var ordenes = await _proveedorBl.ObtenerTodosAsync();
            var documentoPagos = await _documentoPagoBl.ObtenerTodosAsync();

            string html;

            switch (reporte.IdReporte)
            {
                case 1:
                    html = GetHtmlReporteDiario(usuario, ordenes, documentoPagos);  
                    break;
                case 2:
                    ordenes = ordenes.Where(x => x.FechaHora.Date >= reporte.FechaDesde.Date && x.FechaHora.Date <= reporte.FechaHasta.Date).ToList();
                    documentoPagos = documentoPagos.Where(x => x.FechaHora.Date >= reporte.FechaDesde.Date && x.FechaHora.Date <= reporte.FechaHasta.Date).ToList();
                    html = GetHtmlReporteMensual(usuario, ordenes, documentoPagos, reporte);
                    break;
                default:
                    throw new Exception("Id reporte debe ser 1 o 2");
            }

            return _itextSharpClient.CreatePdfBase64(html);
        }

        private string GetHtmlReporteDiario(Usuario usuario, List<OrdenProveedor> ordenes, List<DocumentoPago> documentoPagos)
        {
            ordenes = ordenes.Where(x => x.FechaHora.Date == DateTime.Now.Date).ToList();
            documentoPagos = documentoPagos.Where(x => x.FechaHora.Date == DateTime.Now.Date).ToList();

            var fecha = $"{GetDayName((int)DateTime.Now.DayOfWeek)}, {DateTime.Now.Date:dd-MM-yyyy}";
            var solicitante = $"{usuario.Persona.Nombre} {usuario.Persona.Apellido}";
            var cantOrdenes = ordenes.Count;
            var montoEgresos = ordenes.Sum(x => x.Total);
            var cantPedidos = documentoPagos.Count;
            var montoIngresos = documentoPagos.Sum(x => x.Total);
            var montoBalance = montoIngresos - montoEgresos;
            var rutaImagen = "C:\\Storage\\logo_sxxi.png";

            //Recorrer las boletas/facturas del dia y las ordenes de compra a proveedores INGRESADAS 
            var detalleIngresos = "";
            var detalleEgresos = "";

            ordenes.ForEach(x =>
            {
                detalleEgresos += "<tr>";
                detalleEgresos += $"<td>{x.Id}</td>";
                detalleEgresos += $"<td>{x.FechaHora:HH:mm}</td>";
                detalleEgresos += $"<td>{x.EstadosOrdenProveedor.OrderByDescending(z => z.Fecha).FirstOrDefault()?.Nombre}</td>";
                detalleEgresos += $"<td>$ {x.Total:#,##0}</td>";
                detalleEgresos += "</tr>";
            });

            documentoPagos.ForEach(x =>
            {
                detalleIngresos += "<tr>";
                detalleIngresos += $"<td>{x.Id}</td>";
                detalleIngresos += $"<td>{x.FechaHora:HH:mm}</td>";
                detalleIngresos += $"<td>{x.TipoDocumentoPago.Nombre}</td>";
                detalleIngresos += $"<td>$ {x.Total}</td>";
                detalleIngresos += "</tr>";
            });

            return
                @"<!DOCTYPE html><html><head><meta charset='utf-8'></meta><style>table{page-break-inside:auto}tr{page-break-inside:avoid; page-break-after:auto}thead{display:table-header-group}tfoot{display:table-footer-group}body{font-family: 'Arial', 'Verdana', 'Helvetica', Sans-serif;font-size: 13px;}h1{color: #22776b;}h3{color: #383838;}.center{margin: 0 auto;}.w-100{width: 100%;}.w-50{width: 49%;}.logo{width: 100px;}.text-center{text-align: center;}.text-left{text-align: left;}.text-right{text-align: right;}.d-inline-block{display: inline-block;vertical-align: top;}.tabla-estilizada{width: 100%;margin: 25px 0;}.tabla-estilizada thead tr{background-color: #009879;color: #ffffff;text-align: left;}.tabla-estilizada thead tr th.transparente{background-color: #ffffff !important;}.tabla-estilizada th,.tabla-estilizada td{padding: 12px 15px;}.tabla-estilizada tbody tr{border-bottom: 1px solid #dddddd;}.tabla-estilizada tbody tr:nth-of-type(even){background-color: #f3f3f3;}.tabla-estilizada tbody tr:last:child{border-bottom: 2px solid #009879;}.tabla-estilizada.tabla-estilizada-resumen th:nth-child(even){background-color: #efefef;color: #2d2d2d;}.tabla-estilizada.egresos thead tr{background-color: #d49292;}.tabla-estilizada.ingresos thead tr{background-color: #71a2a5;}.tabla-estilizada.egresos td, .tabla-estilizada.ingresos td{text-align: center;}.tabla-estilizada.egresos td:last-child, .tabla-estilizada.ingresos td:last-child{text-align: right;}</style></head><body><div id='container'><div class='w-100'><img class='logo' src='" + rutaImagen + "'/></div><div class='w-100 text-center'><h1>Reporte de utilidad diaria</h1></div><div class='w-100'><div class='w-50 d-inline-block' style='margin-bottom: 68px;'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>Fecha:</th><th>" + fecha + "</th><th class='transparente'></th><th class='transparente'></th></tr><tr><th>Solicitante:</th><th>" + solicitante + "</th><th class='transparente'></th><th class='transparente'></th></tr></thead></table></div><div class='w-50 d-inline-block'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>N° órdenes</th><th>" + cantOrdenes + "</th><th>Egresos</th><th class='text-right'>" + montoEgresos + "</th></tr><tr><th>N° pedidos</th><th>" + cantPedidos + "</th><th>Ingresos</th><th class='text-right'>" + montoIngresos + "</th></tr><tr><th colspan='4' class='transparente'></th><tr><th class='transparente'></th><th class='transparente'></th><th>Utilidades</th><th class='text-right'>" + montoBalance + "</th></tr></thead></table></div></div><div><h3>Detalle de Egresos</h3><table class='tabla-estilizada egresos text-left'><thead><tr><th>Id Orden</th><th class='text-center'>Hora</th><th class='text-center'>Estado</th><th class='text-center'>Total</th></tr></thead><tbody>" + detalleEgresos + "</tbody></table></div><div><h3>Detalle de Ingresos</h3><table class='tabla-estilizada ingresos text-left'><thead><tr><th>Id Pedido</th><th class='text-center'>Hora</th><th class='text-center'>Medio de pago</th><th class='text-center'>Total</th></tr></thead><tbody>" + detalleIngresos + "</tbody></table></div></div></div></body></html>";
        }

        private string GetHtmlReporteMensual(Usuario usuario, List<OrdenProveedor> ordenes, List<DocumentoPago> documentoPagos, ReporteDto reporte)
        {
            var fecha = $"{reporte.FechaDesde.Date:dd-MM-yyyy} al {reporte.FechaHasta.Date:dd-MM-yyyy}";
            var solicitante = $"{usuario.Persona.Nombre} {usuario.Persona.Apellido}";
            var cantOrdenes = ordenes.Count;
            var montoEgresos = ordenes.Sum(x => x.Total);
            var cantPedidos = documentoPagos.Count;
            var montoIngresos = documentoPagos.Sum(x => x.Total);
            var montoBalance = montoIngresos - montoEgresos;
            var rutaImagen = "C:\\Storage\\logo_sxxi.png";

            //Recorrer las boletas/facturas del mes y las ordenes de compra a proveedores INGRESADAS 
            var detalleIngresos = "";
            var detalleEgresos = "";

            ordenes = ordenes.OrderBy(x => x.FechaHora).ToList();
            documentoPagos = documentoPagos.OrderBy(x => x.FechaHora).ToList();

            var ordenesGroup = ordenes.GroupBy(x => x.FechaHora.Date).ToList();
            var documentosGroup = documentoPagos.GroupBy(x => x.FechaHora.Date).ToList();

            ordenesGroup.ForEach(a =>
            {
                var numeroOrdenes = 0;
                var total = 0;
                ordenes.ForEach(b =>
                {
                    if (a.Key.Date == b.FechaHora.Date)
                    {
                        numeroOrdenes++;
                        total += b.Total;
                    }
                });

                detalleEgresos += "<tr>";
                detalleEgresos += $"<td>{a.Key.Date:dd-MM-yyyy}</td>";
                detalleEgresos += $"<td>{numeroOrdenes}</td>";
                detalleEgresos += $"<td>$ {total:#,##0}</td>";
                detalleEgresos += "</tr>";
            });

            documentosGroup.ForEach(a =>
            {
                var numeroPedidos = 0;
                var total = 0;

                documentoPagos.ForEach(b =>
                {
                    if (a.Key.Date == b.FechaHora.Date)
                    {
                        numeroPedidos++;
                        total += b.Total;
                    }  
                });

                detalleIngresos += "<tr>";
                detalleIngresos += $"<td>{a.Key.Date:dd-MM-yyyy}</td>";
                detalleIngresos += $"<td>{numeroPedidos}</td>";
                detalleIngresos += $"<td>$ {total:#,##0}</td>";
                detalleIngresos += "</tr>";
            });

            return
                @"<!DOCTYPE html><html><head><meta charset='utf-8'><title></title><style type='text/css'> table{page-break-inside:auto}tr{page-break-inside:avoid; page-break-after:auto}thead{display:table-header-group}tfoot{display:table-footer-group}body{font-family: 'Arial', 'Verdana', 'Helvetica', Sans-serif;font-size: 13px;}h1{color: #22776b;}h3{color: #383838;}.center{margin: 0 auto;}.w-100{width: 100%;}.w-50{width: 49%;}.logo{width: 100px;}.text-center{text-align: center;}.text-left{text-align: left;}.text-right{text-align: right;}.d-inline-block{display: inline-block;vertical-align: top;}.tabla-estilizada{width: 100%;margin: 25px 0;}.tabla-estilizada thead tr{background-color: #009879;color: #ffffff;text-align: left;}.tabla-estilizada thead tr th.transparente{background-color: #ffffff !important;}.tabla-estilizada th,.tabla-estilizada td{padding: 12px 15px;}.tabla-estilizada tbody tr{border-bottom: 1px solid #dddddd;}.tabla-estilizada tbody tr:nth-of-type(even){background-color: #f3f3f3;}.tabla-estilizada tbody tr:last:child{border-bottom: 2px solid #009879;}.tabla-estilizada.tabla-estilizada-resumen th:nth-child(even){background-color: #efefef;color: #2d2d2d;}.tabla-estilizada.tabla-estilizada-resumen th{padding: 12px 12px;}.tabla-estilizada.egresos thead tr{background-color: #d49292;}.tabla-estilizada.ingresos thead tr{background-color: #71a2a5;}.tabla-estilizada.egresos td, .tabla-estilizada.ingresos td{text-align: center;}.tabla-estilizada.egresos td:last-child, .tabla-estilizada.ingresos td:last-child{text-align: right;}</style></head><body><div id='container'><div class='w-100'><img class='logo' src='" + rutaImagen + "'/></div><div class='w-100 text-center'><h1>Reporte de utilidad mensual</h1></div><div class='w-100'><div class='w-50 d-inline-block' style='margin-bottom: 68px;'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>Fecha:</th><th>" + fecha + "</th><th class='transparente'></th><th class='transparente'></th></tr><tr><th>Solicitante:</th><th>" + solicitante + "</th><th class='transparente'></th><th class='transparente'></th></tr></thead></table></div><div class='w-50 d-inline-block'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>N° órdenes</th><th>" + cantOrdenes + "</th><th>Egresos</th><th>" + montoEgresos.ToString("#,##0") + "</th></tr><tr><th>N° pedidos</th><th>" + cantPedidos + "</th><th>Ingresos</th><th>" + montoIngresos.ToString("#,##0") + "</th></tr><tr><th colspan='4' class='transparente'></th><tr><th class='transparente'></th><th class='transparente'></th><th>Utilidades</th><th>" + montoBalance.ToString("#,##0") + "</th></tr></thead></table></div></div><div><div><h3>Detalle de Egresos</h3><table class='tabla-estilizada egresos text-left'><thead><tr><th class='text-center'>Fecha</th><th class='text-center'>Cantidad órdenes</th><th class='text-right'>Total</th></tr></thead><tbody>" + detalleEgresos + "</tbody></table></div><div><h3>Detalle de Ingresos</h3><table class='tabla-estilizada ingresos text-left'><thead><tr><th class='text-center'>Fecha</th><th class='text-center'>Cantidad pedidos</th><th class='text-right'>Total</th></tr></thead><tbody>" + detalleIngresos + "</tbody></table></div></div></div></div></body></html>";
        }

        private string GetDayName(int dayNumber)
        {
            string nombre;

            switch (dayNumber)
            {
                case 0:
                    nombre = "Domingo";
                    break;
                case 1:
                    nombre = "Lunes";
                    break;
                case 2:
                    nombre = "Martes";
                    break;
                case 3:
                    nombre = "Miércoles";
                    break;
                case 4:
                    nombre = "Jueves";
                    break;
                case 5:
                    nombre = "Viernes";
                    break;
                case 6:
                    nombre = "Sábado";
                    break;
                default:
                    throw new Exception("No se encontró el día");
            }

            return nombre;
        }
    }
}
