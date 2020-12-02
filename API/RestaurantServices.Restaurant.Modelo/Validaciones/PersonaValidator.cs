using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class PersonaValidator : AbstractValidator<Persona>
    {
        public PersonaValidator()
        {
            RuleFor(x => x.Rut).GreaterThan(0);
            RuleFor(x => x.DigitoVerificador).NotNull().NotEmpty().MaximumLength(1);
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.Nombre).MaximumLength(150);
            RuleFor(x => x.Apellido).MaximumLength(150);
            RuleFor(x => x.ObtenerRutCompleto())
                .NotNull()
                .NotEmpty()
                .Must(ValidaRut).WithMessage("Rut es inválido");
        }

        private bool ValidaRut(string rut)
        {
            var personaHelper = new Persona();
            return personaHelper.ValidaRut(rut);
        }
    }
}
