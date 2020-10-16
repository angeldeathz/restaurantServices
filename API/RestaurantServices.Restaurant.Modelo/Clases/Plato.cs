using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(PlatoValidator))]
    public class Plato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal MinutosPreparacion { get; set; }
        public int IdArticulo { get; set; }
        public int IdTipoPreparacion { get; set; }
        public Articulo Articulo { get; set; }
        public TipoPreparacion TipoPreparacion { get; set; }
    }
}
