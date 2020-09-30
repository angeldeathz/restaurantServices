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

        public async Task<IEnumerable<Articulo>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    nombre,
                    descripcion,
                    precio,
                    estado_articulo_id as IdEstadoArticulo,
                    tipo_consumo_id as IdtipoConsumo
                from articulo";

            return await _repository.GetListAsync<Articulo>(query);
        }

        public async Task<Articulo> GetAsync(int id)
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

            return await _repository.GetAsync<Articulo>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public async Task<int> InsertAsync(Articulo articulo)
        {
            const string query = "PROCEDURE";

            return await _repository.ExecuteProcedureAsync<int>(query, new Dictionary<string, object>
            {
                {"@NOMBRE", articulo.Nombre},
                {"@DESCRIPCION", articulo.Descripcion},
                {"@PRECIO", articulo.Precio},
                {"@ESTADO_ARTICULO_ID", articulo.IdEstadoArticulo},
                {"@TIPO_CONSUMO_ID", articulo.IdTipoConsumo}
            }, CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateAsync(Articulo articulo)
        {
            const string query = "PROCEDURE";

            return await _repository.ExecuteProcedureAsync<bool>(query, new Dictionary<string, object>
            {
                {"@id", articulo.Id},
                {"@NOMBRE", articulo.Nombre},
                {"@DESCRIPCION", articulo.Descripcion},
                {"@PRECIO", articulo.Precio},
                {"@ESTADO_ARTICULO_ID", articulo.IdEstadoArticulo},
                {"@TIPO_CONSUMO_ID", articulo.IdTipoConsumo}
            }, CommandType.StoredProcedure);
        }
    }
}
