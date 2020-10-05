using System;

namespace RestaurantServices.Restaurant.Modelo.TableJoin
{
    public class ReservaJoin
    {
        public int IdReserva { get; set; }
        public DateTime FechaReserva { get; set; }
        public int CantidadComensalesReserva { get; set; }
        public int IdCliente { get; set; }
        public int IdMesa { get; set; }
        public string NombreMesa { get; set; }
        public int CantidadComensalesMesa { get; set; }
        public int IdEstadoMesa { get; set; }
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