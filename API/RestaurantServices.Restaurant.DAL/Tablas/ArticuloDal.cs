using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class ArticuloDal
    {
        private readonly IRepository _repository;

        public ArticuloDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Articulo>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    nombre,
                    descripcion,
                    precio,
                    estado_articulo_id as IdEstadoArticulo,
                    tipo_consumo_id as IdtipoConsumo
                from articulo";

            return _repository.GetListAsync<Articulo>(query);
        }

        public Task<Articulo> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    nombre,
                    descripcion,
                    precio,
                    estado_articulo_id as IdEstadoArticulo,
                    tipo_consumo_id as IdtipoConsumo
                from articulo
                where id = :id";

            return _repository.GetAsync<Articulo>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(Articulo articulo)
        {
            const string spName = "sp_insertArticulo";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_nombre", articulo.Nombre},
                {"@p_descripcion", articulo.Descripcion},
                {"@p_precio", articulo.Precio},
                {"@p_estado_articulo_id", articulo.IdEstadoArticulo},
                {"@p_tipo_consumo_id", articulo.IdTipoConsumo},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(Articulo articulo)
        {
            const string spName = "sp_updateArticulo";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", articulo.Id},
                {"@p_nombre", articulo.Nombre},
                {"@p_descripcion", articulo.Descripcion},
                {"@p_precio", articulo.Precio},
                {"@p_estado_articulo_id", articulo.IdEstadoArticulo},
                {"@p_tipo_consumo_id", articulo.IdTipoConsumo},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
