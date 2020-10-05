using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class ArticuloPedidoBl
    {
        private readonly UnitOfWork _unitOfWork;

        public ArticuloPedidoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<ArticuloPedido>> ObtenerTodosAsync()
        {
            return (List<ArticuloPedido>)await _unitOfWork.ArticuloPedidoDal.GetAsync();
        }

        public Task<ArticuloPedido> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.ArticuloPedidoDal.GetAsync(id);
        }

        public Task<int> GuardarAsync(ArticuloPedido articuloPedido)
        {
            return _unitOfWork.ArticuloPedidoDal.InsertAsync(articuloPedido);
        }

        public Task<int> ModificarAsync(ArticuloPedido articuloPedido)
        {
            return _unitOfWork.ArticuloPedidoDal.UpdateAsync(articuloPedido);
        }
    }
}
