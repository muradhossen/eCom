using Domain.Common;  

namespace Application.DTOs.Orders;

public class OrderItemDto : BaseEntity<long>
{
    public long OrderDetailId { get; set; } 
    public string Name { get; set; }
    public double Price { get; set; }
    public string Code { get; set; }
    public long CategoryId { get; set; }
    public long SubCategoryId { get; set; }
    public string SectionName { get; set; }
    public long SectionId { get; set; }
    public long PricingItemId { get; set; }
}
