using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class EstadoReservaDal
    {
        private readonly IRepository _repository;

        public EstadoReservaDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<EstadoReserva>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_reserva";

            return _repository.GetListAsync<EstadoReserva>(query);
        }

        public Task<EstadoReserva> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_reserva
                where id = :id";

            return _repository.GetAsync<EstadoReserva>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<IEnumerable<EstadoReserva>> GetByReservaAsync(int idReserva)
        {
            const string query = @"SELECT
                    e.id,
                    e.nombre,
                    c.fecha
                from estado_reserva e
                join cambio_estado_reserva c on e.id = c.estado_reserva_id
                where c.reserva_id = :idReserva order by c.fecha asc";

            return _repository.GetListAsync<EstadoReserva>(query, new Dictionary<string, object>
            {
                {"@idReserva", idReserva}
            });
        }

        public Task<int> InsertAsync(EstadoReserva estadoPedido)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@NOMBRE", estadoPedido.Nombre},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(EstadoReserva estadoPedido)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@id", estadoPedido.Id},
                {"@NOMBRE", estadoPedido.Nombre},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
