using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class PlatoDal
    {
        private readonly IRepository _repository;

        public PlatoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Plato>> GetAsync()
        {
            const string query = @"SELECT
                    ID,
                    NOMBRE,
                    MINUTOS_PREPARACION as MinutosPreparacion,
                    ARTICULO_ID as IdArticulo,
                    TIPO_PREPARACION_ID as IdTipoPreparacion
                FROM PLATO";

            return _repository.GetListAsync<Plato>(query);
        }

        public Task<Plato> GetAsync(int id)
        {
            const string query = @"SELECT
                    ID,
                    NOMBRE,
                    MINUTOS_PREPARACION as MinutosPreparacion,
                    ARTICULO_ID as IdArticulo,
                    TIPO_PREPARACION_ID as IdTipoPreparacion
                FROM PLATO
                where id = :id";

            return _repository.GetAsync<Plato>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(Plato pedido)
        {
            const string spName = "sp_insertPlato";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_nombre", pedido.Nombre},
                {"@p_minutos_preparacion", pedido.MinutosPreparacion},
                {"@p_articulo_id", pedido.IdArticulo},
                {"@p_tipo_preparacion_id", pedido.IdTipoPreparacion},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(Plato pedido)
        {
            const string spName = "sp_updatePlato";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", pedido.Id},
                {"@p_nombre", pedido.Nombre},
                {"@p_minutos_preparacion", pedido.MinutosPreparacion},
                {"@p_articulo_id", pedido.IdArticulo},
                {"@p_tipo_preparacion_id", pedido.IdTipoPreparacion},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
