using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class ReservaValidator : AbstractValidator<Reserva>
    {
        public ReservaValidator()
        {
            RuleFor(x => x.IdMesa).GreaterThan(0);
            RuleFor(x => x.Mesa).NotNull();
            RuleFor(x => x.Cliente).NotNull();
            RuleFor(x => x.IdCliente).GreaterThan(0);
            RuleFor(x => x.FechaReserva).NotNull();
            RuleFor(x => x.Cliente).Null();
            RuleFor(x => x.Mesa).Null();
        }
    }
}
