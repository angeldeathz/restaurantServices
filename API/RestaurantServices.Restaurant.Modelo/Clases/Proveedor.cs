using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(ProveedorValidator))]
    public class Proveedor
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public int IdPersona { get; set; }
        public Persona Persona { get; set; }
    }
}
