namespace Application.Common.CurrentUser;
public interface ICurrentUserService
{
    long UserId { get; }
    string UserName { get; }
    string Email { get; }
    string Phone { get; }
}
