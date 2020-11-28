using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class ArticuloPedidoBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PedidoBl _pedidoBl;
        private readonly ArticuloBl _articuloBl;

        public ArticuloPedidoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _pedidoBl = new PedidoBl();
            _articuloBl = new ArticuloBl();
        }

        public async Task<List<ArticuloPedido>> ObtenerTodosAsync()
        {
            var articuloPedidos = await _unitOfWork.ArticuloPedidoDal.GetAsync();

            foreach (var x in articuloPedidos)
            {
                x.Pedido = await _pedidoBl.ObtenerPorIdAsync(x.IdPedido);
                x.Articulo = await _articuloBl.ObtenerPorIdAsync(x.IdArticulo);
                var estados = await _unitOfWork.EstadoArticuloPedidoDal.GetByArticuloPedido(x.Id);
                x.EstadosArticuloPedido = (List<EstadoArticuloPedido>)estados;
            }

            return (List<ArticuloPedido>)articuloPedidos;
        }

        public async Task<List<ArticuloPedido>> ObtenerPorIdPedidoAsync(int idPedido)
        {
            var articuloPedidos = await _unitOfWork.ArticuloPedidoDal.GetByPedidoAsync(idPedido);

            foreach (var x in articuloPedidos)
            {
                x.Pedido = await _pedidoBl.ObtenerPorIdAsync(x.IdPedido);
                x.Articulo = await _articuloBl.ObtenerPorIdAsync(x.IdArticulo);
                var estados = await _unitOfWork.EstadoArticuloPedidoDal.GetByArticuloPedido(x.Id);
                x.EstadosArticuloPedido = (List<EstadoArticuloPedido>)estados;
            }

            return (List<ArticuloPedido>)articuloPedidos;
        }

        public async Task<ArticuloPedido> ObtenerPorIdAsync(int id)
        {
            var articuloPedido = await _unitOfWork.ArticuloPedidoDal.GetAsync(id);
            if (articuloPedido == null) return null;
            articuloPedido.Pedido = await _pedidoBl.ObtenerPorIdAsync(articuloPedido.IdPedido);
            articuloPedido.Articulo = await _articuloBl.ObtenerPorIdAsync(articuloPedido.IdArticulo);

            var estados = await _unitOfWork.EstadoArticuloPedidoDal.GetByArticuloPedido(articuloPedido.Id);
            articuloPedido.EstadosArticuloPedido = (List<EstadoArticuloPedido>)estados;

            return articuloPedido;
        }

        public Task<int> GuardarAsync(ArticuloPedido articuloPedido)
        {
            return _unitOfWork.ArticuloPedidoDal.InsertAsync(articuloPedido);
        }

        public Task<int> ModificarAsync(ArticuloPedido articuloPedido)
        {
            return _unitOfWork.ArticuloPedidoDal.UpdateAsync(articuloPedido);
        }

        public Task<int> AgregarEstadoAsync(ArticuloPedidoEstado estado)
        {
            return _unitOfWork.ArticuloPedidoDal.InsertEstadoAsync(estado);
        }
    }
}
