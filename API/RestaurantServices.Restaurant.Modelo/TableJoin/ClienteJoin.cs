using System;

namespace RestaurantServices.Restaurant.Modelo.TableJoin
{
    public class ClienteJoin
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdPersona { get; set; }
        public int Rut { get; set; }
        public string DigitoVerificador { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public char EsPersonaNatural { get; set; }
    }
}
