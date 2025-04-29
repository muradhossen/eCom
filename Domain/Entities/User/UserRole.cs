using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User;

public class UserRole : IdentityUserRole<long>
{
    public AuthUser User { get; set; }
    public Role Role { get; set; }
}
