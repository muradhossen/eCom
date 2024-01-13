using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category : AuditableWithBaseEntity<long>
{
    [Required]
    public string Code { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }
}
