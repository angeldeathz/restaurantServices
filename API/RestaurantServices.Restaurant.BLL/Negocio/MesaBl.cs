using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class MesaBl
    {
        private readonly UnitOfWork _unitOfWork;

        public MesaBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<Mesa>> ObtenerTodosAsync()
        {
            var mesaJoin = await _unitOfWork.MesaDal.GetAsync();
            return mesaJoin.Select(mesa => new Mesa
            {
                Nombre = mesa.NombreMesa,
                CantidadComensales = mesa.CantidadComensales,
                Id = mesa.IdMesa,
                IdEstadoMesa = mesa.IdEstadoMesa,
                EstadoMesa = new EstadoMesa
                {
                    Nombre = mesa.NombreEstado,
                    Id = mesa.IdEstadoMesa
                }
            }).ToList();
        }

        public async Task<Mesa> ObtenerPorIdAsync(int id)
        {
            var mesa = await _unitOfWork.MesaDal.GetAsync(id);
            if (mesa == null) return null;

            return new Mesa
            {
                Nombre = mesa.NombreMesa,
                CantidadComensales = mesa.CantidadComensales,
                Id = mesa.IdMesa,
                IdEstadoMesa = mesa.IdEstadoMesa,
                EstadoMesa = new EstadoMesa
                {
                    Nombre = mesa.NombreEstado,
                    Id = mesa.IdEstadoMesa
                }
            };
        }

        public Task<int> GuardarAsync(Mesa mesa)
        {
            return _unitOfWork.MesaDal.InsertAsync(mesa);
        }

        public Task<int> ModificarAsync(Mesa mesa)
        {
            return _unitOfWork.MesaDal.UpdateAsync(mesa);
        }
    }
}
