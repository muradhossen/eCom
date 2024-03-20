using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Categories;

public class CategoryCreateDto
{ 
    public string Name { get; set; }
    public string ImageUrl { get; set; } 
    public IFormFile Image { get; set; } 
    public string Description { get; set; }

}
