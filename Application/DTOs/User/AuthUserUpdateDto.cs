using Microsoft.AspNetCore.Http;

namespace Application.DTOs.User
{
    public class AuthUserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; }
        public string Address { get; set; }   
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public string PhotoUrl { get; set; }
        public IFormFile Photo { get; set; }
    }
}
