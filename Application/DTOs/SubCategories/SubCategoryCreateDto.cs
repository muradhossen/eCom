namespace Application.DTOs.SubCategories;

public class SubCategoryCreateDto
{  
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
}
