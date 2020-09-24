using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class UsuarioDal
    {
        private readonly IRepository _repository;

        public UsuarioDal(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Usuario>> GetAsync()
        {
            const string query = "select * from usuario";

            return await _repository.GetListAsync<Usuario>(query);
        }

        public async Task<Usuario> GetAsync(int id)
        {
            const string query = "";

            return await _repository.GetAsync<Usuario>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public async Task<Usuario> GetByRutAsync(int rut)
        {
            const string query = "";

            return await _repository.GetAsync<Usuario>(query, new Dictionary<string, object>
            {
                {"@rut", rut}
            });
        }

        public async Task<Usuario> ValidaLoginAsync(string rut, string contrasena)
        {
            const string query = "";

            return await _repository.GetAsync<Usuario>(query, new Dictionary<string, object>
            {
                {"@rut", rut},
                {"@contrasena", contrasena}
            });
        }
    }
}
