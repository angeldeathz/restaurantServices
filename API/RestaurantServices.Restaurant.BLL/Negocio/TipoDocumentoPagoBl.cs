using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class TipoDocumentoPagoBl
    {
        private readonly UnitOfWork _unitOfWork;

        public TipoDocumentoPagoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
        }

        public async Task<List<TipoDocumentoPago>> ObtenerTodosAsync()
        {
            return (List<TipoDocumentoPago>)await _unitOfWork.TipoDocumentoPagoDal.GetAsync();
        }

        public Task<TipoDocumentoPago> ObtenerPorIdAsync(int id)
        {
            return _unitOfWork.TipoDocumentoPagoDal.GetAsync(id);
        }
    }
}
