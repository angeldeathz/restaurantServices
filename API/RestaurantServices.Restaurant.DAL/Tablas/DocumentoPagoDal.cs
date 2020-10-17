using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class DocumentoPagoDal
    {
        private readonly IRepository _repository;

        public DocumentoPagoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<DocumentoPago>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    fecha_hora as FechaHora,
                    total,
                    tipo_documento_pago_id as idTipoDocumentoPago,
                    pedido_id as idPedido
                from documento_pago";

            return _repository.GetListAsync<DocumentoPago>(query);
        }

        public Task<DocumentoPago> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    fecha_hora as FechaHora,
                    total,
                    tipo_documento_pago_id as idTipoDocumentoPago,
                    pedido_id as idPedido
                from documento_pago
                where id = :id";

            return _repository.GetAsync<DocumentoPago>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(DocumentoPago documentoPago)
        {
            const string spName = "sp_insertDocumentoPago";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_fecha_hora", documentoPago.FechaPago},
                {"@p_total", documentoPago.Total},
                {"@p_tipo_documento_pago_id", documentoPago.IdTipoDocumentoPago},
                {"@p_pedido_id", documentoPago.IdPedido},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(DocumentoPago documentoPago)
        {
            const string spName = "sp_updateDocumentoPago";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", documentoPago.Id},
                {"@p_fecha_hora", documentoPago.FechaPago},
                {"@p_total", documentoPago.Total},
                {"@p_tipo_documento_pago_id", documentoPago.IdTipoDocumentoPago},
                {"@p_pedido_id", documentoPago.IdPedido},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
