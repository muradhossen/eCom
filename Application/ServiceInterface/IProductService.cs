using Application.Common.Pagination;
using Application.DTOs.Products;
using Application.ServiceInterface.Base;
using Domain.Entities;

namespace Application.ServiceInterface;

public interface IProductService : IService<Product>
{
    Task<PagedList<Product>> GetProductsAsync(ProductPageParam pageParam);
    
}
