using Application.RepositoryInterface; 
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities.User;


namespace Application.Service
{
    public class UserService : Service<AuthUser>, IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
