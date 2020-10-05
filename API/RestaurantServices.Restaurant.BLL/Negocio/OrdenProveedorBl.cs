using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class OrdenProveedorBl
    {
        private readonly UnitOfWork _unitOfWork;

        public OrdenProveedorBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<OrdenProveedor>> ObtenerTodosAsync()
        {
            return (List<OrdenProveedor>)await _unitOfWork.OrdenProveedorDal.GetAsync();
        }

        public Task<OrdenProveedor> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.OrdenProveedorDal.GetAsync(id);
        }

        public Task<int> GuardarAsync(OrdenProveedor ordenProveedor)
        {
            return _unitOfWork.OrdenProveedorDal.InsertAsync(ordenProveedor);
        }

        public Task<int> ModificarAsync(OrdenProveedor ordenProveedor)
        {
            return _unitOfWork.OrdenProveedorDal.UpdateAsync(ordenProveedor);
        }
    }
}
