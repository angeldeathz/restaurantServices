using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class DetalleOrdenProveedorValidator : AbstractValidator<DetalleOrdenProveedor>
    {
        public DetalleOrdenProveedorValidator()
        {
            RuleFor(x => x.Precio).GreaterThan(0);
            RuleFor(x => x.Cantidad).GreaterThan(0);
            RuleFor(x => x.IdInsumo).GreaterThan(0);
            RuleFor(x => x.Total).GreaterThan(0);
            RuleFor(x => x.IdOrdenProveedor).GreaterThan(0);
            RuleFor(x => x.Insumo).Null();
        }
    }
}
