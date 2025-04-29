using Domain.Entities;

namespace Application.DTOs.Products;

public class SectionDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string Name { get; set; }
    public ICollection<PricingItemDto> PricingItems { get; set; }
}
