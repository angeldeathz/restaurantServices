using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantServices.Restaurant.DAL.Shared;
using RestaurantServices.Restaurant.Modelo.Clases;

namespace RestaurantServices.Restaurant.BLL.Negocio
{
    public class DocumentoPagoBl
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly TipoDocumentoPagoBl _tipoDocumentoPagoBl;
        private readonly PedidoBl _pedidoBl;

        public DocumentoPagoBl()
        {
            _unitOfWork = new UnitOfWork(new OracleRepository());
            _tipoDocumentoPagoBl = new TipoDocumentoPagoBl();
            _pedidoBl = new PedidoBl();
        }

        public async Task<List<DocumentoPago>> ObtenerTodosAsync()
        {
            var documentos = await _unitOfWork.DocumentoPagoDal.GetAsync();

            foreach (var x in documentos)
            {
                x.TipoDocumentoPago = await _tipoDocumentoPagoBl.ObtenerPorIdAsync(x.IdTipoDocumentoPago);
                x.Pedido = await _pedidoBl.ObtenerPorIdAsync(x.IdPedido);
            }

            return (List<DocumentoPago>)documentos;
        }

        public async Task<DocumentoPago> ObtenerPorIdAsync(int id)
        {
            var documento = await _unitOfWork.DocumentoPagoDal.GetAsync(id);
            if (documento == null) return null;

            documento.TipoDocumentoPago = await _tipoDocumentoPagoBl.ObtenerPorIdAsync(documento.IdTipoDocumentoPago);
            documento.Pedido = await _pedidoBl.ObtenerPorIdAsync(documento.IdPedido);

            return documento;
        }

        public Task<int> GuardarAsync(DocumentoPago articulo)
        {
            return _unitOfWork.DocumentoPagoDal.InsertAsync(articulo);
        }

        public Task<int> ModificarAsync(DocumentoPago articulo)
        {
            return _unitOfWork.DocumentoPagoDal.UpdateAsync(articulo);
        }
    }
}
