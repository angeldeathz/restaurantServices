using System;

namespace RestaurantServices.Restaurant.Modelo.TableJoin
{
    public class PedidoJoin
    {
        public int IdPedido { get; set; }
        public DateTime FechaInicioPedido { get; set; }
        public DateTime FechaFinPedido { get; set; }
        public int Total { get; set; }
        public int IdMesa { get; set; }
        public int IdEstadoPedido { get; set; }
        public string NombreMesa { get; set; }
        public int CantidadComensales { get; set; }
        public int IdEstadoMesa { get; set; }
        public string NombreEstadoPedido { get; set; }
    }
}