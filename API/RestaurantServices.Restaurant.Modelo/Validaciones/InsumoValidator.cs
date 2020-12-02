using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class InsumoValidator : AbstractValidator<Insumo>
    {
        public InsumoValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(x => x.IdProveedor).GreaterThan(0);
            RuleFor(x => x.IdUnidadDeMedida).GreaterThan(0);
            RuleFor(x => x.StockActual).GreaterThan(0);
            RuleFor(x => x.StockOptimo).GreaterThan(0);
            RuleFor(x => x.StockCritico).GreaterThan(0);
            RuleFor(x => x.Proveedor).Null();
            RuleFor(x => x.UnidadMedida).Null();
        }
    }
}
