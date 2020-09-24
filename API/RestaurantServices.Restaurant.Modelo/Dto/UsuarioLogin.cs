using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Dto
{
    [Validator(typeof(UsuarioLoginValidator))]
    public class UsuarioLogin
    {
        public string Rut { get; set; }
        public string Contrasena { get; set; }
    }
}
