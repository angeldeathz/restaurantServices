using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Persona).NotNull();
            RuleFor(x => x.IdTipoUsuario).GreaterThan(0);
            RuleFor(x => x.Contrasena).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.TipoUsuario).Null();
        }
    }
}
