using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class HorarioReservaDal
    {
        private readonly IRepository _repository;

        public HorarioReservaDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<HorarioReserva>> GetAsync()
        {
            const string query = @"SELECT
                        ID,
                        DIA_SEMANA as diaSemana,
                        HORA_INICIO as horaInicio,
                        HORA_FIN as horaFin
                    from horarios_reserva";

            return _repository.GetListAsync<HorarioReserva>(query);
        }

        public Task<HorarioReserva> GetAsync(int id)
        {
            const string query = @"SELECT
                        ID,
                        DIA_SEMANA as diaSemana,
                        HORA_INICIO as horaInicio,
                        HORA_FIN as horaFin
                    from horarios_reserva
                    where id = :id";

            return _repository.GetAsync<HorarioReserva>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(HorarioReserva horarioReserva)
        {
            const string spName = "sp_insertHorarioReserva";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_dia_semana", horarioReserva.DiaSemana},
                {"@p_hora_inicio", horarioReserva.HoraInicio},
                {"@p_hora_fin", horarioReserva.HoraFin},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(HorarioReserva horarioReserva)
        {
            const string spName = "sp_updateHorarioReserva";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", horarioReserva.Id},
                {"@p_dia_semana", horarioReserva.DiaSemana},
                {"@p_hora_inicio", horarioReserva.HoraInicio},
                {"@p_hora_fin", horarioReserva.HoraFin},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
