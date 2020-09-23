namespace RestaurantServices.Shared.Modelo.Clases
{
    public class ArticuloPedido
    {
        public int Id { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public int IdPedido { get; set; }
        public int IdArticulo { get; set; }
        public int IdEstadoArticuloPedido { get; set; }
        public Pedido Pedido { get; set; }
        public Articulo Articulo { get; set; }
    }
}
