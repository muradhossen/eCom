using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Products;

public class ProductCreateDto
{ 
    public string Name { get; set; }
    public string ImageUrl { get; set; } 
    public IFormFile Image { get; set; }
    public string Description { get; set; } 
    public string Details { get; set; }
    public string USP { get; set; } 
    public long SubCategoryId { get; set; }
    public SectionCreateDto Section { get; set; }
}
