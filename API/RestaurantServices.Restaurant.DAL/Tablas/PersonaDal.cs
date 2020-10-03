using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class PersonaDal
    {
        private readonly IRepository _repository;

        public PersonaDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Persona>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    rut,
                    digito_verificador as digitoVerificador,
                    nombre,
                    apellido,
                    email,
                    telefono,
                    persona_natural as esPersonaNatural
                from persona";

            return _repository.GetListAsync<Persona>(query);
        }

        public Task<Persona> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    rut,
                    digito_verificador as digitoVerificador,
                    nombre,
                    apellido,
                    email,
                    telefono,
                    persona_natural as esPersonaNatural
                from persona
                where id = :id";

            return _repository.GetAsync<Persona>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<Persona> GetByRutAsync(int rut)
        {
            const string query = @"SELECT
                    id,
                    rut,
                    digito_verificador as digitoVerificador,
                    nombre,
                    apellido,
                    email,
                    telefono,
                    persona_natural as esPersonaNatural
                from persona
                where rut = :rut";

            return _repository.GetAsync<Persona>(query, new Dictionary<string, object>
            {
                {"@rut", rut}
            });
        }

        public Task<int> InsertAsync(Persona persona)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@rut", persona.Rut},
                {"@digito_verificador", persona.DigitoVerificador},
                {"@nombre", persona.Nombre},
                {"@apellido", persona.Apellido},
                {"@email", persona.Email},
                {"@telefono", persona.Telefono},
                {"@persona_natural", persona.EsPersonaNatural},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<bool> UpdateAsync(Persona persona)
        {
            const string spName = "PROCEDURE";

            return _repository.ExecuteProcedureAsync<bool>(spName, new Dictionary<string, object>
            {
                {"@id", persona.Id},
                {"@rut", persona.Rut},
                {"@digito_verificador", persona.DigitoVerificador},
                {"@nombre", persona.Nombre},
                {"@apellido", persona.Apellido},
                {"@email", persona.Email},
                {"@telefono", persona.Telefono},
                {"@persona_natural", persona.EsPersonaNatural},
                {"p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
