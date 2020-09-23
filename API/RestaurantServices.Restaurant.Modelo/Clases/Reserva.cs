using System;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public int IdCliente { get; set; }
        public int IdMesa { get; set; }
        public int IdEstadoReserva { get; set; }
        public Cliente Cliente { get; set; }
        public Mesa Mesa { get; set; }
    }
}
