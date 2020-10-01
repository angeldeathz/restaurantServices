using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class TipoConsumoDal
    {
        private readonly IRepository _repository;

        public TipoConsumoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TipoConsumo>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from tipo_consumo";

            return _repository.GetListAsync<TipoConsumo>(query);
        }

        public Task<TipoConsumo> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from tipo_consumo
                where id = :id";

            return _repository.GetAsync<TipoConsumo>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }
    }
}
