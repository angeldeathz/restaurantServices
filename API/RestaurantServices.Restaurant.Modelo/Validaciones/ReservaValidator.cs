using System;
using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class ReservaValidator : AbstractValidator<Reserva>
    {
        public ReservaValidator()
        {
            RuleFor(x => x.FechaReserva).NotNull()
                .Must(BeAValidDate).WithMessage("FechaReserva es inválida");

            RuleFor(x => x.CantidadComensales).GreaterThan(0);
            RuleFor(x => x.IdCliente).GreaterThan(0);
            RuleFor(x => x.IdMesa).GreaterThan(0);
            RuleFor(x => x.Cliente).Null();
            RuleFor(x => x.Mesa).Null();
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
