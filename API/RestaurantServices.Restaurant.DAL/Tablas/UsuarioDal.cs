using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.TableJoin;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class UsuarioDal
    {
        private readonly IRepository _repository;

        public UsuarioDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<UsuarioCompleto>> GetAsync()
        {
            const string query =
                @"SELECT
                    u.id as idUsuario,
                    u.tipo_usuario_id as idTipoUsuario,
                    p.id as idPersona,
                    p.rut,
                    p.digito_verificador as digitoVerificador,
                    p.nombre,
                    p.apellido,
                    p.email,
                    p.telefono,
                    p.persona_natural as EsPersonaNatural,
                    t.nombre as nombreTipoUsuario
                FROM USUARIO u
                JOIN persona p on u.persona_id = p.id
                JOIN tipo_usuario t on t.id = u.tipo_usuario_id";

            return _repository.GetListAsync<UsuarioCompleto>(query);
        }

        public Task<UsuarioCompleto> GetAsync(int id)
        {
            const string query =
                @"SELECT
                    u.id as idUsuario,
                    u.tipo_usuario_id as idTipoUsuario,
                    p.id as idPersona,
                    p.rut,
                    p.digito_verificador as digitoVerificador,
                    p.nombre,
                    p.apellido,
                    p.email,
                    p.telefono,
                    p.persona_natural as EsPersonaNatural,
                    t.nombre as nombreTipoUsuario
                FROM USUARIO u
                JOIN persona p on u.persona_id = p.id
                JOIN tipo_usuario t on t.id = u.tipo_usuario_id
                where u.id = :id";

            return _repository.GetAsync<UsuarioCompleto>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<UsuarioCompleto> GetByRutAsync(int rut)
        {
            const string query =
                @"SELECT
                    u.id as idUsuario,
                    u.tipo_usuario_id as idTipoUsuario,
                    p.id as idPersona,
                    p.rut,
                    p.digito_verificador as digitoVerificador,
                    p.nombre,
                    p.apellido,
                    p.email,
                    p.telefono,
                    p.persona_natural as EsPersonaNatural,
                    t.nombre as nombreTipoUsuario
                FROM USUARIO u
                JOIN persona p on u.persona_id = p.id
                JOIN tipo_usuario t on t.id = u.tipo_usuario_id
                where p.rut = :rut";

            return _repository.GetAsync<UsuarioCompleto>(query, new Dictionary<string, object>
            {
                {"@rut", rut}
            });
        }

        public Task<Usuario> ValidaLoginAsync(int rut, string contrasena)
        {
            const string query =
                @"SELECT
                u.id
                FROM USUARIO u
                JOIN persona p on u.persona_id = p.id
                WHERE p.rut = :rut AND u.contrasena = :contrasena";

            return _repository.GetAsync<Usuario>(query, new Dictionary<string, object>
            {
                {"@rut", rut},
                {"@contrasena", contrasena}
            });
        }

        public Task<int> InsertAsync(Usuario usuario)
        {
            const string spName = "sp_insertUsuario";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_rut", usuario.Persona.Rut},
                {"@p_digito_verificador", usuario.Persona.DigitoVerificador},
                {"@p_nombre", usuario.Persona.Nombre},
                {"@p_apellido", usuario.Persona.Apellido},
                {"@p_email", usuario.Persona.Email},
                {"@p_telefono", usuario.Persona.Telefono},
                {"@p_contrasena", usuario.Persona.Telefono},
                {"@p_tipo_usuario_id", usuario.Persona.EsPersonaNatural},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(Usuario usuario)
        {
            const string spName = "sp_updateUsuario";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", usuario.Id},
                {"@p_rut", usuario.Persona.Rut},
                {"@p_digito_verificador", usuario.Persona.DigitoVerificador},
                {"@p_nombre", usuario.Persona.Nombre},
                {"@p_apellido", usuario.Persona.Apellido},
                {"@p_email", usuario.Persona.Email},
                {"@p_telefono", usuario.Persona.Telefono},
                {"@p_contrasena", usuario.Contrasena},
                {"@p_tipo_usuario_id", usuario.IdTipoUsuario},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
