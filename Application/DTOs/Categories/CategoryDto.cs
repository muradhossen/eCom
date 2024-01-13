namespace Application.DTOs.Categories;

public class CategoryDto
{
    public long Id { get; set; }
    public string Code { get; set; } 
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    
    public string Description { get; set; }
}
