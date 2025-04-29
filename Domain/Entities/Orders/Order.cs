using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Orders;

public class Order : AuditableWithBaseEntity<long>
{
    public string Code { get; set; }
    [Required]
    public double TotalAmount { get; set; } 
    public double DiscountAmount { get; set; }
    [Required]
    public double ItemTotal { get; set; }
    [Required]
    public string Status { get; set; } 
    public double DueAmount { get; set; }
    public OrderDetail OrderDetails { get; set; }
}
