using System;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    public class OrdenProveedor
    {
        public int Id { get; set; }
        public DateTime FechaOrden { get; set; }
        public int Total { get; set; }
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public Proveedor Proveedor { get; set; }
        public Usuario Usuario { get; set; }
    }
}
