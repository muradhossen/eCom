namespace Application.DTOs.Orders;

public class OrderCreateDto
{
    public string Code { get; set; } 
    public double TotalAmount { get; set; }
    public double DiscountAmount { get; set; } 
    public double ItemTotal { get; set; } 
    public string Status { get; set; }
    public double DueAmount { get; set; }
    public OrderDetailCreateDto OrderDetails { get; set; }
}
