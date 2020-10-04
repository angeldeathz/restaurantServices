using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class EstadoPedidoBl
    {
        private readonly UnitOfWork _unitOfWork;

        public EstadoPedidoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<EstadoPedido>> ObtenerTodosAsync()
        {
            return (List<EstadoPedido>)await _unitOfWork.EstadoPedidoDal.GetAsync();
        }

        public Task<EstadoPedido> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.EstadoPedidoDal.GetAsync(id);
        }

        public Task<int> GuardarAsync(EstadoPedido estadoPedido)
        {
            return _unitOfWork.EstadoPedidoDal.InsertAsync(estadoPedido);
        }

        public Task<int> ModificarAsync(EstadoPedido estadoPedido)
        {
            return _unitOfWork.EstadoPedidoDal.UpdateAsync(estadoPedido);
        }
    }
}
