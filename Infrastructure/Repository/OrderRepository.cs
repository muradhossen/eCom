using Application.RepositoryInterface;
using Domain.Entities.Orders;
using Infrastructure.Persistance;
using Infrastructure.Repository.Base;

namespace Infrastructure.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
