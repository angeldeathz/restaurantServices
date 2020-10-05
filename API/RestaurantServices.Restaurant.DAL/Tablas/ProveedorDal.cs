using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.TableJoin;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class ProveedorDal
    {
        private readonly IRepository _repository;

        public ProveedorDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ProveedorJoin>> GetAsync()
        {
            const string query = @"select
                    p.id as idproveedor,
                    p.direccion as direccionproveedor,
                    p.persona_id as idpersona,
                    pe.rut,
                    pe.digito_verificador as digitoverificador,
                    pe.nombre,
                    pe.apellido,
                    pe.email,
                    pe.telefono,
                    pe.persona_natural as esPersonaNatural
                from proveedor p
                join persona pe on p.persona_id = pe.id";

            return _repository.GetListAsync<ProveedorJoin>(query);
        }

        public Task<ProveedorJoin> GetAsync(int id)
        {
            const string query = @"select
                    p.id as idproveedor,
                    p.direccion as direccionproveedor,
                    p.persona_id as idpersona,
                    pe.rut,
                    pe.digito_verificador as digitoverificador,
                    pe.nombre,
                    pe.apellido,
                    pe.email,
                    pe.telefono,
                    pe.persona_natural as esPersonaNatural
                from proveedor p
                join persona pe on p.persona_id = pe.id
                where p.id = :id";

            return _repository.GetAsync<ProveedorJoin>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<ProveedorJoin> GetByRutAsync(int rut)
        {
            const string query = @"select
                    p.id as idproveedor,
                    p.direccion as direccionproveedor,
                    p.persona_id as idpersona,
                    pe.rut,
                    pe.digito_verificador as digitoverificador,
                    pe.nombre,
                    pe.apellido,
                    pe.email,
                    pe.telefono,
                    pe.persona_natural as esPersonaNatural
                from proveedor p
                join persona pe on p.persona_id = pe.id
                where p.rut = :rut";

            return _repository.GetAsync<ProveedorJoin>(query, new Dictionary<string, object>
            {
                {"@rut", rut}
            });
        }

        public Task<int> InsertAsync(Proveedor proveedor)
        {
            const string spName = "sp_insertProveedor";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_rut", proveedor.Persona.Rut},
                {"@p_digito_verificador", proveedor.Persona.DigitoVerificador},
                {"@p_nombre", proveedor.Persona.Nombre},
                {"@p_apellido", proveedor.Persona.Apellido},
                {"@p_email", proveedor.Persona.Email},
                {"@p_telefono", proveedor.Persona.Telefono},
                {"@p_persona_natural", proveedor.Persona.EsPersonaNatural},
                {"@P_direccion", proveedor.Direccion},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(Proveedor proveedor)
        {
            const string spName = "sp_updateProveedor";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", proveedor.Id},
                {"@p_rut", proveedor.Persona.Rut},
                {"@p_digito_verificador", proveedor.Persona.DigitoVerificador},
                {"@p_nombre", proveedor.Persona.Nombre},
                {"@p_apellido", proveedor.Persona.Apellido},
                {"@p_email", proveedor.Persona.Email},
                {"@p_telefono", proveedor.Persona.Telefono},
                {"@p_persona_natural", proveedor.Persona.EsPersonaNatural},
                {"@P_direccion", proveedor.Direccion},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}
