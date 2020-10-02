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

        public async Task<IEnumerable<Insumo>> GetAsync()
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

            return await _repository.GetListAsync<Insumo>(query);
        }

        public async Task<Insumo> GetAsync(int id)
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

            return await _repository.GetAsync<Insumo>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public async Task<int> InsertAsync(Insumo articulo)
        {
            const string spName = "PROCEDURE";

            return await _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@NOMBRE", articulo.Nombre},
                {"@STOCK_ACTUAL", articulo.StockActual},
                {"@STOCK_CRITICO", articulo.StockCritico},
                {"@STOCK_OPTIMO", articulo.StockOptimo},
                {"@PROVEEDOR_ID", articulo.IdProveedor},
                {"@UNIDAD_MEDIDA_ID", articulo.IdUnidadDeMedida},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateAsync(Insumo articulo)
        {
            const string spName = "PROCEDURE";

            return await _repository.ExecuteProcedureAsync<bool>(spName, new Dictionary<string, object>
            {
                {"@id", articulo.Id},
                {"@STOCK_ACTUAL", articulo.StockActual},
                {"@STOCK_CRITICO", articulo.StockCritico},
                {"@STOCK_OPTIMO", articulo.StockOptimo},
                {"@PROVEEDOR_ID", articulo.IdProveedor},
                {"@UNIDAD_MEDIDA_ID", articulo.IdUnidadDeMedida},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
