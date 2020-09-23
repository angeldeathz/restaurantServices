using System;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    public class DocumentoPago
    {
        public int Id { get; set; }
        public DateTime FechaPago { get; set; }
        public int Total { get; set; }
        public int IdPedido { get; set; }
        public int IdTipoDocumentoPago { get; set; }
        public Pedido Pedido { get; set; }
        public TipoDocumentoPago TipoDocumentoPago { get; set; }
    }
}
