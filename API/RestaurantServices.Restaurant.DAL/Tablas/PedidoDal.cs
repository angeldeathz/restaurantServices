using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class PedidoDal
    {
        private readonly IRepository _repository;

        public PedidoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Pedido>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    fecha_hora_inicio as FechaHoraInicio,
                    fecha_hora_fin as FechaHoraFin,
                    total,
                    reserva_id as IdReserva,
                    estado_pedido_id as IdEstadoPedido
                from pedido";

            return _repository.GetListAsync<Pedido>(query);
        }

        public Task<Pedido> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    fecha_hora_inicio as FechaHoraInicio,
                    fecha_hora_fin as FechaHoraFin,
                    total,
                    reserva_id as IdReserva,
                    estado_pedido_id as IdEstadoPedido
                from pedido
                where id = :id";

            return _repository.GetAsync<Pedido>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(Pedido pedido)
        {
            const string spName = "sp_insertPedido";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_fecha_hora_inicio", pedido.FechaHoraInicio},
                {"@p_fecha_hora_fin", pedido.FechaHoraFin},
                {"@p_total", pedido.Total},
                {"@p_reserva_id", pedido.IdReserva},
                {"@p_estado_pedido_id", pedido.IdEstadoPedido},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(Pedido pedido)
        {
            const string spName = "sp_updatePedido";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", pedido.Id},
                {"@p_fecha_hora_inicio", pedido.FechaHoraInicio},
                {"@p_fecha_hora_fin", pedido.FechaHoraFin},
                {"@p_total", pedido.Total},
                {"@p_reserva_id", pedido.IdReserva},
                {"@p_estado_pedido_id", pedido.IdEstadoPedido},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
