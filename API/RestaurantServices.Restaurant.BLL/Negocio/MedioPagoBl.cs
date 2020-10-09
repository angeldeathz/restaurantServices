using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class MedioPagoBl
    {
        private readonly UnitOfWork _unitOfWork;

        public MedioPagoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<MedioPago>> ObtenerTodosAsync()
        {
            return (List<MedioPago>)await _unitOfWork.MedioPagoDal.GetAsync();
        }

        public Task<MedioPago> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.MedioPagoDal.GetAsync(id);
        }
    }
}
