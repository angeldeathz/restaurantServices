using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class EstadoArticuloPedidoDal
    {
        private readonly IRepository _repository;

        public EstadoArticuloPedidoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<EstadoArticuloPedido>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_articulo_pedido";

            return _repository.GetListAsync<EstadoArticuloPedido>(query);
        }

        public Task<EstadoArticuloPedido> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_articulo_pedido
                where id = :id";

            return _repository.GetAsync<EstadoArticuloPedido>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }
    }
}
