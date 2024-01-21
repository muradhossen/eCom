using Domain.Entities.User;

namespace Application.ServiceInterface;

public interface IAccountService
{
    Task<bool> IsUserExist(string userName);
    Task<AuthUser> GetUserByUsername(string username);
}
