using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class UnidadMedidaDal
    {
        private readonly IRepository _repository;

        public UnidadMedidaDal(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UnidadMedida>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE,
                    ABREVIACION
                from unidad_medida";

            return await _repository.GetListAsync<UnidadMedida>(query);
        }

        public async Task<UnidadMedida> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE,
                    ABREVIACION 
                from unidad_medida
                where id = :id";

            return await _repository.GetAsync<UnidadMedida>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }
    }
}
