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

        public async Task<IEnumerable<ProveedorJoin>> GetAsync()
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

            return await _repository.GetListAsync<ProveedorJoin>(query);
        }

        public async Task<ProveedorJoin> GetAsync(int id)
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

            return await _repository.GetAsync<ProveedorJoin>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public async Task<int> InsertAsync(Proveedor proveedor)
        {
            const string query = "PROCEDURE";

            return await _repository.ExecuteProcedureAsync<int>(query, new Dictionary<string, object>
            {
                {"@DIRECCION", proveedor.Direccion},
                {"@PERSONA_ID", proveedor.IdPersona}
            }, CommandType.StoredProcedure);
        }

        public async Task<int> UpdateAsync(Proveedor proveedor)
        {
            const string query = "PROCEDURE";

            return await _repository.ExecuteProcedureAsync<int>(query, new Dictionary<string, object>
            {
                {"@id", proveedor.Id},
                {"@DIRECCION", proveedor.Direccion},
                {"@PERSONA_ID", proveedor.IdPersona}
            }, CommandType.StoredProcedure);
        }
    }
}
