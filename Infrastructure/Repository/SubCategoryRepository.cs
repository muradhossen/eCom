using Application.RepositoryInterface;
using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IProductRepository _productRepository;

    public SubCategoryRepository(ApplicationDbContext dbContext
        , IProductRepository productRepository) : base(dbContext)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
    }
    public async Task<bool> DeleteSubCategoryHierarchy(long categoryId, long userId)
    {
        if (categoryId <= 0)
        {
            return false;
        }
        var subcategoryIds = await _dbContext
                .SubCategories
                .Where(c => c.CategoryId == categoryId)
                .Select(c => c.Id)
                .ToListAsync();

        if (subcategoryIds.Any())
        {
            await _productRepository.BulkSoftDeleteByProductsAsync(subcategoryIds,userId);
        }

      var deletedRows = await _dbContext.SubCategories
            .Where(c => c.CategoryId == categoryId)
            .ExecuteUpdateAsync(t => t.SetProperty(c => c.IsDeleted, true)
            .SetProperty(c => c.DeletedById,userId)
            .SetProperty(c => c.DeletedOn, DateTime.UtcNow)
            );
        
        return deletedRows > 0;
    }
}
