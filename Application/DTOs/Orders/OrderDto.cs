
using Domain.Common; 
namespace Application.DTOs.Orders
{
    public class OrderDto : BaseEntity<long>
    {
        public string Code { get; set; } 
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; } 
        public double ItemTotal { get; set; } 
        public string Status { get; set; }
        public double DueAmount { get; set; }
        public OrderDetailDto OrderDetails { get; set; }
    }
}
