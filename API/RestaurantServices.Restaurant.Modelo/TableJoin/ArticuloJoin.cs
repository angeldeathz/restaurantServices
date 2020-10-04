namespace RestaurantServices.Restaurant.Modelo.TableJoin
{
    public class ArticuloJoin
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int IdEstadoArticulo { get; set; }
        public string NombreEstadoArticulo { get; set; }
        public int IdTipoConsumo { get; set; }
        public string NombreTipoConsumo { get; set; }
    }
}
