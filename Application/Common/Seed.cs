using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Common;

public class Seed
{
    public static async Task SeedUsers(RoleManager<Role> roleManager)
    {
        if (await roleManager.Roles.AnyAsync()) return;

       
        var roles = new List<Role>
            {
                new Role { Name = "Customer" },
                new Role { Name = "Admin" },
                new Role { Name = "Moderator" }
            };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
    }
    }
