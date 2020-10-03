using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class EstadoPedidoDal
    {
        private readonly IRepository _repository;

        public EstadoPedidoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<EstadoPedido>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_pedido";

            return _repository.GetListAsync<EstadoPedido>(query);
        }

        public Task<EstadoPedido> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_pedido
                where id = :id";

            return _repository.GetAsync<EstadoPedido>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(EstadoPedido estadoPedido)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@NOMBRE", estadoPedido.Nombre},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<bool> UpdateAsync(EstadoPedido estadoPedido)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<bool>(spName, new Dictionary<string, object>
            {
                {"@id", estadoPedido.Id},
                {"@NOMBRE", estadoPedido.Nombre},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
