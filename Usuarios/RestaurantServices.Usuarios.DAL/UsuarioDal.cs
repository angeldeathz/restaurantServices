using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestaurantServices.Shared.Transversal.Repositorio;

namespace RestaurantServices.Usuarios.DAL
{
    public class UsuarioDal
    {
        private OracleRepository _repository;

        public UsuarioDal(OracleRepository repository)
        {
            _repository = repository;
        }
    }
}
