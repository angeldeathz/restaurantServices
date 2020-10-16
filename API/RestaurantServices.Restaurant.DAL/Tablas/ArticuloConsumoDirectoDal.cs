using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class ArticuloConsumoDirectoDal
    {
        private readonly IRepository _repository;

        public ArticuloConsumoDirectoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ArticuloConsumoDirecto>> GetAsync()
        {
            const string query = @"SELECT
                    ID,
                    insumo_id as idInsumo,
                    articulo_id as idArticulo
                FROM articulo_consumo_directo";

            return _repository.GetListAsync<ArticuloConsumoDirecto>(query);
        }

        public Task<ArticuloConsumoDirecto> GetAsync(int id)
        {
            const string query = @"SELECT
                    ID,
                    insumo_id as idInsumo,
                    articulo_id as idArticulo
                FROM articulo_consumo_directo
                where id = :id";

            return _repository.GetAsync<ArticuloConsumoDirecto>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(ArticuloConsumoDirecto articuloConsumo)
        {
            const string spName = "sp_insertArticuloConsumo";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_insumo_id", articuloConsumo.IdInsumo},
                {"@p_articulo_id", articuloConsumo.IdArticulo},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(ArticuloConsumoDirecto articuloConsumo)
        {
            const string spName = "sp_updateArticuloConsumo";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", articuloConsumo.Id},
                {"@p_insumo_id", articuloConsumo.IdInsumo},
                {"@p_articulo_id", articuloConsumo.IdArticulo},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
