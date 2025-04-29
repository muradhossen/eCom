using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User;

public class Role : IdentityRole<long>
{
    public ICollection<UserRole> UserRoles { get; set; }
}
