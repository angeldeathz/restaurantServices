using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class IngredientePlatoBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly InsumoBl _insumoBl;
        private readonly PlatoBl _platoBl;

        public IngredientePlatoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _insumoBl = new InsumoBl();
            _platoBl = new PlatoBl();
        }

        public async Task<List<IngredientePlato>> ObtenerTodosAsync()
        {
            var ingrediente = await _unitOfWork.IngredientePlatoDal.GetAsync();

            foreach (var x in ingrediente)
            {
                x.Insumo = await _insumoBl.ObtenerPorIdAsync(x.IdInsumo);
                x.Plato = await _platoBl.ObtenerPorIdAsync(x.IdPlato);
            }

            return (List<IngredientePlato>)ingrediente;
        }

        public async Task<IngredientePlato> ObtenerPorIdAsync(int id)
        {
            var ingrediente = await _unitOfWork.IngredientePlatoDal.GetAsync(id);
            if (ingrediente == null) return null;
            ingrediente.Insumo = await _insumoBl.ObtenerPorIdAsync(ingrediente.IdInsumo);
            ingrediente.Plato = await _platoBl.ObtenerPorIdAsync(ingrediente.IdPlato);
            return ingrediente;
        }

        public Task<int> GuardarAsync(IngredientePlato ingredientePlato)
        {
            return _unitOfWork.IngredientePlatoDal.InsertAsync(ingredientePlato);
        }

        public Task<int> ModificarAsync(IngredientePlato ingredientePlato)
        {
            return _unitOfWork.IngredientePlatoDal.UpdateAsync(ingredientePlato);
        }
    }
}
