namespace Domain.Entities;

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
    public string USP { get; set; }
    public SubCategory SubCategory { get; set; }
    public long SubCategoryId { get; set; }
}
