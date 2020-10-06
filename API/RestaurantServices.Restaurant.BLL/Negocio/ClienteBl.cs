using System;
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

        public async Task<Cliente> GetByRutAsync(string rut)
        {
            var personaHelper = new Persona();
            if (!personaHelper.ValidaRut(rut))
            {
                throw new Exception("Rut es inválido");
            }

            var cliente = await _unitOfWork.ClienteDal.GetByRutAsync(personaHelper.Rut);
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

        public async Task<int> GuardarAsync(Cliente cliente)
        {
            var clienteExistente = await this.GetByRutAsync(cliente.Persona.ObtenerRutCompleto());
            if (clienteExistente != null) throw new Exception("Cliente ya existe");
            return await _unitOfWork.ClienteDal.InsertAsync(cliente);
        }

        public Task<int> ModificarAsync(Cliente cliente)
        {
            return _unitOfWork.ClienteDal.UpdateAsync(cliente);
        }
    }
}