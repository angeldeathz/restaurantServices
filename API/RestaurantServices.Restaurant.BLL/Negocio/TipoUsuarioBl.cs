using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class TipoUsuarioBl
    {
        private readonly UnitOfWork _unitOfWork;

        public TipoUsuarioBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<TipoUsuario>> ObtenerTodosAsync()
        {
            return (List<TipoUsuario>)await _unitOfWork.TipoUsuarioDal.GetAsync();
        }

        public async Task<TipoUsuario> ObtenerPorIdAsync(int id)
        {
            return await _unitOfWork.TipoUsuarioDal.GetAsync(id);
        }
    }
}
