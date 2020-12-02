using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class PlatoValidator : AbstractValidator<Plato>
    {
        public PlatoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().NotNull().MaximumLength(150);
            RuleFor(x => x.MinutosPreparacion).GreaterThan(0);
            RuleFor(x => x.IdArticulo).GreaterThan(0);
            RuleFor(x => x.IdTipoPreparacion).GreaterThan(0);
        }
    }
}