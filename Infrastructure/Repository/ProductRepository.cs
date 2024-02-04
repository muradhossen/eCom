using Application.RepositoryInterface;
using Domain.Entities;
using Domain.Views;
using Infrastructure.Persistance;
using Infrastructure.Repository.Base;

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
}
