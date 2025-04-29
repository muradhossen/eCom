using Application.RepositoryInterface;
using Domain.Entities.Carts;
using Infrastructure.Persistance;
using Infrastructure.Repository.Base;

namespace Infrastructure.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
