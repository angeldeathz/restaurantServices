using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.TableJoin;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class PedidoDal
    {
        private readonly IRepository _repository;

        public PedidoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<PedidoJoin>> GetAsync()
        {
            const string query = @"SELECT
                p.id as idPedido,
                p.fecha_hora_inicio as fechaInicioPedido,
                p.fecha_hora_fin as fechaFinPedido,
                p.total,
                p.mesa_id as idMesa,
                p.estado_pedido_id as idEstadoPedido,
                m.nombre as nombreMesa,
                m.cantidad_comensales as CantidadComensales,
                m.estado_mesa_id as idEstadoMesa,
                ep.nombre as nombreEstadoPedido
            FROM PEDIDO p
            JOIN mesa m on p.mesa_id = m.id
            JOIN estado_pedido ep on p.estado_pedido_id = ep.id";

            return _repository.GetListAsync<PedidoJoin>(query);
        }

        public Task<PedidoJoin> GetAsync(int id)
        {
            const string query = @"SELECT
                    p.id as idPedido,
                    p.fecha_hora_inicio as fechaInicioPedido,
                    p.fecha_hora_fin as fechaFinPedido,
                    p.total,
                    p.mesa_id as idMesa,
                    p.estado_pedido_id as idEstadoPedido,
                    m.nombre as nombreMesa,
                    m.cantidad_comensales as CantidadComensales,
                    m.estado_mesa_id as idEstadoMesa,
                    ep.nombre as nombreEstadoPedido
                FROM PEDIDO p
                JOIN mesa m on p.mesa_id = m.id
                JOIN estado_pedido ep on p.estado_pedido_id = ep.id
                where p.id = :id";

            return _repository.GetAsync<PedidoJoin>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(Pedido pedido)
        {
            const string spName = "sp_insertPedido";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_fecha_hora_inicio", pedido.FechaInicio},
                {"@p_fecha_hora_fin", pedido.FechaTermino},
                {"@p_total", pedido.Total},
                {"@p_mesa_id", pedido.IdMesa},
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
                {"@p_fecha_hora_inicio", pedido.FechaInicio},
                {"@p_fecha_hora_fin", pedido.FechaTermino},
                {"@p_total", pedido.Total},
                {"@p_mesa_id", pedido.IdMesa},
                {"@p_estado_pedido_id", pedido.IdEstadoPedido},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
