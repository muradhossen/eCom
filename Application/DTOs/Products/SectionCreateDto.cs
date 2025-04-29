using Domain.Entities;

namespace Application.DTOs.Products
{
    public class SectionCreateDto
    { 
        public string Name { get; set; } = "Section 01";
        public ICollection<PricingItemCreateDto> PricingItems { get; set; }
    }
}
