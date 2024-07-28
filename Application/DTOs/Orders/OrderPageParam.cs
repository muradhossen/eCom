
using API.Helpers;

namespace Application.DTOs.Orders;

public class OrderPageParam : PageParam
{
    public bool IncludeDetails { get; set; }
}
