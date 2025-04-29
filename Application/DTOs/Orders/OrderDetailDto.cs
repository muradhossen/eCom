using Domain.Common; 

namespace Application.DTOs.Orders
{
    public class OrderDetailDto : BaseEntity<long>
    {
        public long OrderId { get; set; }  
        public string ShippingAddress { get; set; }
        public string Type { get; set; }
        public ICollection<DiscountItemDto> DiscountItems { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
    }
}
