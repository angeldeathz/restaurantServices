namespace RestaurantServices.Restaurant.Modelo.TableJoin
{
    public class ProveedorJoin
    {
        public int IdProveedor { get; set; }
        public string DireccionProveedor { get; set; }
        public int IdPersona { get; set; }
        public int Rut { get; set; }
        public string DigitoVerificador { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public char EsPersonaNatural { get; set; }
    }
}
