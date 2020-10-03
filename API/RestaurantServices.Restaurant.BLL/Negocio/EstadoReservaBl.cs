using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class EstadoReservaBl
    {
        private readonly UnitOfWork _unitOfWork;

        public EstadoReservaBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<EstadoReserva>> ObtenerTodosAsync()
        {
            return (List<EstadoReserva>)await _unitOfWork.EstadoReservaDal.GetAsync();
        }

        public Task<EstadoReserva> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.EstadoReservaDal.GetAsync(id);
        }

        public Task<int> GuardarAsync(EstadoReserva estadoReserva)
        {
            return _unitOfWork.EstadoReservaDal.InsertAsync(estadoReserva);
        }

        public Task<bool> ModificarAsync(EstadoReserva estadoReserva)
        {
            return _unitOfWork.EstadoReservaDal.UpdateAsync(estadoReserva);
        }
    }
}
