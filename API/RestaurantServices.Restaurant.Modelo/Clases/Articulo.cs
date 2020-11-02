using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(ArticuloValidator))]
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int IdEstadoArticulo { get; set; }
        public int IdTipoConsumo { get; set; }
        public string UrlImagen { get; set; }
        public EstadoArticulo EstadoArticulo { get; set; }
        public TipoConsumo TipoConsumo { get; set; }
    }
}
