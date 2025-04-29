namespace Domain.Views;

public class VwSearch
{
    public long ItemId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public double? Price { get; set; }
    public bool HasPricing { get; set; }
    public string Type { get; set; }
    public int? SubCategoryId { get; set; }
}
