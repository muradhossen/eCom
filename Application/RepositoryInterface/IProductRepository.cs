using Application.RepositoryInterface.Base;
using Domain.Entities;
using Domain.Views;

namespace Application.RepositoryInterface;

public interface IProductRepository : IRepository<Product>
{
    IQueryable<VwSearch> GetSearchesQueryable();
    Task<bool> BulkSoftDeleteBySubCategoryIds(List<long> subcategoryIds,long userId);
}
