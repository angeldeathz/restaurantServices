using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;

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
                    pedido_id as idPedido,
                    comentarios
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
                    pedido_id as idPedido,
                    comentarios
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
                {"@p_comentarios", articuloPedido.Comentarios},
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
                {"@p_comentarios", articuloPedido.Comentarios},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> InsertEstadoAsync(ArticuloPedidoEstado estado)
        {
            const string spName =
                @"insert into cambio_estado_articulo_pedido (ESTADO_ARTICULO_PEDIDO_ID, ARTICULO_PEDIDO_ID, fecha)
                  values (:EstadoArticuloPedidoId, :ArticuloPedidoId, :Fecha)";

            return _repository.InsertAsync(spName, new Dictionary<string, object>
            {
                {"@EstadoArticuloPedidoId", estado.IdEstadoArticuloPedido},
                {"@ArticuloPedidoId", estado.IdArticuloPedido},
                {"@Fecha", DateTime.Now}
            });
        }
    }
}
