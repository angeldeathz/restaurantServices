using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class MedioPagoDocumentoBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MedioPagoBl _medioPagoBl;
        private readonly DocumentoPagoBl _documentoPagoBl;

        public MedioPagoDocumentoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _medioPagoBl = new MedioPagoBl();
            _documentoPagoBl = new DocumentoPagoBl();
        }

        public async Task<List<MedioPagoDocumento>> ObtenerTodosAsync()
        {
            var medioPagoDocumentos = await _unitOfWork.MedioPagoDocumentoDal.GetAsync();

            foreach (var x in medioPagoDocumentos)
            {
                x.MedioPago = await _medioPagoBl.ObtenerPorIdAsync(x.IdMedioPago);
                x.DocumentoPago = await _documentoPagoBl.ObtenerPorIdAsync(x.IdDocumentoPago);
            }

            return (List<MedioPagoDocumento>)medioPagoDocumentos;
        }

        public async Task<MedioPagoDocumento> ObtenerPorIdAsync(int id)
        {
            var medioPagoDocumento = await _unitOfWork.MedioPagoDocumentoDal.GetAsync(id);
            if (medioPagoDocumento == null) return null;

            medioPagoDocumento.MedioPago = await _medioPagoBl.ObtenerPorIdAsync(medioPagoDocumento.IdMedioPago);
            medioPagoDocumento.DocumentoPago = await _documentoPagoBl.ObtenerPorIdAsync(medioPagoDocumento.IdDocumentoPago);

            return medioPagoDocumento;
        }

        public Task<int> GuardarAsync(MedioPagoDocumento medioPagoDocumento)
        {
            return _unitOfWork.MedioPagoDocumentoDal.InsertAsync(medioPagoDocumento);
        }

        public Task<int> ModificarAsync(MedioPagoDocumento medioPagoDocumento)
        {
            return _unitOfWork.MedioPagoDocumentoDal.UpdateAsync(medioPagoDocumento);
        }
    }
}
