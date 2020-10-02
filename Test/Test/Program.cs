using System.Collections.Generic;
using System.Data;
using Dapper;
using RestaurantServices.Restaurant.DAL.Shared;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new OracleRepository();
            var param = new Dictionary<string, object>
            {
                {"p_nombre", "John"},
                {"p_stockActual", 1},
                {"p_stockCritico", 1},
                {"p_stockOptimo", 1},
                {"p_proveedorId", 1},
                {"p_unidadMedidaId", 1},
                {"p_return", 0}
            };

            var c = repo.ExecuteProcedureAsync<int>(@"sp_insertInsumo", param, CommandType.StoredProcedure).Result;
        }
    }
}
