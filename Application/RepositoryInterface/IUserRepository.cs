using Application.RepositoryInterface.Base;
using Domain.Entities.User;

namespace Application.RepositoryInterface;

public interface IUserRepository : IRepository<AuthUser>
{
}
