using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class EstadoArticuloPedidoBl
    {
        private readonly UnitOfWork _unitOfWork;

        public EstadoArticuloPedidoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<EstadoArticuloPedido>> ObtenerTodosAsync()
        {
            return (List<EstadoArticuloPedido>)await _unitOfWork.EstadoArticuloPedidoDal.GetAsync();
        }

        public Task<EstadoArticuloPedido> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.EstadoArticuloPedidoDal.GetAsync(id);
        }
    }
}
