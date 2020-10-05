using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class OrdenProveedorValidator : AbstractValidator<OrdenProveedor>
    {
        public OrdenProveedorValidator()
        {
            RuleFor(x => x.FechaHora).NotNull().NotEmpty();
            RuleFor(x => x.Total).GreaterThan(0);
            RuleFor(x => x.IdProveedor).GreaterThan(0);
            RuleFor(x => x.IdUsuario).GreaterThan(0);
            RuleFor(x => x.Proveedor).Null();
            RuleFor(x => x.Usuario).Null();
        }
    }
}
