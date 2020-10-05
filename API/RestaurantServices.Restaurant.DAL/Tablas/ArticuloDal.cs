using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.TableJoin;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class ArticuloDal
    {
        private readonly IRepository _repository;

        public ArticuloDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ArticuloJoin>> GetAsync()
        {
            const string query = @"SELECT
                    a.id as IdArticulo,
                    a.nombre,
                    a.descripcion,
                    a.precio,
                    a.estado_articulo_id as IdEstadoArticulo,
                    a.tipo_consumo_id as IdtipoConsumo,
                    ea.nombre as nombreEstadoArticulo,
                    tc.nombre as nombreTipoConsumo
                from articulo a
                join estado_articulo ea on a.estado_articulo_id = ea.id
                join tipo_consumo tc on tc.id = a.tipo_consumo_id";

            return _repository.GetListAsync<ArticuloJoin>(query);
        }

        public Task<ArticuloJoin> GetAsync(int id)
        {
            const string query = @"SELECT
                    a.id as IdArticulo,
                    a.nombre,
                    a.descripcion,
                    a.precio,
                    a.estado_articulo_id as IdEstadoArticulo,
                    a.tipo_consumo_id as IdtipoConsumo,
                    ea.nombre as nombreEstadoArticulo,
                    tc.nombre as nombreTipoConsumo
                from articulo a
                join estado_articulo ea on a.estado_articulo_id = ea.id
                join tipo_consumo tc on tc.id = a.tipo_consumo_id
                where a.id = :id";

            return _repository.GetAsync<ArticuloJoin>(query, new Dictionary<string, object>
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
