using Application.RepositoryInterface.Base;
using Domain.Entities;
using Domain.Views;

namespace Application.RepositoryInterface;

public interface IProductRepository : IRepository<Product>
{
    IQueryable<VwSearch> GetSearchesQueryable();
    Task<bool> BulkSoftDeleteByProductsAsync(List<long> subcategoryIds,long userId);
}
