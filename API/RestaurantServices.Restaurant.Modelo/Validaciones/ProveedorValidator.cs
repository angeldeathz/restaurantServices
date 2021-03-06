﻿using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class ProveedorValidator : AbstractValidator<Proveedor>
    {
        public ProveedorValidator()
        {
            RuleFor(x => x.Direccion).NotNull().NotEmpty().MaximumLength(225);
            RuleFor(x => x.Persona).NotNull().NotEmpty();
        }
    }
}
