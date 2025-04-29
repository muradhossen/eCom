using Application.Common;

namespace Application.Errors;

public static class ProductError
{
    public static readonly Error NotFound = new Error(
       "Product.ProductNotFound", "The requested product was not found!");

    public static Error UpdateFailed(string code) => new Error(
        "Product.UpdateFailed", $"Failed to update product with id {code}");

    public static readonly Error CreateFailed = new Error(
        "Product.CreateFailed", "Failed to create product");
    public static readonly Error NoPricintItemToCreate = new Error(
       "Product.CreateFailed", "No pricing items found to create!");

    public static readonly Error ImageUploadFailed = new Error(
    "Product.PhotoUploadFailed", "Failed to save photo!");
}
