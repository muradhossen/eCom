using Application.ServiceInterface.Base;
using Domain.Entities.User;

namespace Application.ServiceInterface;

public interface IUserService : IService<AuthUser>
{
}
