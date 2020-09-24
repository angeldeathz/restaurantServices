using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class PersonaDal
    {
        private readonly IRepository _repository;

        public PersonaDal(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Persona>> GetAsync()
        {
            const string query = "";

            return await _repository.GetListAsync<Persona>(query);
        }

        public async Task<Persona> GetAsync(int id)
        {
            const string query = "";

            return await _repository.GetAsync<Persona>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public async Task<Persona> GetByRutAsync(int rut)
        {
            const string query = "";

            return await _repository.GetAsync<Persona>(query, new Dictionary<string, object>
            {
                {"@rut", rut}
            });
        }
    }
}
