using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.TableJoin;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class MesaDal
    {
        private readonly IRepository _repository;

        public MesaDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<MesaJoin>> GetAsync()
        {
            const string query = @"SELECT
                    m.id as idMesa,
                    m.nombre as NombreMesa,
                    m.cantidad_comensales as cantidadComensales,
                    m.estado_mesa_id as idEstadoMesa,
                    em.nombre as nombreEstado
                from mesa m 
                join estado_mesa em on m.estado_mesa_id = em.id";

            return _repository.GetListAsync<MesaJoin>(query);
        }

        public Task<MesaJoin> GetAsync(int id)
        {
            const string query = @"SELECT
                    m.id as idMesa,
                    m.nombre as NombreMesa,
                    m.cantidad_comensales as cantidadComensales,
                    m.estado_mesa_id as idEstadoMesa,
                    em.nombre as nombreEstado
                from mesa m 
                join estado_mesa em on m.estado_mesa_id = em.id
                where m.id = :id";

            return _repository.GetAsync<MesaJoin>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(Mesa mesa)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@NOMBRE", mesa.Nombre},
                {"@CANTIDAD_COMENSALES", mesa.CantidadComensales},
                {"@ESTADO_MESA_ID", mesa.IdEstadoMesa},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<bool> UpdateAsync(Mesa mesa)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<bool>(spName, new Dictionary<string, object>
            {
                {"@id", mesa.Id},
                {"@NOMBRE", mesa.Nombre},
                {"@CANTIDAD_COMENSALES", mesa.CantidadComensales},
                {"@ESTADO_MESA_ID", mesa.IdEstadoMesa},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
