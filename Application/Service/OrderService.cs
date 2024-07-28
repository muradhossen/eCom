using Application.Common.Pagination;
using Application.DTOs.Orders;
using Application.DTOs.Products;
using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;
using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<PagedList<Order>> GetOrdersAsync(OrderPageParam pageParam)
        {

            var query = _repository.TableNoTracking.AsQueryable();
 

            if (pageParam.IncludeDetails)
            {
                query = query.Include(c => c.OrderDetails);
            }

            if (!string.IsNullOrWhiteSpace(pageParam.SearchKey))
            {
                string searchKey = pageParam.SearchKey.ToLower().Trim();

                query = query
                    .Where(c => c.Code.ToLower().Contains(searchKey));
            }

            query = query.OrderByDescending(c => c.Id);

            return await PagedList<Order>.CreateAsync(query, pageParam.PageSize, pageParam.PageNumber);
        }

    }
}
