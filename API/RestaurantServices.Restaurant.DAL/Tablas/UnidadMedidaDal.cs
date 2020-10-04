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

        public Task<IEnumerable<UnidadMedida>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE,
                    ABREVIACION
                from unidad_medida";

            return _repository.GetListAsync<UnidadMedida>(query);
        }

        public Task<UnidadMedida> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE,
                    ABREVIACION 
                from unidad_medida
                where id = :id";

            return _repository.GetAsync<UnidadMedida>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }
    }
}
