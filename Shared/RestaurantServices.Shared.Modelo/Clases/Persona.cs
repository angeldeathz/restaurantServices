namespace RestaurantServices.Shared.Modelo.Clases
{
    public class Persona
    {
        public int Id { get; set; }
        public int Rut { get; set; }
        public string DigitoVerificador { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool EsPersonaNatural { get; set; }
    }
}
