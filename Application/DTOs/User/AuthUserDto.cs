using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User;

public class AuthUserDto
{
    public string UserName { get; set; }
    public string Token { get; set; }
    public string PhotoUrl { get; set; } 
    public string Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; } 
    public string Email { get; set; }
    public string PhoneNumber { get; set; }  
    public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
    public string Address { get; set; } 
}
