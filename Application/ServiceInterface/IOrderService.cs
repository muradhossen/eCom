using Application.Common.Pagination;
using Application.Common.Result;
using Application.DTOs.Carts;
using Application.DTOs.Orders; 
using Application.ServiceInterface.Base; 
using Domain.Entities.Orders;

namespace Application.ServiceInterface
{
    public interface IOrderService : IService<Order>
    {
        Task<PagedList<Order>> GetOrdersAsync(OrderPageParam pageParam);
        Task<Result<Order>> AddByCartAsync(CartDto cartDto);
    }
}
