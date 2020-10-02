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
        private OracleTransaction _oracleTransaction;

        private OracleConnection GetConnection
        {
            get
            {
                if (_connection == null)
                {
                    var connectionString = ConfigurationManager.AppSettings["OracleConnection"];
                    _connection = new OracleConnection(connectionString);
                }

                return _connection;
            }
        }

        #region Transaction

        public void BeginTransaction()
        {
            _oracleTransaction = GetConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Commit()
        {
            _oracleTransaction.Commit();
            _oracleTransaction.Dispose();
            _connection.Dispose();
        }

        public void RollBack()
        {
            _oracleTransaction.Rollback();
            _oracleTransaction.Dispose();
            _connection.Dispose();
        }

        #endregion

        #region Get

        public async Task<IEnumerable<T>> GetListAsync<T>(string query)
        {
            GetConnection.Open();
            var result = await GetConnection.QueryAsync<T>(query);
            GetConnection.Close();
            return result;
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(string query, Dictionary<string, object> parameters)
        {
            GetConnection.Open();
            var dynamicParameters = new DynamicParameters(parameters);
            var result = await GetConnection.QueryAsync<T>(query, dynamicParameters);
            GetConnection.Close();
            return result;
        }

        public async Task<T> GetAsync<T>(string query)
        {
            GetConnection.Open();
            var result = await GetConnection.QueryFirstOrDefaultAsync<T>(query);
            GetConnection.Close();
            return result;
        }

        public async Task<T> GetAsync<T>(string query, Dictionary<string, object> parameters)
        {
            GetConnection.Open();
            var dynamicParameters = new DynamicParameters(parameters);
            var result = await GetConnection.QueryFirstOrDefaultAsync<T>(query, dynamicParameters);
            GetConnection.Close();
            return result;
        }

        #endregion

        #region Procedures

        public async Task<T> ExecuteProcedureAsync<T>(string query, Dictionary<string, object> parameters, CommandType commandType)
        {
            GetConnection.Open();
            var param = new DynamicParameters(parameters);
            await GetConnection.ExecuteScalarAsync<T>(query, param, null, null, commandType);
            GetConnection.Close();
            return param.Get<T>("p_return");
        }

        #endregion
    }
}
