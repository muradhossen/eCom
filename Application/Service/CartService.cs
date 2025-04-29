using Application.RepositoryInterface; 
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities.Carts;


namespace Application.Service
{
    public class CartService : Service<Cart>, ICartService
    {
        private readonly ICartRepository _repository;

        public CartService(ICartRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
