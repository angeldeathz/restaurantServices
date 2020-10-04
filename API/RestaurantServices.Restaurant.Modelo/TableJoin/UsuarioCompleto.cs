namespace RestaurantServices.Restaurant.Modelo.TableJoin
{ 
    public class UsuarioCompleto
    {
        // usuario
        public int IdUsuario { get; set; }
        public string Contrasena { get; set; }

        // persona
        public int IdPersona { get; set; }
        public int Rut { get; set; }
        public string DigitoVerificador { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public char EsPersonaNatural { get; set; }

        // tipo usuario
        public int IdTipoUsuario { get; set; }
        public string NombreTipoUsuario { get; set; }
    }
}
