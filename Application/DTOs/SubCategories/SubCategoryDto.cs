namespace Application.DTOs.SubCategories;

public class SubCategoryDto
{
    public long Id { get; set; }
    public string Code { get; set; }    
    public string Name { get; set; }
    public string ImageUrl { get; set; }   
    public string Description { get; set; }
    public long CategoryId { get; set; } 
}
