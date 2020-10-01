using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class TipoUsuarioDal
    {
        private readonly IRepository _repository;

        public TipoUsuarioDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TipoUsuario>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from TIPO_USUARIO";

            return _repository.GetListAsync<TipoUsuario>(query);
        }

        public Task<TipoUsuario> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from TIPO_USUARIO
                where id = :id";

            return _repository.GetAsync<TipoUsuario>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }
    }
}
