namespace RestaurantServices.Shared.Modelo.Clases
{
    public class Plato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal TiempoPreparacion { get; set; }
        public int IdArticulo { get; set; }
        public int IdTipoPreparacion { get; set; }
        public Articulo Articulo { get; set; }
        public TipoPreparacion TipoPreparacion { get; set; }
    }
}
