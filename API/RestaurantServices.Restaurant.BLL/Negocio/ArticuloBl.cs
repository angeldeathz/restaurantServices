using System.Collections.Generic;
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
            return (List<Articulo>)await _unitOfWork.ArticuloDal.GetAsync();
        }

        public Task<Articulo> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.ArticuloDal.GetAsync(id);
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
