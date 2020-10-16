using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(IngredientePlatoValidator))]
    public class IngredientePlato
    {
        public int Id { get; set; }
        public double CantidadInsumo { get; set; }
        public int IdInsumo { get; set; }
        public int IdPlato { get; set; }
        public Insumo Insumo { get; set; }
        public Plato Plato { get; set; }
    }
}
