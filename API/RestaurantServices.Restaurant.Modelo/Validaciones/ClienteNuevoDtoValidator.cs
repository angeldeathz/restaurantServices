using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Dto;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class ClienteNuevoDtoValidator : AbstractValidator<ClienteNuevoDto>
    {
        public ClienteNuevoDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(100).EmailAddress();
        }
    }
}
