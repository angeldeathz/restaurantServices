using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(ClienteValidator))]
    public class Cliente
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public Persona Persona { get; set; }
    }
}
