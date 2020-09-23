using FluentValidation;
using RestaurantServices.Shared.Modelo.Clases;

namespace RestaurantServices.Shared.Modelo.Validaciones
{
    public class PersonaValidator : AbstractValidator<Persona>
    {
        public PersonaValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty();
            RuleFor(x => x.Apellido).NotNull().NotEmpty();
        }
    }
}
