namespace Application.DTOs.Products;

public class PricingItemDto
{
    public long Id { get; set; }
    public long SectionId { get; set; }
    public double Price { get; set; }
    public string Label { get; set; }
    public string DiscountType { get; set; }
    public double? DiscountAmount { get; set; }
    public double? DiscountPercentage { get; set; }
}
