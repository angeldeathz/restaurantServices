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

        public async Task<Articulo> ObtenerPorIdAsync(int id)
        {
            return await _unitOfWork.ArticuloDal.GetAsync(id);
        }

        public async Task<int> GuardarAsync(Articulo articulo)
        {
            return await _unitOfWork.ArticuloDal.InsertAsync(articulo);
        }

        public async Task<bool> ModificarAsync(Articulo articulo)
        {
            return await _unitOfWork.ArticuloDal.UpdateAsync(articulo);
        }
    }
}
