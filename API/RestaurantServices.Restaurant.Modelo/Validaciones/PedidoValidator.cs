﻿using System;
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
                .Must(BeAValidDate).WithMessage("FechaHoraInicio es inválida");

            RuleFor(x => x.FechaHoraFin)
                .NotNull()
                .Must(BeAValidDate).WithMessage("FechaHoraFin es inválida");

            RuleFor(x => x.Total).GreaterThan(0);
            RuleFor(x => x.IdEstadoPedido).GreaterThan(0);
            RuleFor(x => x.IdReserva).GreaterThan(0);
            RuleFor(x => x.Reserva).Null();
            RuleFor(x => x.EstadoPedido).Null();
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
