using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class TipoConsumoBl
    {
        private readonly UnitOfWork _unitOfWork;

        public TipoConsumoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<TipoConsumo>> ObtenerTodosAsync()
        {
            return (List<TipoConsumo>)await _unitOfWork.TipoConsumoDal.GetAsync();
        }

        public async Task<TipoConsumo> ObtenerPorIdAsync(int id)
        {
            return await _unitOfWork.TipoConsumoDal.GetAsync(id);
        }
    }
}
