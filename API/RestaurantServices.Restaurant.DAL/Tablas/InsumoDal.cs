using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class InsumoDal
    {
        private readonly IRepository _repository;

        public InsumoDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Insumo>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    nombre,
                    STOCK_ACTUAL as StockActual,
                    STOCK_CRITICO as StockCritico,
                    STOCK_OPTIMO as StockOptimo,
                    PROVEEDOR_ID as IdProveedor,
                    UNIDAD_MEDIDA_ID as IdUnidadDeMedida
                from insumo";

            return _repository.GetListAsync<Insumo>(query);
        }

        public Task<Insumo> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    nombre,
                    STOCK_ACTUAL as StockActual,
                    STOCK_CRITICO as StockCritico,
                    STOCK_OPTIMO as StockOptimo,
                    PROVEEDOR_ID as IdProveedor,
                    UNIDAD_MEDIDA_ID as IdUnidadDeMedida
                from insumo
                where id = :id";

            return _repository.GetAsync<Insumo>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(Insumo articulo)
        {
            const string spName = "sp_insertInsumo";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_nombre", articulo.Nombre},
                {"@p_stockActual", articulo.StockActual},
                {"@p_stockCritico", articulo.StockCritico},
                {"@p_stockOptimo", articulo.StockOptimo},
                {"@p_proveedorId", articulo.IdProveedor},
                {"@p_unidadMedidaId", articulo.IdUnidadDeMedida},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(Insumo articulo)
        {
            const string spName = "sp_updateInsumo";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@id", articulo.Id},
                {"@p_nombre", articulo.Nombre},
                {"@p_stock_actual", articulo.StockActual},
                {"@p_stock_critico", articulo.StockCritico},
                {"@p_stock_optimo", articulo.StockOptimo},
                {"@p_proveedor_id", articulo.IdProveedor},
                {"@p_unidad_medida_id", articulo.IdUnidadDeMedida},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
