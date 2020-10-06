using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class DetalleOrdenProveedorDal
    {
        private readonly IRepository _repository;

        public DetalleOrdenProveedorDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<DetalleOrdenProveedor>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    precio,
                    cantidad,
                    total,
                    insumo_id as idInsumo,
                    orden_proveedor_id as idOrdenProveedor
                from detalle_orden_proveedor";

            return _repository.GetListAsync<DetalleOrdenProveedor>(query);
        }

        public Task<DetalleOrdenProveedor> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    precio,
                    cantidad,
                    total,
                    insumo_id as idInsumo,
                    orden_proveedor_id as idOrdenProveedor
                from detalle_orden_proveedor
                where id = :id";

            return _repository.GetAsync<DetalleOrdenProveedor>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(DetalleOrdenProveedor estadoArticulo)
        {
            const string spName = "sp_insertDetalleOrdenProveedor";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_precio", estadoArticulo.Precio},
                {"@p_cantidad", estadoArticulo.Cantidad},
                {"@p_total", estadoArticulo.Total},
                {"@p_insumo_id", estadoArticulo.IdInsumo},
                {"@p_orden_proveedor_id", estadoArticulo.IdOrdenProveedor},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(DetalleOrdenProveedor estadoArticulo)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", estadoArticulo.Id},
                {"@p_precio", estadoArticulo.Precio},
                {"@p_cantidad", estadoArticulo.Cantidad},
                {"@p_total", estadoArticulo.Total},
                {"@p_insumo_id", estadoArticulo.IdInsumo},
                {"@p_orden_proveedor_id", estadoArticulo.IdOrdenProveedor},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
