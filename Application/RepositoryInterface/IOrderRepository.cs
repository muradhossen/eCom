using Application.RepositoryInterface.Base;
using Domain.Entities.Orders;

namespace Application.RepositoryInterface
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
