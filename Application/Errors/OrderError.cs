using Application.Common;

namespace Application.Errors;

public class OrderError
{
    public static readonly Error NotFound = new Error(
  "Order.OrderNotFound", "The requested order was not found!");

    public static Error UpdateFailed(string code) => new Error(
        "Order.UpdateFailed", $"Failed to update order with id {code}");

    public static readonly Error CreateFailed = new Error(
        "Order.CreateFailed", "Failed to create order");

    public static readonly Error CartCreateFailed = new Error(
    "Cart.CreateFailed", "Failed to save cart");

    public static readonly Error NoItemAddedToCreate = new Error(
       "Order.CreateFailed", "No items found to place order!");

    public static readonly Error NoItemAddedToUpdate = new Error(
   "Order.UpdateFailed", "No items found to place order!"); 
   
}
