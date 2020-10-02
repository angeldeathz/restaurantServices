using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class MesaValidator : AbstractValidator<Mesa>
    {
        public MesaValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty();
            RuleFor(x => x.CantidadComensales).GreaterThan(0);
            RuleFor(x => x.IdEstadoMesa).GreaterThan(0);
        }
    }
}
