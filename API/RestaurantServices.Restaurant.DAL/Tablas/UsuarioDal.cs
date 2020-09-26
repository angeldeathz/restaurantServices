using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class UsuarioDal
    {
        private readonly IRepository _repository;

        public UsuarioDal(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioCompleto>> GetAsync()
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

            return await _repository.GetListAsync<UsuarioCompleto>(query);
        }

        public async Task<UsuarioCompleto> GetAsync(int id)
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

            return await _repository.GetAsync<UsuarioCompleto>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public async Task<UsuarioCompleto> GetByRutAsync(int rut)
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

            return await _repository.GetAsync<UsuarioCompleto>(query, new Dictionary<string, object>
            {
                {"@rut", rut}
            });
        }

        public async Task<Usuario> ValidaLoginAsync(int rut, string contrasena)
        {
            const string query =
                @"SELECT
                u.id
                FROM USUARIO u
                JOIN persona p on u.persona_id = p.id
                WHERE p.rut = :rut AND u.contrasena = :contrasena";

            return await _repository.GetAsync<Usuario>(query, new Dictionary<string, object>
            {
                {"@rut", rut},
                {"@contrasena", contrasena}
            });
        }
    }
}
