using Domain.Common;

namespace Domain.Entities;

public class Section : BaseEntity<long>
{
    public Product Product { get; set; }
    public long ProductId { get; set; }
    public string Name { get; set; }
    public ICollection<PricingItem> PricingItems { get; set; }
}
