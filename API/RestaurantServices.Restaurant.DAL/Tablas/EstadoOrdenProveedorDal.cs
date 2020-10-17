using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class EstadoOrdenProveedorDal
    {
        private readonly IRepository _repository;

        public EstadoOrdenProveedorDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<EstadoOrdenProveedor>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_orden_proveedor";

            return _repository.GetListAsync<EstadoOrdenProveedor>(query);
        }

        public Task<EstadoOrdenProveedor> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    NOMBRE
                from estado_orden_proveedor
                where id = :id";

            return _repository.GetAsync<EstadoOrdenProveedor>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<IEnumerable<EstadoOrdenProveedor>> GetByOrdenProveedorAsync(int idOrdenProveedor)
        {
            const string query = @"SELECT
                    ee.id,
                    ee.nombre,
                    c.fecha
                from estado_orden_proveedor ee
                join cambio_estado_orden_proveedor c on ee.id = c.estado_orden_proveedor_id
                where c.orden_proveedor_id = :idOrdenProveedor";

            return _repository.GetListAsync<EstadoOrdenProveedor>(query, new Dictionary<string, object>
            {
                {"@idOrdenProveedor", idOrdenProveedor}
            });
        }

        public Task<int> InsertAsync(EstadoOrdenProveedor estadoPedido)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@NOMBRE", estadoPedido.Nombre},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(EstadoOrdenProveedor estadoPedido)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@id", estadoPedido.Id},
                {"@NOMBRE", estadoPedido.Nombre},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
