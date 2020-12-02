using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class ArticuloPedidoValidator : AbstractValidator<ArticuloPedido>
    {
        public ArticuloPedidoValidator()
        {
            RuleFor(x => x.Precio).GreaterThan(0).LessThan(99999999);
            RuleFor(x => x.Cantidad).GreaterThan(0).LessThan(99999999);
            RuleFor(x => x.Total).GreaterThan(0).LessThan(99999999);
            RuleFor(x => x.IdPedido).GreaterThan(0).LessThan(99999999);
            RuleFor(x => x.IdArticulo).GreaterThan(0).LessThan(99999999);
        }
    }
}
