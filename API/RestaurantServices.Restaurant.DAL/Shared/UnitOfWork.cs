using RestaurantServices.Restaurant.DAL.Tablas;

namespace RestaurantServices.Restaurant.DAL.Shared
{
    public class UnitOfWork
    {
        private readonly IRepository _repository;
        private PersonaDal _personaDal;
        private UsuarioDal _usuarioDal;
        private MesaDal _mesaDal;
        private ArticuloDal _articuloDal;
        private InsumoDal _insumoDal;
        private UnidadMedidaDal _unidadMedidaDal;
        private ProveedorDal _proveedorDal;
        private TipoUsuarioDal _tipoUsuarioDal;
        private TipoConsumoDal _tipoConsumoDal;
        private EstadoArticuloDal _estadoArticuloDal;
        private TipoPreparacionDal _tipoPreparacionDal;
        private ClienteDal _clienteDal;
        private ReservaDal _reservaDal;
        private EstadoPedidoDal _estadoPedidoDal;
        private EstadoReservaDal _estadoReservaDal;
        private EstadoMesaDal _estadoMesaDal;
        private EstadoOrdenProveedorDal _estadoOrdenProveedorDal;
        private OrdenProveedorDal _ordenProveedorDal;
        private PedidoDal _pedidoDal;
        private ArticuloPedidoDal _articuloPedidoDal;
        private DetalleOrdenProveedorDal _detalleOrdenProveedorDal;
        private MedioPagoDal _medioPagoDal;
        private TipoDocumentoPagoDal _tipoDocumentoPagoDal;
        private PlatoDal _platoDal;

        public UnitOfWork(IRepository repository)
        {
            _repository = repository;
        }

        public PersonaDal PersonaDal => _personaDal ?? (_personaDal = new PersonaDal(_repository));
        public UsuarioDal UsuarioDal => _usuarioDal ?? (_usuarioDal = new UsuarioDal(_repository));
        public MesaDal MesaDal => _mesaDal ?? (_mesaDal = new MesaDal(_repository));
        public ArticuloDal ArticuloDal => _articuloDal ?? (_articuloDal = new ArticuloDal(_repository));
        public InsumoDal InsumoDal => _insumoDal ?? (_insumoDal = new InsumoDal(_repository));
        public UnidadMedidaDal UnidadMedidaDal => _unidadMedidaDal ?? (_unidadMedidaDal = new UnidadMedidaDal(_repository));
        public ProveedorDal ProveedorDal => _proveedorDal ?? (_proveedorDal = new ProveedorDal(_repository));
        public TipoUsuarioDal TipoUsuarioDal => _tipoUsuarioDal ?? (_tipoUsuarioDal = new TipoUsuarioDal(_repository));
        public TipoConsumoDal TipoConsumoDal => _tipoConsumoDal ?? (_tipoConsumoDal = new TipoConsumoDal(_repository));
        public EstadoArticuloDal EstadoArticuloDal => _estadoArticuloDal ?? (_estadoArticuloDal = new EstadoArticuloDal(_repository));
        public TipoPreparacionDal TipoPreparacionDal => _tipoPreparacionDal ?? (_tipoPreparacionDal = new TipoPreparacionDal(_repository));
        public ClienteDal ClienteDal => _clienteDal ?? (_clienteDal = new ClienteDal(_repository));
        public ReservaDal ReservaDal => _reservaDal ?? (_reservaDal = new ReservaDal(_repository));
        public EstadoPedidoDal EstadoPedidoDal => _estadoPedidoDal ?? (_estadoPedidoDal = new EstadoPedidoDal(_repository));
        public EstadoReservaDal EstadoReservaDal => _estadoReservaDal ?? (_estadoReservaDal = new EstadoReservaDal(_repository));
        public EstadoMesaDal EstadoMesaDal => _estadoMesaDal ?? (_estadoMesaDal = new EstadoMesaDal(_repository));
        public EstadoOrdenProveedorDal EstadoOrdenProveedorDal => _estadoOrdenProveedorDal ?? (_estadoOrdenProveedorDal = new EstadoOrdenProveedorDal(_repository));
        public OrdenProveedorDal OrdenProveedorDal => _ordenProveedorDal ?? (_ordenProveedorDal = new OrdenProveedorDal(_repository));
        public PedidoDal PedidoDal => _pedidoDal ?? (_pedidoDal = new PedidoDal(_repository));
        public ArticuloPedidoDal ArticuloPedidoDal => _articuloPedidoDal ?? (_articuloPedidoDal = new ArticuloPedidoDal(_repository));
        public DetalleOrdenProveedorDal DetalleOrdenProveedorDal => _detalleOrdenProveedorDal ?? (_detalleOrdenProveedorDal = new DetalleOrdenProveedorDal(_repository));
        public MedioPagoDal MedioPagoDal => _medioPagoDal ?? (_medioPagoDal = new MedioPagoDal(_repository));
        public TipoDocumentoPagoDal TipoDocumentoPagoDal => _tipoDocumentoPagoDal ?? (_tipoDocumentoPagoDal = new TipoDocumentoPagoDal(_repository));
        public PlatoDal PlatoDal => _platoDal ?? (_platoDal = new PlatoDal(_repository));
    }
}
