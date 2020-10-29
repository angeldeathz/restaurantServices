using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Dto
{
    [Validator(typeof(ClienteNuevoDtoValidator))]
    public class ClienteNuevoDto
    {
        public string Email { get; set; }
    }
}
