using Application.Common.Pagination;
using Application.DTOs.Products;
using Application.DTOs.SubCategories;
using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        if (pageParam.SubCategoryId > 0)
        {
            query = query.Where(c => c.SubCategoryId == pageParam.SubCategoryId);
        }

        if (pageParam.IncludePricing)
        {
            query = query.Include(c => c.Section).ThenInclude(c => c.PricingItems);
        }

        if (!string.IsNullOrWhiteSpace(pageParam.SearchKey))
        {
            string searchKey = pageParam.SearchKey.ToLower().Trim();

            query = query
                .Where(c => c.Name.ToLower().Contains(searchKey)
                || c.Code.Contains(searchKey));
        }

        query = query.OrderByDescending(c => c.Id);

        return await PagedList<Product>.CreateAsync(query, pageParam.PageSize, pageParam.PageNumber);
    }

    public override async Task<Product> GetByIdAsync(object id)
    {
       return await _repository.GetQueryable()
            .Include(s => s.Section).ThenInclude(c => c.PricingItems)
            .Where(c => c.Id == (long)id)
            .FirstOrDefaultAsync();


    }
}
