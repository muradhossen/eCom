using Domain.Common; 

namespace Application.DTOs.Orders;

public class DiscountItemDto : BaseEntity<long>
{
    public long OrderDetailId { get; set; } 
    public string ReferenceType { get; set; } 
    public long ReferenceId { get; set; }  
    public int DiscountAmount { get; set; }
    public string Type { get; set; } 
}
