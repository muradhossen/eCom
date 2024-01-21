using Domain.Entities.User;

namespace Application.ServiceInterface;

public interface ITokenService
{
    Task<string> CreateToken(AuthUser user);
}
