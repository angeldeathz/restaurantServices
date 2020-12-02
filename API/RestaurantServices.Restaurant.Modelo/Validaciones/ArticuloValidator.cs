using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class ArticuloValidator : AbstractValidator<Articulo>
    {
        public ArticuloValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Descripcion).MaximumLength(500);
            RuleFor(x => x.Precio).GreaterThan(0);
            RuleFor(x => x.IdEstadoArticulo).GreaterThan(0);
            RuleFor(x => x.IdTipoConsumo).GreaterThan(0);

            RuleFor(x => x.EstadoArticulo).Null();
            RuleFor(x => x.TipoConsumo).Null();
            RuleFor(x => x.UrlImagen).MaximumLength(500);
        }
    }
}
