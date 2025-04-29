using Application.DTOs.Products;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Carts;

public class CartDto
{
    public CartDto()
    {
        Items = new List<ProductContainerDto>();
    }
    public List<ProductContainerDto> Items { get; set; } 
    public string DeliveryAddress { get; set; }
   
    public string Mobile { get; set; }

    /// <summary>
    /// Method to get total price of all items, applying discount. 
    /// </summary>
    /// <returns></returns>
    public double GetTotalPrice()
    {
        double totalPrice = 0;

        foreach (var item in Items)
        {
            totalPrice += item.GetTotalPrice();
        }

        return totalPrice;
    }

    // Method to get total discount (currently returns 0 as per original logic)
    public double GetTotalDiscount()
    {
        double totalDiscount = 0;   
        foreach (var item in Items)
        {
            totalDiscount += item.GetTotalDiscount();
        }

        return totalDiscount;
    }

    // Method to get the total number of items
    public int GetTotalItems()
    {
        return Items.Count;
    }

    // Method to add a product to the cart
    public void AddProductToCart(ProductDto product)
    {
        // Check if the product is already in the cart
        if (Items.Any(c => c.Key.Code == product.Code))
        {
            foreach (var pc in Items)
            {
                if (pc.Key.Code == product.Code)
                {
                    pc.Values.Add(product);
                }
            }
        }
        else
        {
            // Add new product container to the cart
            var productContainer = new ProductContainerDto();
            productContainer.Add(product);
            Items.Add(productContainer);
        }
    }
    /// <summary>
    /// Get all items price without calculating any discount.
    /// </summary> 
    public double GetItemsOriginaTotal()
    {
       return Items.Sum(c => c.GetOriginalTotalPrice());
    }

    // Method to remove a product from the cart
    public void RemoveProduct(ProductContainerDto productContainer)
    {
        Items = Items.Where(i => i != productContainer).ToList();
    }
}
