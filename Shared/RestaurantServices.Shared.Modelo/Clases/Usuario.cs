namespace RestaurantServices.Shared.Modelo.Clases
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Contrasena { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoPersona { get; set; }
        public Persona Persona { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
