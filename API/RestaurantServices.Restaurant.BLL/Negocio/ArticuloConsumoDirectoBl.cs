using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class ArticuloConsumoDirectoBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly InsumoBl _insumoBl;
        private readonly ArticuloBl _articuloBl;

        public ArticuloConsumoDirectoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _insumoBl = new InsumoBl();
            _articuloBl = new ArticuloBl();
        }

        public async Task<List<ArticuloConsumoDirecto>> ObtenerTodosAsync()
        {
            var ingrediente = await _unitOfWork.ArticuloConsumoDirectoDal.GetAsync();

            foreach (var x in ingrediente)
            {
                x.Insumo = await _insumoBl.ObtenerPorIdAsync(x.IdInsumo);
                x.Articulo = await _articuloBl.ObtenerPorIdAsync(x.IdArticulo);
            }

            return (List<ArticuloConsumoDirecto>)ingrediente;
        }

        public async Task<ArticuloConsumoDirecto> ObtenerPorIdAsync(int id)
        {
            var ingrediente = await _unitOfWork.ArticuloConsumoDirectoDal.GetAsync(id);
            if (ingrediente == null) return null;
            ingrediente.Insumo = await _insumoBl.ObtenerPorIdAsync(ingrediente.IdInsumo);
            ingrediente.Articulo = await _articuloBl.ObtenerPorIdAsync(ingrediente.IdArticulo);
            return ingrediente;
        }

        public Task<int> GuardarAsync(ArticuloConsumoDirecto articuloConsumoDirecto)
        {
            return _unitOfWork.ArticuloConsumoDirectoDal.InsertAsync(articuloConsumoDirecto);
        }

        public Task<int> ModificarAsync(ArticuloConsumoDirecto articuloConsumoDirecto)
        {
            return _unitOfWork.ArticuloConsumoDirectoDal.UpdateAsync(articuloConsumoDirecto);
        }
    }
}
