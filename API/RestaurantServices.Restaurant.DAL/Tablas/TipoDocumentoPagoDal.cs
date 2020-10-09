using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class TipoDocumentoPagoDal
    {
        private readonly IRepository _repository;

        public TipoDocumentoPagoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TipoDocumentoPago>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from tipo_documento_pago";

            return _repository.GetListAsync<TipoDocumentoPago>(query);
        }

        public Task<TipoDocumentoPago> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from tipo_documento_pago
                where id = :id";

            return _repository.GetAsync<TipoDocumentoPago>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }
    }
}
