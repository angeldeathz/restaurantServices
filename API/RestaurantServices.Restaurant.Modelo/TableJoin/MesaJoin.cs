namespace RestaurantServices.Restaurant.Modelo.TableJoin
{
    public class MesaJoin
    {
        public int IdMesa { get; set; }
        public string NombreMesa { get; set; }
        public int CantidadComensales { get; set; }
        public int IdEstadoMesa { get; set; }
        public string NombreEstado { get; set; }
    }
}
