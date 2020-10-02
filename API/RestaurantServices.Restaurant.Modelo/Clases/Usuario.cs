using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(UsuarioValidator))]
    public class Usuario
    {
        public int Id { get; set; }
        public string Contrasena { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoUsuario { get; set; }
        public Persona Persona { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
