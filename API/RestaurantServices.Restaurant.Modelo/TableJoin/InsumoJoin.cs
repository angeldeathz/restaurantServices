namespace RestaurantServices.Restaurant.Modelo.TableJoin
{
    public class InsumoJoin
    {
        public int IdInsumo { get; set; }
        public string NombreInsumo { get; set; }
        public int StockActual { get; set; }
        public int StockOptimo { get; set; }
        public int StockCritico { get; set; }
        public int IdProveedor { get; set; }
        public int IdUnidadDeMedida { get; set; }
        public string DireccionProveedor { get; set; }
        public int IdPersona { get; set; }
    }
}
