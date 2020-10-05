using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class ArticuloValidator : AbstractValidator<Articulo>
    {
        public ArticuloValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty();
            RuleFor(x => x.Descripcion).NotNull().NotEmpty();
            RuleFor(x => x.Precio).NotNull().GreaterThan(0);
            RuleFor(x => x.IdEstadoArticulo).GreaterThan(0);
            RuleFor(x => x.IdTipoConsumo).NotNull().NotEmpty();
            RuleFor(x => x.EstadoArticulo).Null();
            RuleFor(x => x.TipoConsumo).Null();
        }
    }
}
