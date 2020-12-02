using FluentValidation;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;

namespace RestaurantServices.Restaurant.Modelo.Validaciones
{
    public class UsuarioLoginValidator : AbstractValidator<UsuarioLogin>
    {
        public UsuarioLoginValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Contrasena).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Rut)
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
