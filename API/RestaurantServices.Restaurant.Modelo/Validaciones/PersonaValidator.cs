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
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Nombre).NotNull().NotEmpty();
            RuleFor(x => x.Apellido).NotNull().NotEmpty();
            RuleFor(x => x.ObtenerRutCompleto())
                .NotNull()
                .NotEmpty()
                .Must(ValidaRut).WithMessage("{PropertyName} es inválido");
        }

        private bool ValidaRut(string rut)
        {
            var personaHelper = new Persona();
            return personaHelper.ValidaRut(rut);
        }
    }
}
