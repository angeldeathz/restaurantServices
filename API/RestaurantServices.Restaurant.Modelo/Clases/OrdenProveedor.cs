using System;
using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(OrdenProveedorValidator))]
    public class OrdenProveedor
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int Total { get; set; }
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstadoOrden { get; set; }
        public Proveedor Proveedor { get; set; }
        public Usuario Usuario { get; set; }
    }
}
