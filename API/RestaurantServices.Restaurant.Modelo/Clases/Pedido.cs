using System;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaTermino { get; set; }
        public int Total { get; set; }
        public int IdEstadoPedido { get; set; }
        public int IdMesa { get; set; }
        public Mesa Mesa { get; set; }
    }
}
