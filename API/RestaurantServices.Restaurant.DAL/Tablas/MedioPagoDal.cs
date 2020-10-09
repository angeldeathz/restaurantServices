using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class MedioPagoDal
    {
        private readonly IRepository _repository;

        public MedioPagoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<MedioPago>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from medio_pago";

            return _repository.GetListAsync<MedioPago>(query);
        }

        public Task<MedioPago> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from medio_pago
                where id = :id";

            return _repository.GetAsync<MedioPago>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }
    }
}
