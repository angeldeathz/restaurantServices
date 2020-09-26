using System.Collections.Generic;
using System.Data;
using RestaurantServices.Restaurant.BLL.Negocio;
using RestaurantServices.Restaurant.DAL.Shared;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new OracleRepository();
            //var param = new Dictionary<string, object>
            //{
            //    {"@ID_PRUEBA", 6},
            //    {"@NOMBRE", "sdfsdfsdfs"}
            //};
            //var c = repo.ExecuteProcedureAsync<object>("INSERT_PRUEBA", param, CommandType.StoredProcedure).Result;
            //var a = repo.GetListAsync<List<object>>("select * from usuario").Result;

            var respuesta = new UsuarioBl().ObtenerTodosAsync().Result;
        }
    }
}
