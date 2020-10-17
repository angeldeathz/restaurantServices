using System;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    public class HorarioReserva
    {
        public int Id { get; set; }
        public int DiaSemana { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
    }
}
