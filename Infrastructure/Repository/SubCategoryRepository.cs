using Application.RepositoryInterface;
using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Repository.Base;

namespace Infrastructure.Repository;

public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SubCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
