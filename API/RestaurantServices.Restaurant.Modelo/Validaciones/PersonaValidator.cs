using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class PersonaValidator : AbstractValidator<Persona>
    {
        public PersonaValidator()
        {
            RuleFor(x => x.Rut).GreaterThan(0);
            RuleFor(x => x.DigitoVerificador).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Nombre).NotNull().NotEmpty();
            RuleFor(x => x.Apellido).NotNull().NotEmpty();
        }
    }
}
