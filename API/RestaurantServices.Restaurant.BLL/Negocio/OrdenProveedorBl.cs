using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;
using RestaurantServices.Restaurant.Modelo.Dto;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class OrdenProveedorBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ProveedorBl _proveedorBl;
        private readonly UsuarioBl _usuarioBl;

        public OrdenProveedorBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _proveedorBl = new ProveedorBl();
            _usuarioBl = new UsuarioBl();
        }

        public async Task<List<OrdenProveedor>> ObtenerTodosAsync()
        {
            var ordenesProveedor = await _unitOfWork.OrdenProveedorDal.GetAsync();

            foreach (var x in ordenesProveedor)
            {
                x.Proveedor = await _proveedorBl.ObtenerPorIdAsync(x.IdProveedor);
                x.Usuario = await _usuarioBl.ObtenerPorIdAsync(x.IdUsuario);
                var estados = await _unitOfWork.EstadoOrdenProveedorDal.GetByOrdenProveedorAsync(x.Id);
                x.EstadosOrdenProveedor = (List<EstadoOrdenProveedor>)estados;
            }

            return (List<OrdenProveedor>)ordenesProveedor;
        }

        public async Task<OrdenProveedor> ObtenerPorIdAsync(int id)
        {
            var orden = await _unitOfWork.OrdenProveedorDal.GetAsync(id);
            if (orden == null) return null;

            orden.Proveedor = await _proveedorBl.ObtenerPorIdAsync(orden.IdProveedor);
            orden.Usuario = await _usuarioBl.ObtenerPorIdAsync(orden.IdUsuario);

            var estados = await _unitOfWork.EstadoOrdenProveedorDal.GetByOrdenProveedorAsync(orden.Id);
            orden.EstadosOrdenProveedor = (List<EstadoOrdenProveedor>)estados;

            return orden;
        }

        public Task<int> GuardarAsync(OrdenProveedor ordenProveedor)
        {
            return _unitOfWork.OrdenProveedorDal.InsertAsync(ordenProveedor);
        }

        public Task<int> ModificarAsync(OrdenProveedor ordenProveedor)
        {
            return _unitOfWork.OrdenProveedorDal.UpdateAsync(ordenProveedor);
        }

        public Task<int> AgregarEstadoAsync(OrdenProveedorEstado estado)
        {
            return _unitOfWork.OrdenProveedorDal.InsertEstadoAsync(estado);
        }
    }
}
