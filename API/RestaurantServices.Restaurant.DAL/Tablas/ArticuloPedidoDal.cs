using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class ArticuloPedidoDal
    {
        private readonly IRepository _repository;

        public ArticuloPedidoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ArticuloPedido>> GetAsync()
        {
            const string query = @"select
                    id,
                    precio,
                    cantidad,
                    total,
                    articulo_id as idArticulo,
                    pedido_id as idPedido
                from articulo_pedido";

            return _repository.GetListAsync<ArticuloPedido>(query);
        }

        public Task<ArticuloPedido> GetAsync(int id)
        {
            const string query = @"select
                    id,
                    precio,
                    cantidad,
                    total,
                    articulo_id as idArticulo,
                    pedido_id as idPedido
                from articulo_pedido
                where id = :id";

            return _repository.GetAsync<ArticuloPedido>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(ArticuloPedido articuloPedido)
        {
            const string spName = "sp_insertArticuloPedido";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_precio", articuloPedido.Precio},
                {"@p_cantidad", articuloPedido.Cantidad},
                {"@p_total", articuloPedido.Total},
                {"@p_articulo_id", articuloPedido.IdArticulo},
                {"@p_pedidoId", articuloPedido.IdPedido},
                {"@p_estado_articulo_pedido_id", articuloPedido.IdEstadoArticuloPedido},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(ArticuloPedido articuloPedido)
        {
            const string spName = "sp_updateArticuloPedido";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", articuloPedido.Id},
                {"@p_precio", articuloPedido.Precio},
                {"@p_cantidad", articuloPedido.Cantidad},
                {"@p_total", articuloPedido.Total},
                {"@p_articuloId", articuloPedido.IdArticulo},
                {"@p_pedidoId", articuloPedido.IdPedido},
                {"@p_estado_articulo_pedido_id", articuloPedido.IdEstadoArticuloPedido},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
