using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class IngredientePlatoValidator : AbstractValidator<IngredientePlato>
    {
        public IngredientePlatoValidator()
        {
            RuleFor(x => x.CantidadInsumo).GreaterThan(0).LessThan(99999999);
            RuleFor(x => x.IdInsumo).GreaterThan(0).LessThan(99999999);
            RuleFor(x => x.IdPlato).GreaterThan(0).LessThan(99999999);
        }
    }
}