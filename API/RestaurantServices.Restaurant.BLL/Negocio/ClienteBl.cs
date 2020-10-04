using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class ClienteBl
    {
        private readonly UnitOfWork _unitOfWork;

        public ClienteBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            var clientes = await _unitOfWork.ClienteDal.GetAsync();
            return clientes.Select(cliente => new Cliente
            {
                Id = cliente.IdCliente,
                IdPersona = cliente.IdPersona,
                Persona = new Persona
                {
                    Nombre = cliente.Nombre,
                    DigitoVerificador = cliente.DigitoVerificador,
                    Apellido = cliente.Apellido,
                    Email = cliente.Email,
                    Telefono = cliente.Telefono,
                    Rut = cliente.Rut,
                    EsPersonaNatural = cliente.EsPersonaNatural,
                    Id = cliente.IdPersona
                }
            }).ToList();
        }

        public async Task<Cliente> ObtenerPorIdAsync(int id)
        {
            var cliente = await _unitOfWork.ClienteDal.GetAsync(id);
            if (cliente == null) return null;
            return new Cliente
            {
                Id = cliente.IdCliente,
                IdPersona = cliente.IdPersona,
                Persona = new Persona
                {
                    Nombre = cliente.Nombre,
                    DigitoVerificador = cliente.DigitoVerificador,
                    Apellido = cliente.Apellido,
                    Email = cliente.Email,
                    Telefono = cliente.Telefono,
                    Rut = cliente.Rut,
                    EsPersonaNatural = cliente.EsPersonaNatural,
                    Id = cliente.IdPersona
                }
            };
        }

        public Task<int> GuardarAsync(Cliente cliente)
        {
            return _unitOfWork.ClienteDal.InsertAsync(cliente);
        }

        public Task<int> ModificarAsync(Cliente cliente)
        {
            return _unitOfWork.ClienteDal.UpdateAsync(cliente);
        }
    }
}