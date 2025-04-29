using Domain.Entities.Orders;

namespace Application.DTOs.Orders;

public class OrderDetailCreateDto
{
    public long OrderId { get; set; }
    public string ShippingAddress { get; set; }
    public string Type { get; set; }
    public List<DiscountItemDto> DiscountItems { get; set; }
    public List<OrderItemDto> Items { get; set; }
}
