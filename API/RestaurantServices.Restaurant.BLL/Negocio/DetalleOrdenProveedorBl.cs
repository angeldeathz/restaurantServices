using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class DetalleOrdenProveedorBl
    {
        private readonly UnitOfWork _unitOfWork;

        public DetalleOrdenProveedorBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<DetalleOrdenProveedor>> ObtenerTodosAsync()
        {
            return (List<DetalleOrdenProveedor>)await _unitOfWork.DetalleOrdenProveedorDal.GetAsync();
        }

        public Task<DetalleOrdenProveedor> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.DetalleOrdenProveedorDal.GetAsync(id);
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
