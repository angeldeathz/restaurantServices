using System;
using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(PedidoValidator))]
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public int Total { get; set; }
        public int IdEstadoPedido { get; set; }
        public int IdReserva { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
        public Reserva Reserva { get; set; }
    }
}
