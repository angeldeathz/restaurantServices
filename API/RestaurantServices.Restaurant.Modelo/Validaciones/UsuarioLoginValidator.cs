using System.Security.AccessControl;
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
            RuleFor(x => x.Contrasena).NotNull().NotEmpty();
            RuleFor(x => x.Rut)
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
