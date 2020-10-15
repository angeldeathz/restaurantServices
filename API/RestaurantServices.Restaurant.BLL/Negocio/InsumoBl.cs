using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class InsumoBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ProveedorBl _proveedorBl;
        private readonly UnidadMedidaBl _unidadMedidaBl;

        public InsumoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _proveedorBl = new ProveedorBl();
            _unidadMedidaBl = new UnidadMedidaBl();
        }

        public async Task<List<Insumo>> ObtenerTodosAsync()
        {
            var insumos = await _unitOfWork.InsumoDal.GetAsync();

            foreach (var x in insumos)
            {
                x.Proveedor = await _proveedorBl.ObtenerPorIdAsync(x.IdProveedor);
                x.UnidadMedida = await _unidadMedidaBl.ObtenerPorIdAsync(x.IdUnidadDeMedida);
            }

            return (List<Insumo>) insumos;
        }

        public async Task<Insumo> ObtenerPorIdAsync(int id)
        {
            var insumo = await _unitOfWork.InsumoDal.GetAsync(id);
            if (insumo == null) return null;
            insumo.UnidadMedida = await _unidadMedidaBl.ObtenerPorIdAsync(insumo.IdUnidadDeMedida);
            insumo.Proveedor = await _proveedorBl.ObtenerPorIdAsync(insumo.IdProveedor);
            return insumo;
        }

        public Task<int> GuardarAsync(Insumo insumo)
        {
            return _unitOfWork.InsumoDal.InsertAsync(insumo);
        }

        public Task<int> ModificarAsync(Insumo insumo)
        {
            return _unitOfWork.InsumoDal.UpdateAsync(insumo);
        }
    }
}
