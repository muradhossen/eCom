using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities.Orders; 

public class OrderDetail : BaseEntity<long>
{
    public long OrderId { get; set; }
    public Order Order { get; set; }
    [Required]
    public string ShippingAddress { get; set; }
    public string Type { get; set; }
    public List<DiscountItem>  DiscountItems { get; set; } 
    public List<OrderItem> Items { get; set; }
}

public class DiscountItem : BaseEntity<long>
{
    public long OrderDetailId { get; set; }
    public OrderDetail OrderDetail { get; set; }
    public string ReferenceType { get; set; } //couponId, productId
    public long ReferenceId { get; set; } //Id of coupon, product
    public int DiscountAmount { get; set; }
    public string Type { get; set; } //coupon, flat discount, percentage discount
}

public class OrderItem : BaseEntity<long>
{
    public long OrderDetailId { get; set; }
    public OrderDetail OrderDetail { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Code { get; set; }
    public long CategoryId { get; set; }
    public long SubCategoryId { get; set; }
    public string SectionName { get; set; }
    public long SectionId { get; set; }
    public long PricingItemId { get; set; }
}
