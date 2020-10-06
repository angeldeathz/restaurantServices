﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.DAL.Tablas
{
    public class OrdenProveedorDal
    {
        private readonly IRepository _repository;

        public OrdenProveedorDal(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<OrdenProveedor>> GetAsync()
        {
            const string query = @"SELECT
                    id,
                    fecha_hora as fechaHora,
                    total,
                    proveedor_id as idProveedor,
                    usuario_id as idUsuario
                FROM orden_proveedor";

            return _repository.GetListAsync<OrdenProveedor>(query);
        }

        public Task<OrdenProveedor> GetAsync(int id)
        {
            const string query = @"SELECT
                    id,
                    fecha_hora as fechaHora,
                    total,
                    proveedor_id as idProveedor,
                    usuario_id as idUsuario
                FROM orden_proveedor
                where id = :id";

            return _repository.GetAsync<OrdenProveedor>(query, new Dictionary<string, object>
            {
                {"@id", id}
            });
        }

        public Task<int> InsertAsync(OrdenProveedor ordenProveedor)
        {
            const string spName = "sp_insertOrdenProveedor";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_fecha_hora", ordenProveedor.FechaHora},
                {"@p_total", ordenProveedor.Total},
                {"@p_proveedor_id", ordenProveedor.IdProveedor},
                {"@p_usuario_id", ordenProveedor.IdUsuario},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }

        public Task<int> UpdateAsync(OrdenProveedor ordenProveedor)
        {
            const string spName = "sp_updateOrdenProveedor";

            return _repository.ExecuteProcedureAsync<int>(spName, new Dictionary<string, object>
            {
                {"@p_id", ordenProveedor.Id},
                {"@p_fecha_hora", ordenProveedor.FechaHora},
                {"@p_total", ordenProveedor.Total},
                {"@p_proveedor_id", ordenProveedor.IdProveedor},
                {"@p_usuario_id", ordenProveedor.IdUsuario},
                {"@p_return", 0}
            }, CommandType.StoredProcedure);
        }
    }
}