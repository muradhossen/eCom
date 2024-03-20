using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category : AuditableWithBaseEntity<long>
{
    [Required(ErrorMessage = $"The {nameof(Code)} is required.")]
    [MaxLength(15, ErrorMessage = $"The {nameof(Code)} field cannot exceed 15 characters.")]
    [MinLength(7, ErrorMessage = $"The {nameof(Code)} field must be at least 7 characters long.")]
    public string Code { get; set; }
    
    [Required(ErrorMessage = $"The {nameof(Name)} is required.")]
    [MaxLength(55, ErrorMessage = $"The {nameof(Name)} field cannot exceed 55 characters.")]
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string PhotoPublicId { get; set; }
    [MaxLength(1000, ErrorMessage = $"The {nameof(Description)} field cannot exceed 1000 characters.")]
    public string Description { get; set; }
}
