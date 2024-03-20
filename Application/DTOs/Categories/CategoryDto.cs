using Application.Common;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Categories;

public class CategoryDto : SlNumber<int>
{
    public long Id { get; set; }
    public string Code { get; set; } 
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public IFormFile Image { get; set; }
    public string Description { get; set; }
}
