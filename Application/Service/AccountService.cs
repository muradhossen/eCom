using Application.DTOs.User;
using Application.ServiceInterface;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Service;

public class AccountService : IAccountService
{
    private readonly UserManager<AuthUser> _userManager;

    public AccountService(UserManager<AuthUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<bool> IsUserExist(string userName)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == userName.ToLower());

    }

    public async Task<AuthUser> GetUserByUsername(string username)
    {
        return await _userManager
                    .Users
                    .SingleOrDefaultAsync(c => c.UserName.ToLower() == username.ToLower());

    }
}
