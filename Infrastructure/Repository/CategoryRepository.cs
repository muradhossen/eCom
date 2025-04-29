using Application.RepositoryInterface;
using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Repository.Base;

namespace Infrastructure.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
