using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace RestaurantServices.Restaurant.DAL.Shared
{
    public class OracleRepository : IRepository
    {
        private OracleConnection _connection;

        private OracleConnection GetConnection
        {
            get
            {
                if (_connection == null)
                {
                    var a = ConfigurationManager.AppSettings["OracleConnection"];
                    _connection = new OracleConnection(a);
                }

                return _connection;
            }
        }

        #region Get

        public async Task<IEnumerable<T>> GetListAsync<T>(string query)
        {
            using (var db = GetConnection)
            {
                db.Open();
                return await db.QueryAsync<T>(query);
            }
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(string query, Dictionary<string, object> parameters)
        {
            using (var db = GetConnection)
            {
                db.Open();
                var dynamicParameters = new DynamicParameters(parameters);
                return await db.QueryAsync<T>(query, dynamicParameters);
            }
        }

        public async Task<T> GetAsync<T>(string query)
        {
            using (var db = GetConnection)
            {
                db.Open();
                return await db.QueryFirstOrDefaultAsync<T>(query);
            }
        }

        public async Task<T> GetAsync<T>(string query, Dictionary<string, object> parameters)
        {
            using (var db = GetConnection)
            {
                db.Open();
                var dynamicParameters = new DynamicParameters(parameters);
                return await db.QueryFirstOrDefaultAsync<T>(query);
            }
        }

        #endregion

        #region Insert

        public async Task<T> InsertAsync<T>(string query, Dictionary<string, object> parameters)
        {
            using (var db = GetConnection)
            {
                db.Open();
                var dynamicParameters = new DynamicParameters(parameters);
                return await db.ExecuteScalarAsync<T>(query, dynamicParameters);
            }
        }

        public async Task<T> InsertAsync<T>(string query, Dictionary<string, object> parameters, CommandType commandType)
        {
            using (var db = GetConnection)
            {
                db.Open();
                var dynamicParameters = new DynamicParameters(parameters);
                return await db.ExecuteScalarAsync<T>(query, dynamicParameters, null, null, commandType);
            }
        }

        #endregion
    }
}
