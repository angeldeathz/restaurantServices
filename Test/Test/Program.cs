using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using RestaurantServices.Shared.Transversal.Repositorio;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new OracleRepository();
            var param = new Dictionary<string, object>
            {
                {"@ID_PRUEBA", 6},
                {"@NOMBRE", "sdfsdfsdfs"}
            };
            var c = repo.InsertAsync<object>("INSERT_PRUEBA", param, CommandType.StoredProcedure).Result;
        }
    }
}
