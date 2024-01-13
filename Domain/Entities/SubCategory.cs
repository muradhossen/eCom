using Domain.Common;

namespace Domain.Entities;

public class SubCategory : AuditableWithBaseEntity<long>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
}
