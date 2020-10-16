using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class IngredientePlatoDal
    {
        private readonly IRepository _repository;

        public IngredientePlatoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<IngredientePlato>> GetAsync()
        {
            const string query = @"SELECT
                    ID,
                    cantidad_insumo as CantidadInsumo,
                    insumo_id as IdInsumo,
                    plato_id as IdPlato
                FROM ingrediente_plato";

            return _repository.GetListAsync<IngredientePlato>(query);
        }

        public Task<IngredientePlato> GetAsync(int id)
        {
            const string query = @"SELECT
                    ID,
                    cantidad_insumo as CantidadInsumo,
                    insumo_id as IdInsumo,
                    plato_id as IdPlato
                FROM ingrediente_plato
                where id = :id";

            return _repository.GetAsync<IngredientePlato>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(IngredientePlato ingredientePlato)
        {
            const string spName = "sp_insertIngredientePlato";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_cantidad_insumo", ingredientePlato.CantidadInsumo},
                {"@p_insumo_id", ingredientePlato.IdInsumo},
                {"@p_plato_id", ingredientePlato.IdPlato},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(IngredientePlato ingredientePlato)
        {
            const string spName = "sp_updateIngredientePlato";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", ingredientePlato.Id},
                {"@p_cantidad_insumo", ingredientePlato.CantidadInsumo},
                {"@p_insumo_id", ingredientePlato.IdInsumo},
                {"@p_plato_id", ingredientePlato.IdPlato},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
