using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class EstadoOrdenProveedorBl
    {
        private readonly UnitOfWork _unitOfWork;

        public EstadoOrdenProveedorBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<EstadoOrdenProveedor>> ObtenerTodosAsync()
        {
            return (List<EstadoOrdenProveedor>)await _unitOfWork.EstadoOrdenProveedorDal.GetAsync();
        }

        public Task<EstadoOrdenProveedor> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.EstadoOrdenProveedorDal.GetAsync(id);
        }

        public Task<int> GuardarAsync(EstadoOrdenProveedor estadoOrdenProveedor)
        {
            return _unitOfWork.EstadoOrdenProveedorDal.InsertAsync(estadoOrdenProveedor);
        }

        public Task<int> ModificarAsync(EstadoOrdenProveedor estadoOrdenProveedor)
        {
            return _unitOfWork.EstadoOrdenProveedorDal.UpdateAsync(estadoOrdenProveedor);
        }
    }
}
