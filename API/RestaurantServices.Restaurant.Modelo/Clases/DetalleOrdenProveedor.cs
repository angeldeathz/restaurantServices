using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(DetalleOrdenProveedorValidator))]
    public class DetalleOrdenProveedor
    {
        public int Id { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public int IdInsumo { get; set; }
        public int IdOrdenProveedor { get; set; }
        public Insumo Insumo { get; set; }
        public OrdenProveedor OrdenProveedor { get; set; }
    }
}
