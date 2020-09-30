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
    }
}
