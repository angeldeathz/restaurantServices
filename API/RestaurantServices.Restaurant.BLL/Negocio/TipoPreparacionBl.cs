using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class TipoPreparacionBl
    {
        private readonly UnitOfWork _unitOfWork;

        public TipoPreparacionBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<TipoPreparacion>> ObtenerTodosAsync()
        {
            return (List<TipoPreparacion>)await _unitOfWork.TipoPreparacionDal.GetAsync();
        }

        public Task<TipoPreparacion> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.TipoPreparacionDal.GetAsync(id);
        }
    }
}
