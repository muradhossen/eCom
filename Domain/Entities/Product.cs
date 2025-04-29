using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product : AuditableWithBaseEntity<long>
{
    [Required(ErrorMessage = $"The {nameof(Code)} is required.")]
    [MaxLength(15,ErrorMessage =$"The {nameof(Code)} field cannot exceed 15 characters.")]
    [MinLength(7, ErrorMessage = $"The field must be at least 7 characters long.")]
    public string Code { get; set; }
    [Required(ErrorMessage = $"The {nameof(Name)} is required.")]
    [MaxLength(55, ErrorMessage = $"The {nameof(Name)} field cannot exceed 55 characters.")]
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string PhotoPublicId { get; set; }

    [MaxLength(1000, ErrorMessage = $"The {nameof(Description)} field cannot exceed 1000 characters.")]
    public string Description { get; set; }
    [MaxLength(1000, ErrorMessage = $"The {nameof(Details)} field cannot exceed 1000 characters.")]
    public string Details { get; set; }
    public string USP { get; set; }
    public SubCategory SubCategory { get; set; }
    public long SubCategoryId { get; set; }
    public Section Section { get; set; }
}
