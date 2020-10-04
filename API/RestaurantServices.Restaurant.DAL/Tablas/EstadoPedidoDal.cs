using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class EstadoPedidoDal
    {
        private readonly IRepository _repository;

        public EstadoPedidoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<EstadoPedido>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_pedido";

            return _repository.GetListAsync<EstadoPedido>(query);
        }

        public Task<EstadoPedido> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_pedido
                where id = :id";

            return _repository.GetAsync<EstadoPedido>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }
    }
}
