using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.TableJoin;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class ProveedorBl
    {
        private readonly UnitOfWork _unitOfWork;

        public ProveedorBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<Proveedor>> ObtenerTodosAsync()
        {
            var proveedorJoins = (List<ProveedorJoin>) await _unitOfWork.ProveedorDal.GetAsync();
            return proveedorJoins.Select(proveedorJoin => new Proveedor
            {
                Id = proveedorJoin.IdProveedor,
                Direccion = proveedorJoin.DireccionProveedor,
                IdPersona = proveedorJoin.IdPersona,
                Persona = new Persona
                {
                    DigitoVerificador = proveedorJoin.DigitoVerificador,
                    Nombre = proveedorJoin.Nombre,
                    Telefono = proveedorJoin.Telefono,
                    Apellido = proveedorJoin.Apellido,
                    Rut = proveedorJoin.Rut,
                    Email = proveedorJoin.Email,
                    EsPersonaNatural = proveedorJoin.EsPersonaNatural,
                    Id = proveedorJoin.IdPersona
                }
            }).ToList();
        }

        public async Task<Proveedor> ObtenerPorIdAsync(int id)
        {
            var proveedorJoin = await _unitOfWork.ProveedorDal.GetAsync(id);
            if (proveedorJoin == null) return null;
            return new Proveedor
            {
                Id = proveedorJoin.IdProveedor,
                Direccion = proveedorJoin.DireccionProveedor,
                IdPersona = proveedorJoin.IdPersona,
                Persona = new Persona
                {
                    DigitoVerificador = proveedorJoin.DigitoVerificador,
                    Nombre = proveedorJoin.Nombre,
                    Telefono = proveedorJoin.Telefono,
                    Apellido = proveedorJoin.Apellido,
                    Rut = proveedorJoin.Rut,
                    Email = proveedorJoin.Email,
                    EsPersonaNatural = proveedorJoin.EsPersonaNatural,
                    Id = proveedorJoin.IdPersona
                }
            };
        }

        public async Task<int> GuardarAsync(Proveedor proveedor)
        {
            var idPersona = await _unitOfWork.PersonaDal.InsertAsync(proveedor.Persona);
            if (idPersona == 0) throw new Exception("No se pudo ingresar los datos del proveedor");
            proveedor.IdPersona = idPersona;
            return await _unitOfWork.ProveedorDal.InsertAsync(proveedor);
        }

        public async Task<bool> ModificarAsync(Proveedor proveedor)
        {
            var esActualizado = await _unitOfWork.PersonaDal.UpdateAsync(proveedor.Persona);
            return await _unitOfWork.ProveedorDal.UpdateAsync(proveedor);
        }
    }
}
