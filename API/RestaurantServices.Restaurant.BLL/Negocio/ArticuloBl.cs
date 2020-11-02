using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class ArticuloBl
    {
        private readonly UnitOfWork _unitOfWork;

        public ArticuloBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<Articulo>> ObtenerTodosAsync()
        {
            var articulosJoin = await _unitOfWork.ArticuloDal.GetAsync();
            return articulosJoin.Select(articulo => new Articulo
            {
                Nombre = articulo.Nombre,
                Descripcion = articulo.Descripcion,
                Precio = articulo.Precio,
                IdTipoConsumo = articulo.IdTipoConsumo,
                IdEstadoArticulo = articulo.IdEstadoArticulo,
                Id = articulo.IdArticulo,
                UrlImagen = articulo.UrlImagen,
                EstadoArticulo = new EstadoArticulo
                {
                    Id = articulo.IdEstadoArticulo,
                    Nombre = articulo.NombreEstadoArticulo
                },
                TipoConsumo = new TipoConsumo
                {
                    Nombre = articulo.NombreTipoConsumo,
                    Id = articulo.IdTipoConsumo
                }
            }).ToList();
        }

        public async Task<Articulo> ObtenerPorIdAsync(int id)
        {
            var articulo = await _unitOfWork.ArticuloDal.GetAsync(id);
            if (articulo == null) return null;
            return new Articulo
            {
                Nombre = articulo.Nombre,
                Descripcion = articulo.Descripcion,
                Precio = articulo.Precio,
                IdTipoConsumo = articulo.IdTipoConsumo,
                IdEstadoArticulo = articulo.IdEstadoArticulo,
                Id = articulo.IdArticulo,
                UrlImagen = articulo.UrlImagen,
                EstadoArticulo = new EstadoArticulo
                {
                    Id = articulo.IdEstadoArticulo,
                    Nombre = articulo.NombreEstadoArticulo
                },
                TipoConsumo = new TipoConsumo
                {
                    Nombre = articulo.NombreTipoConsumo,
                    Id = articulo.IdTipoConsumo
                }
            };
        }

        public Task<int> GuardarAsync(Articulo articulo)
        {
            return _unitOfWork.ArticuloDal.InsertAsync(articulo);
        }

        public Task<int> ModificarAsync(Articulo articulo)
        {
            return _unitOfWork.ArticuloDal.UpdateAsync(articulo);
        }
    }
}
