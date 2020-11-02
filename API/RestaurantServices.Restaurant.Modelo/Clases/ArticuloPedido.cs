using System.Collections.Generic;
using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(ArticuloPedidoValidator))]
    public class ArticuloPedido
    {
        public int Id { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public int IdPedido { get; set; }
        public int IdArticulo { get; set; }
        public int IdEstadoArticuloPedido { get; set; }
        public string Comentarios { get; set; }
        public Pedido Pedido { get; set; }
        public Articulo Articulo { get; set; }
        public List<EstadoArticuloPedido> EstadosArticuloPedido { get; set; }
    }
}
