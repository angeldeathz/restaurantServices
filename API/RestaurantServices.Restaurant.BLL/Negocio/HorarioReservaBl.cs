using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class HorarioReservaBl
    {
        private readonly UnitOfWork _unitOfWork;

        public HorarioReservaBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<HorarioReserva>> ObtenerTodosAsync()
        {
            return (List<HorarioReserva>)await _unitOfWork.HorarioReservaDal.GetAsync();
        }

        public Task<HorarioReserva> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.HorarioReservaDal.GetAsync(id);
        }

        public Task<int> GuardarAsync(HorarioReserva horarioReserva)
        {
            return _unitOfWork.HorarioReservaDal.InsertAsync(horarioReserva);
        }

        public Task<int> ModificarAsync(HorarioReserva horarioReserva)
        {
            return _unitOfWork.HorarioReservaDal.UpdateAsync(horarioReserva);
        }
    }
}
