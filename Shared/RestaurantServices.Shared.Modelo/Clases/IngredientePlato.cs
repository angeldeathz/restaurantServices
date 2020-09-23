namespace RestaurantServices.Shared.Modelo.Clases
{
    public class IngredientePlato
    {
        public int Id { get; set; }
        public int CantidadInsumo { get; set; }
        public int IdInsumo { get; set; }
        public int IdPlato { get; set; }
        public Insumo Insumo { get; set; }
        public Plato Plato { get; set; }
    }
}
