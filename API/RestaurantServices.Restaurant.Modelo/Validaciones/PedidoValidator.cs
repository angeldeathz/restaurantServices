using System;
using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleFor(x => x.IdEstadoPedido).GreaterThan(0);

            RuleFor(x => x.FechaHoraInicio)
                .NotNull()
                .Must(BeAValidDate).WithMessage("Fecha inicio es inválida");

            RuleFor(x => x.FechaHoraFin)
                .NotNull()
                .Must(BeAValidDate).WithMessage("Fecha término es inválida");

            RuleFor(x => x.Total).GreaterThan(0);
            RuleFor(x => x.IdEstadoPedido).GreaterThan(0);
            RuleFor(x => x.IdMesa).GreaterThan(0);
            RuleFor(x => x.Mesa).Null();
            RuleFor(x => x.EstadoPedido).Null();
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
