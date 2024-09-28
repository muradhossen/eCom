using Domain.Common;

namespace Domain.Entities.Carts;

public class Cart : AuditableWithBaseEntity<int>
{
    public string OrderRequest { get; set; }
}
