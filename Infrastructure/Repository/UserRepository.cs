using Application.RepositoryInterface;
using Domain.Entities.User;
using Infrastructure.Persistance;
using Infrastructure.Repository.Base; 

namespace Infrastructure.Repository
{
    public class UserRepository : Repository<AuthUser>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
