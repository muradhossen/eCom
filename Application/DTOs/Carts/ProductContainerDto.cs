using Application.DTOs.Products;
using Application.Enums;
using Domain.Entities;

namespace Application.DTOs.Carts;

public class ProductContainerDto
{
    public ProductDto Key { get; set; }
    public List<ProductDto> Values { get; set; } = new List<ProductDto>();

    // Method to get total price of all items
    public double GetTotalPrice()
    {
        return GetItemPrice() * GetTotalItems();
    }

    // Method to get total discount of all items
    public double GetTotalDiscount()
    {
        return GetItemDiscountPrice() * GetTotalItems();
    }

    // Method to get the price of an item (after applying the discount)
    public double GetItemPrice()
    {
        var pricingItem = Key.Section.PricingItems.FirstOrDefault();
        return pricingItem.Price - GetItemDiscountPrice();
    }
    public double GetOriginalItemPrice()
    {
        var pricingItem = Key.Section.PricingItems.FirstOrDefault();
        return pricingItem.Price;
    }

    // Method to get the discount price of an item
    public double GetItemDiscountPrice()
    {
        var pricingItem = Key.Section.PricingItems.FirstOrDefault();

        if (pricingItem?.DiscountType == null)
        {
            return 0;
        }

        if (pricingItem.DiscountType == DiscountType.Flat)
        {
            return pricingItem.DiscountAmount ?? 0;
        }

        if (pricingItem.DiscountType == DiscountType.Percentage)
        {
            return ((pricingItem.DiscountPercentage ?? 0) / 100) * pricingItem.Price;
        }

        return 0;
    }

    // Method to get the total number of items
    public int GetTotalItems()
    {
        return Values.Count;
    }

    public double GetOriginalTotalPrice()
    {
        return Values.Sum(c => c.Section.PricingItems.First().Price);
    }

    public string GetDiscountType()
    {
        var pricingItem = Key.Section.PricingItems.FirstOrDefault(); 
        return pricingItem?.DiscountType;
    }
    // Method to add a product to the container
    public void Add(ProductDto product)
    {
        Key = product;
        Values.Add(product);
    }
}
