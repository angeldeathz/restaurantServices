using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class DetalleOrdenProveedorBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly InsumoBl _insumoBl;
        private readonly OrdenProveedorBl _ordenProveedorBl;

        public DetalleOrdenProveedorBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _insumoBl = new InsumoBl();
            _ordenProveedorBl = new OrdenProveedorBl();
        }

        public async Task<List<DetalleOrdenProveedor>> ObtenerTodosAsync()
        {
            var detalles = await _unitOfWork.DetalleOrdenProveedorDal.GetAsync();

            foreach (var x in detalles)
            {
                x.Insumo = await _insumoBl.ObtenerPorIdAsync(x.IdInsumo);
                x.OrdenProveedor = await _ordenProveedorBl.ObtenerPorIdAsync(x.IdOrdenProveedor);
            }

            return (List<DetalleOrdenProveedor>) detalles;
        }

        public async Task<DetalleOrdenProveedor> ObtenerPorIdAsync(int id)
        {
            var detalle = await _unitOfWork.DetalleOrdenProveedorDal.GetAsync(id);
            if (detalle == null) return null;

            detalle.Insumo = await _insumoBl.ObtenerPorIdAsync(detalle.IdInsumo);
            detalle.OrdenProveedor = await _ordenProveedorBl.ObtenerPorIdAsync(detalle.IdOrdenProveedor);

            return detalle;
        }

        public Task<int> GuardarAsync(DetalleOrdenProveedor detalleOrdenProveedor)
        {
            return _unitOfWork.DetalleOrdenProveedorDal.InsertAsync(detalleOrdenProveedor);
        }

        public Task<int> ModificarAsync(DetalleOrdenProveedor detalleOrdenProveedor)
        {
            return _unitOfWork.DetalleOrdenProveedorDal.UpdateAsync(detalleOrdenProveedor);
        }
    }
}
