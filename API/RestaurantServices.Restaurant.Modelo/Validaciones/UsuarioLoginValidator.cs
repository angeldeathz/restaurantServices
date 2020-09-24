using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Dto;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class UsuarioLoginValidator : AbstractValidator<UsuarioLogin>
    {
        public UsuarioLoginValidator()
        {
            RuleFor(x => x.Contrasena).NotNull().NotEmpty();
            RuleFor(x => x.Rut).NotNull().NotEmpty();
        }
    }
}
