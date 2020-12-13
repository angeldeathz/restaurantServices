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
        private readonly PedidoBl _pedidoBl;
        private readonly ArticuloPedidoBl _articuloPedidoBl;

        public ReportesBl()
        {
            _usuarioBl = new UsuarioBl();
            _proveedorBl = new OrdenProveedorBl();
            _itextSharpClient = new ItextSharpClient();
            _documentoPagoBl = new DocumentoPagoBl();
            _pedidoBl = new PedidoBl();
            _articuloPedidoBl = new ArticuloPedidoBl();
        }

        public async Task<string> ObtenerReporteAsync(ReporteDto reporte)
        {
            var usuario = await _usuarioBl.ObtenerPorIdAsync(reporte.IdUsuario);
            if (usuario == null) throw new Exception($"No existe el usuario con ID {reporte.IdUsuario}");

            string html;

            switch (reporte.IdReporte)
            {
                case 1:
                    html = await GetHtmlReporteDiario(usuario, reporte);
                    break;
                case 2:
                    html = await GetHtmlReporteMensual(usuario, reporte);
                    break;
                case 3:
                    html = await GetHtmlReporteClientes(usuario, reporte);
                    break;
                case 4:
                    html = await GetHtmlReportePlatos(usuario, reporte);
                    break;
                case 5:
                    html = await GetHtmlReporteTiempos(usuario, reporte);
                    break;
                default:
                    throw new Exception("Id reporte debe ser 1 o 2");
            }

            return _itextSharpClient.CreatePdfBase64(html);
        }

        private async Task<string> GetHtmlReporteDiario(Usuario usuario, ReporteDto reporte)
        {
            var ordenes = await _proveedorBl.ObtenerTodosAsync();
            var documentoPagos = await _documentoPagoBl.ObtenerTodosAsync();

            ordenes = ordenes.Where(x => x.FechaHora.Date == reporte.FechaDesde.Date).ToList();
            documentoPagos = documentoPagos.Where(x => x.FechaHora.Date == reporte.FechaDesde.Date).ToList();

            var fecha = $"{GetDayName((int)reporte.FechaDesde.DayOfWeek)}, {reporte.FechaDesde.Date:dd-MM-yyyy}";
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
            if (detalleEgresos == string.Empty)
            {
                detalleEgresos = "<tr><td colspan='4'><p class='lead no-encontrado text-center font-weight-bold'>No se encontraron órdenes en el periodo seleccionado</p></td></tr>";
            }
            documentoPagos.ForEach(x =>
            {
                detalleIngresos += "<tr>";
                detalleIngresos += $"<td>{x.Id}</td>";
                detalleIngresos += $"<td>{x.FechaHora:HH:mm}</td>";
                detalleIngresos += $"<td>{x.TipoDocumentoPago.Nombre}</td>";
                detalleIngresos += $"<td>$ {x.Total}</td>";
                detalleIngresos += "</tr>";
            });
            if (detalleIngresos == string.Empty)
            {
                detalleIngresos = "<tr><td colspan='4'><p class='lead no-encontrado text-center font-weight-bold'>No se encontraron atenciones en el periodo seleccionado</p></td></tr>";
            }
            return
                @"<!DOCTYPE html><html><head><meta charset='utf-8'></meta><style>table{page-break-inside:auto}tr{page-break-inside:avoid; page-break-after:auto}thead{display:table-header-group}tfoot{display:table-footer-group}body{font-family: 'Arial', 'Verdana', 'Helvetica', Sans-serif;font-size: 13px;}h1{color: #22776b;}h3{color: #383838;}.center{margin: 0 auto;}.w-100{width: 100%;}.w-50{width: 49%;}.logo{width: 100px;}.text-center{text-align: center;}.text-left{text-align: left;}.text-right{text-align: right;}.d-inline-block{display: inline-block;vertical-align: top;}.tabla-estilizada{width: 100%;margin: 25px 0;}.tabla-estilizada thead tr{background-color: #009879;color: #ffffff;text-align: left;}.tabla-estilizada thead tr th.transparente{background-color: #ffffff !important;}.tabla-estilizada th,.tabla-estilizada td{padding: 12px 15px;}.tabla-estilizada tbody tr{border-bottom: 1px solid #dddddd;}.tabla-estilizada tbody tr:nth-of-type(even){background-color: #f3f3f3;}.tabla-estilizada tbody tr:last:child{border-bottom: 2px solid #009879;}.tabla-estilizada.tabla-estilizada-resumen th:nth-child(even){background-color: #efefef;color: #2d2d2d;}.tabla-estilizada.egresos thead tr{background-color: #d49292;}.tabla-estilizada.ingresos thead tr{background-color: #71a2a5;}.tabla-estilizada.egresos td, .tabla-estilizada.ingresos td{text-align: center;}.tabla-estilizada.egresos td:last-child, .tabla-estilizada.ingresos td:last-child{text-align: right;}</style></head><body><div id='container'><div class='w-100'><img class='logo' src='" + rutaImagen + "'/></div><div class='w-100 text-center'><h1>Reporte de utilidad diaria</h1></div><div class='w-100'><div class='w-50 d-inline-block' style='margin-bottom: 68px;'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>Fecha:</th><th>" + fecha + "</th><th class='transparente'></th><th class='transparente'></th></tr><tr><th>Solicitante:</th><th>" + solicitante + "</th><th class='transparente'></th><th class='transparente'></th></tr></thead></table></div><div class='w-50 d-inline-block'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>N° órdenes</th><th>" + cantOrdenes + "</th><th>Egresos</th><th class='text-right'>" + montoEgresos.ToString("#,##0") + "</th></tr><tr><th>N° pedidos</th><th>" + cantPedidos + "</th><th>Ingresos</th><th class='text-right'>" + montoIngresos.ToString("#,##0") + "</th></tr><tr><th colspan='4' class='transparente'></th><tr><th class='transparente'></th><th class='transparente'></th><th>Utilidades</th><th class='text-right'>" + montoBalance.ToString("#,##0") + "</th></tr></thead></table></div></div><div><h3>Detalle de Egresos</h3><table class='tabla-estilizada egresos text-left'><thead><tr><th>Id Orden</th><th class='text-center'>Hora</th><th class='text-center'>Estado</th><th class='text-center'>Total</th></tr></thead><tbody>" + detalleEgresos + "</tbody></table></div><div><h3>Detalle de Ingresos</h3><table class='tabla-estilizada ingresos text-left'><thead><tr><th>Id Pedido</th><th class='text-center'>Hora</th><th class='text-center'>Medio de pago</th><th class='text-center'>Total</th></tr></thead><tbody>" + detalleIngresos + "</tbody></table></div></div></div></body></html>";
        }

        private async Task<string> GetHtmlReporteMensual(Usuario usuario,  ReporteDto reporte)
        {
            var ordenes = await _proveedorBl.ObtenerTodosAsync();
            var documentoPagos = await _documentoPagoBl.ObtenerTodosAsync();

            ordenes = ordenes.Where(x => x.FechaHora.Date >= reporte.FechaDesde.Date && x.FechaHora.Date <= reporte.FechaHasta.Date).ToList();
            documentoPagos = documentoPagos.Where(x => x.FechaHora.Date >= reporte.FechaDesde.Date && x.FechaHora.Date <= reporte.FechaHasta.Date).ToList();

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
            if (detalleEgresos == string.Empty)
            {
                detalleEgresos = "<tr><td colspan='4'><p class='lead no-encontrado text-center font-weight-bold'>No se encontraron órdenes en el periodo seleccionado</p></td></tr>";
            }
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
            if (detalleIngresos == string.Empty)
            {
                detalleIngresos = "<tr><td colspan='3'><p class='lead no-encontrado text-center font-weight-bold'>No se encontraron atenciones en el periodo seleccionado</p></td></tr>";
            }
            return
                @"<!DOCTYPE html><html><head><meta charset='utf-8'><title></title><style type='text/css'> table{page-break-inside:auto}tr{page-break-inside:avoid; page-break-after:auto}thead{display:table-header-group}tfoot{display:table-footer-group}body{font-family: 'Arial', 'Verdana', 'Helvetica', Sans-serif;font-size: 13px;}h1{color: #22776b;}h3{color: #383838;}.center{margin: 0 auto;}.w-100{width: 100%;}.w-50{width: 49%;}.logo{width: 100px;}.text-center{text-align: center;}.text-left{text-align: left;}.text-right{text-align: right;}.d-inline-block{display: inline-block;vertical-align: top;}.tabla-estilizada{width: 100%;margin: 25px 0;}.tabla-estilizada thead tr{background-color: #009879;color: #ffffff;text-align: left;}.tabla-estilizada thead tr th.transparente{background-color: #ffffff !important;}.tabla-estilizada th,.tabla-estilizada td{padding: 12px 15px;}.tabla-estilizada tbody tr{border-bottom: 1px solid #dddddd;}.tabla-estilizada tbody tr:nth-of-type(even){background-color: #f3f3f3;}.tabla-estilizada tbody tr:last:child{border-bottom: 2px solid #009879;}.tabla-estilizada.tabla-estilizada-resumen th:nth-child(even){background-color: #efefef;color: #2d2d2d;}.tabla-estilizada.tabla-estilizada-resumen th{padding: 12px 12px;}.tabla-estilizada.egresos thead tr{background-color: #d49292;}.tabla-estilizada.ingresos thead tr{background-color: #71a2a5;}.tabla-estilizada.egresos td, .tabla-estilizada.ingresos td{text-align: center;}.tabla-estilizada.egresos td:last-child, .tabla-estilizada.ingresos td:last-child{text-align: right;}</style></head><body><div id='container'><div class='w-100'><img class='logo' src='" + rutaImagen + "'/></div><div class='w-100 text-center'><h1>Reporte de utilidad mensual</h1></div><div class='w-100'><div class='w-50 d-inline-block' style='margin-bottom: 68px;'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>Fecha:</th><th>" + fecha + "</th><th class='transparente'></th><th class='transparente'></th></tr><tr><th>Solicitante:</th><th>" + solicitante + "</th><th class='transparente'></th><th class='transparente'></th></tr></thead></table></div><div class='w-50 d-inline-block'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>N° órdenes</th><th>" + cantOrdenes + "</th><th>Egresos</th><th>" + montoEgresos.ToString("#,##0") + "</th></tr><tr><th>N° pedidos</th><th>" + cantPedidos + "</th><th>Ingresos</th><th>" + montoIngresos.ToString("#,##0") + "</th></tr><tr><th colspan='4' class='transparente'></th><tr><th class='transparente'></th><th class='transparente'></th><th>Utilidades</th><th>" + montoBalance.ToString("#,##0") + "</th></tr></thead></table></div></div><div><div><h3>Detalle de Egresos</h3><table class='tabla-estilizada egresos text-left'><thead><tr><th class='text-center'>Fecha</th><th class='text-center'>Cantidad órdenes</th><th class='text-right'>Total</th></tr></thead><tbody>" + detalleEgresos + "</tbody></table></div><div><h3>Detalle de Ingresos</h3><table class='tabla-estilizada ingresos text-left'><thead><tr><th class='text-center'>Fecha</th><th class='text-center'>Cantidad pedidos</th><th class='text-right'>Total</th></tr></thead><tbody>" + detalleIngresos + "</tbody></table></div></div></div></div></body></html>";
        }

        private async Task<string> GetHtmlReporteClientes(Usuario usuario, ReporteDto reporte)
        {
            var pedidos = await _pedidoBl.ObtenerTodosAsync();
            pedidos = pedidos.Where(x => x.FechaHoraInicio.Date >= reporte.FechaDesde.Date && x.FechaHoraInicio.Date <= reporte.FechaHasta.Date)
                .OrderBy(x => x.FechaHoraInicio)
                .ToList();

            var pedidosGroup = pedidos.GroupBy(x => x.FechaHoraInicio.Date).ToList();

            var fecha = $"{reporte.FechaDesde.Date:dd-MM-yyyy} al {reporte.FechaHasta.Date:dd-MM-yyyy}";
            var solicitante = $"{usuario.Persona.Nombre} {usuario.Persona.Apellido}";

            var rutaImagen = "C:\\Storage\\logo_sxxi.png";
            var detalleAtenciones = string.Empty;

            var clientesDiarios = new List<int>();

            pedidosGroup.ForEach(a =>
            {
                var atendidos = 0;
                var atendidosPorMesa = new List<int>();

                pedidos.ForEach(b =>
                {
                    if (a.Key.Date == b.FechaHoraInicio.Date)
                    {
                        atendidos += b.Reserva.CantidadComensales;
                        atendidosPorMesa.Add(b.Reserva.CantidadComensales);
                    }
                });
                
                detalleAtenciones += "<tr>";
                detalleAtenciones += $"<td>{a.Key.Date:dd-MM-yyyy}</td>";
                detalleAtenciones += $"<td>{atendidos}</td>";
                detalleAtenciones += $"<td>{Math.Round(atendidosPorMesa.Average(), 1)}</td>";
                detalleAtenciones += "</tr>";

                clientesDiarios.Add(atendidos);
            });

            if(detalleAtenciones == string.Empty)
            {
                detalleAtenciones = "<tr><td colspan='3'><p class='lead no-encontrado text-center font-weight-bold'>No se encontraron atenciones en el periodo seleccionado</p></td></tr>";
            }
            var totalAtendidos = 0;
            var promedioDiario = 0.0;
            var minAtendidos = 0;
            var maxAtendidos = 0;
            if (clientesDiarios.Count > 0)
            {
                totalAtendidos = clientesDiarios.Sum();
                promedioDiario = Math.Round(clientesDiarios.Average(), 1);
                minAtendidos = clientesDiarios.Min();
                maxAtendidos = clientesDiarios.Max();
            }

            string estiloMargen = "";
            if(totalAtendidos >= 1000 && maxAtendidos >= 1000)
            {
                estiloMargen = "margen-tabla-resumen";
            }

            return
                @"<!DOCTYPE html><html><head><meta charset='utf-8'></meta><style>table{page-break-inside:auto}tr{page-break-inside:avoid;page-break-after:auto}thead{display:table-header-group}tfoot{display:table-footer-group}body{font-family:'Arial','Verdana','Helvetica',Sans-serif;font-size:13px}h1{color:#22776b}h3{color:#383838}.center{margin:0 auto}.w-100{width:100%}.w-50{width:49%}.logo{width:100px}.text-center{text-align:center}.text-left{text-align:left}.text-right{text-align:right}.d-inline-block{display:inline-block;vertical-align:top}.tabla-estilizada{width:100%;margin:25px 0}.tabla-estilizada thead tr{background-color:#009879;color:#fff;text-align:left}.tabla-estilizada thead tr th.transparente{background-color:#fff !important}.tabla-estilizada th, .tabla-estilizada td{padding:12px 15px}.tabla-estilizada tbody tr{border-bottom:1px solid #ddd}.tabla-estilizada tbody tr:nth-of-type(even){background-color:#f3f3f3}.tabla-estilizada tbody tr:last:child{border-bottom:2px solid #009879}.tabla-estilizada.tabla-estilizada-resumen th:nth-child(even){background-color:#efefef;color:#2d2d2d}.tabla-estilizada.tabla-estilizada-resumen th{padding:12px 11px}.tabla-estilizada.egresos thead tr{background-color:#d49292}.tabla-estilizada.ingresos thead tr{background-color:#71a2a5}.tabla-estilizada.egresos td, .tabla-estilizada.ingresos td{text-align:center}.tabla-estilizada.egresos td:last-child, .tabla-estilizada.ingresos td:last-child{text-align:right}.no-encontrado{color:#d49292;font-weight:600}.p-1{padding:5px!important}</style></head><body><div id='container'><div class='w-100'> <img class='logo' src='" + rutaImagen + "'/></div><div class='w-100 text-center'><h1>Reporte de clientes atendidos</h1></div><div class='w-100'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>Fecha:</th><th>" + fecha + "</th><th class='transparente p-1'></th><th class='transparente p-1'></th><th>Total</th><th class='text-right'>" + totalAtendidos + "</th><th>Mínimo diario</th><th class='text-right'>" + minAtendidos + "</th></tr><tr><th>Solicitante:</th><th>" + solicitante + "</th><th class='transparente p-1'></th><th class='transparente p-1'></th><th>Promedio diario</th><th class='text-right'>" + promedioDiario + "</th><th>Máximo diario</th><th class='text-right'>" + maxAtendidos + "</th></tr></thead></table></div><div class='w-100'><h3>Detalle de atenciones</h3><table class='tabla-estilizada egresos text-left'><thead><tr><th class='text-center'>Fecha</th><th class='text-center'>Clientes atendidos</th><th class='text-right'>Promedio por atención</th></tr></thead><tbody>" + detalleAtenciones + "</tbody></table></div></div></body></html>";
        }

        private async Task<string> GetHtmlReportePlatos(Usuario usuario, ReporteDto reporte)
        {
            var fecha = $"{reporte.FechaDesde.Date:dd-MM-yyyy} al {reporte.FechaHasta.Date:dd-MM-yyyy}";
            var solicitante = $"{usuario.Persona.Nombre} {usuario.Persona.Apellido}";
            var rutaImagen = "C:\\Storage\\logo_sxxi.png";
            var detallePlatos = "";

            var pedidos = await _pedidoBl.ObtenerTodosAsync();
            pedidos = pedidos.Where(x => x.FechaHoraInicio.Date >= reporte.FechaDesde.Date && x.FechaHoraInicio.Date <= reporte.FechaHasta.Date)
                .OrderBy(x => x.FechaHoraInicio)
                .ToList();

            var articulos = new List<ArticuloPedido>();

            foreach (var x in pedidos)
            {
                var articulosPedidos = await _articuloPedidoBl.ObtenerPorIdPedidoAsync(x.Id);
                articulos.AddRange(articulosPedidos);
            }

            articulos = articulos.OrderBy(x => x.Id).ToList();

            var articulosGroup = articulos.OrderByDescending(x => x.Cantidad).GroupBy(x => x.Articulo.Id).ToList();

            var articuloConMasVentas = new ArticuloPedido
            {
                Cantidad = 0,
                Articulo = new Articulo
                {
                    Nombre = string.Empty
                }
            };

            var articuloConMenosVentas = new ArticuloPedido
            {
                Cantidad = 100,
                Articulo = new Articulo
                {
                    Nombre = string.Empty
                }
            };

            articulosGroup.ForEach(x =>
            {
                var cantidad = 0;
                var nombrePlato = string.Empty;
                var tipoPlato = string.Empty;

                articulos.ForEach(b =>
                {
                    if (x.Key == b.Articulo.Id)
                    {
                        cantidad += b.Cantidad;
                        nombrePlato = b.Articulo.Nombre;
                        tipoPlato = b.Articulo.TipoConsumo.Nombre;
                    }
                });

                detallePlatos += "<tr>";
                detallePlatos += $"<td>{nombrePlato}</td>";
                detallePlatos += $"<td>{tipoPlato}</td>";
                detallePlatos += $"<td>{cantidad}</td>";
                detallePlatos += "</tr>";

                if (cantidad > articuloConMasVentas.Cantidad)
                {
                    articuloConMasVentas.Cantidad = cantidad;
                    articuloConMasVentas.Articulo.Nombre = nombrePlato;
                }

                if (cantidad < articuloConMenosVentas.Cantidad)
                {
                    articuloConMenosVentas.Cantidad = cantidad;
                    articuloConMenosVentas.Articulo.Nombre = nombrePlato;
                }
            });

            if (detallePlatos == string.Empty)
            {
                detallePlatos = "<tr><td colspan='3'><p class='no-encontrado text-center'>No se encontraron platos consumidos en el periodo seleccionado</p></td></tr>";
            }

            var platoMasPedido = "-";
            var platoMenosPedido = "-";
            var unidadesMasPedido = 0;
            var unidadesMenosPedido = 0;

            if (articuloConMasVentas.Articulo.Nombre != string.Empty)
            {
                platoMasPedido = articuloConMasVentas.Articulo.Nombre;
                unidadesMasPedido = articuloConMasVentas.Cantidad;
            }

            if (articuloConMenosVentas.Articulo.Nombre != string.Empty)
            {
                platoMenosPedido = articuloConMenosVentas.Articulo.Nombre;
                unidadesMenosPedido = articuloConMenosVentas.Cantidad;
            }

            return
                @"<!DOCTYPE html><html><head><meta charset='utf-8'></meta><style>table{page-break-inside:auto}tr{page-break-inside:avoid;page-break-after:auto}thead{display:table-header-group}tfoot{display:table-footer-group}body{font-family:'Arial','Verdana','Helvetica',Sans-serif;font-size:13px}h1{color:#22776b}h3{color:#383838}.center{margin:0 auto}.w-100{width:100%}.w-50{width:49%}.logo{width:100px}.text-center{text-align:center}.text-left{text-align:left}.text-right{text-align:right}.d-inline-block{display:inline-block;vertical-align:top}.tabla-estilizada{width:100%;margin:25px 0}.tabla-estilizada thead tr{background-color:#009879;color:#fff;text-align:left}.tabla-estilizada thead tr th.transparente{background-color:#fff !important}.tabla-estilizada th, .tabla-estilizada td{padding:12px 15px}.tabla-estilizada tbody tr{border-bottom:1px solid #ddd}.tabla-estilizada tbody tr:nth-of-type(even){background-color:#f3f3f3}.tabla-estilizada tbody tr:last:child{border-bottom:2px solid #009879}.tabla-estilizada.tabla-estilizada-resumen th:nth-child(even){background-color:#efefef;color:#2d2d2d}.tabla-estilizada.egresos thead tr{background-color:#d49292}.tabla-estilizada.ingresos thead tr{background-color:#71a2a5}.tabla-estilizada.egresos td, .tabla-estilizada.ingresos td{text-align:center}.tabla-estilizada.egresos td:last-child, .tabla-estilizada.ingresos td:last-child{text-align:right}.no-encontrado{color:#d49292;font-weight:600}.margen-tabla-resumen{margin-bottom:29px}.p-1{padding:5px!important}</style></head><body><div id='container'><div class='w-100'> <img class='logo' src='" + rutaImagen + "'/></div><div class='w-100 text-center'><h1>Reporte de platos consumidos</h1></div><div class='w-100'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>Fecha:</th><th>" + fecha + "</th><th class='transparente p-1'></th><th class='transparente p-1'></th><th>Más pedido</th><th class='text-right'>" + platoMasPedido + "</th><th>Unidades</th><th class='text-right'>" + unidadesMasPedido + "</th></tr><tr><th>Solicitante:</th><th>" + solicitante + "</th><th class='transparente p-1'></th><th class='transparente p-1'></th><th>Menos pedido</th><th class='text-right'>" + platoMenosPedido + "</th><th>Unidades</th><th class='text-right'>" + unidadesMenosPedido + "</th></tr></thead></table></div><div class='w-100'><h3>Detalle de platos pedidos</h3><table class='tabla-estilizada egresos text-left'><thead><tr><th class='text-center'>Nombre del plato</th><th class='text-center'>Tipo de plato</th><th class='text-right'>Unidades pedidas</th></tr></thead><tbody>" + detallePlatos + "</tbody></table></div></div></body></html>";
        }

        private async Task<string> GetHtmlReporteTiempos(Usuario usuario, ReporteDto reporte)
        {
            var fecha = $"{reporte.FechaDesde.Date:dd-MM-yyyy} al {reporte.FechaHasta.Date:dd-MM-yyyy}";
            var solicitante = $"{usuario.Persona.Nombre} {usuario.Persona.Apellido}";
            var rutaImagen = "C:\\Storage\\logo_sxxi.png";

            var pedidos = await _pedidoBl.ObtenerTodosAsync();
            pedidos = pedidos.Where(x => x.FechaHoraInicio.Date >= reporte.FechaDesde.Date && x.FechaHoraInicio.Date <= reporte.FechaHasta.Date)
                .OrderBy(x => x.FechaHoraInicio)
                .ToList();

            var pedidosGroup = pedidos.GroupBy(x => x.FechaHoraInicio.Date).ToList();

            var detalleAtenciones = "";

            var minutosAtencionTotal = new List<double>();

            pedidosGroup.ForEach(a =>
            {
                var cantidadComensales = new List<int>();
                var minutosAtencion = new List<double>();

                pedidos.ForEach(b =>
                {
                    if (a.Key.Date == b.FechaHoraInicio.Date)
                    {
                        cantidadComensales.Add(b.Reserva.CantidadComensales);
                        var timespan = b.FechaHoraFin - b.FechaHoraInicio;
                        minutosAtencion.Add(timespan.TotalMinutes);
                    }
                });

                var promedioMinutos = minutosAtencion.Average();
                var timeSpan = TimeSpan.FromMinutes(promedioMinutos);
                var time = DateTime.Today.Add(timeSpan);

                detalleAtenciones += "<tr>";
                detalleAtenciones += $"<td>{a.Key.Date:dd-MM-yyyy}</td>";
                detalleAtenciones += $"<td>{GetDayName((int)a.Key.DayOfWeek)}</td>";
                detalleAtenciones += $"<td>{Math.Round(cantidadComensales.Average(), 1)}</td>";
                detalleAtenciones += $"<td>{time:t}</td>";
                detalleAtenciones += "</tr>";

                minutosAtencionTotal.AddRange(minutosAtencion);
            });
            if (detalleAtenciones == string.Empty)
            {
                detalleAtenciones = "<tr><td colspan='4'><p class='no-encontrado text-center font-weight-bold'>No se encontraron atenciones en el periodo seleccionado</p></td></tr>";
            }

            var duracionPromedio = DateTime.Today.Add(TimeSpan.FromMinutes(minutosAtencionTotal.Count > 0 ? minutosAtencionTotal.Average(): 0));
            var atencionMasLarga = DateTime.Today.Add(TimeSpan.FromMinutes(minutosAtencionTotal.Count > 0 ? minutosAtencionTotal.Max(): 0));
            var atencionMasCorta = DateTime.Today.Add(TimeSpan.FromMinutes(minutosAtencionTotal.Count > 0 ? minutosAtencionTotal.Min() : 0));

            return
                @"<!DOCTYPE html><html><head><meta charset='utf-8'></meta><style>table{page-break-inside:auto}tr{page-break-inside:avoid;page-break-after:auto}thead{display:table-header-group}tfoot{display:table-footer-group}body{font-family:'Arial','Verdana','Helvetica',Sans-serif;font-size:13px}h1{color:#22776b}h3{color:#383838}.center{margin:0 auto}.w-100{width:100%}.w-50{width:49%}.logo{width:100px}.text-center{text-align:center}.text-left{text-align:left}.text-right{text-align:right}.d-inline-block{display:inline-block;vertical-align:top}.tabla-estilizada{width:100%;margin:25px 0}.tabla-estilizada thead tr{background-color:#009879;color:#fff;text-align:left}.tabla-estilizada thead tr th.transparente{background-color:#fff !important}.tabla-estilizada th, .tabla-estilizada td{padding:12px 15px}.tabla-estilizada tbody tr{border-bottom:1px solid #ddd}.tabla-estilizada tbody tr:nth-of-type(even){background-color:#f3f3f3}.tabla-estilizada tbody tr:last:child{border-bottom:2px solid #009879}.tabla-estilizada.tabla-estilizada-resumen{font-size:0.9em}.tabla-estilizada.tabla-estilizada-resumen th{padding:12px 11px}.tabla-estilizada.tabla-estilizada-resumen th:nth-child(even){background-color:#efefef;color:#2d2d2d}.tabla-estilizada.egresos thead tr{background-color:#d49292}.tabla-estilizada.ingresos thead tr{background-color:#71a2a5}.tabla-estilizada.egresos td, .tabla-estilizada.ingresos td{text-align:center}.tabla-estilizada.egresos td:last-child, .tabla-estilizada.ingresos td:last-child{text-align:right}.no-encontrado{color:#d49292;font-weight:600}.margen-tabla-resumen{margin-bottom:29px}.p-1{padding:5px!important}</style></head><body><div id='container'><div class='w-100'> <img class='logo' src='" + rutaImagen + "'/></div><div class='w-100 text-center'><h1>Reporte de tiempos de atención</h1></div><div class='w-100'><table class='tabla-estilizada tabla-estilizada-resumen'><thead><tr><th>Fecha:</th><th>" + fecha + "</th><th class='transparente p-1'></th><th class='transparente p-1'></th><th>Duración promedio</th><th class='text-right'>" + duracionPromedio.ToString("T") + "</th><th>Atención más larga</th><th class='text-right'>" + atencionMasLarga.ToString("T") + "</th></tr><tr><th>Solicitante:</th><th>" + solicitante + "</th><th class='transparente p-1'></th><th class='transparente p-1'></th><th class='transparente'></th><th class='transparente'></th><th>Atención más corta</th><th class='text-right'>" + atencionMasCorta.ToString("T") + "</th></tr></thead></table></div><div class='w-100'><h3>Detalle de tiempos de atención</h3><table class='tabla-estilizada egresos text-left'><thead><tr><th class='text-center'>Fecha</th><th class='text-center'>Día de la semana</th><th class='text-center'>Promedio comensales</th><th class='text-right'>Duración promedio</th></tr></thead><tbody>" + detalleAtenciones + "</tbody></table></div></div></body></html>";
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
