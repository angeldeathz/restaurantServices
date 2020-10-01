using System;
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
            var personaTemp = new Persona();
            if (!personaTemp.ValidaRut(rut)) throw new Exception("Rut es inválido");
            return await _unitOfWork.PersonaDal.GetByRutAsync(personaTemp.Rut);
        }
    }
}
