using Application.RepositoryInterface;
using Domain.Entities;
using Domain.Views;
using Infrastructure.Persistance;
using Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<VwSearch> GetSearchesQueryable()
    {
        return _dbContext.VwSearches.AsQueryable();
    }
    public async Task<bool> BulkSoftDeleteBySubCategoryIds(List<long> subcategoryIds,long userId)
    {
        int affectedRows = await _dbContext
               .Products
               .Where(c => subcategoryIds.Contains(c.SubCategoryId))
               .ExecuteUpdateAsync(t => t.SetProperty(c => c.IsDeleted, true)
               .SetProperty(c => c.DeletedById, userId)
               .SetProperty(c => c.DeletedOn,DateTime.UtcNow));

        return affectedRows > 0;
    }
}
