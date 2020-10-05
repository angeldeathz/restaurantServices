using System;
using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(PedidoValidator))]
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int Total { get; set; }
        public int IdEstadoPedido { get; set; }
        public int IdMesa { get; set; }
        public Mesa Mesa { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
    }
}
