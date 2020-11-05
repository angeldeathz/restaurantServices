using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class PedidoBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ReservaBl _reservaBl;

        public PedidoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _reservaBl = new ReservaBl();
        }

        public async Task<List<Pedido>> ObtenerTodosAsync()
        {
            var pedidos = await _unitOfWork.PedidoDal.GetAsync();

            foreach (var x in pedidos)
            {
                x.Reserva = await _reservaBl.ObtenerPorIdAsync(x.IdReserva);
                x.EstadoPedido = await _unitOfWork.EstadoPedidoDal.GetAsync(x.IdEstadoPedido);
            }

            return (List<Pedido>)pedidos;
        }

        public async Task<Pedido> ObtenerPorIdAsync(int id)
        {
            var pedido = await _unitOfWork.PedidoDal.GetAsync(id);
            if (pedido == null) return null;

            pedido.Reserva = await _reservaBl.ObtenerPorIdAsync(pedido.IdReserva);
            pedido.EstadoPedido = await _unitOfWork.EstadoPedidoDal.GetAsync(pedido.IdEstadoPedido);
            return pedido;
        }

        public Task<int> GuardarAsync(Pedido pedido)
        {
            return _unitOfWork.PedidoDal.InsertAsync(pedido);
        }

        public Task<int> ModificarAsync(Pedido pedido)
        {
            return _unitOfWork.PedidoDal.UpdateAsync(pedido);
        }
    }
}
