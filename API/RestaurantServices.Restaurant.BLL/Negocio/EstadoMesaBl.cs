using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class EstadoMesaBl
    {
        private readonly UnitOfWork _unitOfWork;

        public EstadoMesaBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<EstadoMesa>> ObtenerTodosAsync()
        {
            return (List<EstadoMesa>)await _unitOfWork.EstadoMesaDal.GetAsync();
        }

        public Task<EstadoMesa> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.EstadoMesaDal.GetAsync(id);
        }

        public Task<int> GuardarAsync(EstadoMesa estadoMesa)
        {
            return _unitOfWork.EstadoMesaDal.InsertAsync(estadoMesa);
        }

        public Task<int> ModificarAsync(EstadoMesa estadoMesa)
        {
            return _unitOfWork.EstadoMesaDal.UpdateAsync(estadoMesa);
        }
    }
}
