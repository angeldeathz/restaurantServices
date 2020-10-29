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

        public Task<ClienteJoin> GetByRutAsync(int rut)
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
                where p.rut = :rut";

            return _repository.GetAsync<ClienteJoin>(query, new Dictionary<string, object>
            {
                {"@rut", rut}
            });
        }

        public Task<ClienteJoin> GetByEmailAsync(string email)
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
                where p.email = :email";

            return _repository.GetAsync<ClienteJoin>(query, new Dictionary<string, object>
            {
                {"@email", email}
            });
        }

        public Task<int> InsertAsync(Cliente cliente)
        {
            const string spName = "sp_insertCliente";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_rut", cliente.Persona.Rut},
                {"@p_digito_verificador", cliente.Persona.DigitoVerificador},
                {"@p_nombre", cliente.Persona.Nombre},
                {"@p_apellido", cliente.Persona.Apellido},
                {"@p_email", cliente.Persona.Email},
                {"@p_telefono", cliente.Persona.Telefono},
                {"@p_persona_natural", cliente.Persona.EsPersonaNatural},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(Cliente cliente)
        {
            const string spName = "sp_updateCliente";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", cliente.Id},
                {"@p_rut", cliente.Persona.Rut},
                {"@p_digito_verificador", cliente.Persona.DigitoVerificador},
                {"@p_nombre", cliente.Persona.Nombre},
                {"@p_apellido", cliente.Persona.Apellido},
                {"@p_email", cliente.Persona.Email},
                {"@p_telefono", cliente.Persona.Telefono},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
