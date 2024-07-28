using Application.Common.Pagination;
using Application.DTOs.Orders; 
using Application.ServiceInterface.Base; 
using Domain.Entities.Orders;

namespace Application.ServiceInterface
{
    public interface IOrderService : IService<Order>
    {
        Task<PagedList<Order>> GetOrdersAsync(OrderPageParam pageParam);
    }
}
