using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class PlatoBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ArticuloBl _articuloBl;
        private readonly TipoPreparacionBl _preparacionBl;

        public PlatoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _articuloBl = new ArticuloBl();
            _preparacionBl = new TipoPreparacionBl();
        }

        public async Task<List<Plato>> ObtenerTodosAsync()
        {
            var platos = await _unitOfWork.PlatoDal.GetAsync();

            foreach (var x in platos)
            {
                x.Articulo = await _articuloBl.ObtenerPorIdAsync(x.IdArticulo);
                x.TipoPreparacion = await _preparacionBl.ObtenerPorIdAsync(x.IdTipoPreparacion);
            }

            return (List<Plato>)platos;
        }

        public async Task<Plato> ObtenerPorIdAsync(int id)
        {
            var plato = await _unitOfWork.PlatoDal.GetAsync(id);
            if (plato == null) return null;
            plato.Articulo = await _articuloBl.ObtenerPorIdAsync(plato.IdArticulo);
            plato.TipoPreparacion = await _preparacionBl.ObtenerPorIdAsync(plato.IdTipoPreparacion);
            return plato;
        }

        public Task<int> GuardarAsync(Plato plato)
        {
            return _unitOfWork.PlatoDal.InsertAsync(plato);
        }

        public Task<int> ModificarAsync(Plato plato)
        {
            return _unitOfWork.PlatoDal.UpdateAsync(plato);
        }
    }
}
