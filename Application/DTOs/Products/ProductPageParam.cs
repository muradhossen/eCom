using API.Helpers;

namespace Application.DTOs.Products;

public class ProductPageParam : PageParam
{
    public bool IncludePricing { get; set; }
    public long SubCategoryId { get; set; }
}
