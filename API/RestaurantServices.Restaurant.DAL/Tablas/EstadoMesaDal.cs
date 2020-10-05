using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class EstadoMesaDal
    {
        private readonly IRepository _repository;

        public EstadoMesaDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<EstadoMesa>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_mesa";

            return _repository.GetListAsync<EstadoMesa>(query);
        }

        public Task<EstadoMesa> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_mesa
                where id = :id";

            return _repository.GetAsync<EstadoMesa>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(EstadoMesa estadoPedido)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_nombre", estadoPedido.Nombre},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(EstadoMesa estadoPedido)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", estadoPedido.Id},
                {"@p_nombre", estadoPedido.Nombre},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
