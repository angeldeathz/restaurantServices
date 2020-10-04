using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(InsumoValidator))]
    public class Insumo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int StockActual { get; set; }
        public int StockOptimo { get; set; }
        public int StockCritico { get; set; }
        public int IdProveedor { get; set; }
        public int IdUnidadDeMedida { get; set; }
        public Proveedor Proveedor { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
    }
}