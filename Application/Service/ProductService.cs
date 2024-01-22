using Application.Common.Pagination;
using Application.DTOs.Products;
using Application.DTOs.SubCategories;
using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;

namespace Application.Service;

internal class ProductService : Service<Product>, IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository) : base(repository)
    {
        _repository = repository;
    }
    public async Task<PagedList<Product>> GetProductsAsync(ProductPageParam pageParam)
    {

        var query = _repository.TableNoTracking.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pageParam.SearchKey))
        {
            string searchKey = pageParam.SearchKey.ToLower().Trim();

            query = query
                .Where(c => c.Name.ToLower().Contains(searchKey)
                || c.Code.Contains(searchKey));
        }

        query = query.OrderBy(c => c.Name);

        return await PagedList<Product>.CreateAsync(query, pageParam.PageSize, pageParam.PageNumber);
    }
}
