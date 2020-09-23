namespace RestaurantServices.Restaurant.Modelo.Clases
{
    public class Mesa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantidadComensales { get; set; }
        public int IdEstadoMesa { get; set; }
        public EstadoMesa EstadoMesa { get; set; }
    }
}
