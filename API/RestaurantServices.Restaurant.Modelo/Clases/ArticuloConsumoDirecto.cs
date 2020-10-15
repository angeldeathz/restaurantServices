namespace RestaurantServices.Restaurant.Modelo.Clases
{
    public class ArticuloConsumoDirecto
    {
        public int Id { get; set; }
        public int IdInsumo { get; set; }
        public int IdArticulo { get; set; }
        public Insumo Insumo { get; set; }
        public Articulo Articulo { get; set; }
    }
}
