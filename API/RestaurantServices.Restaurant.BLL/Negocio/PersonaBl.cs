using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class PersonaBl
    {
        private readonly UnitOfWork _unitOfWork;

        public PersonaBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<Persona>> ObtenerTodosAsync()
        {
            return (List<Persona>)await _unitOfWork.PersonaDal.GetAsync();
        }

        public async Task<Persona> ObtenerPorIdAsync(int id)
        {
            return await _unitOfWork.PersonaDal.GetAsync(id);
        }

        public async Task<Persona> ObtenerPorRutAsync(string rut)
        {
            var rutSinDv = 1;
            return await _unitOfWork.PersonaDal.GetByRutAsync(rutSinDv);
        }
    }
}
