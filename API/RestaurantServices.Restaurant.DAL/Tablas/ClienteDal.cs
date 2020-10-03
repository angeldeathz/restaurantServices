using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.TableJoin;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class ClienteDal
    {
        private readonly IRepository _repository;

        public ClienteDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ClienteJoin>> GetAsync()
        {
            const string query = @"SELECT
                    c.id as idCliente,
                    p.id as idPersona,
                    p.rut,
                    p.digito_verificador as digitoVerificador,
                    p.nombre,
                    p.apellido,
                    p.email,
                    p.telefono,
                    p.persona_natural as esPersonaNatural
                FROM CLIENTE c
                JOIN PERSONA p on c.persona_id = p.id";

            return _repository.GetListAsync<ClienteJoin>(query);
        }

        public Task<ClienteJoin> GetAsync(int id)
        {
            const string query = @"SELECT
                    c.id as idCliente,
                    p.id as idPersona,
                    p.rut,
                    p.digito_verificador as digitoVerificador,
                    p.nombre,
                    p.apellido,
                    p.email,
                    p.telefono,
                    p.persona_natural as esPersonaNatural
                FROM CLIENTE c
                JOIN PERSONA p on c.persona_id = p.id
                where c.id = :id";

            return _repository.GetAsync<ClienteJoin>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(Cliente cliente)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@PERSONA_ID", cliente.IdPersona},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
