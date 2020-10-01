using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class EstadoArticuloBl
    {
        private readonly UnitOfWork _unitOfWork;

        public EstadoArticuloBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<EstadoArticulo>> ObtenerTodosAsync()
        {
            return (List<EstadoArticulo>)await _unitOfWork.EstadoArticuloDal.GetAsync();
        }

        public Task<EstadoArticulo> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.EstadoArticuloDal.GetAsync(id);
        }
    }
}
