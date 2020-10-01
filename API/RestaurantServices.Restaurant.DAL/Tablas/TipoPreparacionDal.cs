using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class TipoPreparacionDal
    {
        private readonly IRepository _repository;

        public TipoPreparacionDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TipoPreparacion>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from tipo_preparacion";

            return _repository.GetListAsync<TipoPreparacion>(query);
        }

        public Task<TipoPreparacion> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from tipo_preparacion
                where id = :id";

            return _repository.GetAsync<TipoPreparacion>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }
    }
}
