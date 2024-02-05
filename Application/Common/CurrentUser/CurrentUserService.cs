using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Common.CurrentUser;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        

        if(long.TryParse(userId,out long parsedUserId))
        {
            UserId = parsedUserId;
        }

        Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        Phone = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.MobilePhone);
        
    }

    public long UserId { get; }
    public string UserName { get; }
    public string Email { get; }
    public string Phone { get; }
}
