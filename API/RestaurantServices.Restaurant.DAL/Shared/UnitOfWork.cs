using RestaurantServices.Restaurant.DAL.Tablas;

namespace RestaurantServices.Restaurant.DAL.Shared
{
    public class UnitOfWork
    {
        private readonly IRepository _repository;
        private PersonaDal _personaDal;
        private UsuarioDal _usuarioDal;

        public UnitOfWork(IRepository repository)
        {
            _repository = repository;
        }

        public PersonaDal PersonaDal => _personaDal ?? (_personaDal = new PersonaDal(_repository));
        public UsuarioDal UsuarioDal => _usuarioDal ?? (_usuarioDal = new UsuarioDal(_repository));
    }
}
