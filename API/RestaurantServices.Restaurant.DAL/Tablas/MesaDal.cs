using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class MesaDal
    {
        private readonly IRepository _repository;

        public MesaDal(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Mesa>> GetAsync()
        {
            const string query = "";

            return await _repository.GetListAsync<Mesa>(query);
        }

        public async Task<Mesa> GetAsync(int id)
        {
            const string query = "";

            return await _repository.GetAsync<Mesa>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }
    }
}
