using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class UnidadMedidaBl
    {
        private readonly UnitOfWork _unitOfWork;

        public UnidadMedidaBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<UnidadMedida>> ObtenerTodosAsync()
        {
            return (List<UnidadMedida>)await _unitOfWork.UnidadMedidaDal.GetAsync();
        }

        public async Task<UnidadMedida> ObtenerPorIdAsync(int id)
        {
            return await _unitOfWork.UnidadMedidaDal.GetAsync(id);
        }
    }
}
