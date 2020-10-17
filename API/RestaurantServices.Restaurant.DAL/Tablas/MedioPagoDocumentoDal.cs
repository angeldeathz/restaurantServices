using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class MedioPagoDocumentoDal
    {
        private readonly IRepository _repository;

        public MedioPagoDocumentoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<MedioPagoDocumento>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    monto,
                    medio_pago_id as idMedioPago,
                    documento_pago_id as idDocumentoPago
                from medio_pago_documento";

            return _repository.GetListAsync<MedioPagoDocumento>(query);
        }

        public Task<MedioPagoDocumento> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    monto,
                    medio_pago_id as idMedioPago,
                    documento_pago_id as idDocumentoPago
                from medio_pago_documento
                where id = :id";

            return _repository.GetAsync<MedioPagoDocumento>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(MedioPagoDocumento medioPagoDocumento)
        {
            const string spName = "sp_insertMedioPagoDocumento";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_monto", medioPagoDocumento.Monto},
                {"@p_medio_pago_id", medioPagoDocumento.IdMedioPago},
                {"@p_documento_pago_id", medioPagoDocumento.IdDocumentoPago},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(MedioPagoDocumento medioPagoDocumento)
        {
            const string spName = "sp_updateMedioPagoDocumento";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", medioPagoDocumento.Id},
                {"@p_monto", medioPagoDocumento.Monto},
                {"@p_medio_pago_id", medioPagoDocumento.IdMedioPago},
                {"@p_documento_pago_id", medioPagoDocumento.IdDocumentoPago},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
