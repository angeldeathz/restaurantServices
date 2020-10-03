using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class EstadoArticuloDal
    {
        private readonly IRepository _repository;

        public EstadoArticuloDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<EstadoArticulo>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_articulo";

            return _repository.GetListAsync<EstadoArticulo>(query);
        }

        public Task<EstadoArticulo> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_articulo
                where id = :id";

            return _repository.GetAsync<EstadoArticulo>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(EstadoArticulo estadoArticulo)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@NOMBRE", estadoArticulo.Nombre},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(EstadoArticulo estadoArticulo)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@id", estadoArticulo.Id},
                {"@NOMBRE", estadoArticulo.Nombre},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
