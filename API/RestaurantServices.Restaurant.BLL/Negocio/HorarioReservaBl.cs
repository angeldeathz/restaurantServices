using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> EsReservableAsync(DateTime fechaReserva)
        {
            var diaSemanaReserva = (int) fechaReserva.DayOfWeek;
            var horaReserva = fechaReserva.TimeOfDay;
            var horarios = await ObtenerTodosAsync();

            var diaReserva = horarios.FirstOrDefault(x => x.DiaSemana == diaSemanaReserva);

            return horaReserva >= diaReserva.HoraInicio.TimeOfDay && horaReserva <= diaReserva.HoraFin.TimeOfDay;
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
