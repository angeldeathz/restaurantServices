using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class PedidoBl
    {
        private readonly UnitOfWork _unitOfWork;

        public PedidoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<Pedido>> ObtenerTodosAsync()
        {
            var pedidos = await _unitOfWork.PedidoDal.GetAsync();
            return pedidos.Select(pedido => new Pedido
            {
                IdMesa = pedido.IdMesa,
                FechaHoraFin = pedido.FechaFinPedido,
                FechaHoraInicio = pedido.FechaInicioPedido,
                IdEstadoPedido = pedido.IdEstadoPedido,
                Total = pedido.Total,
                Id = pedido.IdPedido,
                Mesa = new Mesa
                {
                    CantidadComensales = pedido.CantidadComensales,
                    IdEstadoMesa = pedido.IdEstadoMesa,
                    Id = pedido.IdMesa,
                    Nombre = pedido.NombreMesa
                },
                EstadoPedido = new EstadoPedido
                {
                    Nombre = pedido.NombreEstadoPedido,
                    Id = pedido.IdEstadoPedido
                }
            }).ToList();
        }

        public async Task<Pedido> ObtenerPorIdAsync(int id)
        {
            var pedido = await _unitOfWork.PedidoDal.GetAsync(id);
            if (pedido == null) return null;
            return new Pedido
            {
                IdMesa = pedido.IdMesa,
                FechaHoraFin = pedido.FechaFinPedido,
                FechaHoraInicio = pedido.FechaInicioPedido,
                IdEstadoPedido = pedido.IdEstadoPedido,
                Total = pedido.Total,
                Id = pedido.IdPedido,
                Mesa = new Mesa
                {
                    CantidadComensales = pedido.CantidadComensales,
                    IdEstadoMesa = pedido.IdEstadoMesa,
                    Id = pedido.IdMesa,
                    Nombre = pedido.NombreMesa
                },
                EstadoPedido = new EstadoPedido
                {
                    Nombre = pedido.NombreEstadoPedido,
                    Id = pedido.IdEstadoPedido
                }
            };
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
